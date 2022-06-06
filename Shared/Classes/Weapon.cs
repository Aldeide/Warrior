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
            baseSpeed = (int)((float)item.speed * 1000);
            
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
            AttackResult result = AttackTableUtils.GetWhiteHitResult(iteration);
            if (result == AttackResult.Miss)
            {
                Console.WriteLine("[ " + iteration.currentStep + " ] Melee Miss (" + weapon + ")");
                damageSummary.numMiss += 1;
                iteration.auraManager.MeleeNonCriticalTrigger();
                return;
            }
            if (result == AttackResult.Dodge)
            {
                Console.WriteLine("[ " + iteration.currentStep + " ] Melee Dodge (" + weapon + ")");
                damageSummary.numDodge += 1;
                iteration.auraManager.MeleeNonCriticalTrigger();
                return;
            }
            int damage = DamageUtils.WeaponDamage(result, this, iteration, /* bonus = */ 0);
            int rage = DamageUtils.ComputeGeneratedRage(result, damage, this, iteration);
            iteration.IncrementRage(rage);
            damageSummary.totalDamage += damage;
            if (result == AttackResult.Glancing)  
            {
                
                Console.WriteLine("[ " + iteration.currentStep + " ] Melee Glancing: " + damage + " (" + weapon + ")");
                damageSummary.numGlancing += 1;
                damageSummary.glancingDamage += damage;
                iteration.auraManager.MeleeNonCriticalTrigger();
                return;
            }
            if (result == AttackResult.Critical)
            {
                Console.WriteLine("[ " + iteration.currentStep + " ] Melee Critical: " + damage + " (" + weapon + ")");
                damageSummary.numCrit += 1;
                damageSummary.critDamage += damage;
                iteration.auraManager.MeleeCriticalTrigger();
                if (isMainHand)
                {
                    iteration.auraManager.MainHandCriticalTtrigger();
                } else
                {
                    iteration.auraManager.OffHandCriticalTtrigger();
                }
                return;
            }
            Console.WriteLine("[ " + iteration.currentStep + " ] Melee Hit: " + damage + " (" + weapon + ")");
            damageSummary.numHit += 1;
            damageSummary.hitDamage += damage;
            iteration.auraManager.MeleeNonCriticalTrigger();
            return;
        }
        public void UpdateWeaponSpeed()
        {
            effectiveSpeed = (int)(baseSpeed / iteration.statsManager.GetEffectiveHasteMultiplier());
            //Console.WriteLine("Haste Multiplier: " + iteration.statsManager.GetEffectiveHasteMultiplier());
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
            Console.WriteLine("[ " + iteration.currentStep + " ] Effective swing timer: " + effectiveSpeed);
            formerSpeed = effectiveSpeed;
        }
        public void ApplyTime(int delta)  
        {
            swingTimer -= delta;
        }
    }
}
