namespace Warrior
{
    public class StatsManager
    {
        Character character;
        AuraManager auraManager;
        Iteration iteration;  
        CharacterStats additiveCharacterStats;
        CharacterStats multiplicativeCharacterStats;
        CharacterStats tempAdditiveCharacterStats;
        CharacterStats tempMultiplicativeCharacterStats;
        public StatsManager(Character character, AuraManager manager, Iteration iteration)
        {
            this.character = character;
            this.auraManager = manager;
            this.iteration = iteration;

            // Subscribe to events to know when to update stats.
            // When the stance has changed.
            iteration.stanceManager.StanceChanged += OnStanceChanged;

            additiveCharacterStats = character.additiveCharacterStats;
            multiplicativeCharacterStats = character.multiplicativeCharacterStats;
            tempAdditiveCharacterStats = new CharacterStats();
            tempMultiplicativeCharacterStats = new CharacterStats();
            tempMultiplicativeCharacterStats.strength = 1;
            tempMultiplicativeCharacterStats.agility = 1;
            tempMultiplicativeCharacterStats.stamina = 1;
            tempMultiplicativeCharacterStats.armor = 1;
            tempMultiplicativeCharacterStats.hitRating = 1;
            tempMultiplicativeCharacterStats.hasteFactor = 1;
            tempMultiplicativeCharacterStats.attackPower = 1;
            tempMultiplicativeCharacterStats.hasteRating = 1;
            tempMultiplicativeCharacterStats.expertiseRating = 1;
        }

        public void OnStanceChanged(object sender, EventArgs e)
        {
            UpdateCriticalStrikeChance();
            Console.WriteLine("[ " + iteration.currentStep + " ] Stance Has Changed");
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
                + tempAdditiveCharacterStats.strength * 2
                + tempAdditiveCharacterStats.armor
                * TalentUtils.GetArmoredToTheTeethAPBonus(character.talents) * GetEffectiveArmor() / 108.0f)
                * multiplicativeCharacterStats.attackPower * tempMultiplicativeCharacterStats.attackPower
                );
        }
        public int GetEffectiveArmor()
        {
            return (int)((additiveCharacterStats.armor + tempAdditiveCharacterStats.armor) * multiplicativeCharacterStats.armor * tempMultiplicativeCharacterStats.armor);
        }
        public float GetEffectiveDamageMultiplier()
        {
            return multiplicativeCharacterStats.damageMultiplier;
        }
        public float GetEffectiveCritChanceBeforeSuppression()
        {
            return (float)Math.Round(5.0f
                + (float)GetEffectiveAgility() / Constants.kAgilityPerCritPercent
                + TalentUtils.GetCrueltyBonus(character.talents)
                + GetEffectiveCriticalStrikeRating() / Constants.kCritRatingPerPercent
                + character.auraSettings.GetAdditiveStat(EffectStat.Critical), 2);
        }
        public float GetEffectiveCritChanceAfterSuppression()
        {
            return GetEffectiveCritChanceBeforeSuppression()
                - AttackTableUtils.ComputeCritChanceReduction(character.simulation.settings.targetLevel);
        }

        public void UpdateTemporaryHasteMultiplier()
        {
            float value = 1.0f;
            if (auraManager.flurry != null && auraManager.flurry.active)
            {
                value *= auraManager.flurry.effects[0].value;
            }
            
            tempMultiplicativeCharacterStats.hasteFactor = value;
            iteration.mainHand.UpdateWeaponSpeed();
            iteration.offHand.UpdateWeaponSpeed();
        }

        public void UpdateCriticalStrikeChance()
        {
            return;
        }

    }
}
