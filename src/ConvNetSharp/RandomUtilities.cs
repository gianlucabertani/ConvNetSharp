using System;

namespace ConvNetSharp
{
    public class RandomUtilities
    {
        private readonly Random Random = new Random(Seed);

        private double val;
        private bool returnVal;

        public static int Seed
        {
            get { return (int)DateTime.Now.Ticks; }
        }

        public double GaussianRandom()
        {
            if (returnVal)
            {
                returnVal = false;
                return val;
            }

            var u = 2 * Random.NextDouble() - 1;
            var v = 2 * Random.NextDouble() - 1;
            var r = u * u + v * v;

            if (r == 0 || r > 1)
            {
                return GaussianRandom();
            }

            var c = Math.Sqrt(-2 * Math.Log(r) / r);
            val = v*c; // cache this
            returnVal = true;

            return u * c;
        }

        public double Randn(double mu, double std)
        {
            return mu + GaussianRandom() * std;
        }
    }
}