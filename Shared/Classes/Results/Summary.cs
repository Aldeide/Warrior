namespace Warrior
{
    public class DamageResults : ICloneable
    {
        public string name { get; set; } = "";
        public float totalDamage { get; set; } = 0;
        public float hitDamage { get; set; } = 0;
        public float critDamage { get; set; } = 0;
        public float glancingDamage { get; set; } = 0;
        public float numCasts { get; set; } = 0;
        public float numHit { get; set; } = 0;
        public float numCrit { get; set; } = 0;
        public float numDodge { get; set; } = 0;
        public float numMiss { get; set; } = 0;
        public float numGlancing { get; set; } = 0;

        public object Clone()
        {
            var item = new DamageResults()
            {
                name = name,
                totalDamage = totalDamage,
                hitDamage = hitDamage,
                critDamage = critDamage,
                numCasts = numCasts,
                numHit = numHit,
                numCrit = numCrit,
                numDodge = numDodge,
                numMiss = numMiss,
                numGlancing = numGlancing

            };
            return item;
        }

    }
    public class AuraResults : ICloneable
    {
        public string name { get; set; } = "";
        public int uptime { get; set; } = 0;
        public int procs { get; set; } = 0;
        public int refreshes { get; set; } = 0;

        public int totalDamage { get; set; } = 0;

        public int ticks { get; set; } = 0;

        public object Clone()
        {
            var item = new AuraResults()
            {
                name = name,
                uptime = uptime,
                procs = procs,
                refreshes = refreshes,
                totalDamage = totalDamage,
                ticks = ticks
            };
            return item;
        }

        public void Reset()
        {
            uptime = 0;
            procs = 0;
            refreshes = 0;
            totalDamage = 0;
            ticks = 0;
        }
    }
    public class RageResults
    {
        public int wastedRage { get; set; } = 0;
        public int rageGenerated { get; set; } = 0;
        public int rageStarved { get; set; } = 0;

        public Dictionary<string, int> generated { get;set; } = new Dictionary<string, int>();
        public Dictionary<string, int> ticks { get; set; } = new Dictionary<string, int>();
    }
    public class DotDamageResults : ICloneable
    {
        public string name { get; set; } = "";
        public int uptime { get; set; } = 0;
        public int applications { get; set; } = 0;
        public int refreshes { get; set; } = 0;
        public int totalDamage { get; set; } = 0;
        public int ticks { get; set; } = 0;

        public object Clone()
        {
            var item = new DotDamageResults()
            {
                name = name,
                uptime = uptime,
                applications = applications,
                refreshes = refreshes,
                totalDamage = totalDamage,
                ticks = ticks
            };
            return item;
        }

        public void Reset()
        {
            uptime = 0;
            applications = 0;
            refreshes = 0;
            totalDamage = 0;
            ticks = 0;
        }
    }


}
