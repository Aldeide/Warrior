﻿namespace Warrior
{
    public class AuraManager
    {
        public List<Aura> auras { get; set; } = new List<Aura>();
        public Iteration iteration;

        public Flurry? flurry;
        public DeepWounds? deepWounds;
        public Bloodsurge? bloodsurge;
        public BloodRage? bloodRage;

        // Enchants.
        public MainHandBerserking? mainHandBerserking;
        public OffHandBerserking? offHandBerserking;

        // Cooldowns.
        public HeroismAura? heroism;
        public DeathWishAura? deathWish;
        public ShatteringThrowAura? shatteringThrow;
        public AuraManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (TalentUtils.HasFlurry(iteration.settings.talentSettings)) flurry = new Flurry(this);
            if (TalentUtils.HasDeepWounds(iteration.settings.talentSettings)) deepWounds = new DeepWounds(this);
            if (iteration.computedConstants.hasBloodsurge) bloodsurge = new Bloodsurge(this);
            bloodRage = new BloodRage(this);

            // Enchants.
            if (iteration.computedConstants.hasMHBerserking) mainHandBerserking = new MainHandBerserking(this);
            if (iteration.computedConstants.hasOHBerserking) offHandBerserking = new OffHandBerserking(this);

            // Cooldowns.
            if (iteration.settings.simulationSettings.useHeroism) heroism = new HeroismAura(this);
            if (iteration.settings.simulationSettings.useDeathWish && iteration.settings.talentSettings.DeathWish.rank > 0) deathWish = new DeathWishAura(this);
            if (iteration.settings.simulationSettings.useShatteringThrow) shatteringThrow = new ShatteringThrowAura(this);
        }
        public void Reset()
        {
            deepWounds?.Reset();
            flurry?.Reset();
            bloodRage?.Reset();
            heroism?.Reset();
            deathWish?.Reset();
            shatteringThrow?.Reset();
        }
        public void MeleeCriticalTrigger()
        {
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
            
        }
        public void MeleeNonCriticalTrigger()
        {
            flurry?.Trigger(AuraTrigger.AllMeleeNonCritical);
        }
        public void MeleeNonCriticalSwing()
        {
            flurry?.Trigger(AuraTrigger.AllMeleeNonCritical);
        }
        public void MainHandNonCriticalTrigger()
		{
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void MainHandCriticalTrigger()
        {
            deepWounds?.Trigger(AuraTrigger.MainHandCritical);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
            MeleeNonCriticalTrigger();
        }
        public void OffHandNonCriticalTrigger()
        {
            MeleeNonCriticalTrigger();
            offHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void OffHandCriticalTtrigger()
        {
            deepWounds?.Trigger(AuraTrigger.OffHandCritical);
            offHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void BloodthirstTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Bloodthirst);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void BloodthirstCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Bloodthirst);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
            deepWounds?.Trigger(AuraTrigger.Bloodthirst);
        }
        public void WhirlwindTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Whirlwind);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void WhirlwindCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Whirlwind);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
            deepWounds?.Trigger(AuraTrigger.Whirlwind);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void HeroicStrikeTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.HeroicStrike);
            flurry?.Trigger(AuraTrigger.AllMeleeNonCritical);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void HeroicStrikeCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.HeroicStrike);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
            deepWounds?.Trigger(AuraTrigger.HeroicStrike);
            mainHandBerserking?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void ApplyTime(int d)
        {
            return;
        }
        public void GetNext()
        {
            int next = int.MaxValue;
            if (deepWounds != null && deepWounds.next < next && deepWounds.active) next = deepWounds.next;
            if (bloodsurge != null && bloodsurge.next < next && bloodsurge.active) next = bloodsurge.next;
            if (bloodRage != null && bloodRage.next < next && bloodRage.active) next = bloodRage.next;
            if (mainHandBerserking != null && mainHandBerserking.next < next && mainHandBerserking.active) next = mainHandBerserking.next;
            if (offHandBerserking != null && offHandBerserking.next < next && offHandBerserking.active) next = offHandBerserking.next;
            if (heroism != null && heroism.next < next && heroism.active) next = heroism.next;
            if (deathWish != null && deathWish.next < next && deathWish.active) next = deathWish.next;
            if (shatteringThrow != null && shatteringThrow.next < next && shatteringThrow.active) next = shatteringThrow.next;
            iteration.nextStep.auras = next;
        }
    }
}