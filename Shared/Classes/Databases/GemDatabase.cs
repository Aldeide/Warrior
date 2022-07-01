using Warrior.Entities;

namespace Warrior.Databases
{
	public static class GemDatabase
	{
		public static List<Gem> gems = new List<Gem>()
		{
			new Gem() { id = 0, name = "None", color = Color.None },
			new Gem() { id = 36766, name = "Bright Dragon's Eye", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.AttackPower, 68)} },
			new Gem() { id = 40111, name = "Bold Cardinal Ruby", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Strength, 20)} },
			new Gem() { id = 40112, name = "Delicate Cardinal Ruby", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Agility, 20)} },
			new Gem() { id = 40114, name = "Bright Cardinal Ruby", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.AttackPower, 20)} },
			new Gem() { id = 40117, name = "Fractured Cardinal Ruby", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.ArmorPenetrationRating, 20)} },
			new Gem() { id = 40118, name = "Precise Cardinal Ruby", color = Color.Red, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.ExpertiseRating, 20)} },

			new Gem() { id = 40124, name = "Smooth King's Amber", color = Color.Yellow, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.CriticalRating, 20)} },
			new Gem() { id = 40125, name = "Rigid King's Amber", color = Color.Yellow, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.HitRating, 20)} },
			new Gem() { id = 40128, name = "Quick King's Amber", color = Color.Yellow, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.HasteRating, 20)} },

			new Gem() { id = 40129, name = "Sovereign Dreadstone", color = Color.Purple, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Strength, 10), new Effect(EffectType.Additive, Stat.Stamina, 15) } },
			new Gem() { id = 40130, name = "Shifting Dreadstone", color = Color.Purple, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Agility, 10), new Effect(EffectType.Additive, Stat.Stamina, 15) } },
			new Gem() { id = 40136, name = "Balanced Dreadstone", color = Color.Purple, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.AttackPower, 20), new Effect(EffectType.Additive, Stat.Stamina, 15) } },
			new Gem() { id = 40140, name = "Puissant Dreadstone", color = Color.Purple, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.ArmorPenetrationRating, 10), new Effect(EffectType.Additive, Stat.Stamina, 15) } },
			new Gem() { id = 40141, name = "Guardian's Dreadstone", color = Color.Purple, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.ExpertiseRating, 10), new Effect(EffectType.Additive, Stat.Stamina, 15) } },

			new Gem() { id = 40142, name = "Inscribed Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Strength, 10), new Effect(EffectType.Additive, Stat.CriticalRating, 10) } },
			new Gem() { id = 40143, name = "Etched Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Strength, 10), new Effect(EffectType.Additive, Stat.HitRating, 10) } },
			new Gem() { id = 40146, name = "Fierce Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Strength, 10), new Effect(EffectType.Additive, Stat.HasteRating, 10) } },
			new Gem() { id = 40147, name = "Deadly Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Agility, 10), new Effect(EffectType.Additive, Stat.CriticalRating, 10) } },
			new Gem() { id = 40148, name = "Glinting Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Agility, 10), new Effect(EffectType.Additive, Stat.HitRating, 10) } },
			new Gem() { id = 40150, name = "Deft Ametrine", color = Color.Orange, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.Agility, 10), new Effect(EffectType.Additive, Stat.HasteRating, 10) } },

			new Gem() { id = 49110, name = "Nightmare Tear", color = Color.Prismatic, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.AllBase, 10) } },

			new Gem() { id = 41266, name = "Chaotic Skyflare Diamond", color = Color.Meta, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.CriticalRating, 21), new Effect(EffectType.Multiplicative, Stat.CriticalDamage, 1.03f) } },
			new Gem() { id = 41339, name = "Swifth Skyflare Diamond", color = Color.Meta, effects = new List<Effect>() {new Effect(EffectType.Additive, Stat.AttackPower, 42) } },

		};
	}
}
