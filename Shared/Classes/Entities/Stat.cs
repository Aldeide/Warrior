using System.ComponentModel;
namespace Warrior.Entities
{
    public enum Stat
    {
        AllBase,
        [Description("Strength")]
        Strength,
        Intellect,
        Stamina,
        Spirit,
        [Description("Agility")]
        Agility,
        [Description("Attack Power")]
        AttackPower,
        Damage,
        Haste,
        Critical,
        MeleeHaste,
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
        BleedDamage,
        MeleeDamage,
        Resilience
    }
}
