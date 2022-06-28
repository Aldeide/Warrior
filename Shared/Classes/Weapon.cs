namespace Warrior
{
    public class Weapon
    {
        private Iteration iteration;
        public Item item { get; private set; }
        public bool isMainHand = false;
        public int baseSpeed = 0;
        public int effectiveSpeed = 0;
        public int minDamage = 0;
        public int maxDamage = 0;
        public int swingTimer = 0;
        public DamageResults damageSummary;
        public int formerSpeed = 0;

        public Weapon(Iteration iteration, ItemSlot slot, Item item)
        {
            this.iteration = iteration;
            damageSummary = new DamageResults();
            baseSpeed = (int)((float)item.speed * Constants.kStepsPerSecond);
            
            effectiveSpeed = (int)(baseSpeed / iteration.statsManager.GetEffectiveHasteMultiplier());
            formerSpeed = effectiveSpeed;
            minDamage = item.minDamage;
            maxDamage = item.maxDamage;
            if (slot == ItemSlot.MainHand)
            {
                isMainHand = true;
                swingTimer = 0;
            } else
            {
                swingTimer = (int)(effectiveSpeed / 2.0f);
                iteration.nextStep.offHand = swingTimer;
            }
            this.item = item;
        }
        public void Damage()
        {
            string weapon = isMainHand ? "MH" : "OH";
            if (swingTimer > 0)
            {
                return;
            }
            if (isMainHand)
            {
                iteration.nextStep.mainHand = iteration.currentStep + (int)(baseSpeed / iteration.statsManager.GetEffectiveHasteMultiplier());
            } else
            {
                iteration.nextStep.offHand = iteration.currentStep + (int)(baseSpeed / iteration.statsManager.GetEffectiveHasteMultiplier());
            }
            
            if (isMainHand && iteration.abilityManager.heroicStrike.isQueued)
            {
                if (iteration.abilityManager.heroicStrike.CanUse())
                {
                    iteration.abilityManager.heroicStrike.Use();
                    swingTimer = effectiveSpeed;
                    iteration.nextStep.mainHand = iteration.currentStep + swingTimer;
                    return;
                }
            }

            damageSummary.numCasts += 1;
            swingTimer = effectiveSpeed;
            AttackResult result = AttackTableUtils.GetWhiteHitResult(iteration, isMainHand);
            if (result == AttackResult.Miss)
            {
                damageSummary.numMiss += 1;
                return;
            }
            if (result == AttackResult.Dodge)
            {
                damageSummary.numDodge += 1;
                return;
            }
            int damage = DamageUtils.WeaponDamage(result, this, iteration, /* bonus = */ 0);
            int rage = DamageUtils.ComputeGeneratedRage(result, damage, this, iteration);
            iteration.IncrementRage(rage, "Melee");
            damageSummary.totalDamage += damage;
            if (result == AttackResult.Glancing)  
            {
                damageSummary.numGlancing += 1;
                damageSummary.glancingDamage += damage;
                if (isMainHand)
                {
                    iteration.auraManager.MainHandNonCriticalTrigger();
                }
                else
                {
                    iteration.auraManager.OffHandNonCriticalTrigger();
                }
                return;
            }
            if (result == AttackResult.Critical)
            {
                damageSummary.numCrit += 1;
                damageSummary.critDamage += damage;
                iteration.auraManager.MeleeCriticalTrigger();
                if (isMainHand)
                {
                    iteration.auraManager.MainHandCriticalTrigger();
                } else
                {
                    iteration.auraManager.OffHandCriticalTtrigger();
                }
                return;
            }
            damageSummary.numHit += 1;
            damageSummary.hitDamage += damage;
            if (isMainHand)
            {
                iteration.auraManager.MainHandNonCriticalTrigger();
            }
            else
            {
                iteration.auraManager.OffHandNonCriticalTrigger();
            }
            return;
        }
        public void UpdateWeaponSpeed()
        {
            effectiveSpeed = (int)(baseSpeed / iteration.statsManager.GetEffectiveHasteMultiplier());
            if (effectiveSpeed != formerSpeed)
            {
                swingTimer = (int)(effectiveSpeed * (float)swingTimer / baseSpeed);

            }
            if (isMainHand)
            {
                iteration.nextStep.mainHand = iteration.currentStep + swingTimer;
            }
            else
            {
                iteration.nextStep.offHand = iteration.currentStep + swingTimer;
            }
            formerSpeed = effectiveSpeed;
        }
        public void ApplyTime(int delta)  
        {
            // Swing timers are frozen while slam or shattering throw are being cast.
            if (iteration.abilityManager.slam.isCasting || iteration.abilityManager.shatteringThrow.isCasting)
            {
                if (isMainHand)
                {
                    iteration.nextStep.mainHand = iteration.currentStep + swingTimer;
                }
                else
                {
                    iteration.nextStep.offHand = iteration.currentStep + swingTimer;
                }
                return;
            }
            swingTimer -= delta;
            if (isMainHand)
            {
                iteration.nextStep.mainHand = iteration.currentStep + swingTimer;
            }
            else
            {
                iteration.nextStep.offHand = iteration.currentStep + swingTimer;
            }
            return;
        }
    }
}
