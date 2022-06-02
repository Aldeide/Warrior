using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;

namespace Warrior
{
    public class Results
    {
        public float dps { get; set; }
        public WeaponResults mainHand { get; set; }
        public WeaponResults offHand { get; set; }

        public Results()
        {
            mainHand = new WeaponResults();
            offHand = new WeaponResults();
        }
    }

    public class SimulationResults
    {
        public float dps { get; set; } = 0;
        public int numSteps { get; set; } = 0;
        public int numIterations { get; set; } = 0;
        public int combatDuration { get; set; } = 0;
        public int totalDamage { get; set; } = 0;    
        public RageSummary rageSummary { get; set; } = new RageSummary();
        public DamageSummary mainHand { get; set; } = new DamageSummary();
        public DamageSummary offHand { get; set; } = new DamageSummary();
        public List<AuraSummary> auraSummaries { get; set; } = new List<AuraSummary>();
        public List<DotDamageSummary> dotSummaries { get; set; } = new List<DotDamageSummary>();

        public List<DamageSummary> abilitySummaries { get; set; } = new List<DamageSummary>();
    }

    public class WeaponResults
    {
        public int hitCount { get; set; }
        public int missesCount { get; set; }
        public int glancesCount { get; set; }
        public int critCount { get; set; }
        public int dodgeCount { get; set; }

        public int normalHitDamage { get; set; }
        public int minNormalHitDamage { get; set; }
        public int maxNormalHitDamage { get; set; }

        public int glancingDamage { get; set; }
        public int minGlancingDamage { get; set; }
        public int maxGlancingDamage { get; set; }

        public int critDamage { get; set; }
        public int minCritDamage { get; set; }
        public int maxCritDamage { get; set; }
    }

    public class Simulation : IDisposable
    {
        public Character character { get; private set; }
        public Settings settings { get; set; }
        public ItemDatabase itemDatabase { get; set; }
        public ComputedConstants computedConstants {get;set;}
        public SimulationResults simulationResults { get; set; } = new SimulationResults();
        Random random = new Random();
        public Simulation()
        {
            this.itemDatabase = new ItemDatabase();
            this.settings = new Settings();
            this.character = new Character(this);
            this.simulationResults = new SimulationResults();
        }
        public Simulation(List<Item> items)
        {
            this.itemDatabase = new ItemDatabase();
            this.itemDatabase.items = items;
            this.settings = new Settings();
            this.character = new Character(this);
            this.simulationResults = new SimulationResults();
        }
        public void Simulate()
        {
            Setup();
            simulationResults = new SimulationResults();
            Iteration iteration = new Iteration(this);
            List<IterationResults> iterationsResults = new List<IterationResults>();

            for (int i = 0; i < settings.iterations; i++)
            {
                iteration.Setup();
                iterationsResults.Add(iteration.Iterate());
            }

            foreach(IterationResults a in iterationsResults)
            {
                simulationResults.numSteps += a.numSteps;


                simulationResults.mainHand.numCasts += a.mainHand.numCasts;
                simulationResults.mainHand.numHit += a.mainHand.numHit;
                simulationResults.mainHand.numCrit += a.mainHand.numCrit;
                simulationResults.mainHand.numMiss += a.mainHand.numMiss;
                simulationResults.mainHand.numGlancing += a.mainHand.numGlancing;
                simulationResults.mainHand.numDodge += a.mainHand.numDodge;

                simulationResults.mainHand.totalDamage += a.mainHand.totalDamage;
                simulationResults.totalDamage += a.mainHand.totalDamage;

                simulationResults.offHand.numCasts += a.offHand.numCasts;
                simulationResults.offHand.numHit += a.offHand.numHit;
                simulationResults.offHand.numCrit += a.offHand.numCrit;
                simulationResults.offHand.numMiss += a.offHand.numMiss;
                simulationResults.offHand.numGlancing += a.offHand.numGlancing;
                simulationResults.offHand.numDodge += a.offHand.numDodge;

                simulationResults.offHand.totalDamage += a.offHand.totalDamage;
                simulationResults.totalDamage += a.offHand.totalDamage;

                foreach(AuraSummary auraSummary in a.auraSummaries)
                {

                    if (simulationResults.auraSummaries.Find(a => a.name == auraSummary.name) is var summary && summary != null)
                    {
                        summary.uptime += auraSummary.uptime;
                        summary.totalDamage += auraSummary.totalDamage;
                        simulationResults.totalDamage += auraSummary.totalDamage;
                    } else
                    {
                        simulationResults.auraSummaries.Add((AuraSummary)auraSummary.Clone());
                    }
                }

                foreach (DotDamageSummary summary in a.dotDamageSummaries)
                {
                    if (simulationResults.dotSummaries.Find(s => s.name == summary.name) is var s && s != null)
                    {
                        s.uptime += summary.uptime;
                        s.ticks += summary.ticks;
                        s.applications += summary.applications;
                        s.refreshes += summary.refreshes;
                        s.totalDamage += summary.totalDamage;
                        simulationResults.totalDamage += summary.totalDamage;
                    }
                    else
                    {
                        simulationResults.dotSummaries.Add((DotDamageSummary)summary.Clone());
                    }
                }

                foreach (DamageSummary abilitySummary in a.abilitySummaries)
                {
                    // Temporary fix.
                    if (abilitySummary.name == "")
                    {
                        continue;
                    }
                    if (simulationResults.abilitySummaries.Find(a => a.name == abilitySummary.name) is var summary && summary != null)
                    {
                        summary.numCasts += abilitySummary.numCasts;
                        summary.numHit += abilitySummary.numHit;
                        summary.numCrit += abilitySummary.numCrit;
                        summary.numMiss += abilitySummary.numMiss;
                        summary.numDodge += abilitySummary.numDodge;
                        summary.totalDamage += abilitySummary.totalDamage;
                        summary.hitDamage += abilitySummary.hitDamage;
                        summary.critDamage += abilitySummary.critDamage;

                        simulationResults.totalDamage += abilitySummary.totalDamage;
                    }
                    else
                    {
                        simulationResults.abilitySummaries.Add((DamageSummary)abilitySummary.Clone());
                    }
                }

                simulationResults.rageSummary.rageGenerated += a.rageSummary.rageGenerated;
                simulationResults.rageSummary.wastedRage += a.rageSummary.wastedRage;

            }
            simulationResults.combatDuration = settings.combatLength;
            simulationResults.numIterations = settings.iterations;
            simulationResults.dps = simulationResults.totalDamage / simulationResults.combatDuration;
            return;
        }
        public void Dispose()
        {
            Console.WriteLine("DataService disposed!");
        }

