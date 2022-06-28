using Warrior.Entities;

namespace Warrior
{
    public static class ArmorUtils
    {
        public static float ComputeEffectiveTargetArmor(Iteration iteration)
        {
            // Wrath of the Lich King Patch 3.2.2 (2009-09-22): The amount of armor penetration gained per point of this rating has been reduced by 12%.
            // Wrath of the Lich King Patch 3.1.2(2009 - 05 - 19): Capped to 100 % (or 1232 armor penetration rating)
            // Wrath of the Lich King Patch 3.1.0(2009 - 04 - 14): All classes now receive 25 % more benefit from Armor Penetration Rating.
            // Using 3.2.2 for now.
            // Considers armor penetration and raid armor debuffs.
            float armor = iteration.settings.simulationSettings.targetArmor;
            float shatteringthrow = 0;
            if (iteration.auraManager.shatteringThrow != null && iteration.auraManager.shatteringThrow.active)
            {
                shatteringthrow = 0.2f;
            }
            armor *= (100 - iteration.settings.debuffSettings.GetAdditiveStat(Stat.Armor) - shatteringthrow) / 100.0f;
            float armorConstant = 400
                + 85 * iteration.settings.simulationSettings.targetLevel
                + 4.5f * 85 * (iteration.settings.simulationSettings.targetLevel - 59);
            float armorPenetrationCap = (armor + armorConstant) / 3;
            float armorPenetrationPercent = iteration.statsManager.GetEffectiveArmorPenetrationRating()
                / Constants.kArmorPenetrationPerPercent;
            if (iteration.stanceManager.IsInBattleStance())
            {
                armorPenetrationPercent += 10;
            }
            float armorReduction = Math.Min(iteration.settings.simulationSettings.targetArmor, armorPenetrationCap) * armorPenetrationPercent / 100;

            float effectiveArmor = armor - armorReduction;
            return effectiveArmor;
        }

        // Computes the physical damage reduction offered by the target's armor.
        public static float ComputeEffectiveArmorDamageReductionMultiplier(Iteration iteration)
        {
            float effectiveArmor = ComputeEffectiveTargetArmor(iteration);
            return 1 - effectiveArmor
                / ((467.5f * iteration.settings.simulationSettings.targetLevel) + effectiveArmor - 22167.5f);
        }
    }
}
