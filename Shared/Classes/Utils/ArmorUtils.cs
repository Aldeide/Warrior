namespace Warrior
{
    public static class ArmorUtils
    {
        public static float ComputeEffectiveTargetArmor(Simulation simulation)
        {
            // Wrath of the Lich King Patch 3.2.2 (2009-09-22): The amount of armor penetration gained per point of this rating has been reduced by 12%.
            // Wrath of the Lich King Patch 3.1.2(2009 - 05 - 19): Capped to 100 % (or 1232 armor penetration rating)
            // Wrath of the Lich King Patch 3.1.0(2009 - 04 - 14): All classes now receive 25 % more benefit from Armor Penetration Rating.
            // Using 3.2.2 for now.
            // Considers armor penetration and raid armor debuffs.
            float armor = simulation.settings.targetArmor;
            armor *= simulation.character.debuffSettings.GetMultiplicativeStat(EffectStat.Armor);
            float armorConstant = 400 + 85 * simulation.settings.targetLevel + 4.5f * 85 * (simulation.settings.targetLevel - 59);
            float armorPenetrationCap = (armor + armorConstant) / 3;
            float armorPenetrationPercent = simulation.character.GetArmorPenetrationRating() / 13.99f;

            float armorReduction = Math.Min(simulation.settings.targetArmor, armorPenetrationCap) * armorPenetrationPercent / 100;

            //TODO(replace targetArmor by armor after debuffs)
            float effectiveArmor = armor - armorReduction;
            return effectiveArmor;
        }

        // Computes the physical damage reduction offered by the target's armor.
        public static float ComputeEffectiveArmorDamageReductionMultiplier(Simulation simulation)
        {
            float effectiveArmor = ComputeEffectiveTargetArmor(simulation);
            return 1 - effectiveArmor / ((467.5f * 80) + effectiveArmor - 22167.5f);
        }
    }
}
