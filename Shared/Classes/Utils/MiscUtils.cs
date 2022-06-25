namespace Warrior.Utils
{
    public static class MiscUtils
    {
        public static string CombineItemAndSlot(int itemid, ItemSlot slot)
        {
            return itemid.ToString() + ":" + slot.ToString();
        }
    }
}
