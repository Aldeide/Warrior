﻿namespace Warrior
{
    public class Equipment
    {
        public Dictionary<ItemSlot, Item> items { get; set; }
        public Simulation simulation;
        public CharacterStats equipmentStats { get; set; } = new CharacterStats();
        public Enchants enchants { get; set; } = new Enchants();
        public Equipment(Simulation simulation)
        {
            items = new Dictionary<ItemSlot, Item>();
            this.simulation = simulation;
            /*
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
            items[ItemSlot.Ring1] = ItemDatabase.items.Single(i => i.id == 50657);
            items[ItemSlot.Ring2] = ItemDatabase.items.Single(i => i.id == 54576);
            items[ItemSlot.Trinket1] = ItemDatabase.items.Single(i => i.id == 54590);
            items[ItemSlot.Trinket2] = ItemDatabase.items.Single(i => i.id == 50363);
            items[ItemSlot.Ranged] = ItemDatabase.items.Single(i => i.id == 50733);
            items[ItemSlot.Hands] = ItemDatabase.items.Single(i => i.id == 51226);
            items[ItemSlot.Boots] = ItemDatabase.items.Single(i => i.id == 54578);
            */
            UpdateEquipmentStats();
        }
        public int GetSlotId(ItemSlot slot)
        {
            bool success = items.TryGetValue(slot, out var item);
            if (success)
            {
                return item.id;
            }
            return -1;
        }
        public void EquipItem(ItemSlot slot, int id)
        {
            items[slot] = ItemDatabase.items.Single(i => i.id == id);
            UpdateEquipmentStats();
            //simulation.character.UpdateAdditiveCharacterStats();
        }
        public void UpdateEquipmentStats()
        {
            equipmentStats.agility = ComputeGearAgility();
            equipmentStats.intellect = ComputeGearIntellect();
            equipmentStats.stamina = ComputeGearStamina();
            equipmentStats.spirit = ComputeGearSpirit();
            equipmentStats.strength = ComputeGearStrength();
            equipmentStats.armor = ComputeGearArmor();
            equipmentStats.hitRating = ComputeGearHitRating();
            equipmentStats.criticalStrikeRating = ComputeGearCritRating();
            equipmentStats.hasteRating = ComputeGearHasteRating();
            equipmentStats.expertiseRating = ComputeGearExpertiseRating();
            equipmentStats.armorPenetrationRating = ComputeGearArmorPenetrationRating();
            equipmentStats.attackPower = ComputeGearAP();
        }
        public Item GetItemBySlot(ItemSlot slot)
        {
            bool success = items.TryGetValue(slot, out var item);
            if (success)
            {
                return item;
            }
            return item;
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
        public int MainHandMinDamage()
        {
            bool success = items.TryGetValue(ItemSlot.MainHand, out var item);
            if (success)
            {
                return item.minDamage;
            }
            return -1;
        }
        public int MainHandMaxDamage()
        {
            bool success = items.TryGetValue(ItemSlot.MainHand, out var item);
            if (success)
            {
                return item.minDamage;
            }
            return -1;
        }
    }
}
