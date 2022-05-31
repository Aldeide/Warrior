namespace Warrior
{
    public class DamageSummary : ICloneable
    {
        public string name { get; set; } = "";
        public int totalDamage { get; set; } = 0;
        public int hitDamage { get; set; } = 0;
        public int critDamage { get; set; } = 0;
        public int glancingDamage { get; set; } = 0;
        public int numCasts { get; set; } = 0;
        public int numHit { get; set; } = 0;
        public int numCrit { get; set; } = 0;
        public int numDodge { get; set; } = 0;
        public int numMiss { get; set; } = 0;
        public int numGlancing { get; set; } = 0;

        public object Clone()
        {
            var item = new DamageSummary()
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
    public class AuraSummary : ICloneable
    {
        public string name { get; set; } = "";
        public int uptime { get; set; } = 0;
        public int procs { get; set; } = 0;
        public int refreshes { get; set; } = 0;

        public int totalDamage { get; set; } = 0;

        public int ticks { get; set; } = 0;

        public object Clone()
        {
            var item = new AuraSummary()
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
    public class RageSummary
    {
        public int wastedRage { get; set; } = 0;
        public int rageGenerated { get; set; } = 0;
        public int rageStarved { get; set; } = 0;
    }

    public class DotDamageSummary : ICloneable
    {
        public string name { get; set; } = "";
        public int uptime { get; set; } = 0;
        public int applications { get; set; } = 0;
        public int refreshes { get; set; } = 0;
        public int totalDamage { get; set; } = 0;
        public int ticks { get; set; } = 0;

        public object Clone()
        {
            var item = new DotDamageSummary()
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
