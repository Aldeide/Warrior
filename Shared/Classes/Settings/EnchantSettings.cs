using Warrior.Databases;

namespace Warrior.Settings
{
	public class EnchantSettings
	{
		public Dictionary<ItemSlot, Enchant> enchants { get; set; }
		public EnchantSettings()
		{
			enchants = new Dictionary<ItemSlot, Enchant>();
			enchants[ItemSlot.Head] = EnchantDatabase.GetEnchantById(59954);
			enchants[ItemSlot.Shoulders] = EnchantDatabase.GetEnchantById(59934);
			enchants[ItemSlot.Back] = EnchantDatabase.GetEnchantById(60663);
			enchants[ItemSlot.Chest] = EnchantDatabase.GetEnchantById(60692);
			enchants[ItemSlot.Wrist] = EnchantDatabase.GetEnchantById(44575);
			enchants[ItemSlot.Hands] = EnchantDatabase.GetEnchantById(60668);
			enchants[ItemSlot.Waist] = EnchantDatabase.GetEnchantById(55655);
			enchants[ItemSlot.Legs] = EnchantDatabase.GetEnchantById(38374);
			enchants[ItemSlot.Boots] = EnchantDatabase.GetEnchantById(60763);
			enchants[ItemSlot.Ring1] = EnchantDatabase.GetEnchantById(0);
			enchants[ItemSlot.Ring2] = EnchantDatabase.GetEnchantById(0);
			enchants[ItemSlot.MainHand] = EnchantDatabase.GetEnchantById(59621);
			enchants[ItemSlot.OffHand] = EnchantDatabase.GetEnchantById(59621);
		}

		public void SetEnchant(ItemSlot slot, int id)
		{
			enchants[slot] = EnchantDatabase.GetEnchantById(id);
		}

		public Enchant GetEnchant(ItemSlot slot)
		{
			return enchants[slot];
		}
	}
}
