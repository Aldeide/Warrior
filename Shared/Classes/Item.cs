namespace Warrior
{
    public enum ItemSlot
    {
        MainHand,
        OffHand,
        Head,
        Neck,
        Shoulders,
        Back,
        Chest,
        Wrist,
        Waist,
        Legs,
        Gloves,
        Boots,
        Ring1,
        Ring2,
        Trinket1,
        Trinket2,
        Ranged
    }

    public enum WeaponType
    {
        OneHand,
        OffHand,
        Dagger,
        TwoHand
    }

    public enum ItemType
    {
        ClothArmor,
        LeatherArmor,
        MailArmor,
        PlateArmor,
        Dagger,
        OneHandedSword,
        TwoHandedSword,
        OneHandedAxe
    }

    [Serializable]
    public struct Item
    {
        public int id { get; set; }
        public int ilvl { get; set; }

        public bool isHeroic { get; set; }
        public string name { get; set; }
        public ItemSlot itemSlot { get; set; }
        public ItemType itemType { get; set; }
        public WeaponType weaponType {get;set;}
        public int armor { get; set; }
        public int strength { get; set; }
        public int agility { get; set; }
        public int intellect { get; set; }
        public int spirit { get; set; }
        public int stamina { get; set; }

        public int redSockets { get; set; }
        public int yellowSockets { get; set; }
        public int blueSockets { get; set; }
        public int metaSockets { get; set; }

        public int gemBonusStrength { get; set; }
        public int gemBonusAgility { get; set; }
        public int gemBonusCriticalStrikeRating { get; set; }

        public int attackPower { get; set; }
        public int criticalStrikeRating { get; set; }
        public int hitRating { get; set; }
        public int hasteRating { get; set; }
        public int armorPenetrationRating { get; set; }
        public int expertiseRating { get; set; }
        public string source { get; set; }

        public float speed { get; set; }
        public int minDamage { get; set; }
        public int maxDamage { get; set; }


    }
}
