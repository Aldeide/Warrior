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

        public int stance { get; set; } = int.MaxValue;

        public int GetNextStep()
        {
            int nextStep = int.MaxValue;
            if (passiveTicks < nextStep)
            {
                nextStep = passiveTicks;
                //Console.WriteLine("NextStep (passive ticks): " + nextStep);
            }
            if (auras < nextStep)
            {
                nextStep = auras;
                //Console.WriteLine("NextStep (auras): " + nextStep);
            }
            if (mainHand < nextStep)
            {
                nextStep = mainHand;
                //Console.WriteLine("NextStep (MH): " + nextStep);
            }
            if (offHand < nextStep)
            {
                nextStep = offHand;
                //Console.WriteLine("NextStep (OH): " + nextStep);
            }
            if (globalCooldown < nextStep)
            {
                nextStep = globalCooldown;
                // Console.WriteLine("NextStep (GCD): " + nextStep);
            }
            if (abilities < nextStep)
            {
                nextStep = abilities;
                //Console.WriteLine("NextStep (abilities): " + nextStep);
            }
            if (stance < nextStep)
            {
                nextStep = stance;
                //Console.WriteLine("NextStep (stance): " + nextStep);
            }
            //Console.WriteLine("Selected NextStep: " + nextStep);
            return nextStep;

        }

    }
}
