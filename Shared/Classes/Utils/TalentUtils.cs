namespace Warrior
{
    public static class TalentUtils
    {
        public static bool HasBloodthirst(Talents talents)
        {
            return talents.Bloodthirst.rank == 1;
        }
        public static bool HasTitansGrip(Talents talents)
        {
            return talents.TitansGrip.rank == 1;
        }
        public static bool HasAngerManagement(Talents talents)
        {
            return talents.AngerManagement.rank == 1;
        }
        public static bool HasFlurry(Talents talents)
        {
            return talents.Flurry.rank > 0;
        }
        public static bool HasDeepWounds(Talents talents)
        {
            return talents.DeepWounds.rank > 0;
        }
        public static float GetArmoredToTheTeethAPBonus(Talents talents)
        {
            return talents.ArmoredToTheTeeth.rank;
        }
        public static float GetDeepWoundsMultiplier(Talents talents)
        {
            return talents.DeepWounds.rank * 0.16f;
        }
        public static float GetFlurryHasteMultiplier(Talents talents)
        {
            return 1 + (talents.Flurry.rank * 0.05f);
        }
        public static int GetCrueltyBonus(Talents talents)
        {
            return talents.Cruelty.rank;
        }
        public static float GetTitansDamageReductionMultiplier(Talents talents, Equipment equipment)
        {
            if (equipment.GetItemBySlot(ItemSlot.MainHand).weaponType == WeaponType.TwoHand 
                || equipment.GetItemBySlot(ItemSlot.OffHand).weaponType == WeaponType.TwoHand)
            {
                return 0.9f;
            }
            return 1.0f;
        }
        public static float GetDualWieldSpecializationMultiplier(Talents talents)
        {
            return 1 + talents.DualWieldSpecialization.rank * 0.05f;
        }
        public static float GetCritBonusImpaleMultiplier(Talents talents)
        {
            return 1 + talents.Impale.rank * 0.05f;
        }
        public static float GetTwoHandedWeaponSpecializationDamageMultiplier(Talents talents)
        {
            return 1 + talents.TwoHandedWeaponSpecialization.rank * 0.02f;
        }
        public static float GetOneHandedWeaponSpecializationDamageMultiplier(Talents talents)
        {
            return 1 + talents.OneHandedWeaponSpecialization.rank * 0.02f;
        }
        public static float GetImprovedWhirlwindDamageMultiplier(Talents talents)
        {
            return 1 + talents.ImprovedWhirlwind.rank * 0.1f;
        }
        public static int GetImprovedHeroicStrikeReduction(Talents talents)
        {
            return talents.ImprovedHeroicStrike.rank;
        }
        public static int GetFocusedRageReduction(Talents talents)
        {
            return talents.FocusedRage.rank;
        }
        public static int GetPrecisionExtraHitChance(Talents talents)
        {
            return talents.Precision.rank;
        }
        public static float GetUnendingFuryDamageMultiplier(Talents talents)
        {
            return 1 + talents.UnendingFury.rank * 0.02f;
        }
        public static int GetUnbridledWrathPPM(Talents talents)
        {
            return 3 * talents.UnbridledWrath.rank;
        }
        public static float GetBloodSurgeChance(Talents talents)
        {
            if (talents.Bloodsurge.rank == 0)
            {
                return 0;
            }
            if (talents.Bloodsurge.rank == 1)
            {
                return 7;
            }
            if (talents.Bloodsurge.rank == 2)
            {
                return 13;
            }
            return 20;
        }
        public static float GetImprovedBerserkerStanceStrengthMultiplier(Talents talents)
        {
            return 1 + talents.ImprovedBerserkerStance.rank * 0.04f;
        }
    }
}
