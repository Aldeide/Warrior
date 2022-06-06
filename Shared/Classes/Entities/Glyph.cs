namespace Warrior.Entities
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
}
