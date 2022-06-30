namespace Warrior.Entities
{
    public class Stance
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool isActive { get; set; }

    }

    public class StanceManager
    {
        public Stance currentStance = new Stance() { name = "Berserker Stance", id = 2458, isActive = true };

        public List<Stance> stances = new List<Stance>()
        {
            new Stance() {name = "Battle Stance", id = 2457, isActive = false},
            new Stance() {name = "Berserker Stance", id = 2458, isActive = true},
            new Stance() {name = "Defensive Stance", id = 71, isActive = false}
        };

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
            }
        }
    }


}
