namespace Warrior
{
    public enum ProcType
    {
        ChanceOnMeleeHit,
        ChanceOnMeleeCriticalHit,
        ChanceOnAbility,
        ChanceOnAbilityCriticalHit,
        Use
    }

    public class Proc
    {
        public int id { get; set; } = 0;
        public string name { get; set; } = "";
        public ProcType procType { get; set; } = ProcType.ChanceOnMeleeHit;
        public int icd { get; set; } = 0;
        public int ppm { get; set; } = 0;
        public int cooldown { get; set; } = 0;
        public int duration { get; set; } = 0;
        public List<Effect> effects { get; set; } = new List<Effect>();
    }
}
