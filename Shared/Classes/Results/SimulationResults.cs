namespace Warrior.Results
{
    public class SimulationResults
    {
        public float dps { get; set; } = 0;
        public int numSteps { get; set; } = 0;
        public int numIterations { get; set; } = 0;
        public int combatDuration { get; set; } = 0;
        public int totalDamage { get; set; } = 0;
        public RageResults rageSummary { get; set; } = new RageResults();
        public DamageResults mainHand { get; set; } = new DamageResults();
        public DamageResults offHand { get; set; } = new DamageResults();
        public List<AuraResults> auraSummaries { get; set; } = new List<AuraResults>();
        public List<DotDamageResults> dotSummaries { get; set; } = new List<DotDamageResults>();
        public List<DamageResults> abilitySummaries { get; set; } = new List<DamageResults>();

        public void Populate(List<IterationResults> results)
		{
            foreach (IterationResults a in results)
            {
                numSteps += a.numSteps;
                mainHand.numCasts += a.mainHand.numCasts;
                mainHand.numHit += a.mainHand.numHit;
                mainHand.numCrit += a.mainHand.numCrit;
                mainHand.numMiss += a.mainHand.numMiss;
                mainHand.numGlancing += a.mainHand.numGlancing;
                mainHand.numDodge += a.mainHand.numDodge;
                mainHand.totalDamage += a.mainHand.totalDamage;
                totalDamage += a.mainHand.totalDamage;

                offHand.numCasts += a.offHand.numCasts;
                offHand.numHit += a.offHand.numHit;
                offHand.numCrit += a.offHand.numCrit;
                offHand.numMiss += a.offHand.numMiss;
                offHand.numGlancing += a.offHand.numGlancing;
                offHand.numDodge += a.offHand.numDodge;

                offHand.totalDamage += a.offHand.totalDamage;
                totalDamage += a.offHand.totalDamage;

                foreach (AuraResults auraSummary in a.auraSummaries)
                {

                    if (auraSummaries.Find(a => a.name == auraSummary.name) is var summary && summary != null)
                    {
                        summary.uptime += auraSummary.uptime;
                        summary.totalDamage += auraSummary.totalDamage;
                        totalDamage += auraSummary.totalDamage;
                    }
                    else
                    {
                        var s = (AuraResults)auraSummary.Clone();
                        auraSummaries.Add(s);
                        totalDamage += s.totalDamage;
                    }
                }

                foreach (DotDamageResults summary in a.dotDamageSummaries)
                {
                    if (dotSummaries.Find(s => s.name == summary.name) is var s && s != null)
                    {
                        s.uptime += summary.uptime;
                        s.ticks += summary.ticks;
                        s.applications += summary.applications;
                        s.refreshes += summary.refreshes;
                        s.totalDamage += summary.totalDamage;
                        totalDamage += summary.totalDamage;
                    }
                    else
                    {
                        var d = (DotDamageResults)summary.Clone();
                        dotSummaries.Add(d);
                        totalDamage += d.totalDamage;
                    }
                }

                foreach (DamageResults abilitySummary in a.abilitySummaries)
                {
                    // Temporary fix.
                    if (abilitySummary.name == "")
                    {
                        continue;
                    }
                    if (abilitySummaries.Find(a => a.name == abilitySummary.name) is var summary && summary != null)
                    {
                        summary.numCasts += abilitySummary.numCasts;
                        summary.numHit += abilitySummary.numHit;
                        summary.numCrit += abilitySummary.numCrit;
                        summary.numMiss += abilitySummary.numMiss;
                        summary.numDodge += abilitySummary.numDodge;
                        summary.totalDamage += abilitySummary.totalDamage;
                        summary.hitDamage += abilitySummary.hitDamage;
                        summary.critDamage += abilitySummary.critDamage;

                        totalDamage += abilitySummary.totalDamage;
                    }
                    else
                    {
                        var c = (DamageResults)abilitySummary.Clone();
                        abilitySummaries.Add(c);
                        totalDamage += c.totalDamage;
                    }
                }

                rageSummary.rageGenerated += a.rageSummary.rageGenerated;
                rageSummary.wastedRage += a.rageSummary.wastedRage;

            }
            
        }
    }
}
