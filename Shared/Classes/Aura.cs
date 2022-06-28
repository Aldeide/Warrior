﻿using Warrior.Entities;
using System.Diagnostics;
namespace Warrior
{
    public abstract class Aura
    {
        public AuraManager manager;
        public AuraResults auraSummary = new AuraResults();
        public DotDamageResults dotSummary { get; private set; } = new DotDamageResults();
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
        public float ppm { get; set; } = 0;
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
            tickSize = 0;
            dotSummary.Reset();
            auraSummary.Reset();
        }

        public virtual void Update()
        {
            return;
        }

        public virtual int NextTick()
        {
            return (int)(manager.iteration.settings.simulationSettings.combatLength * Constants.kStepsPerSecond);
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
                    Stat.MeleeHaste,
                    TalentUtils.GetFlurryHasteMultiplier(arg.iteration.settings.talentSettings)));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            if (trigger == AuraTrigger.AllMeleeCritical)
            {
                if (active)
                {
                    auraSummary.refreshes += 1;
                } else
                {
                    auraSummary.procs += 1;
                    start = manager.iteration.currentStep;
                }
                stacks = 3;
                active = true;
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Applied Flurry");

                manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
                return;
            }
            if (trigger == AuraTrigger.AllMeleeNonCritical && stacks > 0)
            {
                if (!active)
                {
                    return;
                }
                stacks -= 1;
                if (stacks == 0)
                {
                    Console.WriteLine("Flurry Faded.");
                    auraSummary.uptime += (manager.iteration.currentStep - start);
                    active = false;
                    manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
                    
                }
            }
        }
        public void Fade()
        {
            
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
        }
    }
    public class DeepWounds : Aura
    {
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
            
            next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
            currentDuration = duration;
        }
        public override void Update()
        {
            if (!active)
            {
                return;
            }
            if (manager.iteration.currentStep != next)
            {
                return;
            }
            Console.WriteLine("[ " + manager.iteration.currentStep + " ] Deep Wounds Tick: " + tickSize);
            Console.WriteLine("[ " + manager.iteration.currentStep + " ] Total Ticks: " + dotSummary.ticks);
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
            dotSummary.uptime += 1 * Constants.kStepsPerSecond;
            dotSummary.totalDamage += tickSize;
            dotSummary.ticks += 1;
        }
        public float DeepWoundsDamage(AuraTrigger trigger)
        {
            float dmg = 0;
            if (trigger == AuraTrigger.MainHandCritical)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding MH Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            if (trigger == AuraTrigger.OffHandCritical)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding OH Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.offHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            if (trigger == AuraTrigger.Whirlwind)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding WW Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.offHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            if (trigger == AuraTrigger.Bloodthirst)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding BT Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.offHand, manager.iteration, 0) + "). Damage: " + damage);
            }
            if (trigger == AuraTrigger.HeroicStrike)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
                Console.WriteLine("[ " + manager.iteration.currentStep + " ] Adding BT Deep Wounds (" + DamageUtils.AverageWeaponDamage(manager.iteration.offHand, manager.iteration, 0) + "). Damage: " + damage);
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
            if (roll > manager.iteration.computedConstants.bloodsurgeChance * 100)
            {
                return;
            }
            if (!active)
            {
                //Console.WriteLine("Bloodsurge Proc");
                auraSummary.procs += 1;
                active = true;
                next = manager.iteration.currentStep + duration;
                fade = next;
                start = manager.iteration.currentStep;
                return;
            }
            if (active)
            {
                auraSummary.refreshes += 1;
                next = manager.iteration.currentStep + duration;
                fade = next;
            }
        }

        public override void Update()
        {
            if (!active) return;
            if (manager.iteration.currentStep >= fade)
            {
                //Console.WriteLine("Bloodsurge Fade");
                active = false;
                auraSummary.uptime += fade - start;
                next = int.MaxValue;
            }
        }
    }
    public class BloodRage 
        : Aura
    {
        public BloodRage(AuraManager arg) : base(arg)
        {
            name = "Bloodrage";
            auraSummary.name = name;
            duration = 10 * Constants.kStepsPerSecond;
        }

        public override void Trigger(AuraTrigger trigger)
        {
                manager.iteration.IncrementRage(10, "Bloodrage");
                auraSummary.procs += 1;
                active = true;
                next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
                fade = manager.iteration.currentStep + duration;
                start = manager.iteration.currentStep;
                return;
        }

        public override void Update()
        {
            if (!active) return;
            if (manager.iteration.currentStep != next)
            {
                return;
            }
            manager.iteration.IncrementRage(1, "Bloodrage");
            next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
            if (manager.iteration.currentStep >= fade)
            {
                active = false;
                auraSummary.uptime += fade - start;
                next = int.MaxValue;
            }
        }
    }

    // Enchants
    public class MainHandBerserking : Aura
	{
        public MainHandBerserking(AuraManager arg) : base(arg)
        {
            name = "Berserking (MH)";
            ppm = 1.0f;
            auraSummary.name = name;
            duration = 15 * Constants.kStepsPerSecond;
        }

        public override void Trigger(AuraTrigger trigger)
        {
            var pph = manager.iteration.mainHand.baseSpeed * ppm / (60 * Constants.kStepsPerSecond); //0.08
            var roll = manager.iteration.random.Next(0, 10000);
            if (roll > pph * 10000)
			{
                return;
			}
            // Refresh.
            if (active)
			{
                fade = manager.iteration.currentStep + duration;
                next = fade;
                auraSummary.refreshes += 1;
                return;
            }
            manager.iteration.statsManager.UpdateTemporaryAdditiveAttackPower();
            fade = manager.iteration.currentStep + duration;
            auraSummary.procs += 1;
            active = true;
            next = fade;
            start = manager.iteration.currentStep;
            return;
        }

        public override void Update()
        {
            if (!active) return;
            if (manager.iteration.currentStep != next)
            {
                return;
            }
            active = false;
            auraSummary.uptime += fade - start;
            next = int.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryAdditiveAttackPower();
        }
    }
    public class OffHandBerserking : Aura
    {
        public OffHandBerserking(AuraManager arg) : base(arg)
        {
            name = "Berserking (OH)";
            ppm = 1.0f;
            auraSummary.name = name;
            duration = 15 * Constants.kStepsPerSecond;
        }

        public override void Trigger(AuraTrigger trigger)
        {
            var pph = manager.iteration.mainHand.baseSpeed * ppm / (60 * Constants.kStepsPerSecond); //0.08
            var roll = manager.iteration.random.Next(0, 10000);
            if (roll > pph * 10000)
            {
                return;
            }
            // Refresh.
            if (active)
            {
                fade = manager.iteration.currentStep + duration;
                next = fade;
                auraSummary.refreshes += 1;
                return;
            }

            fade = manager.iteration.currentStep + duration;
            auraSummary.procs += 1;
            active = true;
            next = fade;
            start = manager.iteration.currentStep;
            return;
        }

        public override void Update()
        {
            if (!active) return;
            if (manager.iteration.currentStep != next)
            {
                return;
            }
            active = false;
            auraSummary.uptime += fade - start;
            next = int.MaxValue;
        }
    }

    // Cooldown
    public class HeroismAura : Aura
    {
        public HeroismAura(AuraManager arg) : base(arg)
        {
            name = "Heroism";
            auraSummary.name = name;
            duration = 40 * Constants.kStepsPerSecond;
            trigger.Add(AuraTrigger.AllMeleeCritical);
            trigger.Add(AuraTrigger.AllMeleeNonCritical);
            effects.Add(
                new Effect(
                    EffectType.Multiplicative,
                    Stat.MeleeHaste,
                    1.30f));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            start = manager.iteration.currentStep;
            active = true;
            next = start + duration;
            manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
            auraSummary.procs += 1;
        }

        public override void Update()
        {
            if (manager.iteration.currentStep != next) { return; }
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
        }

        public void Fade()
        {
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryHasteMultiplier();
        }
    }

    public class DeathWishAura : Aura
    {
        public DeathWishAura(AuraManager arg) : base(arg)
        {
            name = "Death Wish";
            auraSummary.name = name;
            duration = 30 * Constants.kStepsPerSecond;
            effects.Add(
                new Effect(
                    EffectType.Multiplicative,
                    Stat.MeleeDamage,
                    1.20f));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            start = manager.iteration.currentStep;
            active = true;
            next = start + duration;
            manager.iteration.statsManager.UpdateTemporaryDamageMultiplier();
            auraSummary.procs += 1;
        }

        public override void Update()
        {
            if (manager.iteration.currentStep != next) { return; }
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryDamageMultiplier();
        }

        public void Fade()
        {
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryDamageMultiplier();
        }
    }

}
 