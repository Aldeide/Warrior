using Warrior.Entities;

namespace Warrior
{
    public class StanceManager
    {
        Iteration iteration;
        Stance stance;
        Stance defaultStance;

        float stanceGCD = 1.5f * Constants.kStepsPerSecond;
        float currentGCD = 0;

        public StanceManager(Iteration iteration)
        {
            this.iteration = iteration;
            defaultStance = iteration.settings.stanceSettings.currentStance;
            stance = defaultStance;
        }
        public bool CanChangeStance()
        {
            return currentGCD <= 0;
        }
        public void ChangeStance(Stance newStance)
        {
            if (!CanChangeStance()) return;
            stance = newStance;
        }
        public void DefaultStance()
        {
            if (!CanChangeStance()) return;
            stance = defaultStance;
        }
        public void ApplyTime(int steps)
        {
            currentGCD -= steps;
            if (currentGCD < 0)
            {
                currentGCD = 0;
            }
        }
        public bool IsInBerserkerStance()
        {
            return stance.id == 2458;
        }
        public bool IsInBattleStance()
        {
            return stance.id == 2457;
        }
        public bool IsInDefensiveStance()
        {
            return stance.id == 71;
        }
    }
}
