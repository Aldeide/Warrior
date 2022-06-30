using Warrior.Entities;

namespace Warrior
{
    public class StanceManager
    {
        Iteration iteration;
        Stance stance;
        Stance defaultStance;

        float stanceGCD = 1.0f * Constants.kStepsPerSecond;
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
            currentGCD = stanceGCD;
            iteration.rage = RageCap(iteration.rage);
        }
        public void DefaultStance()
        {
            if (!CanChangeStance()) return;
            ChangeStance(defaultStance);
        }
        public void ApplyTime(int d)
        {
            currentGCD -= d;
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
        public bool IsInDefaultStance()
        {
            return stance == defaultStance;
        }
        public int RageCap(int currentRage)
        {
            if (currentRage > 10 + iteration.settings.talentSettings.TacticalMastery.rank * 5)
            {
                return 10 + iteration.settings.talentSettings.TacticalMastery.rank * 5;
            }
            return 10;
        }
    }
}
