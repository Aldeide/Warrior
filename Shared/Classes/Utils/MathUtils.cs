using System.Runtime.Serialization.Formatters.Binary;

namespace Warrior
{
    public static class MathUtils
    {
        public static float RoundToSignificantDigits(this float f, int digits)
        {
            if (f == 0)
                return 0;

            float scale = (float)Math.Pow(10, (float)Math.Floor(Math.Log10(Math.Abs(f))) + 1);
            return scale * (float)Math.Round(f / scale, digits);
        }

        public static readonly Random random = new Random();
        public static readonly object syncLock = new object();
    }


}
