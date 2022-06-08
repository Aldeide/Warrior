using Warrior.Entities;
namespace Warrior.Settings
{
	public class GemSettings
	{
		public Dictionary<int, List<Gem>> itemGemsPair = new Dictionary<int, List<Gem>>();

		public List<Gem> GetGemsByItemId(int id)
		{
			var found = itemGemsPair.TryGetValue(id, out var gems);
			if (found && gems != null)
			{
				return gems;
			}
			return new List<Gem>();
		}
	}
}
