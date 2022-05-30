using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warrior
{
    public enum Race
    {
        [Description("Undead")]
        Undead,
        [Description("Gnome")]
        Gnome,
        [Description("Draenei")]
        Draenei,
        [Description("Dwarf")]
        Dwarf,
        [Description("Night Elf")]
        NightElf,
        [Description("Human")]
        Human,
        [Description("Orc")]
        Orc,
        [Description("Blood Elf")]
        BloodElf,
        [Description("Troll")]
        Troll,
        [Description("Tauren")]
        Tauren
    }
    public class Settings
    {
        public Settings()
        {
            this.combatLength = 180;
            this.executeProportion = 15.0f;
            this.iterations = 4;
            this.initialRage = 0;
            this.targetLevel = 83;
            this.targetResistance = 24;
            this.targetArmor = 10643;
        }
        public Race race { get; set; }

        public int combatLength { get; set; }
        public float executeProportion { get; set; }

        public int iterations { get; set; }
        public int initialRage { get; set; }
        public int targetLevel { get; set; }
        public int targetResistance { get; set; }
        public int targetArmor { get; set; }

        public bool useHeroism { get; set; }
        public int heroismStart { get; set; }
    }
}
