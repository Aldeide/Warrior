using Warrior.Databases;
using Warrior.Entities;

namespace Warrior
{
    public class Enchant
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public ItemSlot slot { get; set; }
        public List<Effect> effects { get; set; }

    }

    public class Enchants
    {
        public Dictionary<ItemSlot, Enchant> enchants = new Dictionary<ItemSlot, Enchant>();

        public Enchants() {
            enchants[ItemSlot.Head] = EnchantDatabase.enchants.Single(e => e.name == "Arcanum of Torment");
            enchants[ItemSlot.Shoulders] = EnchantDatabase.enchants.Single(e => e.name == "Master's Inscription of the Axe");
            enchants[ItemSlot.Back] = EnchantDatabase.enchants.Single(e => e.name == "Major Agility");
        }

    }
}
