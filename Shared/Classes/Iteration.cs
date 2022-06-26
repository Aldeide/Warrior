namespace Warrior
{
    public class Iteration
    {
        public Settings.Settings settings { get; set; }
        public ComputedConstants computedConstants { get; set; }
        public IterationResults iterationResults  {get; set;}

        public int currentStep { get; set; }
        public int rage { get; set; }
        public int globalCooldown { get; set; } = 0;

        public AbilityManager abilityManager;
        public Weapon mainHand;
        public Weapon offHand;
        public Random random { get; set; } = new Random();
        public NextStep nextStep { get; set; }

        // Temporary buffs and procs.
        public StatsManager statsManager { get; set; }
        public AuraManager auraManager { get; set; }

        // Main simulation methods.
        public Iteration(Settings.Settings settings, ComputedConstants computedConstants)
        {
            this.computedConstants = computedConstants;
            this.settings = settings;
            this.abilityManager = new AbilityManager(this);
            this.auraManager = new AuraManager(this);
            this.statsManager = new StatsManager(this);
            this.iterationResults = new IterationResults();
            Setup();
        }

        public IterationResults Iterate()
        {
            // Resetting what needs to be reset between iterations, namely temporary auras.
            Setup();

            // Preparing the results of the iteration.
            iterationResults = new IterationResults();
            iterationResults.combatLength = settings.simulationSettings.combatLength;

            // TODO: add variability of combat length.
            int numSteps = settings.simulationSettings.combatLength * Constants.kStepsPerSecond;
            currentStep = 0;

            // Main simulation loop.
            while (currentStep < numSteps)
            {
                // Passive ticks such as anger management.
                PassiveTicks();

                // Main hand and off hand attacks.
                Attacks();

                // Abilities.
                Abilities();
                if (globalCooldown <= 0)
                {
                    nextStep.globalCooldown = numSteps;
                } else
                {
                    nextStep.globalCooldown = currentStep + globalCooldown;
                }

                // Auras
                
                auraManager.deepWounds?.Update();
                auraManager.bloodsurge?.Update();
                auraManager.bloodRage?.Update();
                auraManager.GetNext();

                int next = nextStep.GetNextStep();
                int delta = next - currentStep;
                Console.WriteLine("[ " + currentStep + " ] NextStep: (" + next + ")");

                // The passage of time.
                mainHand.ApplyTime(delta);
                offHand.ApplyTime(delta);
                globalCooldown -= delta;
                abilityManager.ApplyTime(delta);
                auraManager.ApplyTime(delta);
                // Next Step.
                currentStep = next;
                iterationResults.numSteps += 1;
            }

            // Fading all auras to update final uptime.
            auraManager.flurry?.Fade();

            // Moving the results.
            iterationResults.mainHand = (DamageResults)mainHand.damageSummary.Clone();
            iterationResults.offHand = (DamageResults)offHand.damageSummary.Clone();
            auraManager.auras.ForEach(aura => iterationResults.auraSummaries.Add(aura.auraSummary));
            abilityManager.abilities.ForEach(ability => iterationResults.abilitySummaries.Add(ability.damageSummary));

            if (abilityManager.bloodthirst != null) iterationResults.abilitySummaries.Add(abilityManager.bloodthirst.damageSummary);
            iterationResults.abilitySummaries.Add(abilityManager.whirlwind.damageSummary);
            iterationResults.abilitySummaries.Add(abilityManager.heroicStrike.damageSummary);
            iterationResults.abilitySummaries.Add(abilityManager.slam.damageSummary);
            if (auraManager.deepWounds != null) iterationResults.dotDamageSummaries.Add((DotDamageResults)auraManager.deepWounds.dotSummary.Clone());
            if (auraManager.flurry != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.flurry.auraSummary.Clone());
            if (auraManager.bloodsurge != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.bloodsurge.auraSummary.Clone());
            return iterationResults;
        }

        public void Setup()
        {
            nextStep = new NextStep();
            auraManager.Reset();
            abilityManager = new AbilityManager(this);
            mainHand = new Weapon(this, ItemSlot.MainHand, settings.equipmentSettings.GetItemBySlot(ItemSlot.MainHand));
            offHand = new Weapon(this, ItemSlot.OffHand, settings.equipmentSettings.GetItemBySlot(ItemSlot.OffHand));
            rage = 0;
            globalCooldown = 0;
        }
        public void PassiveTicks()
        {
            // Anger Management.
            if (computedConstants.HasAngerManagement && currentStep % (3 * Constants.kStepsPerSecond) == 0)
            {
                IncrementRage(1, "Anger Management");
                Console.WriteLine("[ " + currentStep + " ] Anger Management Tick");
                nextStep.passiveTicks = currentStep + 3 * Constants.kStepsPerSecond;
            }
            return;
        }
        public void Attacks()
        {
            mainHand.Damage();
            offHand.Damage();
            return;
        }
        public void Abilities()
        {
            // Bloodrage.
            if (settings.simulationSettings.useBloodRage && rage < settings.simulationSettings.bloodRageThreshold) abilityManager.bloodrage.Use();

            // Bloodthirst.
            if (abilityManager.bloodthirst != null) {
                abilityManager.bloodthirst.Use();
            }
            // Whirlwind.
            abilityManager.whirlwind.Use();

            if (auraManager.bloodsurge != null && auraManager.bloodsurge.active)
            {
                if(abilityManager.bloodthirst.currentCooldown > 1.6f)
                {
                    abilityManager.slam.Use();
                    auraManager.bloodsurge.active = false;
                }
            }

            if (settings.simulationSettings.useHeroicStrike)
			{
                if (rage >= settings.simulationSettings.heroicStrikeRagethreshold && !abilityManager.heroicStrike.isQueued)
                {
                    Console.WriteLine("[ " + currentStep + " ] Iteration: Queueing Heroic Strike");
                    abilityManager.heroicStrike.Use();
                }
            }
            abilityManager.GetNext();
        }
        public void IncrementRage(int value, string source)
        {
            Console.WriteLine("[ " + currentStep + " ] Gained Rage from " + source + ": " + value);
            iterationResults.rageSummary.rageGenerated += value;
            if (rage + value > 100)
            {
                iterationResults.rageSummary.wastedRage += rage + value - 100;
                rage = 100;
                return;
            }
            rage += value;
        }

    }
}
