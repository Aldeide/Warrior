namespace Warrior
{
    public class NextStep
    {
        public int passiveTicks { get; set; } = int.MaxValue;

        public int auras { get; set; } = int.MaxValue;
        public int mainHand { get; set; } = 0;
        public int offHand { get; set; } = 0;
        public int globalCooldown { get; set; } = 0;

        public int abilities { get; set; } = 0;

        public int GetNextStep()
        {
            int nextStep = int.MaxValue;
            if (passiveTicks < nextStep)
            {
                nextStep = passiveTicks;
                Console.WriteLine("NextStep: Passive Ticks: " + nextStep);
            }
            if (auras < nextStep)
            {
                nextStep = auras;
                Console.WriteLine("NextStep: Auras" + nextStep);
            }
            if (mainHand < nextStep)
            {
                nextStep = mainHand;
                Console.WriteLine("NextStep: Main Hand" + nextStep);
            }
            if (offHand < nextStep)
            {
                nextStep = offHand;
                Console.WriteLine("NextStep: Off Hand" + nextStep);
            }
            if (globalCooldown < nextStep)
            {
                nextStep = globalCooldown;
                Console.WriteLine("NextStep: GCD" + nextStep);
            }
            if (abilities < nextStep)
            {
                nextStep = abilities;
                Console.WriteLine("NextStep: Abilities" + nextStep);
            }
            return nextStep;
        }

    }
}
