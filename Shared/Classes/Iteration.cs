namespace Warrior
{
    public class Iteration
    {
        public Settings.Settings settings { get; set; }
        public int index { get; set; } = 0;
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
        public StanceManager stanceManager { get; set; }


        // Main simulation methods.
        public Iteration(Settings.Settings settings, ComputedConstants computedConstants, int index)
        {
            this.computedConstants = computedConstants;
            this.settings = settings;
            this.abilityManager = new AbilityManager(this);
            this.auraManager = new AuraManager(this);
            this.statsManager = new StatsManager(this);
            this.stanceManager = new StanceManager(this);
            this.iterationResults = new IterationResults();
            this.index = index;
            this.nextStep = new NextStep();
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
            int executeStep = (int)(numSteps * (1 - settings.simulationSettings.executePercent / 100.0f));

            Utils.MiscUtils.Log(this, "Execute step: " + executeStep);

            currentStep = 0;

            // Main simulation loop.
            while (currentStep < numSteps)
            {
                // Stance.
                if (!stanceManager.IsInDefaultStance() && stanceManager.CanChangeStance())
                {
                    stanceManager.DefaultStance();
                }
                nextStep.stance = stanceManager.next;
                // Passive ticks such as anger management.
                PassiveTicks();

                // Cooldowns.
                Cooldowns(currentStep, numSteps);

                // Main hand and off hand attacks.
                Attacks();

                if (currentStep > executeStep && settings.simulationSettings.useExecute)
                {
                    // Execution Abilities.
                    
                    ExecuteAbilities();
                } else
                {
                    // Abilities.
                    Abilities();
                }
                

                // Heroic Strike.
                if (settings.simulationSettings.useHeroicStrike && stanceManager.IsInDefaultStance())
                {
                    if (currentStep > executeStep && settings.simulationSettings.useExecuteHeroicStrike)
                    {
                        if (rage >= settings.simulationSettings.executeHeroicStrikeRagethreshold && !abilityManager.heroicStrike.isQueued)
                        {
                            abilityManager.heroicStrike.Use();
                        }
                    } else
                    {
                        if (rage >= settings.simulationSettings.heroicStrikeRagethreshold && !abilityManager.heroicStrike.isQueued)
                        {
                            abilityManager.heroicStrike.Use();
                        }
                    }
                    
                }

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
                auraManager.mainHandBerserking?.Update();
                auraManager.offHandBerserking?.Update();
                auraManager.heroism?.Update();
                auraManager.deathWish?.Update();
                auraManager.shatteringThrow?.Update();
                auraManager.bloodFury?.Update();
                auraManager.berserking?.Update();
                auraManager.wreckingCrew?.Update();
                auraManager.sunderArmor?.Update();
                auraManager.rend?.Update();
                auraManager.GetNext();

                int next = nextStep.GetNextStep();
                int delta = next - currentStep;

                // The passage of time.
                mainHand.ApplyTime(delta);
                offHand.ApplyTime(delta);
                globalCooldown -= delta;
                abilityManager.ApplyTime(delta);
                auraManager.ApplyTime(delta);
                stanceManager.ApplyTime(delta);

                // Next Step.
                currentStep = next;
                iterationResults.numSteps += 1;
            }

            // Fading all auras to update final uptime.
            auraManager.flurry?.Fade();
            auraManager.heroism?.Fade();
            auraManager.shatteringThrow?.Fade();
            auraManager.bloodFury?.Fade();
            auraManager.berserking?.Fade();
            auraManager.deepWounds?.Fade();
            auraManager.wreckingCrew?.Fade();
            auraManager.sunderArmor?.Fade();
            auraManager.rend?.Fade();
            stanceManager.Fade();

            // Moving the results.
            iterationResults.mainHand = (DamageResults)mainHand.damageSummary.Clone();
            iterationResults.offHand = (DamageResults)offHand.damageSummary.Clone();
            //auraManager.auras.ForEach(aura => iterationResults.auraSummaries.Add(aura.auraSummary));
            

            if (abilityManager.bloodthirst != null) iterationResults.abilitySummaries.Add((DamageResults)abilityManager.bloodthirst.damageSummary.Clone());
            iterationResults.abilitySummaries.Add((DamageResults)abilityManager.whirlwind.damageSummary.Clone());
            iterationResults.abilitySummaries.Add((DamageResults)abilityManager.heroicStrike.damageSummary.Clone());
            iterationResults.abilitySummaries.Add((DamageResults)abilityManager.slam.damageSummary.Clone());
            iterationResults.abilitySummaries.Add((DamageResults)abilityManager.execute.damageSummary.Clone());
            if (abilityManager.mortalStrike != null) iterationResults.abilitySummaries.Add((DamageResults)abilityManager.mortalStrike.damageSummary.Clone());

            if (auraManager.deepWounds != null) iterationResults.dotDamageSummaries.Add((DotDamageResults)auraManager.deepWounds.dotSummary.Clone());
            if (auraManager.rend != null) iterationResults.dotDamageSummaries.Add((DotDamageResults)auraManager.rend.dotSummary.Clone());

            if (auraManager.flurry != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.flurry.auraSummary.Clone());
            if (auraManager.bloodsurge != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.bloodsurge.auraSummary.Clone());
            if (auraManager.mainHandBerserking != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.mainHandBerserking.auraSummary.Clone());
            if (auraManager.offHandBerserking != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.offHandBerserking.auraSummary.Clone());
            if (auraManager.bloodRage != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.bloodRage.auraSummary.Clone());
            if (auraManager.heroism != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.heroism.auraSummary.Clone());
            if (auraManager.deathWish != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.deathWish.auraSummary.Clone());
            if (auraManager.shatteringThrow != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.shatteringThrow.auraSummary.Clone());
            if (auraManager.berserking != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.berserking.auraSummary.Clone());
            if (auraManager.bloodFury != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.bloodFury.auraSummary.Clone());
            if (auraManager.wreckingCrew != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.wreckingCrew.auraSummary.Clone());
            if (auraManager.sunderArmor != null) iterationResults.auraSummaries.Add((AuraResults)auraManager.sunderArmor.auraSummary.Clone());

            iterationResults.battleStanceResults = (StanceResults)stanceManager.battleStanceResults.Clone();
            iterationResults.berserkerStanceResults = (StanceResults)stanceManager.berserkerStanceResults.Clone();
            iterationResults.defensiveStanceResults = (StanceResults)stanceManager.defensiveStanceResults.Clone();

            return iterationResults;
        }

        public void Setup()
        {
            nextStep = new NextStep();
            auraManager.Reset();
            abilityManager.Reset();

                mainHand = new Weapon(this, ItemSlot.MainHand, settings.equipmentSettings.GetItemBySlot(ItemSlot.MainHand));


                offHand = new Weapon(this, ItemSlot.OffHand, settings.equipmentSettings.GetItemBySlot(ItemSlot.OffHand));

            
            rage = settings.simulationSettings.initialRage;
            globalCooldown = 0;
        }
        public void PassiveTicks()
        {
            // Anger Management.
            if (computedConstants.HasAngerManagement && currentStep % (3 * Constants.kStepsPerSecond) == 0)
            {
                IncrementRage(1, "Anger Management");
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
            if (!GCDAvailable() && !abilityManager.slam.isCasting && !abilityManager.shatteringThrow.isCasting) return;

            // Sunder Armor.
            if (settings.simulationSettings.useSunderArmor)
            {
                if (settings.simulationSettings.useBloodthirst && abilityManager.bloodthirst != null && abilityManager.bloodthirst?.currentCooldown < 1.5f * Constants.kStepsPerSecond)
                {
                    goto NoSunder;
                }
                if (settings.simulationSettings.useWhirlwind && abilityManager.whirlwind != null && abilityManager.whirlwind?.currentCooldown < 1.5f * Constants.kStepsPerSecond)
                {
                    goto NoSunder;
                }
                if (settings.simulationSettings.useMortalStrike && abilityManager.mortalStrike != null && abilityManager.mortalStrike?.currentCooldown < 1.5f * Constants.kStepsPerSecond)
                {
                    goto NoSunder;
                }
                if ((auraManager.sunderArmor?.next - currentStep) > 4.5f * Constants.kStepsPerSecond && auraManager.sunderArmor?.stacks == 5)
                {
                    goto NoSunder;
                }
                abilityManager.sunderArmor?.Use();
            }
            NoSunder:

            // Bloodthirst.
            if (abilityManager.bloodthirst != null
                && settings.simulationSettings.useBloodthirst
                && abilityManager.bloodthirst.CanUse()) {
                abilityManager.bloodthirst.Use();
                abilityManager.GetNext();
                return;
            }

            // Whirlwind.
            if (settings.simulationSettings.useWhirlwind
                && abilityManager.whirlwind != null
                && abilityManager.whirlwind.CanUse())
            {
                if (settings.simulationSettings.useWhirlwind && abilityManager.whirlwind.CanUse())
                {
                    if (!stanceManager.IsInBerserkerStance() && stanceManager.CanChangeStance())
                    {
                        stanceManager.ChangeStance(new Entities.Stance() { id = 2458 });
                    }
                    if (stanceManager.IsInBerserkerStance())
                    {
                        abilityManager.whirlwind.Use();
                        abilityManager.GetNext();
                        return;
                    }
                }
            }

            // Rend.
            if (settings.simulationSettings.useRend &&
                ((abilityManager.whirlwind != null && abilityManager.whirlwind.currentCooldown >= 3.0f * Constants.kStepsPerSecond) || !settings.simulationSettings.useWhirlwind)
                && abilityManager.rend.CanUse()
                && (auraManager.bloodsurge == null || !auraManager.bloodsurge.active)
                && ((auraManager.rend != null && auraManager.rend.currentDuration <= 1.0f * Constants.kStepsPerSecond) || !auraManager.rend.active))
            {
                if (!stanceManager.IsInBattleStance() && stanceManager.CanChangeStance())
                {
                    stanceManager.ChangeStance(new Entities.Stance() { id = 2457 });
                }
                if (stanceManager.IsInBattleStance())
                {
                    abilityManager.rend.Use();
                }
                
                return;
            }

            // Slam.
            if (abilityManager.slam.isCasting && currentStep == abilityManager.slam.endCast)
            {
                abilityManager.slam.Use();
            }

            if (settings.simulationSettings.useSlam)
            {
                if (settings.simulationSettings.slamOnlyOnBloodsurge
                    && auraManager.bloodsurge != null
                    && auraManager.bloodsurge.active
                    && settings.talentSettings.Bloodsurge.rank > 0
                    // Never delay BT / WW (TODO: make it more subtle)
                    && (abilityManager.bloodthirst == null
                        || abilityManager.bloodthirst?.currentCooldown > 1.5f * Constants.kStepsPerSecond
                        || !settings.simulationSettings.useBloodthirst)
                    && (abilityManager.whirlwind == null
                        || abilityManager.whirlwind.currentCooldown > 1.5f * Constants.kStepsPerSecond
                        || !settings.simulationSettings.useWhirlwind))
                {
                    abilityManager.slam.Use();
                    abilityManager.GetNext();
                    return;
                } else
                {
                    // Use slam as filler.
                    if (!settings.simulationSettings.slamOnlyOnBloodsurge
                        && (abilityManager.bloodthirst == null
                            || abilityManager.bloodthirst?.currentCooldown > 1.5f * Constants.kStepsPerSecond
                            || !settings.simulationSettings.useBloodthirst))
                    {
                        abilityManager.slam.Use();
                        abilityManager.GetNext();
                        return;
                    }
                }
            }


            abilityManager.GetNext();
        }

        public void ExecuteAbilities()
        {
            if (!GCDAvailable()) return;

            // Whirlwind hits more than execute.
            if (settings.simulationSettings.useWhirlwind && abilityManager.whirlwind.CanUse())
            {
                if (!stanceManager.IsInBerserkerStance() && stanceManager.CanChangeStance())
                {
                    stanceManager.ChangeStance(new Entities.Stance() { id = 2458 });
                    
                }
                if (stanceManager.IsInBerserkerStance())
                {
                    abilityManager.whirlwind.Use();
                    Utils.MiscUtils.Log(this, "Execute WW");
                }
            }

            // Depending on gear, bloodthirst may hit more than execute.
            // TODO: compute actual AP threshold and add as option.
            if (settings.simulationSettings.prioritizeBloodthirst
                && settings.simulationSettings.useBloodthirst
                && abilityManager.bloodthirst != null
                && abilityManager.bloodthirst.CanUse())
            {
                abilityManager.bloodthirst.Use();
                Utils.MiscUtils.Log(this, "Execute BT");
            }

            // TODO: check what the story is with Mortal Strike.

            // Depending on gear, slam on bloodthirst may be better than execute. 
            if (settings.simulationSettings.prioritizeSlamOnBloodsurge
                && (auraManager.bloodsurge!= null && auraManager.bloodsurge.active))
            {
                Utils.MiscUtils.Log(this, "Execute Slam");
                abilityManager.slam.Use();
            }

            // Execute.
            // TODO: look into delaying execute if rage is too low.
            abilityManager.execute.Use();
            abilityManager.GetNext();
            return;
        }

        public void IncrementRage(int value, string source)
        {
            iterationResults.rageSummary.rageGenerated += value;

            // TODO: add losses per source.
            if (iterationResults.rageSummary.generated.TryGetValue(source, out var val))
            {
                iterationResults.rageSummary.generated[source] += value;
            } else
            {
                iterationResults.rageSummary.generated.Add(source, value);
            }

            if (iterationResults.rageSummary.ticks.TryGetValue(source, out var val2))
            {
                iterationResults.rageSummary.ticks[source] += 1;
            }
            else
            {
                iterationResults.rageSummary.ticks.Add(source, 1);
            }

            if (rage + value > 100)
            {
                iterationResults.rageSummary.wastedRage += rage + value - 100;



                rage = 100;
                return;
            }
            rage += value;
        }
        public void Cooldowns(int currentStep, int numSteps)
		{
            // Bloodrage.
            if (settings.simulationSettings.useBloodRage && rage < settings.simulationSettings.bloodRageThreshold) abilityManager.bloodrage.Use();

            // Heroism.
            if (settings.simulationSettings.useHeroism && RemainingTime(currentStep, numSteps) < settings.simulationSettings.heroismOnLastSeconds && !auraManager.heroism.active)
			{
                abilityManager.heroism?.Use();
            }

            // Blood Fury.
            if (settings.simulationSettings.useBloodFury
                && settings.characterSettings.race == Settings.Race.Orc
                && abilityManager.bloodFury.CanUse()
                && RemainingTime(currentStep, numSteps) <= settings.simulationSettings.bloodFuryOnLastSeconds)
            {
                abilityManager.bloodFury?.Use();
            }

            // Berserking.
            if (settings.simulationSettings.useBerserking
                && settings.characterSettings.race == Settings.Race.Troll
                && abilityManager.berserking.CanUse()
                && RemainingTime(currentStep, numSteps) <= settings.simulationSettings.berserkingOnLastSeconds)
            {
                abilityManager.berserking?.Use();
            }

            // Death Wish.
            if (settings.simulationSettings.useDeathWish && !auraManager.deathWish.active)
            {
                if (RemainingTime(currentStep, numSteps) > settings.simulationSettings.deathWishOnLastSeconds + abilityManager.deathWish.cooldown / Constants.kStepsPerSecond && abilityManager.deathWish.CanUse())
                {
                    abilityManager.deathWish.Use();
                }
                if (RemainingTime(currentStep, numSteps) < settings.simulationSettings.deathWishOnLastSeconds && abilityManager.deathWish.CanUse())
                {
                    abilityManager.deathWish.Use();
                }
            }

            // Shattering Throw.
            if ((settings.simulationSettings.useShatteringThrow && !auraManager.shatteringThrow.active && abilityManager.shatteringThrow.CanUse()) || abilityManager.shatteringThrow.endCast == currentStep)
            {
                if (RemainingTime(currentStep, numSteps) > settings.simulationSettings.shatteringThrowOnLastSeconds + abilityManager.shatteringThrow.cooldown / Constants.kStepsPerSecond
                    || RemainingTime(currentStep, numSteps) < settings.simulationSettings.shatteringThrowOnLastSeconds)
                {
                    if (!stanceManager.IsInBattleStance() && stanceManager.CanChangeStance())
                    {
                        stanceManager.ChangeStance(new Entities.Stance() { id = 2457 });
                    }
                    abilityManager.shatteringThrow.Use();
                    abilityManager.GetNext();
                }
            }


        }
        public float RemainingTime(int currentStep, int numSteps)
		{
            return (numSteps - currentStep) / Constants.kStepsPerSecond;
		}
        public bool GCDAvailable()
        {
            return globalCooldown <= 0;
        }
    }
}
