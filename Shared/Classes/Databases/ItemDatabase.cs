using Warrior.Entities;

namespace Warrior.Databases
{
    public static class ItemDatabase
    {
        public static List<Gem> GetGemSockets(int itemId)
        {
            List<Gem> gemSockets = new List<Gem>();
            Item item = items.Where(i => i.id == itemId).FirstOrDefault();
             
            for (int i = 0; i < item.metaSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Meta });
            }
            for (int i = 0; i < item.redSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Red });
            }
            for (int i = 0; i < item.blueSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Red });
            }
            for (int i = 0; i < item.yellowSockets; i++)
            {
                gemSockets.Add(new Gem() { color = Color.Yellow });
            }
            return gemSockets;
        }
        public static List<Item> items { get; set; } = new List<Item>()
        {
            new Item() {
                id = 51227,
                name = "Sanctified Ymirjar Lord's Helmet",
                itemSlot = ItemSlot.Head,
                armor = 2239,
                strength = 185,
                stamina = 209,
                criticalStrikeRating = 114,
                armorPenetrationRating = 106,
                redSockets = 1,
                metaSockets = 1,
                gemBonusStrength = 8
            },
            new Item() {
                id = 54581,
                name = "Penumbra Pendant",
                itemSlot = ItemSlot.Neck,
                strength = 116,
                stamina = 124,
                criticalStrikeRating = 73,
                armorPenetrationRating = 65,
                yellowSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 51229,
                name = "Sanctified Ymirjar Lord's Shoulderplates",
                itemSlot = ItemSlot.Shoulders,
                armor = 2067,
                strength = 147,
                stamina = 155,
                criticalStrikeRating = 90,
                armorPenetrationRating = 82,
                redSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 50677,
                name = "Winding Sheet",
                itemSlot = ItemSlot.Back,
                armor = 185,
                strength = 108,
                stamina = 116,
                criticalStrikeRating = 68,
                hasteRating = 60,
                yellowSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 51225,
                name = "Sanctified Ymirjar Lord's Battleplate",
                itemSlot = ItemSlot.Chest,
                armor = 2756,
                strength = 193,
                stamina = 209,
                criticalStrikeRating = 122,
                armorPenetrationRating = 106,
                redSockets = 1,
                blueSockets = 1,
                gemBonusStrength = 6
            },
            new Item() {
                id = 50659,
                name = "Polar Bear Claw Bracers",
                itemSlot = ItemSlot.Wrist,
                armor = 1206,
                strength = 108,
                stamina = 116,
                hitRating = 122,
                criticalStrikeRating = 60,
                yellowSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 51226,
                name = "Sanctified Ymirjar Lord's Gauntlets",
                itemSlot = ItemSlot.Hands,
                armor = 2239,
                strength = 185,
                stamina = 209,
                criticalStrikeRating = 114,
                hitRating = 82,
                redSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 50620,
                name = "Coldwraith Links",
                itemSlot = ItemSlot.Waist,
                armor = 1550,
                strength = 139,
                stamina = 155,
                hitRating = 122,
                criticalStrikeRating = 85,
                armorPenetrationRating = 78,
                yellowSockets = 1,
                redSockets = 1,
                gemBonusStrength = 6
            },
            new Item() {
                id = 51228,
                name = "Sanctified Ymirjar Lord's Legplates",
                itemSlot = ItemSlot.Legs,
                armor = 2412,
                strength = 193,
                stamina = 208,
                expertiseRating = 106,
                criticalStrikeRating = 122,
                yellowSockets = 1,
                blueSockets = 1,
                gemBonusStrength = 6
            },
            new Item() {
                id = 54578,
                name = "Apocalypse's Advance",
                itemSlot = ItemSlot.Boots,
                armor = 1939,
                strength = 165,
                stamina = 165,
                criticalStrikeRating = 97,
                redSockets = 2,
                gemBonusStrength = 6
            },
            new Item() {
                id = 50657,
                name = "Skeleton Lord's Circle",
                itemSlot = ItemSlot.Ring1,
                strength = 108,
                stamina = 116,
                expertiseRating = 60,
                criticalStrikeRating = 68,
                yellowSockets = 1,
                gemBonusStrength = 4
            },
            new Item() {
                id = 54576,
                name = "Signet of Twilight",
                itemSlot = ItemSlot.Ring1,
                agility = 109,
                stamina = 109,
                attackPower = 145,
                hitRating = 57,
                criticalStrikeRating = 73,
                yellowSockets = 1,
                gemBonusAgility = 4
            },
            new Item() {
                id = 50363,
                name = "Deathbringer's Will",
                itemSlot = ItemSlot.Trinket1,
                armorPenetrationRating = 167
            },
            new Item() {
                id = 54590,
                name = "Sharpened Twilight Scale",
                itemSlot = ItemSlot.Trinket1,
                armorPenetrationRating = 184
            },
            new Item() {
                id = 50730,
                name = "Glorenzelg, High-Blade of the Silver Hand",
                itemSlot = ItemSlot.MainHand,
                itemType = ItemType.TwoHandedSword,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedSword,
                speed = 3.6f,
                minDamage = 991,
                maxDamage = 1487,
                strength = 198,
                stamina = 222,
                criticalStrikeRating = 122,
                expertiseRating = 114,
                redSockets = 3,
                gemBonusStrength = 8
            },
            new Item() {
                id = 50733,
                name = "Fal'inrush Defender of Quel'thalas",
                itemSlot = ItemSlot.Ranged,
                agility = 62,
                stamina = 62,
                attackPower = 66,
                criticalStrikeRating = 41,
                armorPenetrationRating = 33,
                redSockets = 1,
                gemBonusAgility = 4
            },
            new Item() {
                id = 50606,
                name = "Gendarme's Cuirass",
                itemSlot = ItemSlot.Chest,
                armor = 2756,
                strength = 185,
                stamina = 209,
                hitRating = 106,
                criticalStrikeRating = 114,
                yellowSockets = 1,
                redSockets = 1,
                blueSockets = 1,
                gemBonusStrength = 8
            },
        };
    }
}
