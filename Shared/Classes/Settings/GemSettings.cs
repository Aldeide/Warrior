using Warrior.Entities;
using Warrior.Databases;

namespace Warrior.Settings
{
	public class GemSettings
	{
		public Dictionary<int, List<Gem>> itemGemsPair { get; set; } = new Dictionary<int, List<Gem>>();

		public List<Gem> GetGemsByItemId(int id)
		{
			var found = itemGemsPair.TryGetValue(id, out var gems);
			if (found && gems != null)
			{
				return gems;
			}
			var sockets = ItemDatabase.GetGemSockets(id);
			for (int i = 0; i < sockets.Count; i++)
			{
				sockets[i] = new Gem() { id = 0, name = "None" };
			}
			return sockets;
		}

		public void SetGemForItemId(int itemId, int gemId, int index)
		{

			bool success = itemGemsPair.TryGetValue(index, out var gems);
			if (success)
			{
				itemGemsPair[itemId][index].id = gemId;
				return;
			}
			itemGemsPair.Add(itemId, GetGemsByItemId(itemId));
			itemGemsPair[itemId][index].id = gemId;
			return;
		}
	}
}
