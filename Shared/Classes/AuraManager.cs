namespace Warrior
{
    public class AuraManager
    {
        public List<Aura> auras { get; set; } = new List<Aura>();
        public Iteration iteration;

        public Flurry? flurry;
        public DeepWounds? deepWounds;
        public Bloodsurge? bloodsurge;

        public AuraManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (TalentUtils.HasFlurry(iteration.settings.talentSettings)) flurry = new Flurry(this);
            if (TalentUtils.HasDeepWounds(iteration.settings.talentSettings)) deepWounds = new DeepWounds(this);
            if (iteration.computedConstants.hasBloodsurge) bloodsurge = new Bloodsurge(this);

        }
        public void Reset()
        {
            deepWounds?.Reset();
            flurry?.Reset();
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
        public void MainHandCriticalTtrigger()
        {
            deepWounds?.Trigger(AuraTrigger.MainHandCritical);
        }
        public void OffHandCriticalTtrigger()
        {
            deepWounds?.Trigger(AuraTrigger.OffHandCritical);
        }
        public void BloodthirstTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Bloodthirst);
        }
        public void BloodthirstCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Bloodthirst);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
        }
        public void WhirlwindTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Whirlwind);
        }
        public void WhirlwindCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.Whirlwind);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
        }
        public void HeroicStrikeTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.HeroicStrike);
            flurry?.Trigger(AuraTrigger.AllMeleeNonCritical);
        }
        public void HeroicStrikeCriticalTrigger()
        {
            bloodsurge?.Trigger(AuraTrigger.HeroicStrike);
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
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
            iteration.nextStep.auras = next;
        }
    }
}
