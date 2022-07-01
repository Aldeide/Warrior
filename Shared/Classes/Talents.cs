namespace Warrior
{
    [Serializable]
    public enum TalentTree
    {
        Arms,
        Fury,
        Protection
    }
    [Serializable]
    public class Talent
    {
        public string name { get; set; } = "";
        public TalentTree talentTree { get; set; }
        public int maxRank { get; set; }
        public int rank { get; set; }
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public int[] spellIds { get; set; } = new int[0];
        public int currentSpellId { get; set; }

        public Talent()
        {
        }
        public void Increment()
        {
            if (rank < maxRank)
            {
                rank += 1;
            }
        }

        public void Decrement()
        {
            if (rank > 0)
            {
                rank -= 1;
            }
        }

        public void ProcessClick(long button)
        {
            if (button == 0)
            {
                Increment();
            }
            else if (button == 2)
            {
                Decrement();
            }
            if (rank == maxRank)
            {
                currentSpellId = spellIds[rank - 1];
            } else
            {
                currentSpellId = spellIds[rank];
            }
        }
    }
}
