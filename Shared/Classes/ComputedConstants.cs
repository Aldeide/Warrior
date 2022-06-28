namespace Warrior
{
    public class ComputedConstants
    {
        public float bleedDamageMultiplier { get; set; } = 1;

        public float deepWoundsDamageMultiplier { get; set; } = 1;
        public float allDamageMultiplier { get; set; } = 1;
        public float meleeDamageMultiplier { get; set; } = 1;
        public float offHandDamageMultiplier { get; set; } = 1;
        public float criticalDamageMultiplier { get; set; } = 1;
        public float titansGripDamageMultiplier { get; set; } = 1;
        public float rendDamageMultiplier { get; set; } = 1;

        public float slamDamageMultiplier { get; set; } = 1;
        public float unendingFuryDamageMultiplier { get; set; } = 1;
        public float dualWieldSpecializationMultiplier { get; set; } = 1;
        public bool hasBloodthirst { get; set; } = false;
        public bool hasMortalStrike { get; set; } = false;
        public float improvedMortalStrikeMultiplier { get; set; } = 1;
        public bool hasDeepWounds { get; set; } = false;
        public bool hasBloodsurge { get; set; } = false;
        public float bloodsurgeChance { get; set; } = 0;

        public bool HasAngerManagement { get; set; } = false;

        public int focusedRageRageReduction { get; set; } = 0;


        public bool hasMHBerserking { get; set; } = false;
        public bool hasOHBerserking { get; set; } = false;

        public bool hasEndlessRage { get; set; } = false;

        public float bloodFrenzySpeedMultiplier { get; set; } = 1;

    }
}
