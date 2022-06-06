namespace Warrior.Settings
{
    public class TalentsSettings
    {
        #region Arms
        public Talent ImprovedHeroicStrike { get; set; }
        public Talent Deflection { get; set; }
        public Talent ImprovedRend { get; set; }
        public Talent ImprovedCharge { get; set; }
        public Talent IronWill { get; set; }
        public Talent TacticalMastery { get; set; }
        public Talent ImprovedOverPower { get; set; }
        public Talent AngerManagement { get; set; }
        public Talent Impale { get; set; }
        public Talent DeepWounds { get; set; }
        public Talent TwoHandedWeaponSpecialization { get; set; }
        public Talent TasteForBlood { get; set; }
        public Talent PoleaxeSpecialization { get; set; }
        public Talent SweepingStrikes { get; set; }
        public Talent MaceSpecialization { get; set; }
        public Talent SwordSpecialization { get; set; }
        public Talent WeaponMastery { get; set; }
        public Talent ImprovedHamstring { get; set; }
        public Talent Trauma { get; set; }
        public Talent SecondWind { get; set; }
        public Talent MortalStrike { get; set; }
        public Talent StrengthOfArms { get; set; }
        public Talent ImprovedSlam { get; set; }
        public Talent Juggernaut { get; set; }
        public Talent ImprovedMortalStrike { get; set; }
        public Talent UnrelentingAssault { get; set; }
        public Talent SuddenDeath { get; set; }
        public Talent EndlessRage { get; set; }
        public Talent BloodFrenzy { get; set; }
        public Talent WreckingCrew { get; set; }
        public Talent Bladestorm { get; set; }
        #endregion

        #region Fury
        public Talent ArmoredToTheTeeth { get; set; }
        public Talent BoomingVoice { get; set; }
        public Talent Cruelty { get; set; }
        public Talent ImprovedDemoralizingShout { get; set; }
        public Talent UnbridledWrath { get; set; }
        public Talent ImprovedCleave { get; set; }
        public Talent PiercingHowl { get; set; }
        public Talent BloodCraze { get; set; }
        public Talent CommandingPresence { get; set; }
        public Talent DualWieldSpecialization { get; set; }
        public Talent ImprovedExecute { get; set; }
        public Talent Enrage { get; set; }
        public Talent Precision { get; set; }
        public Talent DeathWish { get; set; }
        public Talent ImprovedIntercept { get; set; }
        public Talent ImprovedBerserkerRage { get; set; }
        public Talent Flurry { get; set; }
        public Talent IntensifyRage { get; set; }
        public Talent Bloodthirst { get; set; }
        public Talent ImprovedWhirlwind { get; set; }
        public Talent FuriousAttacks { get; set; }
        public Talent ImprovedBerserkerStance { get; set; }
        public Talent HeroicFury { get; set; }
        public Talent Rampage { get; set; }
        public Talent Bloodsurge { get; set; }
        public Talent UnendingFury { get; set; }
        public Talent TitansGrip { get; set; }
        #endregion

        #region Protection
        public Talent ImprovedBloodrage { get; set; }
        public Talent ShieldSpecialization { get; set; }
        public Talent ImprovedThunderClap { get; set; }
        public Talent Incite { get; set; }
        public Talent Anticipation { get; set; }
        public Talent LastStand { get; set; }
        public Talent ImprovedRevenge { get; set; }
        public Talent ShieldMastery { get; set; }
        public Talent Toughness { get; set; }
        public Talent ImprovedSpellReflection { get; set; }
        public Talent ImprovedDisarm { get; set; }
        public Talent Puncture { get; set; }
        public Talent ImprovedDisciplines { get; set; }
        public Talent ConcussionBlow { get; set; }
        public Talent GagOrder { get; set; }
        public Talent OneHandedWeaponSpecialization { get; set; }
        public Talent ImprovedDefensiveStance { get; set; }
        public Talent Vigilance { get; set; }
        public Talent FocusedRage { get; set; }
        public Talent Vitality { get; set; }
        public Talent Safeguard { get; set; }
        public Talent Warbringer { get; set; }
        public Talent Devastate { get; set; }
        public Talent CriticalBlock { get; set; }
        public Talent SwordAndBoard { get; set; }
        public Talent DamageShield { get; set; }
        public Talent Shockwave { get; set; }
        #endregion
        public TalentsSettings()
        {
            // Arms.
            #region Arms
            ImprovedHeroicStrike = new Talent() { name = "Improved Heroic Strike", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12282, 12663, 12664 }, currentSpellId = 12664, rank = 3 };
            Deflection = new Talent() { name = "Deflection", talentTree = TalentTree.Arms, maxRank = 5, spellIds = new int[] { 16462, 16463, 16464, 16465, 16466 }, currentSpellId = 16462 };
            ImprovedRend = new Talent() { name = "Improved Rend", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 12286, 12658 }, currentSpellId = 12658, rank = 2 };
            ImprovedCharge = new Talent() { name = "Improved Charge", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 12285, 12697 }, currentSpellId = 12285 };
            IronWill = new Talent() { name = "Iron Will", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12300, 12959, 12960 }, currentSpellId = 12959, rank = 2 };
            TacticalMastery = new Talent() { name = "Tactical Mastery", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12295, 12676, 12677 }, currentSpellId = 12677, rank = 3 };
            ImprovedOverPower = new Talent() { name = "Improved Overpower", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 12290, 12963 }, currentSpellId = 12290 };
            AngerManagement = new Talent() { name = "Tactical Mastery", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 12296 }, currentSpellId = 12296 };
            Impale = new Talent() { name = "Impale", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 16493, 16494 }, currentSpellId = 16494, rank = 2 };
            DeepWounds = new Talent() { name = "Deep Wounds", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12834, 12849, 12867 }, currentSpellId = 12867, rank = 3 };
            TwoHandedWeaponSpecialization = new Talent() { name = "Two-Handed Weapon Specialization", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12163, 12711, 12712 }, currentSpellId = 12712, rank = 3 };
            TasteForBlood = new Talent() { name = "Taste for Blood", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 56636, 56637, 56638 }, currentSpellId = 56636 };
            PoleaxeSpecialization = new Talent() { name = "Poleaxe Specialization", talentTree = TalentTree.Arms, maxRank = 5, spellIds = new int[] { 12700, 12781, 12783, 12784, 12785 }, currentSpellId = 12700 };
            SweepingStrikes = new Talent() { name = "Sweeping Strikes", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 12328 }, currentSpellId = 12328 };
            MaceSpecialization = new Talent() { name = "Mace Specialization", talentTree = TalentTree.Arms, maxRank = 5, spellIds = new int[] { 12284, 12701, 12702, 12703, 12704, 12815 }, currentSpellId = 12284 };
            SwordSpecialization = new Talent() { name = "Sword Specialization", talentTree = TalentTree.Arms, maxRank = 5, spellIds = new int[] { 12281, 12812, 12813, 12814, 12815 }, currentSpellId = 12281 };
            WeaponMastery = new Talent() { name = "Weapon Mastery", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 20504, 20505 }, currentSpellId = 20504 };
            ImprovedHamstring = new Talent() { name = "Improved Hamstring", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 12289, 12668, 23695 }, currentSpellId = 12289 };
            Trauma = new Talent() { name = "Trauma", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 46854, 46855 }, currentSpellId = 46854 };
            SecondWind = new Talent() { name = "Second Wind", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 29834, 29838 }, currentSpellId = 29834 };
            MortalStrike = new Talent() { name = "Mortal Strike", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 12294 }, currentSpellId = 12294 };
            StrengthOfArms = new Talent() { name = "Strength Of Arms", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 46865, 46866 }, currentSpellId = 46865 };
            ImprovedSlam = new Talent() { name = "Improved Slam", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 12862, 12330 }, currentSpellId = 12862 };
            Juggernaut = new Talent() { name = "Juggernaut", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 64976 }, currentSpellId = 64976 };
            ImprovedMortalStrike = new Talent() { name = "Improved Mortal Strike", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 35446, 35448, 35449 }, currentSpellId = 35446 };
            UnrelentingAssault = new Talent() { name = "Unrelenting Assault", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 46859, 46860 }, currentSpellId = 46859 };
            SuddenDeath = new Talent() { name = "Sudden Death", talentTree = TalentTree.Arms, maxRank = 3, spellIds = new int[] { 29723, 29725, 29724 }, currentSpellId = 29723 };
            EndlessRage = new Talent() { name = "Endless Rage", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 29623 }, currentSpellId = 29623 };
            BloodFrenzy = new Talent() { name = "Blood Frenzy", talentTree = TalentTree.Arms, maxRank = 2, spellIds = new int[] { 29836, 29859 }, currentSpellId = 29836 };
            WreckingCrew = new Talent() { name = "Wrecking Crew", talentTree = TalentTree.Arms, maxRank = 5, spellIds = new int[] { 46867, 56611, 56612, 56613, 56614 }, currentSpellId = 46867 };
            Bladestorm = new Talent() { name = "Bladestorm", talentTree = TalentTree.Arms, maxRank = 1, spellIds = new int[] { 46924 }, currentSpellId = 46924 };
            #endregion

            // Fury.
            ArmoredToTheTeeth = new Talent() { name = "Armored to the Teeth", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 61216, 61221, 61222 }, currentSpellId = 61222, rank = 3 };
            BoomingVoice = new Talent() { name = "Booming Voice", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 12321, 12835 }, currentSpellId = 12835, rank = 2 };
            Cruelty = new Talent() { name = "Cruelty", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12320, 12852, 12853, 12855, 12856 }, currentSpellId = 12856, rank = 5 };
            ImprovedDemoralizingShout = new Talent() { name = "Improved Demoralizing Shout", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12324, 12876, 12877, 12878, 12879 }, currentSpellId = 12324 };
            UnbridledWrath = new Talent() { name = "Unbridled Wrath", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12322, 12999, 13000, 13001, 13002 }, currentSpellId = 12322 };
            ImprovedCleave = new Talent() { name = "Improved Cleave", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 12329, 12950, 20496 }, currentSpellId = 20496, rank = 3 };
            PiercingHowl = new Talent() { name = "Piercing Howl", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 12323 }, currentSpellId = 12323 };
            BloodCraze = new Talent() { name = "Blood Craze", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 16487, 16489, 16492 }, currentSpellId = 16487 };
            CommandingPresence = new Talent() { name = "Commanding Presence", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12318, 12857, 12858, 12860, 12861 }, currentSpellId = 12857, rank = 2 };
            DualWieldSpecialization = new Talent() { name = "Dual Wield Specialization", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 23584, 23585, 23586, 23587, 23588 }, currentSpellId = 23588, rank = 5 };
            ImprovedExecute = new Talent() { name = "Improved Execute", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 20502, 20503 }, currentSpellId = 20502 };
            Enrage = new Talent() { name = "Enrage", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12317, 13045, 13046, 13047, 13048 }, currentSpellId = 13046, rank = 3 };
            Precision = new Talent() { name = "Precision", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 29590, 29591, 29592 }, currentSpellId = 29592, rank = 3 };
            DeathWish = new Talent() { name = "Death Wish", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 12292 }, currentSpellId = 12292, rank = 1 };
            ImprovedIntercept = new Talent() { name = "Improved Intercept", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 29888, 29889 }, currentSpellId = 29888 };
            ImprovedBerserkerRage = new Talent() { name = "Improved BerserkerRage", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 20500, 20501 }, currentSpellId = 20500 };
            Flurry = new Talent() { name = "Flurry", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 12319, 12971, 12972, 12973, 12974 }, currentSpellId = 12974, rank = 5 };
            IntensifyRage = new Talent() { name = "Intensify Rage", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 46908, 46909, 56924 }, currentSpellId = 56924, rank = 3 };
            Bloodthirst = new Talent() { name = "Bloodthirst", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 23881 }, currentSpellId = 23881, rank = 1 };
            ImprovedWhirlwind = new Talent() { name = "Improved Whirlwind", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 29721, 29776 }, currentSpellId = 29776, rank = 2 };
            FuriousAttacks = new Talent() { name = "Furious Attacks", talentTree = TalentTree.Fury, maxRank = 2, spellIds = new int[] { 46910, 46911 }, currentSpellId = 46910 };
            ImprovedBerserkerStance = new Talent() { name = "Improved Berserker Stance", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 29759, 29760, 29761, 29762, 29763 }, currentSpellId = 29763, rank = 5 };
            HeroicFury = new Talent() { name = "Heroic Fury", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 60970 }, currentSpellId = 60970 };
            Rampage = new Talent() { name = "Rampage", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 29801 }, currentSpellId = 29801, rank = 1 };
            Bloodsurge = new Talent() { name = "Bloodsurge", talentTree = TalentTree.Fury, maxRank = 3, spellIds = new int[] { 46913, 46914, 46915 }, currentSpellId = 46915, rank = 3 };
            UnendingFury = new Talent() { name = "Unending Fury", talentTree = TalentTree.Fury, maxRank = 5, spellIds = new int[] { 56927, 56929, 56930, 56931, 56932 }, currentSpellId = 56932, rank = 5 };
            TitansGrip = new Talent() { name = "Titan's Grip", talentTree = TalentTree.Fury, maxRank = 1, spellIds = new int[] { 46917 }, currentSpellId = 46917, rank = 1 };

            // Protection
            ImprovedBloodrage = new Talent() { name = "Improved Bloodrage", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 12301, 12818 }, currentSpellId = 12301 };
            ShieldSpecialization = new Talent() { name = "Shield Specialization", talentTree = TalentTree.Protection, maxRank = 5, spellIds = new int[] { 16253, 16298, 12724, 12725, 12727 }, currentSpellId = 16253 };
            ImprovedThunderClap = new Talent() { name = "Improved Thunder Clap", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 12287, 12665, 12666 }, currentSpellId = 12287 };
            Incite = new Talent() { name = "Incite", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 50685, 50686, 50687 }, currentSpellId = 50685 };
            Anticipation = new Talent() { name = "Anticipation", talentTree = TalentTree.Protection, maxRank = 5, spellIds = new int[] { 12297, 12750, 12751, 12752, 12753 }, currentSpellId = 12297 };
            LastStand = new Talent() { name = "LastStand", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 12975 }, currentSpellId = 12975 };
            ImprovedRevenge = new Talent() { name = "Improved Revenge", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 12797, 12799 }, currentSpellId = 12797 };
            ShieldMastery = new Talent() { name = "Shield Mastery", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 29598, 29599 }, currentSpellId = 29598 };
            Toughness = new Talent() { name = "Toughness", talentTree = TalentTree.Protection, maxRank = 5, spellIds = new int[] { 12299, 12761, 12762, 12763, 12764 }, currentSpellId = 12299 };
            ImprovedSpellReflection = new Talent() { name = "Improved Spell Reflection", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 59088, 59089 }, currentSpellId = 59088 };
            ImprovedDisarm = new Talent() { name = "Improved Disarm", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 12313, 12804 }, currentSpellId = 12313 };
            Puncture = new Talent() { name = "Puncture", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 12308, 12810, 12811 }, currentSpellId = 12308 };
            ImprovedDisciplines = new Talent() { name = "Improved Disciplines", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 12312, 12803 }, currentSpellId = 12312 };
            ConcussionBlow = new Talent() { name = "Concussion Blow", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 12809 }, currentSpellId = 12809 };
            GagOrder = new Talent() { name = "Gag Order", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 12311, 12958 }, currentSpellId = 12311 };
            OneHandedWeaponSpecialization = new Talent() { name = "One-Handed Weapon Specialization", talentTree = TalentTree.Protection, maxRank = 5, spellIds = new int[] { 16538, 16539, 16540, 16541, 16542 }, currentSpellId = 16538 };
            ImprovedDefensiveStance = new Talent() { name = "Improved Defensive Stance", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 29593, 29594 }, currentSpellId = 29593 };
            Vigilance = new Talent() { name = "Vigilance", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 50720 }, currentSpellId = 50720 };
            FocusedRage = new Talent() { name = "Focused Rage", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 29787, 29790, 29792 }, currentSpellId = 29787 };
            Vitality = new Talent() { name = "Vitality", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 29149, 29143, 29144 }, currentSpellId = 29149 };
            Safeguard = new Talent() { name = "Safeguard Rage", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 46945, 46949 }, currentSpellId = 46945 };
            Warbringer = new Talent() { name = "Warbringer", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 57499 }, currentSpellId = 57499 };
            Devastate = new Talent() { name = "Devastate", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 20243 }, currentSpellId = 20243 };
            CriticalBlock = new Talent() { name = "Critical Block", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 47294, 47295, 47296 }, currentSpellId = 47294 };
            SwordAndBoard = new Talent() { name = "Sword and Board", talentTree = TalentTree.Protection, maxRank = 3, spellIds = new int[] { 46951, 46952, 46953 }, currentSpellId = 46951 };
            DamageShield = new Talent() { name = "Damage Shield", talentTree = TalentTree.Protection, maxRank = 2, spellIds = new int[] { 58872, 58874 }, currentSpellId = 58872 };
            Shockwave = new Talent() { name = "Shockwave", talentTree = TalentTree.Protection, maxRank = 1, spellIds = new int[] { 46968 }, currentSpellId = 46968 };
        }
    }
}
