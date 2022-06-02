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
        public static float ComputeDodgeChance(Simulation simulation)
        {
            int expertiseRating = simulation.character.GetExpertiseRating();
            float reductionChance = expertiseRating / Constants.kExpertisePerPoint / 4;
            if (simulation.settings.targetLevel == 83)
            {
                return Math.Max(0, 6.5f - reductionChance);
            }
            if (simulation.settings.targetLevel == 82)
            {
                return Math.Max(0, 5.4f - reductionChance);
            }
            if (simulation.settings.targetLevel == 81)
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
        public static float ComputeEffectiveCritChance(Simulation simulation)
        {
            return simulation.character.GetMeleeCritChancePercent() - ComputeCritChanceReduction(simulation.settings.targetLevel);
        }
        public static float ComputeMissChance(Simulation simulation)
        {
            float missChance = 24;
            if (simulation.settings.targetLevel == 83)
            {
                missChance = 27;
            }
            if (simulation.settings.targetLevel == 82)
            {
                missChance = 25;
            }
            if (simulation.settings.targetLevel == 81)
            {
                missChance = 24.5f;
            }
            return missChance - simulation.character.GetMeleeHitChance();
        }
        public static float ComputeYellowMissChance(Simulation simulation)
        {
            float missChance = 5;
            if (simulation.settings.targetLevel == 83)
            {
                missChance = 8;
            }
            if (simulation.settings.targetLevel == 82)
            {
                missChance = 6;
            }
            if (simulation.settings.targetLevel == 81)
            {
                missChance = 5.5f;
            }
            float chance = missChance - simulation.character.GetMeleeHitChance();
            return chance <= 0 ? 0 : chance;
        }
        public static AttackResult GetWhiteHitResult(Random random, Simulation simulation)
        {
            int roll = random.Next(0, 10000);
            float missChance = ComputeMissChance(simulation) * 100;
            float dodgeChance = ComputeDodgeChance(simulation) * 100;
            float glancingChance = ComputeGlancingChance(simulation.settings.targetLevel) * 100;
            float critChance = ComputeEffectiveCritChance(simulation) * 100;

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
        public static AttackResult GetYellowHitResult(Random random, Simulation simulation)
        {
            int roll = random.Next(0, 10000);
            float missChance = ComputeYellowMissChance(simulation) * 100;
            float dodgeChance = ComputeDodgeChance(simulation) * 100;
            float critChance = ComputeEffectiveCritChance(simulation) * 100;

            if (roll < missChance)
            {
                return AttackResult.Miss;
            }
            if (roll < missChance + dodgeChance)
            {
                return AttackResult.Dodge;
            }

            int secondRoll = random.Next(0, 10000);

            if (secondRoll < (int)((10000 - missChance - dodgeChance) * critChance / 10000))
            {
                return AttackResult.Critical;
            }
            return AttackResult.Hit;
        }
        public static float GetNormalizedWeaponSpeed(Weapon weapon)
        {
            if (weapon.item.weaponType == WeaponType.TwoHand)
            {
                return 3.3f;
            }
            if (weapon.item.weaponType == WeaponType.OneHand)
            {
                return 2.4f;
            }
            return 1.7f;
        }

    }
}
