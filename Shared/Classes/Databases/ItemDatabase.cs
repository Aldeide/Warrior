using Warrior.Entities;

namespace Warrior.Databases
{
    [Serializable]
    public static class ItemDatabase
    {
        public static List<Gem> GetGemSockets(int itemId)
        {
            List<Gem> gemSockets = new List<Gem>();
            if (items == null) return gemSockets;
            Item? item = items.Where(i => i.id == itemId).FirstOrDefault();
             
            if (item == null) return gemSockets;

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
            #region Default Equipment
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
            #endregion

            #region weapons below ilvl 200

            new Item() {
                id = 35618,
                ilvl = 174,
                name = "Troll Butcherer",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedSword,
                itemSource = ItemSource.Dungeon,
                speed = 3.1f,
                minDamage = 352,
                maxDamage = 528,
                criticalStrikeRating = 85,
                attackPower = 120
            },
            new Item() {
                id = 41816,
                ilvl = 175,
                name = "De-Raged Waraxe",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedAxe,
                itemSource = ItemSource.Quest,
                speed = 3.5f,
                minDamage = 406,
                maxDamage = 610,
                stamina = 93,
                strength = 61,
                criticalStrikeRating = 30,
                hitRating = 47
            },
            new Item() {
                id = 41188,
                ilvl = 179,
                name = "Saronite Mindcrusher",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedMace,
                itemSource = ItemSource.Crafting,
                speed = 3.5f,
                minDamage = 406,
                maxDamage = 610,
                stamina = 57,
                strength = 88,
                criticalStrikeRating = 38,
                hitRating = 25
            },
            new Item() {
                id = 44708,
                ilvl = 187,
                name = "Dirkee's Superstructure",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedMace,
                itemSource = ItemSource.WorldDrop,
                speed = 3.3f,
                minDamage = 412,
                maxDamage = 618,
                stamina = 99,
                criticalStrikeRating = 44,
                attackPower = 114,
                armorPenetrationRating = 59
            },
            new Item() {
                id = 37108,
                ilvl = 187,
                name = "Dreadlord's Blade",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedSword,
                itemSource = ItemSource.Dungeon,
                speed = 3.4f,
                minDamage = 424,
                maxDamage = 637,
                strength = 70,
                stamina = 102,
                hitRating = 68
            },
            new Item() {
                id = 36962,
                ilvl = 187,
                name = "Wyrmclaw Battleaxe",
                itemSlot = ItemSlot.MainHand,
                weaponHandedness = WeaponHandedness.TwoHand,
                weaponType = WeaponType.TwoHandedAxe,
                itemSource = ItemSource.Dungeon,
                speed = 3.5f,
                minDamage = 437,
                maxDamage = 656,
                agility = 69,
                stamina = 60,
                hasteRating = 50,
                attackPower = 138
            },
            #endregion

            #region Head below ilvl 200
            new Item() {
                id = 37793,
                ilvl = 170,
                name = "Skullcage of Eternal Terror",
                itemSlot = ItemSlot.Head,
                itemSource = ItemSource.WorldDrop,
                armor = 1572,
                strength = 57,
                stamina = 82,
                criticalStrikeRating = 37,
                hitRating = 43
            },
            new Item() {
                id = 40673,
                ilvl = 171,
                name = "Tempered Saronite Helm",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Crafting,
                armor = 1581,
                strength = 46,
                stamina = 52,
            },
            new Item() {
                id = 44040,
                ilvl = 174,
                name = "The Crusader's Resolution",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Quest,
                armor = 1611,
                strength = 43,
                stamina = 91,
                expertiseRating = 38
            },
            new Item() {
                id = 35670,
                ilvl = 183,
                name = "Brann's Lost Mining Helmet",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Dungeon,
                armor = 1681,
                strength = 52,
                stamina = 60,
                criticalStrikeRating = 92
            },
            new Item() {
                id = 44412,
                ilvl = 187,
                name = "Faceguard of Punishment",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Quest,
                armor = 1700,
                strength = 70,
                stamina = 102,
                hitRating = 68
            },
            new Item() {
                id = 41350,
                ilvl = 187,
                name = "Savage Saronite Skullshield",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Crafting,
                armor = 1700,
                strength = 37,
                stamina = 78,
                criticalStrikeRating = 70
            },
            new Item() {
                id = 41344,
                ilvl = 187,
                name = "Helm of Command",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Crafting,
                armor = 1700,
                strength = 95,
                criticalStrikeRating = 55,
                hasteRating = 41
            },
            new Item() {
                id = 36969,
                ilvl = 187,
                name = "Helm of the Ley-Guardian",
                itemSlot = ItemSlot.Head,
                itemSource  = ItemSource.Dungeon,
                armor = 1700,
                strength = 40,
                stamina = 105,
                hitRating = 36,
                hasteRating = 41,
                redSockets = 1,
                gemBonusStamina = 6
            },
            #endregion

            #region Shoulders below ilvl 200
            new Item() {
                id = 40675,
                ilvl = 171,
                name = "Tempered Saronite Shoulders",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Crafting,
                armor = 1460,
                strength = 41,
                stamina = 84,
            },
            new Item() {
                id = 43198,
                ilvl = 174,
                name = "Mantle of Volkhan",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Quest,
                armor = 1487,
                strength = 54,
                stamina = 45,
                hitRating = 46
            },
            new Item() {
                id = 44373,
                ilvl = 175,
                name = "Pauldrons of Reconnaissance",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Dungeon,
                armor = 1496,
                strength = 34,
                stamina = 69,
                expertiseRating = 24
            },
            new Item() {
                id = 43387,
                ilvl = 175,
                name = "Shoulderplates of the Beholder",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Dungeon,
                armor = 1496,
                strength = 40,
                stamina = 67,
                hitRating = 36,
                redSockets = 1,
                gemBonusStamina = 6
            },
            new Item() {
                id = 44112,
                ilvl = 187,
                name = "Glimmershell Shoulder Protectors",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Reputation,
                armor = 1570,
                strength = 50,
                stamina = 75,
            },
            new Item() {
                id = 44111,
                ilvl = 187,
                name = "Gold Star Spaulders",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Reputation,
                armor = 1570,
                strength = 50,
                stamina = 57,
                criticalStrikeRating = 58
            },
            new Item() {
                id = 41351,
                ilvl = 187,
                name = "Savage Saronite Pauldrons",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Crafting,
                armor = 1570,
                strength = 52,
                stamina = 66,
                criticalStrikeRating = 43
            },
            new Item() {
                id = 39534,
                ilvl = 187,
                name = "Pauldrons of the Lightning Revenant",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Dungeon,
                armor = 1570,
                strength = 44,
                stamina = 76,
                hitRating = 42,
                redSockets = 1,
                gemBonusStamina = 6
            },
            new Item() {
                id = 37115,
                ilvl = 187,
                name = "Crusader's Square Pauldrons",
                itemSlot = ItemSlot.Shoulders,
                itemSource = ItemSource.Dungeon,
                armor = 1570,
                strength = 50,
                stamina = 78,
            },
            #endregion
        };
    }
}
