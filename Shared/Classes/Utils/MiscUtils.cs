namespace Warrior.Utils
{
    public static class MiscUtils
    {
        public static string CombineItemAndSlot(int itemid, ItemSlot slot)
        {
            return itemid.ToString() + ":" + slot.ToString();
        }
        public static void Log(Iteration iteration, string message)
        {
            if (iteration.index == 0)
            {
                Console.WriteLine("[ " + Math.Round((double)((float)iteration.currentStep / Constants.kStepsPerSecond), 3) + " ] " + message);
            }
        }
    }
}
