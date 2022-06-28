using Warrior.Entities;
namespace Warrior.Stats
{
    public static class DisplayStats
    {
        public static int DisplayStrength(Settings.Settings settings)
        {
            float berserkerStancemultiplier = 1.0f;
            if (settings.stanceSettings.IsInBerserkerStance())
			{
                berserkerStancemultiplier *= TalentUtils.GetImprovedBerserkerStanceStrengthMultiplier(settings.talentSettings);
            }
            return (int)((174
                + Constants.bonusStatsPerRace["Strength"][settings.characterSettings.race]
                + settings.equipmentSettings.ComputeGearStrength()
                + settings.buffSettings.GetAdditiveStat(Stat.Strength)
                + settings.buffSettings.GetAdditiveStat(Stat.AllBase)
                + GetGemStats(settings, Stat.Strength))
                * settings.buffSettings.GetMultiplicativeStat(Stat.AllBase)
                * berserkerStancemultiplier
                * (1 + settings.talentSettings.StrengthOfArms.rank * 0.02f));
        }
        public static int DisplayAgility(Settings.Settings settings)
        {
            return (int)((113
                + Constants.bonusStatsPerRace["Agility"][settings.characterSettings.race]
                + settings.equipmentSettings.ComputeGearAgility()
                + settings.buffSettings.GetAdditiveStat(Stat.Agility)
                + settings.buffSettings.GetAdditiveStat(Stat.AllBase)) * settings.buffSettings.GetMultiplicativeStat(Stat.AllBase));
        }
        public static int DisplayStamina(Settings.Settings settings)
        {
            return (int)((160
                + Constants.bonusStatsPerRace["Stamina"][settings.characterSettings.race]
                + settings.equipmentSettings.ComputeGearStamina()
                + settings.buffSettings.GetAdditiveStat(Stat.Stamina)
                + settings.buffSettings.GetAdditiveStat(Stat.AllBase))
                * settings.buffSettings.GetMultiplicativeStat(Stat.AllBase)
                * (1 + settings.talentSettings.StrengthOfArms.rank * 0.02f));
        }
        public static int DisplayHealth(Settings.Settings settings)
        {
            return (int)(8121
                + (DisplayStamina(settings) - 20) * 10 + 20);
        }
        public static int DisplayArmor(Settings.Settings settings)
        {
            return (int)(69 + DisplayAgility(settings) * 2
                + settings.equipmentSettings.ComputeGearArmor()
                + settings.buffSettings.GetAdditiveStat(Stat.Armor));
        }
        public static int DisplayAttackPower(Settings.Settings settings)
        {
            return (int)((DisplayStrength(settings) * 2
                + settings.equipmentSettings.ComputeGearAP()
                + settings.buffSettings.GetAdditiveStat(Stat.AttackPower)
                + TalentUtils.GetArmoredToTheTeethAPBonus(settings.talentSettings) * DisplayArmor(settings) / 108.0f) * settings.buffSettings.GetMultiplicativeStat(Stat.AttackPower));
        }
        public static int DisplayArmorPenetrationRating(Settings.Settings settings)
        {
            return settings.equipmentSettings.ComputeGearArmorPenetrationRating()
                + settings.buffSettings.GetAdditiveStat(Stat.ArmorPenetrationRating);
        }
        public static int DisplayHasteRating(Settings.Settings settings)
        {
            return settings.equipmentSettings.ComputeGearHasteRating()
                + settings.buffSettings.GetAdditiveStat(Stat.HasteRating);
        }
        public static int DisplayHitRating(Settings.Settings settings)
        {
            return settings.equipmentSettings.ComputeGearHitRating()
                + settings.buffSettings.GetAdditiveStat(Stat.HitRating);
        }
        public static int DisplayCriticalStrikeRating(Settings.Settings settings)
        {
            return settings.equipmentSettings.ComputeGearCritRating()
                + settings.buffSettings.GetAdditiveStat(Stat.CriticalRating);
        }
        public static int DisplayExpertiseRating(Settings.Settings settings)
        {
            return settings.equipmentSettings.ComputeGearExpertiseRating()
                + settings.buffSettings.GetAdditiveStat(Stat.ExpertiseRating);
        }
        public static float DisplayCriticalChance(Settings.Settings settings)
        {
            return (float)Math.Round(
                5.0f
                + DisplayAgility(settings) / Constants.kAgilityPerCritPercent
                + DisplayCriticalStrikeRating(settings) / Constants.kCritRatingPerPercent
                + TalentUtils.GetCrueltyBonus(settings.talentSettings)
                + settings.debuffSettings.GetAdditiveStat(Stat.Critical)
                + settings.buffSettings.GetAdditiveStat(Stat.Critical)
                - AttackTableUtils.ComputeCritChanceReduction(settings.simulationSettings.targetLevel)
                , 2);
                
        }
        public static float DisplayMeleeHaste(Settings.Settings settings)
        {
            float hasteFromHasteRating = (1 + DisplayHasteRating(settings) / Constants.kHastePerPercent / 100).RoundToSignificantDigits(4);
            return MathF.Round((hasteFromHasteRating
                * settings.buffSettings.GetMultiplicativeStat(Stat.Haste)
                * settings.buffSettings.GetMultiplicativeStat(Stat.MeleeHaste)), 4);

        }
        public static float DisplayExtraHitChance(Settings.Settings settings)
		{
            return DisplayHitRating(settings) / Constants.kHitRatingPerPercent
                + (float)TalentUtils.GetPrecisionExtraHitChance(settings.talentSettings)
                + settings.buffSettings.GetAdditiveStat(Stat.HitChance);
        }
        public static float DisplayGlancingChance(Settings.Settings settings)
		{
            if (settings.simulationSettings.targetLevel == 83)
            {
                return 24;
            }
            if (settings.simulationSettings.targetLevel == 82)
            {
                return 20;
            }
            if (settings.simulationSettings.targetLevel == 81)
            {
                return 15;
            }
            return 10;
        }
        public static float DisplayWhiteMissChance(Settings.Settings settings)
		{
            float missChance = 24;
            if (settings.simulationSettings.targetLevel == 83)
            {
                missChance = 27;
            }
            if (settings.simulationSettings.targetLevel == 82)
            {
                missChance = 25;
            }
            if (settings.simulationSettings.targetLevel == 81)
            {
                missChance = 24.5f;
            }
            return missChance - DisplayExtraHitChance(settings);
        }
        public static float DisplayDodgeChance(Settings.Settings settings)
        {
            int expertiseRating = DisplayExpertiseRating(settings);
            float reductionChance = expertiseRating / Constants.kExpertisePerPoint / 4;
            if (settings.simulationSettings.targetLevel == 83)
            {
                return Math.Max(0, 6.5f - reductionChance);
            }
            if (settings.simulationSettings.targetLevel == 82)
            {
                return Math.Max(0, 5.4f - reductionChance);
            }
            if (settings.simulationSettings.targetLevel == 81)
            {
                return Math.Max(0, 5.2f - reductionChance);
            }
            return Math.Max(0, 5f - reductionChance); ;
        }
        public static float DisplayArmorPenetration(Settings.Settings settings)
		{
            return DisplayArmorPenetrationRating(settings)
                / Constants.kArmorPenetrationPerPercent;
        }
        public static float DisplayEnemyEffectiveArmor(Settings.Settings settings)
		{
            float armor = settings.simulationSettings.targetArmor;
            armor *= (100 - settings.debuffSettings.GetAdditiveStat(Stat.Armor)) / 100.0f;
            float armorConstant = 400
                + 85 * settings.simulationSettings.targetLevel
                + 4.5f * 85 * (settings.simulationSettings.targetLevel - 59);
            float armorPenetrationCap = (armor + armorConstant) / 3;
            float armorPenetrationPercent = DisplayArmorPenetrationRating(settings)
                / Constants.kArmorPenetrationPerPercent;
            if (settings.stanceSettings.IsInBattleStance())
            {
                armorPenetrationPercent += 10;
            }
            float armorReduction = Math.Min(settings.simulationSettings.targetArmor, armorPenetrationCap) * armorPenetrationPercent / 100;
            float effectiveArmor = armor - armorReduction;
            return effectiveArmor;
        }
        public static float DisplayEnemyDamageMultiplier(Settings.Settings settings)
		{
            float effectiveArmor = DisplayEnemyEffectiveArmor(settings);
            return 1 - effectiveArmor /
                ((467.5f * settings.simulationSettings.targetLevel) + effectiveArmor - 22167.5f);
        }

