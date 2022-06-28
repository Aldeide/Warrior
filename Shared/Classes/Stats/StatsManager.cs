using Warrior.Entities;

namespace Warrior
{
    public class StatsManager
    {
        Iteration iteration;  
        CharacterStats additiveCharacterStats = new CharacterStats();
        CharacterStats multiplicativeCharacterStats = new CharacterStats();
        CharacterStats tempAdditiveCharacterStats = new CharacterStats();
        CharacterStats tempMultiplicativeCharacterStats = new CharacterStats();
        public StatsManager(Iteration iteration)
        {
            this.iteration = iteration;
            InitialSetup();
            tempMultiplicativeCharacterStats.strength = 1;
            tempMultiplicativeCharacterStats.agility = 1;
            tempMultiplicativeCharacterStats.stamina = 1;
            tempMultiplicativeCharacterStats.armor = 1;
            tempMultiplicativeCharacterStats.hitRating = 1;
            tempMultiplicativeCharacterStats.hasteFactor = 1;
            tempMultiplicativeCharacterStats.attackPower = 1;
            tempMultiplicativeCharacterStats.criticalStrikeRating = 1;
            tempMultiplicativeCharacterStats.hasteRating = 1;
            tempMultiplicativeCharacterStats.expertiseRating = 1;
            tempMultiplicativeCharacterStats.offHandExpertiseRating = 1;
            tempMultiplicativeCharacterStats.armorPenetrationRating = 1;
            tempMultiplicativeCharacterStats.damageMultiplier = 1;
        }
        public void InitialSetup()
		{
            additiveCharacterStats.strength = (int)(
                174
                + Constants.bonusStatsPerRace["Strength"][iteration.settings.characterSettings.race]
                + iteration.settings.equipmentSettings.ComputeGearStrength()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.Strength)
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.AllBase)
                + Stats.DisplayStats.GetGemStats(iteration.settings, Stat.Strength)
                );
            multiplicativeCharacterStats.strength =
                iteration.settings.buffSettings.GetMultiplicativeStat(Stat.AllBase)
                * (1 + iteration.settings.talentSettings.StrengthOfArms.rank * 0.02f);

