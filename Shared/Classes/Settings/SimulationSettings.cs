namespace Warrior.Settings
{
    public class SimulationSettings
    {
        public int numIterations { get; set; } = 10;
        public int combatLength { get; set; } = 120;
        public int targetLevel { get; set; } = 83;
        public int targetArmor { get; set; } = 10643;

        public bool useBloodthirst { get; set; } = true;
        public bool useWhirlwind { get; set; } = true;
        public bool useMortalStrike { get; set; } = true;
        public bool useSunderArmor { get; set; } = true;
        public bool useSlam { get; set; } = true;
        public bool slamOnlyOnBloodsurge { get; set; } = true;
        public bool useHeroicStrike { get; set; } = true;
        public int heroicStrikeRagethreshold { get; set; } = 30;
        public bool useHeroism { get; set; } = true;
        public int heroismOnLastSeconds { get; set; } = 41;
        public bool useDeathWish { get; set; } = true;
        public int deathWishOnLastSeconds { get; set; } = 31;

        public bool useShatteringThrow { get; set; } = true;
        public int shatteringThrowOnLastSeconds { get; set; } = 33;
        public bool useBloodRage { get; set; } = true;
        public int bloodRageThreshold { get; set; } = 80;
        public bool maintainRend { get; set; } = false;

        // Racial abilities.
        public bool useBloodFury { get; set; } = false;
        public bool useBerserking { get; set; } = false;
        public float bloodFuryOnLastSeconds { get; set; } = 16;
        public float berserkingOnLastSeconds { get; set; } = 11;

        public SimulationSettings()
        {

        }
    }
}
