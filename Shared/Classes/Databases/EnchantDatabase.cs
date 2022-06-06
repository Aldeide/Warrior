using Warrior.Entities;

namespace Warrior.Databases
{
    public static class EnchantDatabase
    {
        public static List<Enchant> enchants { get; set; } = new List<Enchant>()
        {
            { new Enchant() { id = 0, name = "None" } },
            {
                // Head
                new Enchant()
                {
                    id = 59954,
                    name = "Arcanum of Torment",
                    slot = ItemSlot.Head,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 50) },
                        { new Effect(EffectType.Additive, Stat.CriticalRating, 20) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 44879,
                    name = "Arcanum of Triumph",
                    slot = ItemSlot.Head,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 50) },
                        { new Effect(EffectType.Additive, Stat.Resilience, 20) }
                    }
                }
            },
            // Shoulders
            {
                new Enchant()
                {
                    id = 61117,
                    name = "Master's Inscription of the Axe",
                    slot = ItemSlot.Shoulders,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 120) },
                        { new Effect(EffectType.Additive, Stat.CriticalRating, 15) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 59934,
                    name = "Greater Inscription of the Axe",
                    slot = ItemSlot.Shoulders,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 40) },
                        { new Effect(EffectType.Additive, Stat.CriticalRating, 15) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 59929,
                    name = "Lesser Inscription of the Axe",
                    slot = ItemSlot.Shoulders,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 30) },
                        { new Effect(EffectType.Additive, Stat.CriticalRating, 10) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 44067,
                    name = "Inscription of Triumph",
                    slot = ItemSlot.Shoulders,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 40) },
                        { new Effect(EffectType.Additive, Stat.Resilience, 15) }
                    }
                }
            },
            // Back
            {
                new Enchant()
                {
                    id = 60663,
                    name = "Major Agility",
                    slot = ItemSlot.Back,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.Agility, 22) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 44500,
                    name = "Superior Agility",
                    slot = ItemSlot.Back,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.Agility, 16) }
                    }
                }
            },
            // Chest
            {
                new Enchant()
                {
                    id = 60692,
                    name = "Powerful Stats",
                    slot = ItemSlot.Chest,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AllBase, 10) }
                    }
                }
            },
            // Bracers
            {
                new Enchant()
                {
                    id = 44575,
                    name = "Greater Assault",
                    slot = ItemSlot.Wrist,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 50) }
                    }
                }
            },
            // Hands
            {
                new Enchant()
                {
                    id = 60668,
                    name = "Crusher",
                    slot = ItemSlot.Hands,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 44) }
                    }
                }
            },
            // Waist
            {
                new Enchant()
                {
                    id = 55655,
                    name = "Eternal Belt Buckle",
                    slot = ItemSlot.Waist,
                    effects = new List<Effect>()
                    {

                    }
                }
            },
            // Legs
            {
                new Enchant()
                {
                    id = 38374,
                    name = "Icescale Leg Armor",
                    slot = ItemSlot.Legs,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 75) },
                        { new Effect(EffectType.Additive, Stat.CriticalRating, 22) }
                    }
                }
            },
            // Boots
            {
                new Enchant()
                {
                    id = 34007,
                    name = "Cat's Swiftness",
                    slot = ItemSlot.Boots,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.Agility, 6) }
                    }
                }
            },
            {
                new Enchant()
                {
                    id = 60763,
                    name = "Greater Assault",
                    slot = ItemSlot.Boots,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.Agility, 6) }
                    }
                }
            },
            // Weapons
            {
                new Enchant()
                {
                    id = 59621,
                    name = "Berserking",
                    slot = ItemSlot.MainHand,
                    effects = new List<Effect>() {}
                }
            },
            // Ring
            {
                new Enchant()
                {
                    id = 44645,
                    name = "Assault",
                    slot = ItemSlot.Ring1,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AttackPower, 40) }
                    }
                }
            },
            {
            new Enchant()
                {
                    id = 27927,
                    name = "Stats",
                    slot = ItemSlot.Ring1,
                    effects = new List<Effect>()
                    {
                        { new Effect(EffectType.Additive, Stat.AllBase, 4) }
                    }
                }
            }
        };

        public static Enchant GetEnchantById(int id)
        {
            return enchants.Single(e => e.id == id);
        }
    }
}
