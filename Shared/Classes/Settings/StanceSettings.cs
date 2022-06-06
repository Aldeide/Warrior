using Warrior.Entities;

namespace Warrior.Settings
{
	public class StanceSettings
	{
        public Stance currentStance = new Stance() { name = "Berserker Stance", id = 2458, isActive = true };

        public List<Stance> stances { get; set; } = new List<Stance>() 
        {
            new Stance() {name = "Battle Stance", id = 2457, isActive = false},
            new Stance() {name = "Berserker Stance", id = 2458, isActive = true},
            new Stance() {name = "Defensive Stance", id = 71, isActive = false}
        };

        public void ChangeStance(Stance stance)
        {
            this.currentStance = stance;
        }

        public bool IsInBattleStance()
        {
            return currentStance.id == 2457;
        }
        public bool IsInBerserkerStance()
        {
            return currentStance.id == 2458;
        }
        public bool IsInDefensiveStance()
        {
            return currentStance.id == 71;
        }

        public void ProcessClick(long button, int id)
        {
            if (button == 0)
            {
                if (currentStance.id == id)
                {
                    return;
                }
                stances.Single(s => s.id == currentStance.id).isActive = false;
                stances.Single(s => s.id == id).isActive = true;
                currentStance = stances.Single(s => s.id == id);
                Stance stance = stances.Single(s => s.id == id);
            }

        }
}
}
