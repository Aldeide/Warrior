using Warrior.Entities;
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
            if (manager.iteration.currentStep != next || !active)
            {
                return;
            }
            damage -= tickSize;
            currentDuration -= 1 * Constants.kStepsPerSecond;
            next = manager.iteration.currentStep + 1 * Constants.kStepsPerSecond;
            
            dotSummary.totalDamage += tickSize;
            dotSummary.ticks += 1;
            if (currentDuration <= 0)
            {
                damage = 0;
                active = false;
                next = int.MaxValue;
                dotSummary.uptime += (manager.iteration.currentStep - start);
            }

        }
        public void Fade()
        {
            damage = 0;
            active = false;
            next = int.MaxValue;
            dotSummary.uptime += (manager.iteration.currentStep - start);
        }
        public float DeepWoundsDamage(AuraTrigger trigger)
        {
            float dmg = 0;
            if (trigger == AuraTrigger.MainHandCritical)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
            }
            if (trigger == AuraTrigger.OffHandCritical)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
            }
            if (trigger == AuraTrigger.Whirlwind)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
            }
            if (trigger == AuraTrigger.Bloodthirst)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
            }
            if (trigger == AuraTrigger.HeroicStrike)
            {
                dmg = (int)(manager.iteration.computedConstants.deepWoundsDamageMultiplier * DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0));
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
                active = false;
                auraSummary.uptime += fade - start;
                next = int.MaxValue;
            }
        }

        public void Fade()
        {
            active = false;
            auraSummary.uptime += fade - start;
            next = int.MaxValue;
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
                manager.iteration.IncrementRage((int)(10 * (1 + manager.iteration.settings.talentSettings.ImprovedBloodrage.rank * 0.25f)) , "Bloodrage");
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
            manager.iteration.IncrementRage((int)((1 + manager.iteration.settings.talentSettings.ImprovedBloodrage.rank * 0.25f)), "Bloodrage");
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

    public class ShatteringThrowAura : Aura
    {
        public ShatteringThrowAura(AuraManager arg) : base(arg)
        {
            name = "Shattering Throw";
            auraSummary.name = name;
            duration = 10 * Constants.kStepsPerSecond;
        }
        public override void Trigger(AuraTrigger trigger)
        {
            start = manager.iteration.currentStep;
            active = true;
            next = start + duration;
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
        }

        public void Fade()
        {
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
        }
    }

    public class RendAura : Aura
    {
        public RendAura(AuraManager arg) : base(arg)
        {
            name = "Rend";
            auraSummary.name = name;
            dotSummary.name = name;
            trigger.Add(AuraTrigger.MainHandCritical);
            trigger.Add(AuraTrigger.OffHandCritical);
            tickInterval = 3 * Constants.kStepsPerSecond;
            duration = 15 * Constants.kStepsPerSecond;
        }
        public override void Trigger(AuraTrigger trigger)
        {
            if (!active)
            {
                active = true;
                auraSummary.procs += 1;
                start = manager.iteration.currentStep;
                damage = (int)RendDamage();
                tickSize = (int)(damage * tickInterval / (float)duration);
                dotSummary.applications += 1;
            }
            else
            {
                auraSummary.refreshes += 1;
                damage += (int)RendDamage();
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
            damage -= tickSize;
            currentDuration -= tickInterval;
            next = manager.iteration.currentStep + tickInterval;
            dotSummary.totalDamage += tickSize;
            dotSummary.ticks += 1;
            if (currentDuration <= 0)
            {
                damage = 0;
                active = false;
                next = int.MaxValue;
                dotSummary.uptime += (manager.iteration.currentStep - start);
            }

        }
        public void Fade()
        {
            damage = 0;
            active = false;
            next = int.MaxValue;
            dotSummary.uptime += (manager.iteration.currentStep - start);
        }
        public float RendDamage()
        {
            // TODO: extra damage when target over 75% health.
            int damage = (int)(DamageUtils.AverageWeaponDamage(manager.iteration.mainHand, manager.iteration, 0) + 380
                * manager.iteration.computedConstants.rendDamageMultiplier
                * manager.iteration.statsManager.GetEffectiveDamageMultiplier());
            return damage;
        }
        public override int GetNext()
        {
            return next;
        }
    }

    public class BloodFuryAura : Aura
    {
        public BloodFuryAura(AuraManager arg) : base(arg)
        {
            name = "Blood Fury";
            auraSummary.name = name;
            duration = 15 * Constants.kStepsPerSecond;
            effects.Add(
                new Effect(
                    EffectType.Additive,
                    Stat.AttackPower,
                    326f));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            start = manager.iteration.currentStep;
            active = true;
            next = start + duration;
            manager.iteration.statsManager.UpdateTemporaryAdditiveAttackPower();
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
            manager.iteration.statsManager.UpdateTemporaryAdditiveAttackPower();
        }

        public void Fade()
        {
            if (active)
            {
                auraSummary.uptime += (manager.iteration.currentStep - start);
            }
            active = false;
            next = Int32.MaxValue;
            manager.iteration.statsManager.UpdateTemporaryAdditiveAttackPower();
        }
    }

    public class BerserkingAura : Aura
    {
        public BerserkingAura(AuraManager arg) : base(arg)
        {
            name = "Berserking";
            auraSummary.name = name;
            duration = 10 * Constants.kStepsPerSecond;
            effects.Add(
                new Effect(
                    EffectType.Multiplicative,
                    Stat.MeleeHaste,
                    1.20f));
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

    public class WreckingCrew : Aura
    {
        public WreckingCrew(AuraManager arg) : base(arg)
        {
            name = "Wrecking Crew";
            auraSummary.name = name;
            duration = 12 * Constants.kStepsPerSecond;
            effects.Add(
                new Effect(
                    EffectType.Multiplicative,
                    Stat.MeleeDamage,
                    1 + manager.iteration.settings.talentSettings.WreckingCrew.rank * 0.02f));
        }
        public override void Trigger(AuraTrigger trigger)
        {
            if (active)
            {
                next = manager.iteration.currentStep + duration;
                auraSummary.refreshes += 1;
                return;
            }
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
 