namespace Warrior
{
    public enum AttackResult
    {
        Hit,
        Glancing,
        Critical,
        Miss,
        Dodge,
        Parry
    }

    public static class AttackTableUtils
    {
        public static int ComputeGlancingChance(int targetLevel)
        {
            if (targetLevel == 83)
            {
                return 24;
            }
            if (targetLevel == 82)
            {
                return 20;
            }
            if (targetLevel == 81)
            {
                return 15;
            }
            return 10;
        }
        public static float ComputeGlancingMultiplier(int targetLevel, Random random)
        {
            if (targetLevel == 83)
            {
                return random.Next(65000, 85000) / 100000f;
            }
            if (targetLevel == 82)
            {
                return random.Next(75000, 95000) / 100000f;
            }
            if (targetLevel == 81)
            {
                return random.Next(91000, 99000) / 100000f;
            }
            return random.Next(91000, 99000) / 100000f;
        }
        public static float ComputeDodgeChance(Iteration iteration, bool isMainHand)
        {
            int expertiseRating = iteration.statsManager.GetEffectiveExpertiseRating();
            float reductionChance = (expertiseRating + GetBonusExpertise(iteration, isMainHand)) / Constants.kExpertisePerPoint / 4;
            if (iteration.settings.simulationSettings.targetLevel == 83)
            {
                return Math.Max(0, 6.5f - reductionChance);
            }
            if (iteration.settings.simulationSettings.targetLevel == 82)
            {
                return Math.Max(0, 5.4f - reductionChance);
            }
            if (iteration.settings.simulationSettings.targetLevel == 81)
            {
                return Math.Max(0, 5.2f - reductionChance);
            }
            return Math.Max(0, 5f - reductionChance); ;
        }
        public static float ComputeCritChanceReduction(int targetLevel)
        {
            if (targetLevel == 83)
            {
                return 4.8f;
            }
            if (targetLevel == 82)
            {
                return 0.4f;
            }
            if (targetLevel == 81)
            {
                return 0.2f;
            }
            return 0;
        }
        public static float ComputeEffectiveCritChance(Iteration iteration)
        {
            return iteration.statsManager.GetEffectiveCritChanceBeforeSuppression()
                - ComputeCritChanceReduction(iteration.settings.simulationSettings.targetLevel);
        }
        public static float ComputeMissChance(Iteration iteration)
        {
            float missChance = 24;
            if (iteration.settings.simulationSettings.targetLevel == 83)
            {
                missChance = 27;
            }
            if (iteration.settings.simulationSettings.targetLevel == 82)
            {
                missChance = 25;
            }
            if (iteration.settings.simulationSettings.targetLevel == 81)
            {
                missChance = 24.5f;
            }
            return missChance - iteration.statsManager.GetExtraHitChance();
        }
        public static float ComputeYellowMissChance(Iteration iteration)
        {
            float missChance = 5;
            if (iteration.settings.simulationSettings.targetLevel == 83)
            {
                missChance = 8;
            }
            if (iteration.settings.simulationSettings.targetLevel == 82)
            {
                missChance = 6;
            }
            if (iteration.settings.simulationSettings.targetLevel == 81)
            {
                missChance = 5.5f;
            }
            float chance = missChance - iteration.statsManager.GetExtraHitChance();
            return chance <= 0 ? 0 : chance;
        }
        public static AttackResult GetWhiteHitResult(Iteration iteration, bool isMainHand)
        {
            int roll = iteration.random.Next(0, 10000);
            float missChance = ComputeMissChance(iteration) * 100;
            float dodgeChance = ComputeDodgeChance(iteration, isMainHand) * 100;
            float glancingChance = ComputeGlancingChance(iteration.settings.simulationSettings.targetLevel) * 100;
            float critChance = ComputeEffectiveCritChance(iteration) * 100;

            if (roll < missChance)
            {
                return AttackResult.Miss;
            }
            if (roll < missChance + dodgeChance)
            {
                return AttackResult.Dodge;
            }
            if (roll < missChance + dodgeChance + glancingChance)
            {
                return AttackResult.Glancing;
            }
            if (roll < missChance + dodgeChance + glancingChance + critChance)
            {
                return AttackResult.Critical;
            }
            return AttackResult.Hit;
        }
        public static AttackResult GetYellowHitResult(Iteration iteration)
        {
            int roll = iteration.random.Next(0, 10000);
            float missChance = ComputeYellowMissChance(iteration) * 100;
            float dodgeChance = ComputeDodgeChance(iteration, true) * 100;
            float critChance = ComputeEffectiveCritChance(iteration) * 100;

            if (roll < missChance)
            {
                return AttackResult.Miss;
            }
            if (roll < missChance + dodgeChance)
            {
                return AttackResult.Dodge;
            }

            int secondRoll = iteration.random.Next(0, 10000);

            if (secondRoll < (int)((10000 - missChance - dodgeChance) * critChance / 10000))
            {
                return AttackResult.Critical;
            }
            return AttackResult.Hit;
        }
        public static float GetNormalizedWeaponSpeed(Weapon weapon)
        {
            if (weapon.item.weaponHandedness == WeaponHandedness.TwoHand)
            {
                return 3.3f;
            }
            if (weapon.item.weaponHandedness == WeaponHandedness.OneHand)
            {
                return 2.4f;
            }
            return 1.7f;
        }
        public static int GetBonusExpertise(Iteration iteration, bool isMainHand)
        {
            WeaponType weapontype = WeaponType(isMainHand, iteration.settings.equipmentSettings);

            // Human 1H maces and 2H maces
            if (iteration.settings.characterSettings.race == Settings.Race.Human
                && (weapontype == Warrior.WeaponType.OneHandedMace
                   || weapontype == Warrior.WeaponType.TwoHandedMace))
            {
                return 3;
            }
            // Human 1H swords and 2H swords
            if (iteration.settings.characterSettings.race == Settings.Race.Human
                && (weapontype == Warrior.WeaponType.OneHandedSword
                   || weapontype == Warrior.WeaponType.TwoHandedSword))
            {
                return 3;
            }
            // Orcs 1H axes, 2H axes and fist weapons
            if (iteration.settings.characterSettings.race == Settings.Race.Orc
                && (weapontype == Warrior.WeaponType.Fist
                || weapontype == Warrior.WeaponType.OneHandedAxe
                || weapontype == Warrior.WeaponType.TwoHandedAxe))
            {
                return 5;
            }
            return 0;
        }

        public static WeaponType WeaponType(bool isMainHand, Settings.EquipmentSettings settings)
        {
            if (isMainHand)
            {
                return settings.GetItemBySlot(ItemSlot.MainHand).weaponType;
            } 
            return settings.GetItemBySlot(ItemSlot.OffHand).weaponType;

        }
    }
}