        public static int GetGemStats(Settings.Settings settings, Stat stat)
        {
            var output = 0;
            foreach(var item in settings.equipmentSettings.items)
            {
                output += settings.gemSettings.GetItemGemStats(Utils.MiscUtils.CombineItemAndSlot(item.Value.id, item.Key), stat);
            }
            return output;
        }
        public static Tuple<Stat, int>? GetSocketBonus(Settings.Settings settings, ItemSlot slot)
        {
            var item = settings.equipmentSettings.GetItemBySlot(slot);

            item = Databases.ItemDatabase.items.Single(i => i.id == item.id);
            if (item == null) return null;
            if (item.gemBonusStrength > 0)
            {
                return new Tuple<Stat, int>(Stat.Strength, item.gemBonusStrength);
            }
            if (item.gemBonusAgility > 0)
            {
                return new Tuple<Stat, int>(Stat.Agility, item.gemBonusAgility);
            }
            if (item.gemBonusCriticalStrikeRating > 0)
            {
                return new Tuple<Stat, int>(Stat.CriticalRating, item.gemBonusCriticalStrikeRating);
            }
            return null;
        }
        public static bool SocketBonusIsActive(Settings.Settings settings, ItemSlot slot)
        {
            var sockets = settings.equipmentSettings.GetGemSockets(slot);
            //if (sockets.Count() == 0) return false;
            Item? item = settings.equipmentSettings.GetItemBySlot(slot);
            //if (item == null) return false;
            bool output = true;
            int i = 0;
            foreach (var socket in sockets)
            {
                var gem = settings.gemSettings.GetGemsByItemId(Utils.MiscUtils.CombineItemAndSlot(item.id, slot));
                var actualGem = Databases.GemDatabase.gems.Single(g => g.id == gem[i].id);
                if (!GemMatchesSocket(socket.color, actualGem.color)) output = false;
                i++;
            }
            return output;
        }
        public static bool HasGems(Settings.Settings settings, ItemSlot slot)
        {
            return settings.equipmentSettings.GetGemSockets(slot).Count() > 0;
        }
        public static bool GemMatchesSocket(Color socket, Color gem)
		{
            if (socket != Color.Meta && gem == Color.Prismatic) return true;
            if (socket == Color.Prismatic && gem != Color.Meta) return true;
            if (socket == Color.Meta && gem == Color.Meta) return true;
            if (socket == Color.Red && (gem == Color.Red || gem == Color.Orange || gem == Color.Purple)) return true;
            if (socket == Color.Blue && (gem == Color.Blue || gem == Color.Purple || gem == Color.Green)) return true;
            if (socket == Color.Yellow && (gem == Color.Yellow || gem == Color.Orange || gem == Color.Green)) return true;
            return false;
        }
    }
}
