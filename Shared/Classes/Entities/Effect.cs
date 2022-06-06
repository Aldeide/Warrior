namespace Warrior.Entities
{
    public enum EffectType
    {
        Additive,
        Multiplicative
    }

    public class Effect
    {
        public EffectType type { get; set; }
        public Stat stat { get; set; }
        public float value { get; set; }
        public Effect(EffectType type, Stat stat, float value)
        {
            this.type = type;
            this.stat = stat;
            this.value = value;
        }
    }
}
