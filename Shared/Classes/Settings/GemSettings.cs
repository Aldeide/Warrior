using Warrior.Entities;
namespace Warrior.Settings
{
	public class GemSettings
	{
		public Dictionary<int, List<Gem>> itemGemsPair = new Dictionary<int, List<Gem>>();

		public List<Gem>? GetGemsByItemId(int id)
		{
			return itemGemsPair[id];
		}
	}
}
