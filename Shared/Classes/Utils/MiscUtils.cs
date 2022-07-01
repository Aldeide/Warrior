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
                Console.WriteLine("[ " + Math.Round((double)(iteration.currentStep / Constants.kStepsPerSecond), 2) + " ] " + message);
            }
        }
    }
}
