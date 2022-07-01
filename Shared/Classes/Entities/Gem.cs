namespace Warrior.Entities
{
	public enum Color
	{
		None,
		Red,
		Blue,
		Yellow,
		Green,
		Purple,
		Orange,
		Meta,
		Prismatic
	}
	public class Gem
	{
		public int id { get; set; }
		public string name { get; set; }
		public Color color { get; set; }
		public List<Effect> effects { get; set; } = new List<Effect>();

		public string Description()
		{
			string output = "";
			foreach(Effect effect in effects)
			{
				output += "+";
				output += effect.value.ToString() + " ";
				output += Utils.EnumUtils.GetDescription(effect.stat) + " ";
			}
			return output;
		}
	}
}
