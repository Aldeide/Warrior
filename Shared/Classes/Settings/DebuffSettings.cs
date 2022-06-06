using Warrior.Entities;

namespace Warrior.Settings
{
    public enum DebuffGroup
    {
        None,
        MajorArmor,
        MinorArmor,
        BleedEffects,
        CriticalStrike,
        PhysicalVulnerability
    }

    public class Debuff
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; } = false;
        public DebuffGroup debuffGroup { get; set; }
        public List<Effect> effects { get; set; }

        public Debuff(int id, string name, DebuffGroup debuffGroup, List<Effect> effects)
        {
            this.id = id;
            this.name = name;
            this.debuffGroup = debuffGroup;
            this.effects = effects;
        }
    }

    public class DebuffSettings
    {
        public List<Debuff> debuffs { get; set; }

        public DebuffSettings()
        {
            debuffs = new List<Debuff>();
            debuffs.Add(new Debuff(55754, "Acid Spit", DebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 20) }));
            debuffs.Add(new Debuff(8647, "Expose Armor", DebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 20) }));
            debuffs.Add(new Debuff(7386, "Sunder Armor", DebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 20) }));

            debuffs.Add(new Debuff(770, "Faerie Fire", DebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 5) }));
            debuffs.Add(new Debuff(56626, "Sting", DebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 5) }));
            debuffs.Add(new Debuff(50511, "Curse of Weakness", DebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Additive, Stat.Armor, 5) }));

            debuffs.Add(new Debuff(33876, "Mangle", DebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.BleedDamage, 1.3f) }));
            debuffs.Add(new Debuff(57393, "Stampede", DebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.BleedDamage, 1.25f) }));
            debuffs.Add(new Debuff(46855, "Trauma", DebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.BleedDamage, 1.25f) }));

            debuffs.Add(new Debuff(20337, "Heart of the Crusader", DebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Additive, Stat.Critical, 3f) }));
            debuffs.Add(new Debuff(58410, "Master Poisoner", DebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.BleedDamage, 3f) }));
            debuffs.Add(new Debuff(57722, "Totem of Wrath", DebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.BleedDamage, 3f) }));

            debuffs.Add(new Debuff(58413, "Savage Combat", DebuffGroup.PhysicalVulnerability, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.MeleeDamage, 1.04f) }));
            debuffs.Add(new Debuff(29859, "Blood Frenzy", DebuffGroup.PhysicalVulnerability, new List<Effect> { new Effect(EffectType.Multiplicative, Stat.MeleeDamage, 1.04f) }));
        }

        public void ProcessClick(long button, int id, DebuffGroup auraGroup)
        {
            if (button == 0)
            {
                Debuff aura = debuffs.Single(s => s.id == id);
                if (aura.isActive)
                {
                    aura.isActive = false;
                }
                else
                {
                    aura.isActive = true;
                }
                debuffs.Where(s => s.debuffGroup == auraGroup && s.id != id).ToList().ForEach(s => s.isActive = false);
            }
        }
        public int GetAdditiveStat(Stat stat)
        {
            int output = 0;
            foreach (var aura in debuffs)
            {
                if (!aura.isActive)
                {
                    continue;
                }
                foreach (var effect in aura.effects)
                {
                    if (effect.stat == stat && effect.type == EffectType.Additive)
                    {
                        output += (int)effect.value;
                    }
                }
            }
            return output;
        }
        public float GetMultiplicativeStat(Stat stat)
        {
            float output = 1;
            foreach (var aura in debuffs)
            {
                if (!aura.isActive)
                {
                    continue;
                }
                foreach (var effect in aura.effects)
                {
                    if (effect.stat == stat && effect.type == EffectType.Multiplicative)
                    {
                        output *= effect.value;
                    }
                }
            }
            return output;
        }
    }
}
