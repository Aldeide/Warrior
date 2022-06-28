using Warrior.Settings;

namespace Warrior
{
    public static class TalentUtils
    {
        public static bool HasBloodthirst(TalentsSettings talents)
        {
            return talents.Bloodthirst.rank == 1;
        }
        public static bool HasTitansGrip(TalentsSettings talents)
        {
            return talents.TitansGrip.rank == 1;
        }
        public static bool HasAngerManagement(TalentsSettings talents)
        {
            return talents.AngerManagement.rank == 1;
        }
        public static bool HasFlurry(TalentsSettings talents)
        {
            return talents.Flurry.rank > 0;
        }
        public static bool HasDeepWounds(TalentsSettings talents)
        {
            return talents.DeepWounds.rank > 0;
        }
        public static float GetArmoredToTheTeethAPBonus(TalentsSettings talents)
        {
            return talents.ArmoredToTheTeeth.rank;
        }
        public static float GetDeepWoundsMultiplier(TalentsSettings talents)
        {
            return talents.DeepWounds.rank * 0.16f;
        }
        public static float GetFlurryHasteMultiplier(TalentsSettings talents)
        {
            return 1 + (talents.Flurry.rank * 0.05f);
        }
        public static int GetCrueltyBonus(TalentsSettings talents)
        {
            return talents.Cruelty.rank;
        }
        public static float GetTitansDamageReductionMultiplier(TalentsSettings talents, EquipmentSettings equipment)
        {
            if (equipment.GetItemBySlot(ItemSlot.MainHand).weaponHandedness == WeaponHandedness.TwoHand 
                || equipment.GetItemBySlot(ItemSlot.OffHand).weaponHandedness == WeaponHandedness.TwoHand)
            {
                return 0.9f;
            }
            return 1.0f;
        }
        public static float GetDualWieldSpecializationMultiplier(TalentsSettings talents)
        {
            return 1 + talents.DualWieldSpecialization.rank * 0.05f;
        }
        public static float GetCritBonusImpaleMultiplier(TalentsSettings talents)
        {
            return 1 + talents.Impale.rank * 0.05f;
        }
        public static float GetTwoHandedWeaponSpecializationDamageMultiplier(TalentsSettings talents)
        {
            return 1 + talents.TwoHandedWeaponSpecialization.rank * 0.02f;
        }
        public static float GetOneHandedWeaponSpecializationDamageMultiplier(TalentsSettings talents)
        {
            return 1 + talents.OneHandedWeaponSpecialization.rank * 0.02f;
        }
        public static float GetImprovedWhirlwindDamageMultiplier(TalentsSettings talents)
        {
            return 1 + talents.ImprovedWhirlwind.rank * 0.1f;
        }
        public static int GetImprovedHeroicStrikeReduction(TalentsSettings talents)
        {
            return talents.ImprovedHeroicStrike.rank;
        }
        public static int GetFocusedRageReduction(TalentsSettings talents)
        {
            return talents.FocusedRage.rank;
        }
        public static int GetPrecisionExtraHitChance(TalentsSettings talents)
        {
            return talents.Precision.rank;
        }
        public static float GetUnendingFuryDamageMultiplier(TalentsSettings talents)
        {
            return 1 + talents.UnendingFury.rank * 0.02f;
        }
        public static int GetUnbridledWrathPPM(TalentsSettings talents)
        {
            return 3 * talents.UnbridledWrath.rank;
        }
        public static float GetBloodSurgeChance(TalentsSettings talents)
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
        public static float GetImprovedBerserkerStanceStrengthMultiplier(TalentsSettings talents)
        {
            return 1 + talents.ImprovedBerserkerStance.rank * 0.04f;
        }

        public static float GetImprovedMortalStrikeMultiplier(TalentsSettings talents)
        {
            if (talents.ImprovedMortalStrike.rank == 1) return 1.03f;
            if (talents.ImprovedMortalStrike.rank == 2) return 1.06f;
            if (talents.ImprovedMortalStrike.rank == 3) return 1.10f;
            return 1;
        }
    }
}
