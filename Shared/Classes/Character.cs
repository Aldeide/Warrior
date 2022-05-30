namespace Warrior
{
    public enum Profession
    {
        Jewelcrafting, Engineering
    }
    public class Character
    {
        public static Dictionary<string, Dictionary<Race, int>> bonusStatsPerRace = new Dictionary<string, Dictionary<Race, int>>()
        {
            { "Strength", new Dictionary<Race, int>() { {Race.Human, 0}, {Race.Dwarf, 5}, {Race.NightElf, -4}, {Race.Orc, 3}, {Race.Tauren, 5}, {Race.Undead, -1},{Race.Gnome, -5}, {Race.Troll, 1}, {Race.BloodElf, -3}, {Race.Draenei, 1} } },
            { "Agility", new Dictionary<Race, int>() { {Race.Human, 0}, {Race.Dwarf, -4}, {Race.NightElf, 4}, {Race.Orc, -3}, {Race.Tauren, -4}, {Race.Undead, -2},{Race.Gnome, 2}, {Race.Troll, 2}, {Race.BloodElf, 2}, {Race.Draenei, -3} } },
            { "Stamina", new Dictionary<Race, int>() { {Race.Human, 0}, {Race.Dwarf, 1}, {Race.NightElf, 0}, {Race.Orc, 1}, {Race.Tauren, 1}, {Race.Undead, 0},{Race.Gnome, 0}, {Race.Troll, 0}, {Race.BloodElf, 0}, {Race.Draenei, 0} } },
            { "Intellect", new Dictionary<Race, int>() { {Race.Human, 0}, {Race.Dwarf, -1}, {Race.NightElf, 0}, {Race.Orc, -3}, {Race.Tauren, -4}, {Race.Undead, -2},{Race.Gnome, 3}, {Race.Troll, -4}, {Race.BloodElf, 3}, {Race.Draenei, 0} } },
            { "Spirit", new Dictionary<Race, int>() { {Race.Human, 0}, {Race.Dwarf, -1}, {Race.NightElf, 0}, {Race.Orc, 2}, {Race.Tauren, 2}, {Race.Undead, 5},{Race.Gnome, 0}, {Race.Troll, 1}, {Race.BloodElf, -2}, {Race.Draenei, 2} } }
        };
        public Simulation simulation { get; set; }
        public Talents talents { get; set; }

        public int level = 80;
        public Race race { get; set; }
        public int rage { get; set; } = 0;

        // Character stats including base, gear and additive permanent raid buffs.
        public CharacterStats additiveCharacterStats { get; set; }
        // Character stats after multiplicative factors such as talents and multiplicative permanent raid buffs.
        public CharacterStats multiplicativeCharacterStats { get; set; }

        // Character Buffs and boss debuffs.
        public RaidBuffSettings auraSettings { get; set; }
        public RaidDebuffSettings debuffSettings { get; set; }

        // Character equipment and stance.
        public Equipment equipment { get; set; }
        public StanceManager stanceManager { get; set; }
        public GlyphManager glyphManager { get; set; } = new GlyphManager();
        public Character(Simulation simulation)
        {
            this.auraSettings = new RaidBuffSettings(simulation);
            this.debuffSettings = new RaidDebuffSettings(simulation);
            this.simulation = simulation;
            this.talents = new Talents(simulation);
            this.additiveCharacterStats = new CharacterStats();
            this.multiplicativeCharacterStats = new CharacterStats();
            this.equipment = new Equipment(simulation);
            this.stanceManager = new StanceManager();

            // Subscribing to events.
            stanceManager.StanceChanged += OnStanceChanged;

            UpdateAdditiveCharacterStats();

        }

        // Stats update methods.
        public void UpdateAdditiveCharacterStats()
        {
            additiveCharacterStats.agility = (int)(189 + bonusStatsPerRace["Agility"][race] + equipment.equipmentStats.agility + auraSettings.GetAdditiveStat(EffectStat.Agility) + auraSettings.GetAdditiveStat(EffectStat.AllBase));
            additiveCharacterStats.intellect = (int)(151 + bonusStatsPerRace["Intellect"][race] + equipment.equipmentStats.intellect + auraSettings.GetAdditiveStat(EffectStat.Intellect) + auraSettings.GetAdditiveStat(EffectStat.AllBase));
            additiveCharacterStats.stamina = (int)(189 + bonusStatsPerRace["Stamina"][race] + equipment.equipmentStats.stamina + auraSettings.GetAdditiveStat(EffectStat.Stamina) + auraSettings.GetAdditiveStat(EffectStat.AllBase));
            additiveCharacterStats.spirit = (int)(144 + bonusStatsPerRace["Spirit"][race] + equipment.equipmentStats.spirit + auraSettings.GetAdditiveStat(EffectStat.Spirit) + auraSettings.GetAdditiveStat(EffectStat.AllBase));
            additiveCharacterStats.strength = (int)(309 + bonusStatsPerRace["Strength"][race] + equipment.equipmentStats.strength + auraSettings.GetAdditiveStat(EffectStat.Strength) + auraSettings.GetAdditiveStat(EffectStat.AllBase));
            additiveCharacterStats.armor = (int)(equipment.equipmentStats.armor + auraSettings.GetAdditiveStat(EffectStat.Armor));
            additiveCharacterStats.hitRating = (int)(equipment.equipmentStats.hitRating + auraSettings.GetAdditiveStat(EffectStat.HitRating));
            additiveCharacterStats.criticalStrikeRating = (int)(equipment.equipmentStats.criticalStrikeRating + auraSettings.GetAdditiveStat(EffectStat.CriticalRating));
            additiveCharacterStats.hasteRating = (int)(equipment.equipmentStats.hasteRating + auraSettings.GetAdditiveStat(EffectStat.HasteRating));
            additiveCharacterStats.expertiseRating = (int)(equipment.equipmentStats.expertiseRating + auraSettings.GetAdditiveStat(EffectStat.ExpertiseRating));
            additiveCharacterStats.armorPenetrationRating = (int)(equipment.equipmentStats.armorPenetrationRating + auraSettings.GetAdditiveStat(EffectStat.ArmorPenetrationRating));
            additiveCharacterStats.attackPower = (int)(additiveCharacterStats.strength * 2 + equipment.equipmentStats.attackPower + (int)(TalentUtils.GetArmoredToTheTeethAPBonus(talents) * additiveCharacterStats.armor / 108.0f) + +auraSettings.GetAdditiveStat(EffectStat.AttackPower));
            additiveCharacterStats.health = (int)(8121 + (additiveCharacterStats.stamina - 20) * 10 + 20);
            additiveCharacterStats.damageMultiplier = 1;
            UpdateMultiplicativeCharacterStats();
        }
        public void UpdateMultiplicativeCharacterStats()
        {
            multiplicativeCharacterStats.agility = auraSettings.GetMultiplicativeStat(EffectStat.AllBase);
            multiplicativeCharacterStats.intellect = auraSettings.GetMultiplicativeStat(EffectStat.AllBase);
            multiplicativeCharacterStats.stamina = auraSettings.GetMultiplicativeStat(EffectStat.AllBase);
            multiplicativeCharacterStats.spirit = auraSettings.GetMultiplicativeStat(EffectStat.AllBase);
            multiplicativeCharacterStats.strength = auraSettings.GetMultiplicativeStat(EffectStat.AllBase);
            if (stanceManager.IsInBerserkerStance())
            {
                multiplicativeCharacterStats.strength *= TalentUtils.GetImprovedBerserkerStanceStrengthMultiplier(talents);
            }
            multiplicativeCharacterStats.armor = 1;
            multiplicativeCharacterStats.hitRating = 1;
            multiplicativeCharacterStats.criticalStrikeRating = 1;
            multiplicativeCharacterStats.hasteRating = 1;
            multiplicativeCharacterStats.expertiseRating = 1;
            multiplicativeCharacterStats.armorPenetrationRating = 1;
            multiplicativeCharacterStats.attackPower = auraSettings.GetMultiplicativeStat(EffectStat.AttackPower);
            multiplicativeCharacterStats.health = 1;
            multiplicativeCharacterStats.damageMultiplier = auraSettings.GetMultiplicativeStat(EffectStat.Damage) * TalentUtils.GetTitansDamageReductionMultiplier(talents, equipment);
            multiplicativeCharacterStats.hasteFactor = auraSettings.GetMultiplicativeStat(EffectStat.Haste) * auraSettings.GetMultiplicativeStat(EffectStat.MeleeHaste);
        }

        // Main Stats without temporary buffs.
        public int GetStrength()
        {
            return (int)(additiveCharacterStats.strength * multiplicativeCharacterStats.strength);
        }
        public int GetAgility()
        {
            return (int)(additiveCharacterStats.agility * multiplicativeCharacterStats.agility);
        }
        public int GetStamina()
        {
            return (int)(additiveCharacterStats.stamina * multiplicativeCharacterStats.stamina);
        }

        // Secondary Stats without temporary buffs.
        public int GetHitRating()
        {
            return (int)(additiveCharacterStats.hitRating * multiplicativeCharacterStats.hitRating);
        }
        public int GetHasteRating()
        {
            return (int)(additiveCharacterStats.hasteRating * multiplicativeCharacterStats.hasteRating);
        }
        public int GetArmorPenetrationRating()
        {
            return (int)(additiveCharacterStats.armorPenetrationRating * multiplicativeCharacterStats.armorPenetrationRating);
        }
        public int GetCriticalStrikeRating()
        {
            return (int)(additiveCharacterStats.criticalStrikeRating * multiplicativeCharacterStats.criticalStrikeRating);
        }

        // Derived Stats without temporary buffs.
        public float GetHealth()
        {
            int warriorBaseHealth = 8121;
            return warriorBaseHealth + (GetStamina() - 20) * 10 + 20;
        }
        public int GetArmor()
        {
            return (int)(additiveCharacterStats.armor * multiplicativeCharacterStats.armor);
        }
        public int GetAttackPower()
        {
            return (int)(additiveCharacterStats.attackPower * multiplicativeCharacterStats.attackPower);
        }
        public float GetMeleeCritChancePercent()
        {
            int critRating = (int)(additiveCharacterStats.criticalStrikeRating * multiplicativeCharacterStats.criticalStrikeRating);

            return (float)Math.Round(5.0f
                + (float)GetAgility() / Constants.kAgilityPerCritPercent
                + TalentUtils.GetCrueltyBonus(talents)
                + critRating / Constants.kCritRatingPerPercent
                + auraSettings.GetAdditiveStat(EffectStat.Critical), 2)
                + (stanceManager.IsInBerserkerStance() ? 3.0f : 0)
                + debuffSettings.GetAdditiveStat(EffectStat.Critical)
                - AttackTableUtils.ComputeCritChanceReduction(simulation.settings.targetLevel);
        }
        public float GetMeleeHasteFactor()
        {
            const float hasteRatingPerPercent = 25.21f; // Has changed during wotlk with 3.1 was 32.79 before.
            float hasteFromHasteRating = (1 + GetHasteRating() / hasteRatingPerPercent / 100).RoundToSignificantDigits(4);
            return MathF.Round((hasteFromHasteRating * multiplicativeCharacterStats.hasteFactor), 4);

        }
        public float GetMeleeHitChance()
        {
            return GetHitRating() / 32.79f + (float)TalentUtils.GetPrecisionExtraHitChance(talents);
        }

        // Event Handlers.
        public void OnStanceChanged(object sender, EventArgs e)
        {
            // Update stats.
            UpdateAdditiveCharacterStats();
            return;
        }

    }



}
