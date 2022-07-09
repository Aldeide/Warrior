using System.Runtime.Serialization;

namespace Warrior.Results
{
    [Serializable]
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
        public StanceResults battleStanceResults { get; set; } = new StanceResults();
        public StanceResults berserkerStanceResults { get; set; } = new StanceResults();
        public StanceResults defensiveStanceResults { get; set; } = new StanceResults();

        public void Populate(List<IterationResults> results)
		{
            numIterations = results.Count;
            if (numIterations == 0) return;
            totalDamage = 0;
            combatDuration = results[0].combatLength;
            battleStanceResults.name = results.First().battleStanceResults.name;
            berserkerStanceResults.name = results.First().berserkerStanceResults.name;
            defensiveStanceResults.name = results.First().defensiveStanceResults.name;

            foreach (var a in results)
            {
                //numIterations += a.numIterations;

                battleStanceResults.uptime += a.battleStanceResults.uptime;
                berserkerStanceResults.uptime += a.berserkerStanceResults.uptime;
                defensiveStanceResults.uptime += a.defensiveStanceResults.uptime;

                foreach (var b in a.rageSummary.generated)
                {
                    rageSummary.generated[b.Key] = b.Value;
                }
                foreach (var b in a.rageSummary.ticks)
                {
                    rageSummary.ticks[b.Key] = b.Value;
                }
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
                    }
                    else
                    {
                        var s = (AuraResults)auraSummary.Clone();
                        auraSummaries.Add(s);
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
                    }
                    else
                    {
                        var d = (DotDamageResults)summary.Clone();
                        dotSummaries.Add(d);
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
                    }
                    else
                    {
                        var c = (DamageResults)abilitySummary.Clone();
                        abilitySummaries.Add(c);
                    }
                }

            }

            mainHand.numCasts = (float)Math.Round(mainHand.numCasts / numIterations, 2);
            mainHand.numHit = (float)Math.Round(mainHand.numHit / numIterations, 2);
            mainHand.numCrit = (float)Math.Round(mainHand.numCrit / numIterations, 2);
            mainHand.numMiss = (float)Math.Round(mainHand.numMiss / numIterations, 2);
            mainHand.numGlancing = (float)Math.Round(mainHand.numGlancing / numIterations, 2);
            mainHand.numDodge = (float)Math.Round(mainHand.numDodge / numIterations, 2);
            mainHand.totalDamage = (float)Math.Round(mainHand.totalDamage / numIterations, 2);
            totalDamage = (float)Math.Round(totalDamage / numIterations, 2);

            offHand.numCasts = (float)Math.Round(offHand.numCasts / numIterations, 2);
            offHand.numHit = (float)Math.Round(offHand.numHit / numIterations, 2);
            offHand.numCrit = (float)Math.Round(offHand.numCrit / numIterations, 2);
            offHand.numMiss = (float)Math.Round(offHand.numMiss / numIterations, 2);
            offHand.numGlancing = (float)Math.Round(offHand.numGlancing / numIterations, 2);
            offHand.numDodge = (float)Math.Round(offHand.numDodge / numIterations, 2);
            offHand.totalDamage = (float)Math.Round(offHand.totalDamage / numIterations, 2);

            foreach (AuraResults auraSummary in auraSummaries)
            {
                auraSummary.uptime = (int)Math.Round((float)auraSummary.uptime / numIterations, 2);
                auraSummary.totalDamage = (int)Math.Round((float)auraSummary.totalDamage / numIterations, 2);
                totalDamage += auraSummary.totalDamage;
            }
            foreach (DotDamageResults summary in dotSummaries)
            {
                summary.totalDamage /= numIterations;
                summary.ticks /= numIterations;
                summary.applications /= numIterations;
                summary.refreshes /= numIterations;
                summary.uptime /= numIterations;
                totalDamage += summary.totalDamage;
            }
            foreach (DamageResults abilitySummary in abilitySummaries)
            {
                abilitySummary.numCasts = (float)Math.Round(abilitySummary.numCasts / numIterations, 2);
                abilitySummary.numHit = (float)Math.Round(abilitySummary.numHit / numIterations, 2);
                abilitySummary.numCrit = (float)Math.Round(abilitySummary.numCrit / numIterations, 2);
                abilitySummary.numMiss = (float)Math.Round(abilitySummary.numMiss / numIterations, 2);
                abilitySummary.numDodge = (float)Math.Round(abilitySummary.numDodge / numIterations, 2);
                abilitySummary.totalDamage = (float)Math.Round(abilitySummary.totalDamage / numIterations, 2);
                abilitySummary.hitDamage = (float)Math.Round(abilitySummary.hitDamage / numIterations, 2);
                abilitySummary.critDamage = (float)Math.Round(abilitySummary.critDamage / numIterations, 2);
                totalDamage += abilitySummary.totalDamage;
            }
            battleStanceResults.uptime /= numIterations;
            berserkerStanceResults.uptime /= numIterations;
            defensiveStanceResults.uptime /= numIterations;
        }

        public void Populate(List<SimulationResults> results)
        {
            numIterations = results.Count;
            totalDamage = 0;
            combatDuration = results[0].combatDuration;
            battleStanceResults.name = results.First().battleStanceResults.name;
            berserkerStanceResults.name = results.First().berserkerStanceResults.name;
            defensiveStanceResults.name = results.First().defensiveStanceResults.name;

            foreach (var a in results)
            {
                //numIterations += a.numIterations;

                battleStanceResults.uptime += a.battleStanceResults.uptime;
                berserkerStanceResults.uptime += a.berserkerStanceResults.uptime;
                defensiveStanceResults.uptime += a.defensiveStanceResults.uptime;

                foreach (var b in a.rageSummary.generated)
                {
                    rageSummary.generated[b.Key] = b.Value;
                }
                foreach (var b in a.rageSummary.ticks)
                {
                    rageSummary.ticks[b.Key] = b.Value;
                }
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
                    }
                    else
                    {
                        var s = (AuraResults)auraSummary.Clone();
                        auraSummaries.Add(s);
                    }
                }

                foreach (DotDamageResults summary in a.dotSummaries)
                {
                    if (dotSummaries.Find(s => s.name == summary.name) is var s && s != null)
                    {
                        s.uptime += summary.uptime;
                        s.ticks += summary.ticks;
                        s.applications += summary.applications;
                        s.refreshes += summary.refreshes;
                        s.totalDamage += summary.totalDamage;
                    }
                    else
                    {
                        var d = (DotDamageResults)summary.Clone();
                        dotSummaries.Add(d);
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
                    }
                    else
                    {
                        var c = (DamageResults)abilitySummary.Clone();
                        abilitySummaries.Add(c);
                    }
                }

            }

            mainHand.numCasts = (float)Math.Round(mainHand.numCasts / numIterations, 2);
            mainHand.numHit = (float)Math.Round(mainHand.numHit / numIterations, 2);
            mainHand.numCrit = (float)Math.Round(mainHand.numCrit / numIterations, 2);
            mainHand.numMiss = (float)Math.Round(mainHand.numMiss / numIterations, 2);
            mainHand.numGlancing = (float)Math.Round(mainHand.numGlancing / numIterations, 2);
            mainHand.numDodge = (float)Math.Round(mainHand.numDodge / numIterations, 2);
            mainHand.totalDamage = (float)Math.Round(mainHand.totalDamage / numIterations, 2);
            totalDamage = (float)Math.Round(totalDamage / numIterations, 2);

            offHand.numCasts = (float)Math.Round(offHand.numCasts / numIterations, 2);
            offHand.numHit = (float)Math.Round(offHand.numHit / numIterations, 2);
            offHand.numCrit = (float)Math.Round(offHand.numCrit / numIterations, 2);
            offHand.numMiss = (float)Math.Round(offHand.numMiss / numIterations, 2);
            offHand.numGlancing = (float)Math.Round(offHand.numGlancing / numIterations, 2);
            offHand.numDodge = (float)Math.Round(offHand.numDodge / numIterations, 2);
            offHand.totalDamage = (float)Math.Round(offHand.totalDamage / numIterations, 2);

            foreach (AuraResults auraSummary in auraSummaries)
            {
                auraSummary.uptime = (int)Math.Round((float)auraSummary.uptime / numIterations, 2);
                auraSummary.totalDamage = (int)Math.Round((float)auraSummary.totalDamage / numIterations, 2);
                totalDamage += auraSummary.totalDamage;
            }
            foreach (DotDamageResults summary in dotSummaries)
            {
                summary.totalDamage /= numIterations;
                summary.ticks /= numIterations;
                summary.applications /= numIterations;
                summary.refreshes /= numIterations;
                summary.uptime /= numIterations;
                totalDamage += summary.totalDamage;
            }
            foreach (DamageResults abilitySummary in abilitySummaries)
            {
                abilitySummary.numCasts = (float)Math.Round(abilitySummary.numCasts / numIterations, 2);
                abilitySummary.numHit = (float)Math.Round(abilitySummary.numHit / numIterations, 2);
                abilitySummary.numCrit = (float)Math.Round(abilitySummary.numCrit / numIterations, 2);
                abilitySummary.numMiss = (float)Math.Round(abilitySummary.numMiss / numIterations, 2);
                abilitySummary.numDodge = (float)Math.Round(abilitySummary.numDodge / numIterations, 2);
                abilitySummary.totalDamage = (float)Math.Round(abilitySummary.totalDamage / numIterations, 2);
                abilitySummary.hitDamage = (float)Math.Round(abilitySummary.hitDamage / numIterations, 2);
                abilitySummary.critDamage = (float)Math.Round(abilitySummary.critDamage / numIterations, 2);
                totalDamage += abilitySummary.totalDamage;
            }
            battleStanceResults.uptime /= numIterations;
            berserkerStanceResults.uptime /= numIterations;
            defensiveStanceResults.uptime /= numIterations;

        }

    }
}
