namespace Warrior
{
    public class Ability
    {
        public Iteration iteration;
        public string name = "";
        public int rageCost = 0;
        public int cooldown = 0;
        public int currentCooldown = 0;
        public int globalCooldown = 0;
        public int castTime = 0;
        public int endCast = 0;
        public bool isCasting = false;
        public bool isQueued { get; set; } = false;
        public DamageResults damageSummary { get; set; }
        public Ability(Iteration iteration)
        {
            this.iteration = iteration;
            damageSummary = new DamageResults();
            currentCooldown = 0;
        }
        public void Reset()
        {
            currentCooldown = 0;
            castTime = 0;
            endCast = 0;
            isCasting = false;
            isQueued = false;
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
            rageCost = 20 - iteration.settings.talentSettings.FocusedRage.rank;
            cooldown = (int)(4.0f * Constants.kStepsPerSecond);
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }

        public override void Use()
        {
            if (!CanUse()) return;
            AttackResult result = AttackTableUtils.GetYellowHitResult(iteration);
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

            int damage = ComputeBloodthirstDamage(result);
            damageSummary.totalDamage += damage;
            if (result == AttackResult.Hit)
            {
                damageSummary.numHit += 1;
                damageSummary.hitDamage += damage;
            }
            if (result == AttackResult.Critical)
            {
                damageSummary.numCrit += 1;
                damageSummary.critDamage += damage;
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
                * iteration.computedConstants.unendingFuryDamageMultiplier
                * DamageUtils.EffectiveDamageCoefficient(iteration) * 1.06f;

            if (result == AttackResult.Critical)
            {
                damage *= DamageUtils.EffectiveCritCoefficient(iteration.settings.talentSettings);
                iteration.auraManager.BloodthirstCriticalTrigger();
            } else
            {
                iteration.auraManager.BloodthirstTrigger();
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
            rageCost = 25 - iteration.settings.talentSettings.FocusedRage.rank;
            cooldown = iteration.settings.glyphSettings.HasGlyphOfWhirlwind() ? 8 * Constants.kStepsPerSecond : 10 * Constants.kStepsPerSecond;
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }
        public override void Use()
        {
            if (!CanUse()) return;
            damageSummary.numCasts += 1;
            AttackResult mainHandResult = AttackTableUtils.GetYellowHitResult(iteration);
            AttackResult offHandResult = AttackTableUtils.GetYellowHitResult(iteration);

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
                * iteration.computedConstants.unendingFuryDamageMultiplier
                * TalentUtils.GetImprovedWhirlwindDamageMultiplier(iteration.settings.talentSettings)
                * DamageUtils.EffectiveDamageCoefficient(iteration);

            // Offhand penalty
            if (!weapon.isMainHand)
            {
                damage *= 0.5f * iteration.computedConstants.dualWieldSpecializationMultiplier;
            }

            if (result == AttackResult.Critical)
            {
                damage *= DamageUtils.EffectiveCritCoefficient(iteration.settings.talentSettings);
                damageSummary.critDamage += (int)damage;
                damageSummary.numCrit += 1;
                iteration.auraManager.WhirlwindCriticalTrigger();
            } else
            {
                damageSummary.hitDamage += (int)damage;
                damageSummary.numHit += 1;
                iteration.auraManager.WhirlwindTrigger();
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
                - TalentUtils.GetImprovedHeroicStrikeReduction(iteration.settings.talentSettings)
                - iteration.settings.talentSettings.FocusedRage.rank;
            cooldown = 0;
        }

        public bool CanUse()
        {
            return iteration.rage >= rageCost;
        }

        public override void Use()
        {
            if (iteration.rage >= rageCost && !isQueued)
            {
                isQueued = true;
                return;
            }
            if (CanUse() && isQueued)
            {
                iteration.rage -= rageCost;
                AttackResult result = AttackTableUtils.GetYellowHitResult(iteration);
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
                    iteration.auraManager.HeroicStrikeCriticalTrigger();
                } else
                {
                    damageSummary.numHit += 1;
                    damageSummary.hitDamage += damage;
                    iteration.auraManager.HeroicStrikeTrigger();
                }
                isQueued = false;
            }
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

    public class Slam : Ability
    {
        public Slam(Iteration iteration) : base(iteration)
        {
            name = "Slam";
            damageSummary.name = name;
            rageCost = 15 - iteration.settings.talentSettings.FocusedRage.rank;
            castTime = (int)((1.5f - iteration.settings.talentSettings.ImprovedSlam.rank * 0.5f) * Constants.kStepsPerSecond);
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
        }

        public override void Use()
        {
            if (!CanUse() && !isCasting) return;
            

            if (!iteration.auraManager.bloodsurge.active && !isCasting && CanUse())
            {
                endCast = iteration.currentStep + castTime;
                isCasting = true;
                return;
            }

            if (iteration.auraManager.bloodsurge.active || (isCasting && endCast == iteration.currentStep))
            {
                iteration.auraManager.bloodsurge.Fade();
                endCast = Int32.MaxValue;
                isCasting = false;
                AttackResult result = AttackTableUtils.GetYellowHitResult(iteration);

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
                int damage = (int)(DamageUtils.AverageWeaponDamage(iteration.mainHand, iteration, 250) * iteration.computedConstants.slamDamageMultiplier);

                if (result == AttackResult.Hit)
                {
                    damageSummary.numHit += 1;
                    damageSummary.hitDamage += damage;
                    damageSummary.totalDamage += damage;
                }
                if (result == AttackResult.Critical)
                {
                    damageSummary.numCrit += 1;
                    damageSummary.critDamage += (int)(damage * DamageUtils.EffectiveCritCoefficient(iteration.settings.talentSettings));
                    damageSummary.totalDamage += (int)(damage * DamageUtils.EffectiveCritCoefficient(iteration.settings.talentSettings));
                }
                
            }
            base.Use();
        }
    }

    public class BloodRageAbility : Ability
    {
        public BloodRageAbility(Iteration iteration) : base(iteration)
        {
            name = "Blood Rage";
            globalCooldown = 0;
            cooldown = (int)Math.Round((double)(60 * Constants.kStepsPerSecond * (1.0f - iteration.settings.talentSettings.IntensifyRage.rank * 0.11f)));
        }

        public override void Use()
        {
            if (!CanUse()) return;
            iteration.auraManager.bloodRage?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }

    public class DeathWish : Ability
    {
        public DeathWish(Iteration iteration) : base(iteration)
        {
            name = "Death Wish";
            damageSummary.name = name;
            rageCost = 10;
            cooldown = (int)Math.Round((double)(180 * Constants.kStepsPerSecond * (1.0f - iteration.settings.talentSettings.IntensifyRage.rank * 0.11f)));
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
            
        }
        public override void Use()
        {
            if (!CanUse()) return;
            damageSummary.numCasts += 1;
            iteration.auraManager.deathWish?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }

    public class Heroism : Ability
    {
        public Heroism(Iteration iteration) : base(iteration)
        {
            name = "Heroism";
            damageSummary.name = name;
            cooldown = 600 * Constants.kStepsPerSecond;
            globalCooldown = 0;
            currentCooldown = 0;

        }
        public override void Use()
        {
            if (!CanUse()) return;
            damageSummary.numCasts += 1;
            iteration.auraManager.heroism?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }

    public class ShatteringThrow : Ability
    {
        public ShatteringThrow(Iteration iteration) : base(iteration)
        {
            name = "Shattering Throw";
            rageCost = 25 - iteration.settings.talentSettings.FocusedRage.rank;
            damageSummary.name = name;
            castTime = (int)(1.5f * Constants.kStepsPerSecond);
            cooldown = 600 * Constants.kStepsPerSecond;
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;

        }
        public override void Use()
        {
            if (!CanUse() && !isCasting) return;
            if (!isCasting && CanUse())
            {
                isCasting = true;
                endCast = iteration.currentStep + castTime;
                return;
            }
            if (isCasting && endCast == iteration.currentStep)
            {
                endCast = Int32.MaxValue;
                isCasting = false;
                damageSummary.numCasts += 1;
                iteration.auraManager.shatteringThrow?.Trigger(AuraTrigger.Use);
            }

            base.Use();
        }
    }

    public class MortalStrike : Ability
    {
        public MortalStrike(Iteration iteration) : base(iteration)
        {
            name = "Mortal Strike";
            rageCost = 25 - iteration.settings.talentSettings.FocusedRage.rank;
            damageSummary.name = name;
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            cooldown = (int)(6f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }
        public override void Use()
        {
            if (!CanUse()) return;
            Console.WriteLine("[ " + iteration.currentStep + " ] Casting Mortal Strike");
            AttackResult result = AttackTableUtils.GetYellowHitResult(iteration);
            damageSummary.numCasts++;
            if (result == AttackResult.Miss)
            {
                damageSummary.numMiss++;
                return;
            }
            if (result == AttackResult.Dodge)
            {
                damageSummary.numDodge++;
                return;
            }
            if (result == AttackResult.Hit)
            {
                damageSummary.numHit++;
            }
            if (result == AttackResult.Critical)
            {
                damageSummary.numCrit++;
            }
            damageSummary.totalDamage += ComputeDamage(result);

            base.Use();
        }

        private int ComputeDamage(AttackResult result)
        {
            return (int)(
                DamageUtils.WeaponDamage(result, iteration.mainHand, iteration, 380)
                * iteration.computedConstants.improvedMortalStrikeMultiplier);
        }
    }

    public class Rend : Ability
    {
        public Rend(Iteration iteration) : base(iteration)
        {
            name = "Rend";
            rageCost = 10 - iteration.settings.talentSettings.FocusedRage.rank;
            damageSummary.name = name;
            globalCooldown = (int)(1.5f * Constants.kStepsPerSecond);
            currentCooldown = 0;
        }
        public override void Use()
        {
            if (!CanUse()) return;
            iteration.auraManager.rend?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }

    public class BloodFury : Ability
    {
        public BloodFury(Iteration iteration) : base(iteration)
        {
            name = "Blood Fury";
            cooldown = (int)(120f * Constants.kStepsPerSecond);
            damageSummary.name = name;
        }
        public override void Use()
        {
            if (!CanUse()) return;
            if (iteration.settings.characterSettings.race != Settings.Race.Orc) return;
            iteration.auraManager.bloodFury?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }

    public class Berserking : Ability
    {
        public Berserking(Iteration iteration) : base(iteration)
        {
            name = "Berserking";
            cooldown = (int)(180f * Constants.kStepsPerSecond);
            damageSummary.name = name;
        }
        public override void Use()
        {
            if (!CanUse()) return;
            if (iteration.settings.characterSettings.race != Settings.Race.Troll) return;
            iteration.auraManager.berserking?.Trigger(AuraTrigger.Use);
            base.Use();
        }
    }
}
