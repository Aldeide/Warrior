using Warrior.Entities;

namespace Warrior.Settings
{
    [Serializable]
    public class GlyphSettings
    {
        public List<Glyph> glyphs { get; set; }
        public int majorOne { get; set; }
        public int majorTwo { get; set; }
        public int majorThree { get; set; }

        public GlyphSettings()
        {
            majorOne = 58403;
            majorTwo = 58390;
            majorThree = 58405;
            glyphs = new List<Glyph>();
        }

        public bool HasGlyphOfWhirlwind()
        {
            return majorOne == 58390 || majorTwo == 58390 || majorThree == 58390;
        }

        public bool HasGlyphOfHeroicStrike()
        {
            return majorOne == 58403 || majorTwo == 58403 || majorThree == 58403;
        }

        public bool HasGlyphOfMortalStrike()
        {
            return majorOne == 57160 || majorTwo == 57160 || majorThree == 57160;
        }

        public bool HasGlyphOfExecution()
        {
            return majorOne == 58405 || majorTwo == 58405 || majorThree == 58405;
        }

        public bool HasGlyphOfRending()
        {
            return majorOne == 57163 || majorTwo == 57163 || majorThree == 57163;
        }
    }

    public static class GlyphDatabase
    {
        public static List<Glyph> glyphs = new List<Glyph>()
        {
            { new Glyph(58403, "Glyph of Heroic Strike", GlyphType.Major) },
            { new Glyph(58390, "Glyph of Whirlwind", GlyphType.Major) },
            { new Glyph(58394, "Glyph of Sweeping Strikes", GlyphType.Major) },
            { new Glyph(58407, "Glyph of Cleaving", GlyphType.Major) },
            { new Glyph(58405, "Glyph of Execution", GlyphType.Major) },

            { new Glyph(64295, "Glyph of Bladestorm", GlyphType.Major) },
            { new Glyph(64296, "Glyph of Shockwave", GlyphType.Major) },
            { new Glyph(57160, "Glyph of Mortal Strike", GlyphType.Major) },
            { new Glyph(57163, "Glyph of Rending", GlyphType.Major) }
        };
    }
}
