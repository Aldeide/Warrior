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
        public MortalStrike? mortalStrike { get; set; }
        public Whirlwind whirlwind { get; set; }
        public HeroicStrike heroicStrike { get; set; }

        public Slam slam { get; set; }
        public BloodRageAbility bloodrage { get; set; }

        public DeathWish deathWish { get; set; }
        public Heroism heroism { get; set; }
        public ShatteringThrow shatteringThrow { get; set; }

        // Racial abilities
        public Berserking? berserking { get; set; }
        public BloodFury? bloodFury { get; set; }

        public AbilityManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (iteration.computedConstants.hasBloodthirst)
            {
                bloodthirst = new Bloodthirst(iteration);
            }
            if (iteration.computedConstants.hasMortalStrike)
            {
                mortalStrike = new MortalStrike(iteration);
            }
            whirlwind = new Whirlwind(iteration);
            heroicStrike = new HeroicStrike(iteration);
            slam = new Slam(iteration);
            bloodrage = new BloodRageAbility(iteration);
            deathWish = new DeathWish(iteration);
            heroism = new Heroism(iteration);
            shatteringThrow = new ShatteringThrow(iteration);
            if (iteration.settings.characterSettings.race == Settings.Race.Orc
                && iteration.settings.simulationSettings.useBloodFury)
            {
                bloodFury = new BloodFury(iteration);
            }
            if (iteration.settings.characterSettings.race == Settings.Race.Troll
                && iteration.settings.simulationSettings.useBerserking)
            {
                berserking = new Berserking(iteration);
            }

        }
        public void ApplyTime(int d)
        {
            bloodthirst?.ApplyTime(d);
            whirlwind.ApplyTime(d);
            heroicStrike.ApplyTime(d);
            bloodrage.ApplyTime(d);
            deathWish.ApplyTime(d);
            heroism.ApplyTime(d);
            shatteringThrow.ApplyTime(d);
            mortalStrike?.ApplyTime(d);
            bloodFury?.ApplyTime(d);
            berserking?.ApplyTime(d);
        }

        public void GetNext()
        {
            iteration.nextStep.abilities = int.MaxValue;
            if (slam.isCasting) iteration.nextStep.abilities = slam.endCast;
            if (shatteringThrow.isCasting && shatteringThrow.endCast < iteration.nextStep.abilities) iteration.nextStep.abilities = shatteringThrow.endCast;
        }

        public void Reset()
        {
            bloodthirst?.Reset();
            mortalStrike?.Reset();
            whirlwind.Reset();
            heroicStrike.Reset();
            slam.Reset();
            bloodrage.Reset();
            deathWish.Reset();
            heroism.Reset();
            shatteringThrow.Reset();
            berserking?.Reset();
            bloodFury?.Reset();
        }

    }
}
