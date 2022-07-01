using System.ComponentModel;
namespace Warrior.Entities
{
    [Serializable]
    public enum Stat
    {
        [Description("All Stats")]
        AllBase,
        [Description("Strength")]
        Strength,
        [Description("Intellect")]
        Intellect,
        [Description("Stamina")]
        Stamina,
        [Description("Spirit")]
        Spirit,
        [Description("Agility")]
        Agility,
        [Description("Attack Power")]
        AttackPower,
        [Description("Damage")]
        Damage,
        [Description("Haste")]
        Haste,
        [Description("Critical")]
        Critical,
        [Description("Melee Haste")]
        MeleeHaste,
        [Description("Armor")]
        Armor,
        [Description("Hit Rating")]
        HitRating,
        [Description("Hit Chance")]
        HitChance,
        [Description("Critical Rating")]
        CriticalRating,
        [Description("Increased Critical Damage")]
        CriticalDamage,
        [Description("Expertise Rating")]
        ExpertiseRating,
        [Description("Armor Penetration Rating")]
        ArmorPenetrationRating,
        ArmorPenetration,
        [Description("Haste Rating")]
        HasteRating,
        [Description("Bleed Damage")]
        BleedDamage,
        [Description("Melee Damage")]
        MeleeDamage,
        [Description("Resilience")]
        Resilience
    }
}
