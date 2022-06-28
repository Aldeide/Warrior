using Warrior.Entities;

namespace Warrior.Settings
{
    public enum BuffGroup
    {
        None,
        AgilityStrength,
        AttackPower,
        AttackPowerPercent,
        Heroism,
        DamagePercent,
        HastePercent,
        MeleeCrit,
        MeleeHaste,
        StatAdd,
        StatPercent,
        GuardianElixir,
        BattleElixir,
        Flask,
        Potion,
        HeroicPresence
    }
    public class Buff
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; } = false;
        public BuffGroup buffGroup { get; set; }
        public List<Entities.Effect> effects { get; set; }
        public Buff(int id, string name, BuffGroup buffGroup, List<Entities.Effect> effects)
        {
            this.id = id;
            this.name = name;
            this.buffGroup = buffGroup;
            this.effects = effects;
        }
    }

    public class BuffSettings
    {
        public List<Buff> buffs { get; set; }
        public BuffSettings()
        {
            buffs = new List<Buff>();
            buffs.Add(new Buff(57623, "Horn of Winter", BuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, Stat.Strength, 155), new Effect(EffectType.Additive, Stat.Agility, 155) }));
            buffs.Add(new Buff(65991, "Strength of Earth Totem", BuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, Stat.Strength, 155), new Effect(EffectType.Additive, Stat.Agility, 155) }));
            buffs.Add(new Buff(56520, "Blessing of Might", BuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, Stat.AttackPower, 550) }));
            buffs.Add(new Buff(20045, "Improved Blessing of Might", BuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, Stat.AttackPower, 688) }));
            buffs.Add(new Buff(47436, "Battle Shout", BuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, Stat.AttackPower, 548) }));
            buffs.Add(new Buff(12861, "Commanding Presence", BuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, Stat.AttackPower, 685) }));
            buffs.Add(new Buff(53138, "Abomination's Might", BuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.AttackPower, 1.1f) }));
            buffs.Add(new Buff(30809, "Unleashed Rage", BuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.AttackPower, 1.1f) }));
            buffs.Add(new Buff(75447, "Ferocious Inspiration", BuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.Damage, 1.03f) }));
            buffs.Add(new Buff(31869, "Sanctified Retribution", BuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.Damage, 1.03f) }));
            buffs.Add(new Buff(31583, "Arcane Empowerment", BuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.Damage, 1.03f) }));
            buffs.Add(new Buff(48396, "Improved Moonkin Form", BuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.Haste, 1.03f) }));
            buffs.Add(new Buff(53648, "Swift Retribution", BuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.Haste, 1.03f) }));
            buffs.Add(new Buff(17007, "Leader of the Pack", BuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, Stat.Critical, 5) }));
            buffs.Add(new Buff(29801, "Rampage", BuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, Stat.Critical, 5) }));
            buffs.Add(new Buff(55610, "Improved Icy Talons", BuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.MeleeHaste, 1.2f) }));
            buffs.Add(new Buff(8512, "Windfury Totem", BuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.MeleeHaste, 1.16f) }));
            buffs.Add(new Buff(29193, "Improved Windfury Totem", BuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.MeleeHaste, 1.2f) }));
            buffs.Add(new Buff(48469, "Mark of the Wild", BuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, Stat.AllBase, 37), new Effect(EffectType.Additive, Stat.Armor, 750) }));
            buffs.Add(new Buff(17051, "Improved Mark of the Wild", BuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, Stat.AllBase, 52), new Effect(EffectType.Additive, Stat.Armor, 1050) }));
            buffs.Add(new Buff(20217, "Blessing of Kings", BuffGroup.StatPercent, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.AllBase, 1.1f) }));
            buffs.Add(new Buff(39666, "Elixir of Mighty Agility", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.Agility, 45) }));
            buffs.Add(new Buff(40068, "Wrath Elixir", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.AttackPower, 90) }));
            buffs.Add(new Buff(40073, "Elixir of Mighty Strength", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.Strength, 50) }));
            buffs.Add(new Buff(40076, "Guru's Elixir", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.AllBase, 20) }));
            buffs.Add(new Buff(44325, "Elixir of Accuracy", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.HitRating, 45) }));
            buffs.Add(new Buff(44327, "Elixir of Deadly Strikes", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.CriticalRating, 45) }));
            buffs.Add(new Buff(44329, "Elixir of Expertise", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.ExpertiseRating, 45) }));
            buffs.Add(new Buff(44330, "Elixir of Armor Piercing", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.ArmorPenetrationRating, 45) }));
            buffs.Add(new Buff(44331, "Elixir of Lightning Speed", BuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.HasteRating, 45) }));
            buffs.Add(new Buff(40097, "Elixir of Protection", BuffGroup.GuardianElixir, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 800) }));
            buffs.Add(new Buff(28878, "Heroic Presence", BuffGroup.HeroicPresence, new List<Effect> { new Effect(EffectType.Additive, Stat.HitChance, 1) }));
        }
        public void ProcessClick(long button, int id, BuffGroup buffGroup)
        {
            if (button == 0)
            {
                Buff buff = buffs.Single(s => s.id == id);
                if (buff.isActive)
                {
                    buff.isActive = false;
                }
                else
                {
                    buff.isActive = true;
                }
                buffs.Where(s => s.buffGroup == buffGroup && s.id != id).ToList().ForEach(s => s.isActive = false);
            }
        }
        public int GetAdditiveStat(Entities.Stat stat)
        {
            int output = 0;
            foreach (var buff in buffs)
            {
                if (!buff.isActive)
                {
                    continue;
                }
                foreach (var effect in buff.effects)
                {
                    if (effect.stat == stat && effect.type == Entities.EffectType.Additive)
                    {   
                        output += (int)effect.value;
                    }
                }
            }
            return output;
        }
        public float GetMultiplicativeStat(Entities.Stat stat)
        {
            float output = 1;
            foreach (var buff in buffs)
            {
                if (!buff.isActive)
                {
                    continue;
                }
                foreach (var effect in buff.effects)
                {
                    if (effect.stat == stat && effect.type == Entities.EffectType.Multiplicative)
                    {
                        output *= effect.value;
                    }
                }
            }
            return output;
        }
    }

}
