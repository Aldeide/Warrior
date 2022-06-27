namespace Warrior
{
    public enum AuraTrigger
    {
        None,
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
        public Iteration iteration;
        public List<Ability> abilities = new List<Ability>();

        public Bloodthirst? bloodthirst { get; set; }
        public Whirlwind whirlwind { get; set; }
        public HeroicStrike heroicStrike { get; set; }

        public Slam slam { get; set; }
        public BloodRageAbility bloodrage { get; set; }

        public DeathWish deathWish { get; set; }

        public AbilityManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (iteration.computedConstants.hasBloodthirst)
            {
                bloodthirst = new Bloodthirst(iteration);
            }
            whirlwind = new Whirlwind(iteration);
            heroicStrike = new HeroicStrike(iteration);
            slam = new Slam(iteration);
            bloodrage = new BloodRageAbility(iteration);
            deathWish = new DeathWish(iteration);
        }
        public void ApplyTime(int d)
        {
            bloodthirst?.ApplyTime(d);
            whirlwind.ApplyTime(d);
            heroicStrike.ApplyTime(d);
            bloodrage.ApplyTime(d);
            deathWish.ApplyTime(d);
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
