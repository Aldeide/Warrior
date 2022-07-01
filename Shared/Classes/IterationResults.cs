namespace Warrior
{
    [Serializable]
    public class IterationResults
    {
        public int numSteps { get; set; } = 0;
        public int combatLength { get; set; } = 0;
        public RageResults rageSummary { get; set; } = new RageResults();
        public DamageResults mainHand { get; set; } = new DamageResults();
        public DamageResults offHand { get; set; } = new DamageResults();
        public List<AuraResults> auraSummaries { get; set; } = new List<AuraResults>();
        public List<DamageResults> abilitySummaries { get; set; } = new List<DamageResults>();
        public List<DotDamageResults> dotDamageSummaries { get; set; } = new List<DotDamageResults>();
        public StanceResults battleStanceResults { get; set; } = new StanceResults();
        public StanceResults berserkerStanceResults { get; set; } = new StanceResults();
        public StanceResults defensiveStanceResults { get; set; } = new StanceResults();

        public int Damage()
        {
            float damage = 0;
            damage += mainHand.totalDamage;
            damage += offHand.totalDamage;



            return (int)damage;

        }

    }
}
