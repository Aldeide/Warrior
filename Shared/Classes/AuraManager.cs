﻿namespace Warrior
{
    public class AuraManager
    {
        public List<Aura> auras { get; set; } = new List<Aura>();
        public Iteration iteration;

        public Flurry? flurry;
        public DeepWounds? deepWounds;

        public AuraManager(Iteration iteration)
        {
            this.iteration = iteration;

            if (TalentUtils.HasFlurry(iteration.simulation.character.talents)) auras.Add(new Flurry(this));
            if (TalentUtils.HasDeepWounds(iteration.simulation.character.talents)) auras.Add(new DeepWounds(this));
            if (TalentUtils.HasFlurry(iteration.simulation.character.talents)) flurry = new Flurry(this);
            if (TalentUtils.HasDeepWounds(iteration.simulation.character.talents)) deepWounds = new DeepWounds(this);

        }
        public void Reset()
        {
            auras.ForEach(a => a.Reset());
        }
        public void MeleeCriticalTrigger()
        {
            flurry?.Trigger(AuraTrigger.AllMeleeCritical);
            
            /*
            auras.Where(
                s => s.trigger.Contains(AuraTrigger.AllMeleeCritical)
            ).ToList().ForEach(
                s => s.Trigger(AuraTrigger.AllMeleeCritical)
            );
            */
        }
        public void MeleeNonCriticalTrigger()
        {
            flurry?.Trigger(AuraTrigger.AllMeleeNonCritical);
            /*
            auras.Where(
                s => s.trigger.Contains(AuraTrigger.AllMeleeNonCritical)
            ).ToList().ForEach(
                s => s.Trigger(AuraTrigger.AllMeleeNonCritical)
            );
            */
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
            auras.Where(
                s => s.trigger.Contains(AuraTrigger.Bloodthirst)
            ).ToList().ForEach(
                s => s.Trigger(AuraTrigger.Bloodthirst)
            );
        }
        public void WhirlwindTrigger()
        {
            auras.Where(
                s => s.trigger.Contains(AuraTrigger.Whirlwind)
            ).ToList().ForEach(
                s => s.Trigger(AuraTrigger.Whirlwind)
            );
        }
        public void HeroicStrikeTrigger()
        {
            auras.Where(
                s => s.trigger.Contains(AuraTrigger.HeroicStrike)
            ).ToList().ForEach(
                s => s.Trigger(AuraTrigger.HeroicStrike)
            );
        }
        public void ApplyTime(int d)
        {
            return;
        }
        public void GetNext()
        {
            int next = int.MaxValue;
            if (deepWounds != null && deepWounds.next < next && deepWounds.active) next = deepWounds.next;
            iteration.nextStep.auras = next;
        }
    }
}