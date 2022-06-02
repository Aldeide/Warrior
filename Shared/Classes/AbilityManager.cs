﻿namespace Warrior
{
    public enum AuraTrigger
    {
        AllMeleeAttacks,
        AllMeleeNonCritical,
        AllMeleeCritical,
        MainHandCritical,
        OffHandCritical,
        Bloodthirst,
        Whirlwind,
        HeroicStrike,
        Use

    }
    public class AbilityManager
    {
        public event EventHandler AbilityHit;
        public event EventHandler AbilityCrit;

        public Iteration iteration;
        public List<Ability> abilities = new List<Ability>();

        public Bloodthirst? bloodthirst { get; set; }
        public Whirlwind whirlwind { get; set; }
        public HeroicStrike heroicStrike { get; set; }

        public Slam slam { get; set; }

        public AbilityManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (iteration.simulation.computedConstants.hasBloodthirst)
            {
                bloodthirst = new Bloodthirst(iteration);
            }
            whirlwind = new Whirlwind(iteration);
            heroicStrike = new HeroicStrike(iteration);
            slam = new Slam(iteration);
        }
        public void ApplyTime(int d)
        {
            bloodthirst?.ApplyTime(d);
            whirlwind.ApplyTime(d);
            heroicStrike.ApplyTime(d);
        }
        public void UseAbility(string name)
        {
        }
        public void GetNext()
        {
            int next = int.MaxValue;
            if (abilities.Where(a => a.currentCooldown > 0).Count() == 0)
            {
                iteration.nextStep.abilities = int.MaxValue;
            } else
            {
                iteration.nextStep.abilities = iteration.currentStep + abilities.Where(a => a.currentCooldown > 0).Min(a => a.currentCooldown);
            }
        }

    }
}
