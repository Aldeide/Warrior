using System.ComponentModel;
namespace Warrior.Settings
{
    [Serializable]
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
    [Serializable]
    public class CharacterSettings
    {
        public Race race { get; set; } = Race.Undead;
        public CharacterSettings()
        {

        }
    }
}
