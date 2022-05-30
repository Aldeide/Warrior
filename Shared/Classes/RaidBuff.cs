namespace Warrior
{
    public enum RaidBuffGroup
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
    }

    public enum EffectType
    {
        Additive,
        Multiplicative
    }

    public enum EffectStat
    {
        AllBase,
        Strength,
        Intellect,
        Stamina,
        Spirit,
        Agility,
        AttackPower,
        Damage,
        Haste,
        Critical,
        MeleeHaste,
        Armor,
        HitRating,
        CriticalRating,
        ExpertiseRating,
        ArmorPenetrationRating,
        HasteRating,
        BleedDamage,
        MeleeDamage
    }

    public class Effect
    {
        public EffectType effectType { get; set; }
        public EffectStat effectStat { get; set; }
        public float value;

        public Effect(EffectType effectType, EffectStat effectStat, float value)
        {
            this.effectType = effectType;
            this.effectStat = effectStat;
            this.value = value;
        }
    }

    public class RaidBuff
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; } = false;
        public RaidBuffGroup auraGroup { get; set; }
        public List<Effect> effects { get; set; }

        public RaidBuff(int id, string name, RaidBuffGroup auraGroup, List<Effect> effects)
        {
            this.id = id;
            this.name = name;
            this.auraGroup = auraGroup;
            this.effects = effects;
        }
    }

    public class RaidBuffManager
    {
        public List<RaidBuff> auras;
        public RaidBuffDatabase auraDatabase;

        public RaidBuffManager(RaidBuffDatabase auraDatabase)
        {
            this.auraDatabase = auraDatabase;
            this.auras = new List<RaidBuff>();
        }

        
    }

    public class RaidBuffDatabase
    {
        public List<RaidBuff> auras { get; set; }

        public RaidBuffDatabase()
        {
            auras = new List<RaidBuff>();
            auras.Add(new RaidBuff(57623, "Horn of Winter", RaidBuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Strength, 155), new Effect(EffectType.Additive, EffectStat.Agility, 155) }));
            auras.Add(new RaidBuff(65991, "Strength of Earth Totem", RaidBuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Strength, 155), new Effect(EffectType.Additive, EffectStat.Agility, 155) }));

            auras.Add(new RaidBuff(56520, "Blessing of Might", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 550) }));
            auras.Add(new RaidBuff(20045, "Improved Blessing of Might", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 688) }));
            auras.Add(new RaidBuff(47436, "Battle Shout", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 548) }));
            auras.Add(new RaidBuff(12861, "Commanding Presence", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 685) }));

            auras.Add(new RaidBuff(53138, "Abomination's Might", RaidBuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.1f) }));
            auras.Add(new RaidBuff(30809, "Unleashed Rage", RaidBuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.1f) }));

            // auras.Add(new Aura(65980, "Bloodlust", AuraGroup.Heroism, 1.0f));

            auras.Add(new RaidBuff(34459, "Ferocious Inspiration", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.02f) }));
            auras.Add(new RaidBuff(31869, "Sanctified Retribution", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.03f) }));
            auras.Add(new RaidBuff(31583, "Arcane Empowerment", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.03f) }));

            auras.Add(new RaidBuff(48396, "Improved Moonkin Form", RaidBuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Haste, 1.03f) }));
            auras.Add(new RaidBuff(53648, "Swift Retribution", RaidBuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Haste, 1.03f) }));

            auras.Add(new RaidBuff(17007, "Leader of the Pack", RaidBuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Critical, 5) }));
            auras.Add(new RaidBuff(29801, "Rampage", RaidBuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Critical, 5) }));

            auras.Add(new RaidBuff(55610, "Improved Icy Talons", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.2f) }));
            auras.Add(new RaidBuff(8512, "Windfury Totem", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.16f) }));
            auras.Add(new RaidBuff(29192, "Improved Windfury Totem", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.2f) }));

            auras.Add(new RaidBuff(26990, "Mark of the Wild", RaidBuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AllBase, 14) }));
            auras.Add(new RaidBuff(17051, "Improved Mark of the Wild", RaidBuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AllBase, 20) }));

            auras.Add(new RaidBuff(20217, "Blessing of Kings", RaidBuffGroup.StatPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AllBase, 1.1f) }));

            
            auras.Add(new RaidBuff(39666, "Elixir of Mighty Agility", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Agility, 45) }));
            /*
            auras.Add(new Aura(40068, "Wrath Elixir", AuraGroup.BattleElixir, 90));
            auras.Add(new Aura(40073, "Elixir of Mighty Strength", AuraGroup.BattleElixir, 50));
            auras.Add(new Aura(40076, "Guru's Elixir", AuraGroup.BattleElixir, 20));
            auras.Add(new Aura(44325, "Elixir of Accuracy", AuraGroup.BattleElixir, 45));
            auras.Add(new Aura(44327, "Elixir of Deadly Strikes", AuraGroup.BattleElixir, 45));
            auras.Add(new Aura(44329, "Elixir of Expertise", AuraGroup.BattleElixir, 45));
            auras.Add(new Aura(44330, "Elixir of Armor Piercing", AuraGroup.BattleElixir, 45));
            auras.Add(new Aura(44331, "Elixir of Lightning Speed", AuraGroup.BattleElixir, 45));

            auras.Add(new Aura(40211, "Potion of Speed", AuraGroup.Potion, 500));
            auras.Add(new Aura(40212, "Potion of Wild Magic", AuraGroup.Potion, 200));
            */
        }
    }

    public class RaidBuffSettings
    {
        public List<RaidBuff> auras { get; set; }
        public Simulation simulation { get; set; }

        public RaidBuffSettings(Simulation simulation)
        {
            this.simulation = simulation;

            auras = new List<RaidBuff>();
            auras.Add(new RaidBuff(57623, "Horn of Winter", RaidBuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Strength, 155), new Effect(EffectType.Additive, EffectStat.Agility, 155) }));
            auras.Add(new RaidBuff(65991, "Strength of Earth Totem", RaidBuffGroup.AgilityStrength, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Strength, 155), new Effect(EffectType.Additive, EffectStat.Agility, 155) }));

            auras.Add(new RaidBuff(56520, "Blessing of Might", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 550) }));
            auras.Add(new RaidBuff(20045, "Improved Blessing of Might", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 688) }));
            auras.Add(new RaidBuff(47436, "Battle Shout", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 548) }));
            auras.Add(new RaidBuff(12861, "Commanding Presence", RaidBuffGroup.AttackPower, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 685) }));

            auras.Add(new RaidBuff(53138, "Abomination's Might", RaidBuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.1f) }));
            auras.Add(new RaidBuff(30809, "Unleashed Rage", RaidBuffGroup.AttackPowerPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AttackPower, 1.1f) }));

            // auras.Add(new Aura(65980, "Bloodlust", AuraGroup.Heroism, 1.0f));

            auras.Add(new RaidBuff(75447, "Ferocious Inspiration", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Damage, 1.03f) }));
            auras.Add(new RaidBuff(31869, "Sanctified Retribution", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Damage, 1.03f) }));
            auras.Add(new RaidBuff(31583, "Arcane Empowerment", RaidBuffGroup.DamagePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Damage, 1.03f) }));

            auras.Add(new RaidBuff(48396, "Improved Moonkin Form", RaidBuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Haste, 1.03f) }));
            auras.Add(new RaidBuff(53648, "Swift Retribution", RaidBuffGroup.HastePercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.Haste, 1.03f) }));

            auras.Add(new RaidBuff(17007, "Leader of the Pack", RaidBuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Critical, 5) }));
            auras.Add(new RaidBuff(29801, "Rampage", RaidBuffGroup.MeleeCrit, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Critical, 5) }));

            auras.Add(new RaidBuff(55610, "Improved Icy Talons", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.2f) }));
            auras.Add(new RaidBuff(8512, "Windfury Totem", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.16f) }));
            auras.Add(new RaidBuff(29193, "Improved Windfury Totem", RaidBuffGroup.MeleeHaste, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.MeleeHaste, 1.2f) }));

            auras.Add(new RaidBuff(48469, "Mark of the Wild", RaidBuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AllBase, 37), new Effect(EffectType.Additive, EffectStat.Armor, 750) }));
            // 51?
            auras.Add(new RaidBuff(17051, "Improved Mark of the Wild", RaidBuffGroup.StatAdd, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AllBase, 52), new Effect(EffectType.Additive, EffectStat.Armor, 1050) }));

            auras.Add(new RaidBuff(20217, "Blessing of Kings", RaidBuffGroup.StatPercent, new List<Effect> { new Effect(EffectType.Multiplicative, EffectStat.AllBase, 1.1f) }));


            auras.Add(new RaidBuff(39666, "Elixir of Mighty Agility", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Agility, 45) }));
            auras.Add(new RaidBuff(40068, "Wrath Elixir", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AttackPower, 90) }));
            auras.Add(new RaidBuff(40073, "Elixir of Mighty Strength", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Strength, 50) }));
            auras.Add(new RaidBuff(40076, "Guru's Elixir", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.AllBase, 20) }));
            auras.Add(new RaidBuff(44325, "Elixir of Accuracy", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.HitRating, 45) }));
            auras.Add(new RaidBuff(44327, "Elixir of Deadly Strikes", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.CriticalRating, 45) }));
            auras.Add(new RaidBuff(44329, "Elixir of Expertise", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.ExpertiseRating, 45) }));
            auras.Add(new RaidBuff(44330, "Elixir of Armor Piercing", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.ArmorPenetrationRating, 45) }));
            auras.Add(new RaidBuff(44331, "Elixir of Lightning Speed", RaidBuffGroup.BattleElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.HasteRating, 45) }));

            auras.Add(new RaidBuff(40097, "Elixir of Protection", RaidBuffGroup.GuardianElixir, new List<Effect> { new Effect(EffectType.Additive, EffectStat.Armor, 800) }));
        }

        public void ProcessClick(long button, int id, RaidBuffGroup auraGroup)
        {
            if (button == 0)
            {
                RaidBuff aura = auras.Single(s => s.id == id);
                if (aura.isActive)
                {
                    aura.isActive = false;
                } else
                {
                    aura.isActive = true;
                }
                auras.Where(s => s.auraGroup == auraGroup && s.id != id).ToList().ForEach(s => s.isActive = false);
            }
            simulation.character.UpdateAdditiveCharacterStats();
        }

        public int GetAdditiveStat(EffectStat stat)
        {
            int output = 0;
            foreach (var aura in auras)
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
            foreach (var aura in auras)
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
