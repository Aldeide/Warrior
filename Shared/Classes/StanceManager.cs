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

        public int next { get; set; } = int.MaxValue;

        public StanceResults berserkerStanceResults { get; set; } = new StanceResults() { name = "Berserker Stance"};
        public StanceResults battleStanceResults { get; set; } = new StanceResults() { name = "Battle Stance" };
        public StanceResults defensiveStanceResults { get; set; } = new StanceResults() { name = "Defensive Stance" };

        public StanceManager(Iteration iteration)
        {
            this.iteration = iteration;
            defaultStance = iteration.settings.stanceSettings.currentStance;
            stance = defaultStance;
        }

        int lastChanged = 0;

        public bool CanChangeStance()
        {
            return currentGCD <= 0;
        }
        public void ChangeStance(Stance newStance)
        {
            if (!CanChangeStance()) return;

            if (stance.id == 2457)
            {
                battleStanceResults.uptime += (iteration.currentStep - lastChanged);
            }
            if (stance.id == 2458)
            {
                berserkerStanceResults.uptime += (iteration.currentStep - lastChanged);
            }
            if (stance.id == 71)
            {
                defensiveStanceResults.uptime += (iteration.currentStep - lastChanged);
            }

            lastChanged = iteration.currentStep;

            stance = newStance;
            currentGCD = stanceGCD;
            iteration.rage = RageCap(iteration.rage);
            if (stance != defaultStance)
            {
                next = iteration.currentStep + (int)(stanceGCD);
                Console.WriteLine("[ " + iteration.currentStep + " ] Schedule stance change: " + next + " (Current: " + iteration.currentStep + ")");
            } else
            {
                next = int.MaxValue;
            }
            
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
        public void SwitchToBattleStance()
        {
            ChangeStance(new Stance { id = 2457 });
        }
        public void SwitchToBerserkerStance()
        {
            ChangeStance(new Stance { id = 2458 });
        }
        public void SwitchToDefensiveStance()
        {
            ChangeStance(new Stance { id = 71 });
        }

        public void Fade()
        {
            if (stance.id == 2457)
            {
                battleStanceResults.uptime += (iteration.currentStep - lastChanged);
            }
            if (stance.id == 2458)
            {
                berserkerStanceResults.uptime += (iteration.currentStep - lastChanged);
            }
            if (stance.id == 71)
            {
                defensiveStanceResults.uptime += (iteration.currentStep - lastChanged);
            }
        }
    }
}
