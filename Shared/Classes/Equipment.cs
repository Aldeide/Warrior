namespace Warrior
{
    public class Equipment
    {
        public Dictionary<ItemSlot, Item> items { get; set; }
        public Simulation simulation;
        public CharacterStats equipmentStats { get; set; } = new CharacterStats();
        public Equipment(Simulation simulation)
        {
            items = new Dictionary<ItemSlot, Item>();
            this.simulation = simulation;
            
            items[ItemSlot.MainHand] = simulation.itemDatabase.GetItemById(50730);
            items[ItemSlot.OffHand] = simulation.itemDatabase.GetItemById(50730);
            items[ItemSlot.Head] = simulation.itemDatabase.GetItemById(51227);
            items[ItemSlot.Neck] = simulation.itemDatabase.GetItemById(54581);
            items[ItemSlot.Shoulders] = simulation.itemDatabase.GetItemById(51229);
            items[ItemSlot.Back] = simulation.itemDatabase.GetItemById(50677);
            items[ItemSlot.Chest] = simulation.itemDatabase.GetItemById(51225);
            items[ItemSlot.Wrist] = simulation.itemDatabase.GetItemById(50659);
            items[ItemSlot.Waist] = simulation.itemDatabase.GetItemById(50620);
            items[ItemSlot.Legs] = simulation.itemDatabase.GetItemById(51228);
            items[ItemSlot.Ring1] = simulation.itemDatabase.GetItemById(50657);
            items[ItemSlot.Ring2] = simulation.itemDatabase.GetItemById(54576);
            items[ItemSlot.Trinket1] = simulation.itemDatabase.GetItemById(54590);
            items[ItemSlot.Trinket2] = simulation.itemDatabase.GetItemById(50363);
            items[ItemSlot.Ranged] = simulation.itemDatabase.GetItemById(50733);
            items[ItemSlot.Gloves] = simulation.itemDatabase.GetItemById(51226);
            items[ItemSlot.Boots] = simulation.itemDatabase.GetItemById(54578);
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
            items[slot] = simulation.itemDatabase.GetItemById(id);
            UpdateEquipmentStats();
            simulation.character.UpdateAdditiveCharacterStats();
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
