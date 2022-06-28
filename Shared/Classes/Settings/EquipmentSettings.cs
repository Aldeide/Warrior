using Warrior.Entities;
using Warrior.Databases;

namespace Warrior.Settings
{
    public class EquipmentSettings
    {
        public Dictionary<ItemSlot, Item> items { get; set; }
        public Dictionary<ItemSlot, Enchant> enchants { get; set; }
        public EquipmentSettings()
        {
            items = new Dictionary<ItemSlot, Item>();
            items[ItemSlot.MainHand] = ItemDatabase.items.Single(i => i.id == 50730);
            items[ItemSlot.OffHand] = ItemDatabase.items.Single(i => i.id == 50730);
            items[ItemSlot.Head] = ItemDatabase.items.Single(i => i.id == 51227);
            items[ItemSlot.Neck] = ItemDatabase.items.Single(i => i.id == 54581);
            items[ItemSlot.Shoulders] = ItemDatabase.items.Single(i => i.id == 51229);
            items[ItemSlot.Back] = ItemDatabase.items.Single(i => i.id == 50677);
            items[ItemSlot.Chest] = ItemDatabase.items.Single(i => i.id == 51225);
            items[ItemSlot.Wrist] = ItemDatabase.items.Single(i => i.id == 50659);
            items[ItemSlot.Waist] = ItemDatabase.items.Single(i => i.id == 50620);
            items[ItemSlot.Legs] = ItemDatabase.items.Single(i => i.id == 51228);
            items[ItemSlot.Ring1] = ItemDatabase.items.Single(i => i.id == 54576);
            items[ItemSlot.Ring2] = ItemDatabase.items.Single(i => i.id == 50657);
            items[ItemSlot.Trinket1] = ItemDatabase.items.Single(i => i.id == 50363);
            items[ItemSlot.Trinket2] = ItemDatabase.items.Single(i => i.id == 54590);
            items[ItemSlot.Ranged] = ItemDatabase.items.Single(i => i.id == 50733);
            items[ItemSlot.Hands] = ItemDatabase.items.Single(i => i.id == 51226);
            items[ItemSlot.Boots] = ItemDatabase.items.Single(i => i.id == 54578);
        }
        public void EquipItem(ItemSlot slot, int id)
        {
            items[slot] = ItemDatabase.items.Single(i => i.id == id);
        }
        public int? GetSlotId(ItemSlot slot)
        {
            bool success = items.TryGetValue(slot, out var item);
            if (success)
            {
                return item.id;
            }
            return null;
        }
        public Item? GetItemBySlot(ItemSlot slot)
        {
            bool success = items.TryGetValue(slot, out var item);
            if (success)
            {
                return item;
            }
            return null;
        }
        public int ComputeGearStrength()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.strength;
            }
            return output;
        }
        public int ComputeGearAgility()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.agility;
            }
            return output;
        }
        public int ComputeGearIntellect()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.intellect;
            }
            return output;
        }
        public int ComputeGearStamina()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.stamina;
            }
            return output;
        }
        public int ComputeGearSpirit()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.spirit;
            }
            return output;
        }
        public int ComputeGearAP()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.attackPower;
            }
            return output;
        }
        public int ComputeGearHitRating()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.hitRating;
            }
            return output;
        }
        public int ComputeGearCritRating()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.criticalStrikeRating;
            }
            return output;
        }
        public int ComputeGearHasteRating()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.hasteRating;
            }
            return output;
        }
        public int ComputeGearArmorPenetrationRating()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.armorPenetrationRating;
            }
            return output;
        }
        public int ComputeGearExpertiseRating()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.expertiseRating;
            }
            return output;
        }
        public int ComputeGearArmor()
        {
            int output = 0;
            foreach (KeyValuePair<ItemSlot, Item> item in items)
            {
                output += item.Value.armor;
            }
            return output;
        }
        public List<Gem> GetGemSockets(ItemSlot slot)
		{
            List<Gem> gemSockets = new List<Gem>();
            Item? item = GetItemBySlot(slot);

            if (item == null)
			{
                return gemSockets;
			}

            for (int i = 0; i < item.metaSockets; i++)
			{
                gemSockets.Add(new Gem() { color = Color.Meta });
			}
            for (int i = 0; i < item.yellowSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Yellow });
            }
            for (int i = 0; i < item.redSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Red });
            }
            for (int i = 0; i < item.blueSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Blue });
            }

            return gemSockets;
		}


    }
}
