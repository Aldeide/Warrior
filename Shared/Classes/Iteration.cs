namespace Warrior
{
    public class Iteration
    {
        public Simulation simulation { get; set; }
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
        public StanceManager stanceManager { get; set; }

        // Main simulation methods.
        public Iteration(Simulation simulation)
        {
            this.simulation = simulation;
            this.stanceManager = new StanceManager();
            this.abilityManager = new AbilityManager(this);
            this.auraManager = new AuraManager(this);
            this.statsManager = new StatsManager(simulation.character, auraManager, this);
            
            this.iterationResults = new IterationResults();
            Setup();
        }

        public IterationResults Iterate()
        {
            // Resetting what needs to be reset between iterations, namely temporary auras.
            Setup();

            // Preparing the results of the iteration.
            iterationResults = new IterationResults();
            iterationResults.combatLength = simulation.settings.combatLength;

            // TODO: add variability of combat length.
            int numSteps = simulation.settings.combatLength * Constants.kStepsPerSecond;
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
            iterationResults.mainHand = (DamageSummary)mainHand.damageSummary.Clone();
            iterationResults.offHand = (DamageSummary)offHand.damageSummary.Clone();
            auraManager.auras.ForEach(aura => iterationResults.auraSummaries.Add(aura.auraSummary));
            abilityManager.abilities.ForEach(ability => iterationResults.abilitySummaries.Add(ability.damageSummary));


            if (auraManager.deepWounds != null) iterationResults.dotDamageSummaries.Add((DotDamageSummary)auraManager.deepWounds.dotSummary.Clone());
            if (auraManager.flurry != null) iterationResults.auraSummaries.Add((AuraSummary)auraManager.flurry.auraSummary.Clone());
            return iterationResults;
        }

        public void Setup()
        {
            nextStep = new NextStep();
            auraManager.Reset();
            abilityManager = new AbilityManager(this);
            mainHand = new Weapon(this, ItemSlot.MainHand, simulation.character.equipment.GetItemBySlot(ItemSlot.MainHand));
            offHand = new Weapon(this, ItemSlot.OffHand, simulation.character.equipment.GetItemBySlot(ItemSlot.OffHand));
            rage = 0;
            globalCooldown = 0;
        }
        public void PassiveTicks()
        {
            // Anger Management.
            if (simulation.computedConstants.HasAngerManagement && currentStep % (3 * Constants.kStepsPerSecond) == 0)
            {
                IncrementRage(1);
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
            // Bloodthirst.
            if (TalentUtils.HasBloodthirst(simulation.character.talents)) {
                abilityManager.UseAbility("Bloodthirst");
            }
            abilityManager.UseAbility("Whirlwind");
            if (rage >= 45 && !abilityManager.abilities.Single(a=>a.name == "Heroic Strike").isQueued)
            {
                Console.WriteLine("[ " + currentStep + " ] Iteration: Queueing Heroic Strike");
                abilityManager.UseAbility("Heroic Strike");
            }
            abilityManager.GetNext();
        }
        public void IncrementRage(int value)
        {
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