            additiveCharacterStats.agility = (int)(113
                + Constants.bonusStatsPerRace["Agility"][iteration.settings.characterSettings.race]
                + iteration.settings.equipmentSettings.ComputeGearAgility()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.Agility)
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.AllBase));
            multiplicativeCharacterStats.agility = iteration.settings.buffSettings.GetMultiplicativeStat(Stat.AllBase);

            additiveCharacterStats.armor = (int)(69 + (additiveCharacterStats.agility * multiplicativeCharacterStats.agility) * 2
                + iteration.settings.equipmentSettings.ComputeGearArmor()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.Armor));
            multiplicativeCharacterStats.armor = 1.0f;

            additiveCharacterStats.attackPower = (int)(iteration.settings.equipmentSettings.ComputeGearAP()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.AttackPower));
            multiplicativeCharacterStats.attackPower = iteration.settings.buffSettings.GetMultiplicativeStat(Stat.AttackPower);

            additiveCharacterStats.hasteRating = iteration.settings.equipmentSettings.ComputeGearHasteRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.HasteRating);
            multiplicativeCharacterStats.hasteRating = 1.0f;

            additiveCharacterStats.hitRating = iteration.settings.equipmentSettings.ComputeGearHitRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.HitRating);
            multiplicativeCharacterStats.hitRating = 1.0f;

            additiveCharacterStats.criticalStrikeRating = iteration.settings.equipmentSettings.ComputeGearCritRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.CriticalRating);
            multiplicativeCharacterStats.criticalStrikeRating = 1.0f;

            additiveCharacterStats.expertiseRating = iteration.settings.equipmentSettings.ComputeGearExpertiseRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.ExpertiseRating);
            multiplicativeCharacterStats.expertiseRating = 1.0f;

            additiveCharacterStats.offHandExpertiseRating = iteration.settings.equipmentSettings.ComputeGearExpertiseRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.ExpertiseRating);
            multiplicativeCharacterStats.offHandExpertiseRating = 1.0f;

            additiveCharacterStats.armorPenetrationRating = iteration.settings.equipmentSettings.ComputeGearArmorPenetrationRating()
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.ArmorPenetrationRating);
            multiplicativeCharacterStats.armorPenetrationRating = 1.0f;

            multiplicativeCharacterStats.hasteFactor = iteration.settings.buffSettings.GetMultiplicativeStat(Stat.Haste)
                * iteration.settings.buffSettings.GetMultiplicativeStat(Stat.MeleeHaste)
                * iteration.computedConstants.bloodFrenzySpeedMultiplier;

            multiplicativeCharacterStats.damageMultiplier = 1.0f
                * TalentUtils.GetTitansDamageReductionMultiplier(iteration.settings.talentSettings, iteration.settings.equipmentSettings)
                * iteration.settings.buffSettings.GetMultiplicativeStat(Stat.Damage);
        }
        public int GetEffectiveStrength()
        {
            float value = (int)((additiveCharacterStats.strength
                + tempAdditiveCharacterStats.strength)
                * multiplicativeCharacterStats.strength
                * tempMultiplicativeCharacterStats.strength);
            if (iteration.stanceManager.IsInBerserkerStance())
            {
                value *=
                    TalentUtils.GetImprovedBerserkerStanceStrengthMultiplier(iteration.settings.talentSettings);
            }
            return (int)value;
        }
        public int GetEffectiveAgility()
        {
            return (int)((additiveCharacterStats.agility
                + tempAdditiveCharacterStats.agility)
                * multiplicativeCharacterStats.agility
                * tempMultiplicativeCharacterStats.agility);
        }
        public int GetEffectiveHitRating()
        {
            return (int)((additiveCharacterStats.hitRating + tempAdditiveCharacterStats.hitRating) * multiplicativeCharacterStats.hitRating * tempMultiplicativeCharacterStats.hitRating);
        }
        public int GetEffectiveHasteRating()
        {
            return (int)((additiveCharacterStats.hasteRating
                + tempAdditiveCharacterStats.hasteRating)
                * multiplicativeCharacterStats.hasteRating
                * tempMultiplicativeCharacterStats.hasteRating);
        }
        public int GetEffectiveCriticalStrikeRating()
        {
            return (int)((
                additiveCharacterStats.criticalStrikeRating
                + tempAdditiveCharacterStats.criticalStrikeRating) 
                * multiplicativeCharacterStats.criticalStrikeRating
                * tempMultiplicativeCharacterStats.criticalStrikeRating);
        }
        public int GetEffectiveExpertiseRating()
        {
            return (int)((additiveCharacterStats.expertiseRating
                + tempAdditiveCharacterStats.expertiseRating
                ) * multiplicativeCharacterStats.expertiseRating
                * tempMultiplicativeCharacterStats.expertiseRating);
        }
        public int GetEffectiveArmorPenetrationRating()
        {
            return (int)((additiveCharacterStats.armorPenetrationRating
                + tempAdditiveCharacterStats.armorPenetrationRating
                ) * multiplicativeCharacterStats.armorPenetrationRating
                * tempMultiplicativeCharacterStats.armorPenetrationRating);
        }
        public float GetEffectiveHasteMultiplier()
        {
            const float hasteRatingPerPercent = 25.21f;
            float hasteFromHasteRating = (1 + GetEffectiveHasteRating() / hasteRatingPerPercent / 100).RoundToSignificantDigits(4);
            return MathF.Round((hasteFromHasteRating * multiplicativeCharacterStats.hasteFactor * tempMultiplicativeCharacterStats.hasteFactor), 4);
        }
        public int GetEffectiveAttackPower()
        {
            return (int)((
                additiveCharacterStats.attackPower
                + tempAdditiveCharacterStats.attackPower
                + GetEffectiveStrength() * 2
                + tempAdditiveCharacterStats.armor
                * TalentUtils.GetArmoredToTheTeethAPBonus(iteration.settings.talentSettings) * GetEffectiveArmor() / 108.0f)
                * multiplicativeCharacterStats.attackPower * tempMultiplicativeCharacterStats.attackPower
                );
        }
        public int GetEffectiveArmor()
        {
            return (int)((additiveCharacterStats.armor + tempAdditiveCharacterStats.armor) * multiplicativeCharacterStats.armor * tempMultiplicativeCharacterStats.armor);
        }
        public float GetEffectiveDamageMultiplier()
        {
            return multiplicativeCharacterStats.damageMultiplier
                * tempMultiplicativeCharacterStats.damageMultiplier
                * iteration.settings.debuffSettings.GetMultiplicativeStat(Stat.MeleeDamage);
        }
        public float GetEffectiveCritChanceBeforeSuppression()
        {
            return (float)Math.Round(5.0f
                + (float)GetEffectiveAgility() / Constants.kAgilityPerCritPercent
                + TalentUtils.GetCrueltyBonus(iteration.settings.talentSettings)
                + GetEffectiveCriticalStrikeRating() / Constants.kCritRatingPerPercent
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.Critical)
                + iteration.settings.debuffSettings.GetAdditiveStat(Stat.Critical)
                + (iteration.stanceManager.IsInBerserkerStance() ? 3 : 0), 2);
        }
        public float GetEffectiveCritChanceAfterSuppression()
        {
            return GetEffectiveCritChanceBeforeSuppression()
                - AttackTableUtils.ComputeCritChanceReduction(iteration.settings.simulationSettings.targetLevel);
        }
        public float GetExtraHitChance()
		{
            return GetEffectiveHitRating() / Constants.kHitRatingPerPercent
                + (float)TalentUtils.GetPrecisionExtraHitChance(iteration.settings.talentSettings)
                + iteration.settings.buffSettings.GetAdditiveStat(Stat.HitChance); ;
        }

        // Update methods.
        public void UpdateTemporaryHasteMultiplier()
        {
            float value = 1.0f;
            if (iteration.auraManager.flurry != null && iteration.auraManager.flurry.active)
            {
                value *= iteration.auraManager.flurry.effects[0].value;
            }
            
            if (iteration.auraManager.heroism != null && iteration.auraManager.heroism.active)
            {
                value *= iteration.auraManager.heroism.effects[0].value;
            }

            if (iteration.auraManager.berserking != null && iteration.auraManager.berserking.active)
            {
                value *= iteration.auraManager.heroism.effects[0].value;
            }

            tempMultiplicativeCharacterStats.hasteFactor = value;
            iteration.mainHand.UpdateWeaponSpeed();
            iteration.offHand.UpdateWeaponSpeed();
        }
        public void UpdateCriticalStrikeChance()
        {
            return;
        }
        public void UpdateTemporaryAdditiveAttackPower()
		{
            int value = 0;
            if (iteration.auraManager.mainHandBerserking != null && iteration.auraManager.mainHandBerserking.active)
			{
                value += 400;
			}
            if (iteration.auraManager.offHandBerserking != null && iteration.auraManager.offHandBerserking.active)
            {
                value += 400;
            }
            if (iteration.auraManager.bloodFury != null && iteration.auraManager.bloodFury.active)
            {
                value += 326;
            }
            tempAdditiveCharacterStats.attackPower = value;
        }
        public void UpdateTemporaryDamageMultiplier()
        {
            float value = 1.0f;
            // Death wish
            if (iteration.auraManager.deathWish != null && iteration.auraManager.deathWish.active)
            {
                value *= iteration.auraManager.deathWish.effects[0].value;
            }

            // Wrecking Crew
            if (iteration.auraManager.wreckingCrew != null && iteration.auraManager.wreckingCrew.active)
            {
                value *= iteration.auraManager.wreckingCrew.effects[0].value;
            }

            tempMultiplicativeCharacterStats.damageMultiplier = value;
        }

    }
}
