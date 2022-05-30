namespace Warrior
{
    public class IterationResults
    {
        public int numSteps { get; set; } = 0;
        public int combatLength { get; set; } = 0;
        public RageSummary rageSummary { get; set; } = new RageSummary();
        public DamageSummary mainHand { get; set; } = new DamageSummary();
        public DamageSummary offHand { get; set; } = new DamageSummary();
        public List<AuraSummary> auraSummaries { get; set; } = new List<AuraSummary>();
        public List<DamageSummary> abilitySummaries { get; set; } = new List<DamageSummary>();
        public List<DotDamageSummary> dotDamageSummaries { get; set; } = new List<DotDamageSummary>();

    }
}
