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


        public bool HasBloodthirst { get; set; } = false;
        public bool HasMortalStrike { get; set; } = false;

        public bool HasDeepWounds { get; set; } = false;
        public bool HasBloodsurge { get; set; } = false;
        public bool HasAngerManagement { get; set; } = false;


    }
}
