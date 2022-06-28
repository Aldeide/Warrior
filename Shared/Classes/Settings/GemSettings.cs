using Warrior.Entities;
using Warrior.Databases;

namespace Warrior.Settings
{
	public class GemSettings
	{
		public Dictionary<string, List<Gem>> itemGemsPair { get; set; } = new Dictionary<string, List<Gem>>();

		public List<Gem> GetGemsByItemId(string id)
		{
			var found = itemGemsPair.TryGetValue(id, out var gems);
			if (found && gems != null)
			{
				return gems;
			}
			int itemid = Int32.Parse(id.Split(':')[0]);
			var sockets = ItemDatabase.GetGemSockets(itemid);
			for (int i = 0; i < sockets.Count; i++)
			{
				sockets[i] = new Gem() { id = 0, name = "None" };
			}
			return sockets;
		}

		public List<Gem> GetGemsByItemId(string id, int extraslot)
		{
			int itemid = Int32.Parse(id.Split(':')[0]);
			var sockets = ItemDatabase.GetGemSockets(itemid);

			var found = itemGemsPair.TryGetValue(id, out var gems);
			if (found && gems != null)
			{
				if (gems.Count >= sockets.Count + extraslot)
				{
					return gems;
				}
				for (int i = 0; i < extraslot; i++)
				{
					gems.Add(new Gem() { id = 0, name = "None" });
				}
				return gems;
			}
			for (int i = 0; i < sockets.Count; i++)
			{
				sockets[i] = new Gem() { id = 0, name = "None" };

			}
			for (int i = 0; i < extraslot; i++)
			{
				sockets.Add(new Gem() { id = 0, name = "None" });
			}
			return sockets;
		}

		public void SetGemForItemId(string itemId, int gemId, int index)
		{

			bool success = itemGemsPair.TryGetValue(itemId, out var gems);
			if (success)
			{
				itemGemsPair[itemId][index].id = gemId;
				return;
			}
			itemGemsPair.Add(itemId, GetGemsByItemId(itemId));
			itemGemsPair[itemId][index].id = gemId;
			return;
		}

		public int GetItemGemStats(string itemId, Stat stat)
        {
			var gems = GetGemsByItemId(itemId);
			int output = 0;
			foreach(var gem in gems)
            {
				var actualGem = GemDatabase.gems.Where(g => g.id == gem.id).FirstOrDefault();
				foreach(var effect in actualGem.effects)
                {
					if (effect.stat == stat)
                    {
						output += (int)effect.value;
                    }
                }
            }
			return output;
        }
	}
}
