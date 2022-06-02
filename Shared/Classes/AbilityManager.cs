namespace Warrior
{
    public enum AuraTrigger
    {
        AllMeleeAttacks,
        AllMeleeNonCritical,
        AllMeleeCritical,
        MainHandCritical,
        OffHandCritical,
        Bloodthirst,
        Whirlwind,
        HeroicStrike,
        Use

    }
    public class AbilityManager
    {
        public event EventHandler AbilityHit;
        public event EventHandler AbilityCrit;

        public Iteration iteration;
        public List<Ability> abilities = new List<Ability>();
        public AbilityManager(Iteration iteration)
        {
            this.iteration = iteration;
            if (iteration.simulation.computedConstants.hasBloodthirst) abilities.Add(new Bloodthirst(iteration));
            abilities.Add(new Whirlwind(iteration));
            abilities.Add(new HeroicStrike(iteration));
        }
        public void ApplyTime(int d)
        {
            abilities.ForEach(a => a.ApplyTime(d));
        }
        public void UseAbility(string name)
        {
            abilities.Single(a => a.name == name).Use();
        }
        public void GetNext()
        {
            int next = int.MaxValue;
            if (abilities.Where(a => a.currentCooldown > 0).Count() == 0)
            {
                iteration.nextStep.abilities = int.MaxValue;
            } else
            {
                iteration.nextStep.abilities = iteration.currentStep + abilities.Where(a => a.currentCooldown > 0).Min(a => a.currentCooldown);
            }
        }
        public virtual void OnAbilityHit(Ability a, EventArgs e)
        {
            AbilityHit?.Invoke(a, e);
        }
        public virtual void OnAbilityCrit(Ability a, EventArgs e)
        {
            AbilityCrit?.Invoke(a, e);
        }

    }
}
