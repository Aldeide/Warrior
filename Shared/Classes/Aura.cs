﻿using System.Diagnostics;
namespace Warrior
{

    public abstract class Aura
    {
        public AuraManager manager;
        public AuraSummary auraSummary = new AuraSummary();
        public Aura(AuraManager manager)
        {
            this.manager = manager;
            this.auraSummary.name = name;
        }
        public string name { get; set; } = "";
        public int stacks { get; set; } = 0;
        public bool active { get; set; } = false;
        public int start { get; set; } = 0;
        public int previousUpdate { get; set; } = 0;
        public int duration { get; set; } = 0;
        public int currentDuration { get; set; } = 0;
        public int timer { get; set; } = 0;
        public int fade { get; set; } = 0;
        public int damage { get; set; } = 0;
        public int tickInterval { get; set; } = 0;
        public int tickSize = 0;
        public int next { get; set; } = int.MaxValue;
        public List<AuraTrigger> trigger { get; set; } = new List<AuraTrigger>();
        public List<Effect> effects { get; set; } = new List<Effect>();
        public abstract void Trigger(AuraTrigger trigger);

        public void Reset()
        {
            stacks = 0;
            active = false;
            start = 0;
            auraSummary.uptime = 0;
        }

        public virtual void Update()
        {
            return;
        }

        public virtual int NextTick()
        {
            return (int)(manager.iteration.simulation.settings.combatLength * Constants.kStepsPerSecond);
        }

        public virtual int GetNext()
        {
            return next;
        }

    }

    public class Flurry : Aura
    {
        public Flurry(AuraManager arg) : base(arg)
        {
            name = "Flurry";
            auraSummary.name = name;
            trigger.Add(AuraTrigger.AllMeleeCritical);
            trigger.Add(AuraTrigger.AllMeleeNonCritical);
            effects.Add(
                new Effect(
                    EffectType.Multiplicative,
                    EffectStat.MeleeHaste,
                    TalentUtils.GetFlurryHasteMultiplier(arg.iteration.simulation.character.talents)));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            if (trigger == AuraTrigger.AllMeleeCritical)
            {
                if (active)
                {
                    auraSummary.refreshes += 1;
                    auraSummary.uptime += (manager.iteration.currentStep - previousUpdate);
                    previousUpdate = manager.iteration.currentStep;
                } else
                {
                    auraSummary.procs += 1;
                }
                stacks = 3;
                if (!active) {
                    start = manager.iteration.currentStep;
                    previousUpdate = start;
                 }
                active = true;
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Applied Flurry");
                
                manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
                return;
            }
            if (trigger == AuraTrigger.AllMeleeNonCritical && stacks > 0)
            {
                stacks -= 1;
                auraSummary.uptime += (manager.iteration.currentStep - previousUpdate);
                previousUpdate = manager.iteration.currentStep;
                Console.WriteLine("Flurry current stacks: " + stacks);
                if (stacks == 0)
                {
                    Console.WriteLine("Flurry Faded.");
                    auraSummary.uptime += (manager.iteration.currentStep - start);
                    active = false;
                    manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
                    
                }
            }
        }
    }

    public class DeepWounds : Aura
    {
        public DotDamageSummary dotSummary { get; private set; } = new DotDamageSummary();
        public DeepWounds(AuraManager arg) : base(arg)
        {
            name = "Deep Wounds";
            auraSummary.name = name;
            dotSummary.name = name;
            trigger.Add(AuraTrigger.MainHandCritical);
            trigger.Add(AuraTrigger.OffHandCritical);
            tickInterval = 1 * Constants.kStepsPerSecond;
            duration = 6 * Constants.kStepsPerSecond;
        }
        public override void Trigger(AuraTrigger trigger)
        {
            if (!active)
            {
                active = true;
                auraSummary.procs += 1;
                start = manager.iteration.currentStep;
                damage = (int)DeepWoundsDamage(trigger);
                tickSize = (int)(damage * tickInterval / (float)duration);
                dotSummary.applications += 1;
            } else
            {
                start = manager.iteration.currentStep;
                auraSummary.refreshes += 1;
                damage += (int)DeepWoundsDamage(trigger);
                tickSize = (int)(damage * tickInterval / (float)duration);
                dotSummary.refreshes += 1;
            }
            dotSummary.uptime += 1 * Constants.kStepsPerSecond;
            next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
            currentDuration = duration;
        }
        public override void Update()
        {
            if (!active)
            {
                return;
            }
            Console.WriteLine("[ " + manager.iteration.currentStep + " ] Deep Wounds Tick: " + tickSize);
            damage -= tickSize;
            Console.WriteLine("[ " + manager.iteration.currentStep + " ] Deep Wounds Damage Remaining: " + damage);
            currentDuration -= 1 * Constants.kStepsPerSecond;
            next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
            if (currentDuration <= 0)
            {
                damage = 0;
                active = false;
                next = int.MaxValue;
            }
            dotSummary.totalDamage += tickSize;
            dotSummary.ticks += 1;
        }
        public float DeepWoundsDamage(AuraTrigger trigger)
        {
            float dmg = 0;
            if (trigger == AuraTrigger.MainHandCritical)
            {
                dmg = (int)(manager.iteration.simulation.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding MH Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            if (trigger == AuraTrigger.OffHandCritical)
            {
                dmg = (int)(manager.iteration.simulation.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding OH Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.offHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            return dmg;
        }
        public override int GetNext()
        {
            return next;
        }
    }

    public class Bloodsurge : Aura
    {
        public Bloodsurge(AuraManager arg) : base(arg)
        {
            name = "Bloodsurge";
            auraSummary.name = name;
            duration = 5 * Constants.kStepsPerSecond;
            trigger.Add(AuraTrigger.Bloodthirst);
            trigger.Add(AuraTrigger.Whirlwind);
            trigger.Add(AuraTrigger.HeroicStrike);
        }

        public override void Trigger(AuraTrigger trigger)
        {
            int roll = manager.iteration.random.Next(1, 10000);
            if (roll > (int)(TalentUtils.GetBloodSurgeChance(manager.iteration.simulation.character.talents) * 100))
            {
                return;
            }
            if (!active)
            {
                active = true;
                fade = manager.iteration.currentStep + 5 * Constants.kStepsPerSecond;
                return;
            }
            if (active)
            {
                fade = manager.iteration.currentStep + 5 * Constants.kStepsPerSecond;
            }
        }
    }
}