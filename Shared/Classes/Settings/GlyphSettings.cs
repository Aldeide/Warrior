using Warrior.Entities;

namespace Warrior.Settings
{

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
            return majorOne == 43421 || majorTwo == 43421 || majorThree == 43421;
        }

        public bool HasGlyphOfExecution()
        {
            return majorOne == 43421 || majorTwo == 43421 || majorThree == 43421;
        }

        public bool HasGlyphOfRending()
        {
            return majorOne == 43423 || majorTwo == 43423 || majorThree == 43423;
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

            { new Glyph(45790, "Glyph of Bladestorm", GlyphType.Major) },
            { new Glyph(45792, "Glyph of Shockwave", GlyphType.Major) },
            { new Glyph(43421, "Glyph of Mortal Strike", GlyphType.Major) },
            { new Glyph(43423, "Glyph of Rending", GlyphType.Major) }
        };
    }
}
