﻿namespace Warrior.Entities
{
	public enum Color
	{
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
	}
}
