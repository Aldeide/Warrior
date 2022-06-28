using Warrior.Settings;

namespace Warrior
{
    public static class DamageUtils
    {
        // Return the effective damage multiplier including talents, buffs, armor reduction.
        public static float EffectiveDamageCoefficient(Iteration iteration)
        {
            float coefficient = 1.0f;

            // Talents and permanent buffs.
            coefficient *= iteration.statsManager.GetEffectiveDamageMultiplier();
            // Boss armor reduction.
            coefficient *= ArmorUtils.ComputeEffectiveArmorDamageReductionMultiplier(iteration);
            return coefficient;

        }
        // Return the effective damage multiplier including talents, buffs and without armor reduction.
        public static float EffectiveBleedDamageCoefficient(Iteration iteration)
        {
            float coefficient = 1.0f;
            // Talents and permanent buffs.
            coefficient *= iteration.statsManager.GetEffectiveDamageMultiplier();
            return coefficient;

        }
        public static float EffectiveCritCoefficient(TalentsSettings talents)
        {
            return 1.0f + 1.0f * TalentUtils.GetCritBonusImpaleMultiplier(talents);
        }
        public static int WeaponDamage(AttackResult result, Weapon weapon, Iteration iteration, int bonus)
        {
            // Base weapon damage.
            float damage = iteration.random.Next(weapon.minDamage, weapon.maxDamage) + bonus;
            // Attack power contribution. Speed value?
            damage += iteration.statsManager.GetEffectiveAttackPower() / 14.0f * weapon.baseSpeed / Constants.kStepsPerSecond;

            // Two-Handed Specialization talent.
            if (weapon.item.weaponHandedness == WeaponHandedness.TwoHand)
            {
                damage *= TalentUtils.GetTwoHandedWeaponSpecializationDamageMultiplier(iteration.settings.talentSettings);
            }
            // One-Handed Specialization talent.
            if (weapon.item.weaponHandedness == WeaponHandedness.OneHand)
            {
                damage *= TalentUtils.GetOneHandedWeaponSpecializationDamageMultiplier(iteration.settings.talentSettings);
            }
            // Other multipliers (raid buffs, titan's grip if applicable, boss armor reduction).
            damage *= EffectiveDamageCoefficient(iteration);
            // Offhand damage penalty.
            if (!weapon.isMainHand)
            {
                damage *= iteration.computedConstants.offHandDamageMultiplier;
            }
            // Glancing penalty.
            if (result == AttackResult.Glancing)
            {
                damage *= AttackTableUtils.ComputeGlancingMultiplier(iteration.settings.simulationSettings.targetLevel, iteration.random);
            }
            // Critical bonus.
            if (result == AttackResult.Critical)
            {
                
                damage *= iteration.computedConstants.criticalDamageMultiplier;
            }
            return (int)damage;
        }
        public static int GetWeaponDamageRoll(Weapon weapon)
        {
            return MathUtils.random.Next(weapon.minDamage, weapon.maxDamage);
        }
        public static int ComputeGeneratedRage(AttackResult result, int damage, Weapon weapon, Iteration iteration)
        {
            float factor = 1.75f;
            if (weapon.isMainHand)
            {
                factor *= 2;
            }
            if (result == AttackResult.Critical)
            {
                factor *= 2;
            }

            float rage = (int)(15 * damage / (4 * 453.3f) + 0.5f * factor * (float)weapon.baseSpeed / Constants.kStepsPerSecond);
            if (TalentUtils.GetUnbridledWrathPPM(iteration.settings.talentSettings) > 0)
            {
                float pph = weapon.baseSpeed * TalentUtils.GetUnbridledWrathPPM(iteration.settings.talentSettings) / 60 * 10;
                int roll = iteration.random.Next(0, 10000);
                if (pph < roll)
                {
                    rage += 1;
                }
            }
            if (iteration.computedConstants.hasEndlessRage)
            {
                rage *= 1.25f;
            }
            return (int)rage;
        }
        public static int AverageWeaponDamage(Weapon weapon, Iteration iteration, int bonus)
        {
            // Base weapon damage.
            float damage = 0.5f * (weapon.minDamage + weapon.maxDamage) + bonus;
            // Attack power contribution. Speed value?
            damage += iteration.statsManager.GetEffectiveAttackPower() / 14f * (float)weapon.baseSpeed / 1000;

            // Two-Handed Specialization talent.
            if (weapon.item.weaponHandedness == WeaponHandedness.TwoHand)
            {
                damage *= TalentUtils.GetTwoHandedWeaponSpecializationDamageMultiplier(iteration.settings.talentSettings);
            }
            // One-Handed Specialization talent.
            if (weapon.item.weaponHandedness == WeaponHandedness.OneHand)
            {
                damage *= TalentUtils.GetOneHandedWeaponSpecializationDamageMultiplier(iteration.settings.talentSettings);
            }
            // Other multipliers (raid buffs, titan's grip if applicable).
            damage *= EffectiveBleedDamageCoefficient(iteration);
            // Offhand damage penalty.
            if (!weapon.isMainHand)
            {
                damage *= 0.5f;
                damage *= TalentUtils.GetDualWieldSpecializationMultiplier(iteration.settings.talentSettings);
            }
            return (int)damage;
        }

    }
}
