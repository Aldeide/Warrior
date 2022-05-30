﻿namespace Warrior
{
    public enum GlyphType
    {
        Major,
        Minor
    }

    public class Glyph
    {
        public GlyphType glyphType { get; set; }
        public string name { get; set; }
        public int id { get; set; }

        public Glyph(int id, string name, GlyphType glyphType)
        {
            this.id = id;
            this.name = name;
            this.glyphType = glyphType;
        }
    }

    public class GlyphManager
    {
        public List<Glyph> glyphs { get; set; }
        public int majorOne { get; set; }
        public int majorTwo { get; set; }
        public int majorThree { get; set; }

        public GlyphManager()
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
    }

    public static class GlyphDatabase
    {
        public static List<Glyph> glyphs = new List<Glyph>()
        {
            { new Glyph(58403, "Glyph of Heroic Strike", GlyphType.Major) },
            { new Glyph(58390, "Glyph of Whirlwind", GlyphType.Major) },
            { new Glyph(58394, "Glyph of Sweeping Strikes", GlyphType.Major) },
            { new Glyph(58407, "Glyph of Cleaving", GlyphType.Major) },
            { new Glyph(58405, "Glyph of Execution", GlyphType.Major) }
        };
    }
}
