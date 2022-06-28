namespace Warrior.Results
{
    public class SimulationResults
    {
        public float dps { get; set; } = 0;
        public int numSteps { get; set; } = 0;
        public int numIterations { get; set; } = 0;
        public int combatDuration { get; set; } = 0;
        public float totalDamage { get; set; } = 0;
        public RageResults rageSummary { get; set; } = new RageResults();
        public DamageResults mainHand { get; set; } = new DamageResults();
        public DamageResults offHand { get; set; } = new DamageResults();
        public List<AuraResults> auraSummaries { get; set; } = new List<AuraResults>();
        public List<DotDamageResults> dotSummaries { get; set; } = new List<DotDamageResults>();
        public List<DamageResults> abilitySummaries { get; set; } = new List<DamageResults>();
        public List<RageResults> rageSummaries { get; set; } = new List<RageResults>();

        public void Populate(List<IterationResults> results)
		{
            foreach(var a in results)
            {
                foreach(var b in a.rageSummary.generated)
                {
                    rageSummary.generated[b.Key] = b.Value;
                }
            }
            foreach (var a in results)
            {
                foreach (var b in a.rageSummary.ticks)
                {
                    rageSummary.ticks[b.Key] = b.Value;
                }
            }


            mainHand.numCasts = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numCasts), 3);
            mainHand.numHit = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numHit), 3);
            mainHand.numCrit = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numCrit), 3);
            mainHand.numMiss = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numMiss), 3);
            mainHand.numGlancing = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numGlancing), 3);
            mainHand.numDodge = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.numDodge), 3);
            mainHand.totalDamage = (float)Math.Round(results.Select(r => r.mainHand).Average(x => x.totalDamage), 3);
            totalDamage = (float)Math.Round(mainHand.totalDamage, 3);

            offHand.numCasts = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numCasts), 3);
            offHand.numHit = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numHit), 3);
            offHand.numCrit = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numCrit), 3);
            offHand.numMiss = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numMiss), 3);
            offHand.numGlancing = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numGlancing), 3);
            offHand.numDodge = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.numDodge), 3);

            offHand.totalDamage = (float)Math.Round(results.Select(r => r.offHand).Average(x => x.totalDamage), 3);
            totalDamage += (float)Math.Round(offHand.totalDamage, 3);

            foreach (IterationResults a in results)
            {
                numSteps += a.numSteps;



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

            }
            
        }
    }
}
