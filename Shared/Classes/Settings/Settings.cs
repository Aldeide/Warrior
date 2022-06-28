using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Warrior.Settings
{
    public class Settings
    {
        public string version { get; set; } = "1.0.0";
        public SimulationSettings simulationSettings {get;set;} = new SimulationSettings();
        public CharacterSettings characterSettings { get; set; } = new CharacterSettings();
        public EquipmentSettings equipmentSettings { get; set; } = new EquipmentSettings();
        public TalentsSettings talentSettings { get; set; } = new TalentsSettings();
        public GlyphSettings glyphSettings { get; set; } = new GlyphSettings();
        public EnchantSettings enchantSettings { get; set; } = new EnchantSettings();
        public GemSettings gemSettings { get; set; } = new GemSettings();
        public StanceSettings stanceSettings { get; set; } = new StanceSettings();

        public BuffSettings buffSettings { get; set; } = new BuffSettings();
        public DebuffSettings debuffSettings { get; set; } = new DebuffSettings();

        public Settings()
        {
        }
        public Race race { get; set; }

        public int initialRage { get; set; }
        public int targetLevel { get; set; }



    }
}
