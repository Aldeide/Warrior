namespace Warrior
{
    public class ComputedConstants
    {
        public float bleedDamageMultiplier { get; set; } = 0;

        public float deepWoundsDamageMultiplier { get; set; } = 0;
        public float allDamageMultiplier { get; set; } = 0;
        public float meleeDamageMultiplier { get; set; } = 0;
        public float offHandDamageMultiplier { get; set; } = 0;
        public float criticalDamageMultiplier { get; set; } = 0;
        public float titansGripDamageMultiplier { get; set; } = 0;
        public float rendDamageMultiplier { get; set; } = 0;


        public bool hasBloodthirst { get; set; } = false;
        public bool hasMortalStrike { get; set; } = false;

        public bool hasDeepWounds { get; set; } = false;
        public bool hasBloodsurge { get; set; } = false;
        public float bloodsurgeChance { get; set; } = 0;

        public bool HasAngerManagement { get; set; } = false;

        public int focusedRageRageReduction { get; set; } = 0;


    }
}