        public void Setup()
        {
            // Computing what can be computed to avoid doing it for each iteration.
            // Damage.
            computedConstants = new ComputedConstants();
            computedConstants.bleedDamageMultiplier = character.debuffSettings.GetMultiplicativeStat(EffectStat.BleedDamage);
            computedConstants.deepWoundsDamageMultiplier = TalentUtils.GetDeepWoundsMultiplier(character.talents) * computedConstants.bleedDamageMultiplier;
            computedConstants.meleeDamageMultiplier = TalentUtils.GetTwoHandedWeaponSpecializationDamageMultiplier(character.talents);
            computedConstants.criticalDamageMultiplier = 1.0f + (1.0f + character.talents.Impale.rank * 0.1f);
            computedConstants.titansGripDamageMultiplier = TalentUtils.GetTitansDamageReductionMultiplier(character.talents, character.equipment);
            computedConstants.offHandDamageMultiplier = 0.5f * (1 + character.talents.DualWieldSpecialization.rank * 0.05f);

            computedConstants.HasAngerManagement = character.talents.AngerManagement.rank > 0;
            computedConstants.hasBloodsurge = character.talents.Bloodsurge.rank > 0;
            computedConstants.hasBloodthirst = character.talents.Bloodthirst.rank > 0;
            computedConstants.bloodsurgeChance = TalentUtils.GetBloodSurgeChance(character.talents);
            computedConstants.focusedRageRageReduction = character.talents.FocusedRage.rank;
        }

        public float ComputeDodgeChance()
        {
            if (settings.targetLevel == 83)
            {
                return 6.5f;
            }
            if (settings.targetLevel == 82)
            {
                return 5.4f;
            }
            if (settings.targetLevel == 81)
            {
                return 5.2f;
            }
            return 5;
        }
        public float ComputeMissChance()
        {
            float missChance = 24;
            if (settings.targetLevel == 83)
            {
                missChance = 27;
            }
            if (settings.targetLevel == 82)
            {
                missChance = 25;
            }
            if (settings.targetLevel == 81)
            {
                missChance = 24.5f;
            }
            return missChance - character.GetMeleeHitChance();
        }
    }
}
