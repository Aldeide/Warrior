using Warrior.Databases;
using Warrior.Entities;

namespace Warrior
{
    [Serializable]
    public class Enchant
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = "";
        public ItemSlot slot { get; set; }
        public List<Effect> effects { get; set; } = new List<Effect>();

    }
}
