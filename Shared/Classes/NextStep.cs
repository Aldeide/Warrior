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
            }
            if (auras < nextStep)
            {
                nextStep = auras;
            }
            if (mainHand < nextStep)
            {
                nextStep = mainHand;
            }
            if (offHand < nextStep)
            {
                nextStep = offHand;
            }
            if (globalCooldown < nextStep)
            {
                nextStep = globalCooldown;
            }
            if (abilities < nextStep)
            {
                nextStep = abilities;
            }
            return nextStep;
        }

    }
}
