namespace Warrior
{
    public enum RaidDebuffGroup
    {
        None,
        MajorArmor,
        MinorArmor,
        BleedEffects,
        CriticalStrike,
        PhysicalVulnerability
    }
    public class RaidDebuff
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; } = false;
        public RaidDebuffGroup auraGroup { get; set; }
        public List<Effect> effects { get; set; }

        public RaidDebuff(int id, string name, RaidDebuffGroup auraGroup, List<Effect> effects)
        {
            this.id = id;
            this.name = name;
            this.auraGroup = auraGroup;
            this.effects = effects;
        }
    }
    public class RaidDebuffSettings
    {
        public List<RaidDebuff> debuffs { get; set; }
        public Simulation simulation { get; set; }

        public RaidDebuffSettings(Simulation simulation)
        {
            this.simulation = simulation;

            debuffs = new List<RaidDebuff>();
            debuffs.Add(new RaidDebuff(55754, "Acid Spit", RaidDebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.8f) }));
            debuffs.Add(new RaidDebuff(8647, "Expose Armor", RaidDebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.8f) }));
            debuffs.Add(new RaidDebuff(7386, "Sunder Armor", RaidDebuffGroup.MajorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.8f) }));

            debuffs.Add(new RaidDebuff(770, "Faerie Fire", RaidDebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.95f) }));
            debuffs.Add(new RaidDebuff(56626, "Sting", RaidDebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.95f) }));
            debuffs.Add(new RaidDebuff(50511, "Curse of Weakness", RaidDebuffGroup.MinorArmor, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Armor, 0.95f) }));

            debuffs.Add(new RaidDebuff(33876, "Mangle", RaidDebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.BleedDamage, 1.3f) }));
            debuffs.Add(new RaidDebuff(57393, "Stampede", RaidDebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.BleedDamage, 1.25f) }));
            debuffs.Add(new RaidDebuff(46855, "Trauma", RaidDebuffGroup.BleedEffects, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.BleedDamage, 1.25f) }));

            debuffs.Add(new RaidDebuff(20337, "Heart of the Crusader", RaidDebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Critical, 3f) }));
            debuffs.Add(new RaidDebuff(58410, "Master Poisoner", RaidDebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.BleedDamage, 3f) }));
            debuffs.Add(new RaidDebuff(57722, "Totem of Wrath", RaidDebuffGroup.CriticalStrike, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.BleedDamage, 3f) }));

            debuffs.Add(new RaidDebuff(58413, "Savage Combat", RaidDebuffGroup.PhysicalVulnerability, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeDamage, 1.04f) }));
            debuffs.Add(new RaidDebuff(29859, "Blood Frenzy", RaidDebuffGroup.PhysicalVulnerability, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeDamage, 1.04f) }));
        }

        public void ProcessClick(long button, int id, RaidDebuffGroup auraGroup)
        {
            if (button == 0)
            {
                RaidDebuff aura = debuffs.Single(s => s.id == id);
                if (aura.isActive)
                {
                    aura.isActive = false;
                }
                else
                {
                    aura.isActive = true;
                }
                debuffs.Where(s => s.auraGroup == auraGroup && s.id != id).ToList().ForEach(s => s.isActive = false);
            }
            simulation.character.UpdateAdditiveCharacterStats();
        }

        public int GetAdditiveStat(EffectStat stat)
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
                    if (effect.effectStat == stat && effect.effectType == EffectType.Additive)
                    {
                        output += (int)effect.value;
                    }
                }
            }
            return output;
        }

        public float GetMultiplicativeStat(EffectStat stat)
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
                    if (effect.effectStat == stat && effect.effectType == EffectType.Multiplicative)
                    {
                        output *= effect.value;
                    }
                }
            }
            return output;
        }
    }
}
