﻿namespace Warrior
{
    public class Ability
    {
        public Iteration iteration;
        public string name = "";
        public int rageCost = 0;
        public int cooldown = 0;
        public int currentCooldown = 0;
        public int globalCooldown = 0;

        public bool isQueued { get; set; } = false;
        public DamageSummary damageSummary { get; set; }
        public Ability(Iteration iteration)
        {
            this.iteration = iteration;
            damageSummary = new DamageSummary();
            currentCooldown = 0;
        }
        public void Reset()
        {
            currentCooldown = 0;
        }
        public bool CanUse()
        {
            return iteration.rage >= rageCost && currentCooldown <= 0 && iteration.globalCooldown <= 0;
        }
        public void ApplyTime(int deltaStep)
        {
            currentCooldown -= deltaStep;
            if (currentCooldown < 0)
            {
                currentCooldown = 0;
            }
        }
        public virtual void Use()
        {
            if (!CanUse()) return;
            currentCooldown = cooldown;
            iteration.rage -= rageCost;
            iteration.globalCooldown = globalCooldown;
        }
    }

    public class Bloodthirst : Ability
    {
        public Bloodthirst(Iteration iteration) : base(iteration)
        {
            name = "Bloodthirst";
            damageSummary.name = name;
            rageCost = 20;
            cooldown = (int)(4.0f * Constants.kStepsPerSecond);
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }

        public override void Use()
        {
            if (!CanUse()) return;

            Console.WriteLine("[ " + iteration.currentStep + " ] Casting Bloodthirst");
            AttackResult result = AttackTableUtils.GetYellowHitResult(iteration.random, iteration.simulation);
            damageSummary.numCasts += 1;
            if (result == AttackResult.Miss)
            {
                damageSummary.numMiss += 1;
                base.Use();
                return;
            }
            if (result == AttackResult.Dodge)
            {
                damageSummary.numDodge += 1;
                base.Use();
                return;
            }

            // Bloodthirst has connected. Sending a notification.
            
            int damage = ComputeBloodthirstDamage(result);
            damageSummary.totalDamage += damage;
            if (result == AttackResult.Hit)
            {
                damageSummary.numHit += 1;
                damageSummary.hitDamage += damage;
                iteration.abilityManager.OnAbilityHit(this, EventArgs.Empty);
            }
            if (result == AttackResult.Critical)
            {
                damageSummary.numCrit += 1;
                damageSummary.critDamage += damage;
                iteration.abilityManager.OnAbilityCrit(this, EventArgs.Empty);
            }
            base.Use();
        }

        private int ComputeBloodthirstDamage(AttackResult result)
        {
            float damage = 0;
            
            // Affected by:
            // Two-Handed Weapon Specialization.
            // Titan's Grip.
            // Unending Fury.
            damage = 0.50f 
                * iteration.statsManager.GetEffectiveAttackPower() 
                * TalentUtils.GetUnendingFuryDamageMultiplier(iteration.simulation.character.talents) 
                * DamageUtils.EffectiveDamageCoefficient(iteration);

            if (result == AttackResult.Critical)
            {
                damage *= DamageUtils.EffectiveCritCoefficient(iteration.simulation.character.talents);
                iteration.auraManager.MeleeCriticalTrigger();
            }
            return (int)damage;
        }
    }

    public class Whirlwind : Ability
    {
        public Whirlwind(Iteration iteration) : base(iteration)
        {
            name = "Whirlwind";
            damageSummary.name = name;
            rageCost = 25;
            cooldown = iteration.simulation.character.glyphManager.HasGlyphOfWhirlwind() ? 8 * Constants.kStepsPerSecond : 10 * Constants.kStepsPerSecond;
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }

        public override void Use()
        {
            if (!CanUse()) return;
            damageSummary.numCasts += 1;
            Console.WriteLine("[ " + iteration.currentStep + " ] Casting Whirlwind");
            AttackResult mainHandResult = AttackTableUtils.GetYellowHitResult(iteration.random, iteration.simulation);
            AttackResult offHandResult = AttackTableUtils.GetYellowHitResult(iteration.random, iteration.simulation);

            if (mainHandResult == AttackResult.Miss)
            {
                damageSummary.numMiss += 1;
            }
            if (offHandResult == AttackResult.Dodge)
            {
                damageSummary.numDodge += 1;
            }
            if (offHandResult == AttackResult.Miss)
            {
                damageSummary.numMiss += 1;
            }
            if (offHandResult == AttackResult.Dodge)
            {
                damageSummary.numDodge += 1;
            }

            int mainHandDamage = ComputeWhirlwindDamage(mainHandResult, iteration.mainHand);
            int offHandDamage = ComputeWhirlwindDamage(offHandResult, iteration.offHand);
            damageSummary.totalDamage += mainHandDamage + offHandDamage;
            base.Use();
        }

        private int ComputeWhirlwindDamage(AttackResult result, Weapon weapon)
        {
            if (result == AttackResult.Dodge || result == AttackResult.Miss)
            {
                return 0;
            }
            float damage = 0;
            int weaponRoll = DamageUtils.GetWeaponDamageRoll(weapon);
            int attackPower = iteration.statsManager.GetEffectiveAttackPower();

            damage = weaponRoll + attackPower * AttackTableUtils.GetNormalizedWeaponSpeed(weapon) / 14.0f;
            // Affected by:
            // Two-Handed Weapon Specialization.
            // Titan's Grip.
            // Unending Fury.
            // Improved Whirlwind
            damage = damage
                * TalentUtils.GetUnendingFuryDamageMultiplier(iteration.simulation.character.talents)
                * TalentUtils.GetImprovedWhirlwindDamageMultiplier(iteration.simulation.character.talents)
                * DamageUtils.EffectiveDamageCoefficient(iteration);

            // Offhand penalty
            if (!weapon.isMainHand)
            {
                damage *= 0.5f;
                damage *= TalentUtils.GetDualWieldSpecializationMultiplier(iteration.simulation.character.talents);
            }

            if (result == AttackResult.Critical)
            {
                damage *= DamageUtils.EffectiveCritCoefficient(iteration.simulation.character.talents);
                damageSummary.critDamage += (int)damage;
                damageSummary.numCrit += 1;
                iteration.auraManager.MeleeCriticalTrigger();
            } else
            {
                damageSummary.hitDamage += (int)damage;
                damageSummary.numHit += 1;
            }
            return (int)damage;
        }
    }

    public class HeroicStrike : Ability
    {
        public HeroicStrike(Iteration iteration) : base(iteration)
        {
            name = "Heroic Strike";
            damageSummary.name = name;
            rageCost = 15
                - TalentUtils.GetImprovedHeroicStrikeReduction(iteration.simulation.character.talents)
                - TalentUtils.GetFocusedRageReduction(iteration.simulation.character.talents);
            cooldown = 0;
            globalCooldown = 0;
            currentCooldown = 0;
        }

        public bool CanUse()
        {
            return iteration.rage >= rageCost;
        }

        public override void Use()
        {
            if (iteration.rage >= rageCost && !isQueued)
            {
                Console.WriteLine("[ " + iteration.currentStep + " ] Queueing Heric Strike");
                isQueued = true;
                return;
            }
            if (CanUse() && isQueued)
            {
                Console.WriteLine("[ " + iteration.currentStep + " ] Casting Heroic Strike");
                iteration.rage -= rageCost;
                AttackResult result = AttackTableUtils.GetYellowHitResult(iteration.random, iteration.simulation);
                damageSummary.numCasts += 1;

                if (result == AttackResult.Miss)
                {
                    damageSummary.numMiss += 1;
                    isQueued = false;
                    return;
                }
                if (result == AttackResult.Dodge)
                {
                    damageSummary.numDodge += 1;
                    isQueued = false;
                    return;
                }

                int damage = DamageUtils.WeaponDamage(result, iteration.mainHand, iteration, /* bonus = */ 495);
                damageSummary.totalDamage += damage;
                if (result == AttackResult.Critical)
                {
                    damageSummary.numCrit += 1;
                    damageSummary.critDamage += damage;
                } else
                {
                    damageSummary.numHit += 1;
                    damageSummary.hitDamage += damage;
                }
            }
            isQueued = false;
        }

        public void Consumed(AttackResult result, int damage)
        {
            damageSummary.numCasts += 1;
            if (result == AttackResult.Critical)
            {
                damageSummary.numCrit += 1;
                damageSummary.critDamage += damage;
            }
        }
    }
}