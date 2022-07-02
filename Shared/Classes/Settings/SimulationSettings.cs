using System.Runtime.Serialization;

namespace Warrior.Settings
{
    [DataContract]
    public class SimulationSettings
    {
        [DataMember]
        public int numIterations { get; set; } = 10;
        [DataMember]
        public int combatLength { get; set; } = 120;
        [DataMember]
        public int targetLevel { get; set; } = 83;
        [DataMember]
        public int targetArmor { get; set; } = 10643;
        [DataMember]
        public float executePercent { get; set; } = 19;
        [DataMember]
        public int initialRage { get; set; } = 0;

        [DataMember]
        public bool useBloodthirst { get; set; } = true;
        [DataMember]
        public bool useWhirlwind { get; set; } = true;
        [DataMember]
        public bool useMortalStrike { get; set; } = true;
        [DataMember]
        public bool useSunderArmor { get; set; } = true;
        [DataMember]
        public bool useSlam { get; set; } = true;
        [DataMember]
        public bool slamOnlyOnBloodsurge { get; set; } = true;
        [DataMember]
        public bool useHeroicStrike { get; set; } = true;
        [DataMember]
        public int heroicStrikeRagethreshold { get; set; } = 30;
        [DataMember]
        public bool useHeroism { get; set; } = true;
        [DataMember]
        public int heroismOnLastSeconds { get; set; } = 41;
        [DataMember]
        public bool useDeathWish { get; set; } = true;
        [DataMember]
        public int deathWishOnLastSeconds { get; set; } = 31;

        [DataMember]
        public bool useShatteringThrow { get; set; } = true;
        [DataMember]
        public float targetOverSeventyFiveThreshold { get; set; } = 25;
        [DataMember]
        public int shatteringThrowOnLastSeconds { get; set; } = 33;
        [DataMember]
        public bool useBloodRage { get; set; } = true;
        [DataMember]
        public int bloodRageThreshold { get; set; } = 80;
        [DataMember]
        public bool useRend { get; set; } = false;

        // Racial abilities.
        [DataMember]
        public bool useBloodFury { get; set; } = false;
        [DataMember]
        public bool useBerserking { get; set; } = false;
        [DataMember]
        public float bloodFuryOnLastSeconds { get; set; } = 16;
        [DataMember]
        public float berserkingOnLastSeconds { get; set; } = 11;

        // Execute phase
        [DataMember]
        public bool useExecute { get; set; } = true;
        [DataMember]
        public bool prioritizeBloodthirst { get; set; } = true;
        [DataMember]
        public bool prioritizeWhirlwind { get; set; } = true;
        [DataMember]
        public bool prioritizeSlamOnBloodsurge { get; set; } = true;
        [DataMember]
        public bool useExecuteHeroicStrike { get; set; } = true;
        [DataMember]
        public int executeHeroicStrikeRagethreshold { get; set; } = 30;
    }
}
