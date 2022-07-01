using Microsoft.AspNetCore.Components.Forms;
using AutoMapper;
using Warrior.Entities;
using Warrior.Results;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.Serialization;

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

    [KnownType(typeof(Settings.Settings))]
    public class Simulation : IDisposable
    {
        public Settings.Settings settings { get; set; }
        public ComputedConstants computedConstants {get;set;}
        public SimulationResults simulationResults { get; set; } = new SimulationResults();

        public float damage { get; set; } = 0;
        public int numResults { get; set; } = 0;

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
            
            List<IterationResults> iterationsResults = new List<IterationResults>();

            for (int i = 0; i < settings.simulationSettings.numIterations; i++)
            {
                Iteration iteration = new Iteration(settings, computedConstants, i);
                iteration.Setup();
                IterationResults results = iteration.Iterate();
                if (i % 10 == 0)
                {
                    damage += results.Damage();
                    numResults++;
                }
                iterationsResults.Add(results);
            }
            simulationResults.Populate(iterationsResults.ToList());
            simulationResults.combatDuration = settings.simulationSettings.combatLength;
            simulationResults.numIterations = settings.simulationSettings.numIterations;
            simulationResults.dps = simulationResults.totalDamage / simulationResults.combatDuration;
            return;
        }
        /*
        public SimulationResults Simulate()
        {
            Setup();
            simulationResults = new SimulationResults();

            List<IterationResults> iterationsResults = new List<IterationResults>();

            for (int i = 0; i < settings.simulationSettings.numIterations; i++)
            {
                Iteration iteration = new Iteration(settings, computedConstants, i);
                iteration.Setup();
                IterationResults results = iteration.Iterate();
                if (i % 10 == 0)
                {
                    damage += results.Damage();
                    numResults++;
                }
                iterationsResults.Add(results);
            }
            simulationResults.Populate(iterationsResults.ToList());
            simulationResults.combatDuration = settings.simulationSettings.combatLength;
            simulationResults.numIterations = settings.simulationSettings.numIterations;
            simulationResults.dps = simulationResults.totalDamage / simulationResults.combatDuration;
            return simulationResults;
        }
        */
        public float GetDPSUpdate()
        {
            return damage / (numResults * settings.simulationSettings.combatLength);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed!");
        }
        public void Setup()
        {


            // Computing what can be computed to avoid doing it for each iteration.
            // Damage.
            
            computedConstants.bleedDamageMultiplier = settings.debuffSettings.GetMultiplicativeStat(Stat.BleedDamage);
            computedConstants.deepWoundsDamageMultiplier = TalentUtils.GetDeepWoundsMultiplier(settings.talentSettings) * computedConstants.bleedDamageMultiplier;
            computedConstants.rendDamageMultiplier = computedConstants.bleedDamageMultiplier * (1 + settings.talentSettings.ImprovedRend.rank * 0.1f);
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
            computedConstants.hasMortalStrike = settings.talentSettings.MortalStrike.rank > 0;
            computedConstants.improvedMortalStrikeMultiplier = TalentUtils.GetImprovedMortalStrikeMultiplier(settings.talentSettings);
            computedConstants.bloodsurgeChance = TalentUtils.GetBloodSurgeChance(settings.talentSettings);
            computedConstants.focusedRageRageReduction = settings.talentSettings.FocusedRage.rank;
            computedConstants.slamDamageMultiplier = computedConstants.titansGripDamageMultiplier * computedConstants.meleeDamageMultiplier * computedConstants.unendingFuryDamageMultiplier;
            computedConstants.hasMHBerserking = settings.enchantSettings.GetEnchant(ItemSlot.MainHand).id == 59621;
            // TODO: Check if OH is equipped.
            computedConstants.hasOHBerserking = settings.enchantSettings.GetEnchant(ItemSlot.OffHand).id == 59621;
            computedConstants.dualWieldSpecializationMultiplier = TalentUtils.GetDualWieldSpecializationMultiplier(settings.talentSettings);

            // Rage
            computedConstants.hasEndlessRage = settings.talentSettings.EndlessRage.rank > 0;

            // Attack speed
            computedConstants.bloodFrenzySpeedMultiplier = 1 + settings.talentSettings.BloodFrenzy.rank * 0.05f;
        }
    }
}
