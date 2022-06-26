using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using Warrior.Entities;
using Warrior.Results;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Warrior
{

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
        public Settings.Settings settings { get; set; }
        public ComputedConstants computedConstants {get;set;}
        public SimulationResults simulationResults { get; set; } = new SimulationResults();

        public Simulation()
        {
            this.settings = new Settings.Settings();
            this.simulationResults = new SimulationResults();
            this.computedConstants = new ComputedConstants();
        }
        public void Simulate()
        {
            Setup();
            simulationResults = new SimulationResults();
            
            ConcurrentBag<IterationResults> iterationsResults = new ConcurrentBag<IterationResults>();

            Parallel.For(0, settings.simulationSettings.numIterations, index =>
			{
                Iteration iteration = new Iteration(settings, computedConstants);
                iteration.Setup();
                iterationsResults.Add(iteration.Iterate());
            });
            simulationResults.Populate(iterationsResults.ToList());
            simulationResults.combatDuration = settings.simulationSettings.combatLength;
            simulationResults.numIterations = settings.simulationSettings.numIterations;
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
            
            computedConstants.bleedDamageMultiplier = settings.debuffSettings.GetMultiplicativeStat(Stat.BleedDamage);
            computedConstants.deepWoundsDamageMultiplier = TalentUtils.GetDeepWoundsMultiplier(settings.talentSettings) * computedConstants.bleedDamageMultiplier;
            computedConstants.meleeDamageMultiplier =
                TalentUtils.GetTwoHandedWeaponSpecializationDamageMultiplier(settings.talentSettings)
                * TalentUtils.GetTitansDamageReductionMultiplier(settings.talentSettings, settings.equipmentSettings);
            computedConstants.criticalDamageMultiplier = 1.0f + (1.0f + settings.talentSettings.Impale.rank * 0.1f);
            computedConstants.titansGripDamageMultiplier = TalentUtils.GetTitansDamageReductionMultiplier(settings.talentSettings, settings.equipmentSettings);
            computedConstants.offHandDamageMultiplier = 0.5f * (1 + settings.talentSettings.DualWieldSpecialization.rank * 0.05f);
            computedConstants.unendingFuryDamageMultiplier = TalentUtils.GetUnendingFuryDamageMultiplier(settings.talentSettings);
            computedConstants.HasAngerManagement = settings.talentSettings.AngerManagement.rank > 0;
            computedConstants.hasBloodsurge = settings.talentSettings.Bloodsurge.rank > 0;
            computedConstants.hasBloodthirst = settings.talentSettings.Bloodthirst.rank > 0;
            computedConstants.bloodsurgeChance = TalentUtils.GetBloodSurgeChance(settings.talentSettings);
            computedConstants.focusedRageRageReduction = settings.talentSettings.FocusedRage.rank;
            computedConstants.slamDamageMultiplier = computedConstants.titansGripDamageMultiplier * computedConstants.meleeDamageMultiplier * computedConstants.unendingFuryDamageMultiplier;
            computedConstants.hasMHBerserking = settings.enchantSettings.GetEnchant(ItemSlot.MainHand).id == 59621;
            // TODO: Check if OH is equipped.
            computedConstants.hasOHBerserking = settings.enchantSettings.GetEnchant(ItemSlot.OffHand).id == 59621;
        }
    }
}
