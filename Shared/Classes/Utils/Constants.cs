namespace Warrior
{
    public static class Constants
    {
        public static int kStepsPerSecond = 1000;
        public static float kCritRatingPerPercent = 45.9f;
        public static float kExpertisePerPoint = 8.197f;
        public static float kHitRatingPerPercent = 32.79f;
        public static float kAgilityPerCritPercent = 36.35f;
        public static float kHastePerPercent = 32.79f; // Has changed during wotlk with 3.1 was 32.79 before.
        public static float kArmorPenetrationPerPercent = 13.99f;
        public static Dictionary<string, Dictionary<Settings.Race, int>> bonusStatsPerRace = new Dictionary<string, Dictionary<Settings.Race, int>>()
        {
            { "Strength", new Dictionary<Settings.Race, int>() { {Settings.Race.Human, 0}, {Settings.Race.Dwarf, 5}, {Settings.Race.NightElf, -4}, {Settings.Race.Orc, 3}, {Settings.Race.Tauren, 5}, {Settings.Race.Undead, -1},{Settings.Race.Gnome, -5}, {Settings.Race.Troll, 1}, {Settings.Race.BloodElf, -3}, {Settings.Race.Draenei, 1} } },
            { "Agility", new Dictionary<Settings.Race, int>() { {Settings.Race.Human, 0}, {Settings.Race.Dwarf, -4}, {Settings.Race.NightElf, 4}, {Settings.Race.Orc, -3}, {Settings.Race.Tauren, -4}, {Settings.Race.Undead, -2},{Settings.Race.Gnome, 2}, {Settings.Race.Troll, 2}, {Settings.Race.BloodElf, 2}, {Settings.Race.Draenei, -3} } },
            { "Stamina", new Dictionary<Settings.Race, int>() { {Settings.Race.Human, 0}, {Settings.Race.Dwarf, 1}, {Settings.Race.NightElf, 0}, {Settings.Race.Orc, 1}, {Settings.Race.Tauren, 1}, {Settings.Race.Undead, 0},{Settings.Race.Gnome, 0}, {Settings.Race.Troll, 0}, {Settings.Race.BloodElf, 0}, {Settings.Race.Draenei, 0} } },
            { "Intellect", new Dictionary<Settings.Race, int>() { {Settings.Race.Human, 0}, {Settings.Race.Dwarf, -1}, {Settings.Race.NightElf, 0}, {Settings.Race.Orc, -3}, {Settings.Race.Tauren, -4}, {Settings.Race.Undead, -2},{Settings.Race.Gnome, 3}, {Settings.Race.Troll, -4}, {Settings.Race.BloodElf, 3}, {Settings.Race.Draenei, 0} } },
            { "Spirit", new Dictionary<Settings.Race, int>() { {Settings.Race.Human, 0}, {Settings.Race.Dwarf, -1}, {Settings.Race.NightElf, 0}, {Settings.Race.Orc, 2}, {Settings.Race.Tauren, 2}, {Settings.Race.Undead, 5},{Settings.Race.Gnome, 0}, {Settings.Race.Troll, 1}, {Settings.Race.BloodElf, -2}, {Settings.Race.Draenei, 2} } }
        };
    }
}
