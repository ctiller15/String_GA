using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_GA
{
    class Services
    {
        static public Random Rand = new Random();

        // Creates a random number based on a Gaussian.
        //https://stackoverflow.com/questions/218060/random-gaussian-variables?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        static public double RandGaussian()
        {
            int mean = 0;
            double stdDev = .3;
            double u1 = 1.0 - Rand.NextDouble();
            double u2 = 1.0 - Rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = (mean + stdDev * randStdNormal + 1) / 2; // Shift gaussian over to the right.
            Console.WriteLine(randNormal);
            return randNormal;
        }

        static public Dictionary<char, int> CreateBucket(List<string> str)
        {
           var groupBucket = str
                .GroupBy(g => g)
                .Select(s => new
                {
                    Letter = s.Distinct().First(),
                    Count = s.Count()
                });

            Dictionary<char, int> groupDict = groupBucket.ToDictionary(TD => Convert.ToChar(TD.Letter), TD => TD.Count);

            return groupDict;
        }

        static public int CountMatchedChars(List<char> let1, List<char> let2)
        {
            int count = 0;

            for(int i = 0; i < let2.Count(); i++)
            {
                if(let1[i] == let2[i])
                {
                    count += 1;
                }
            }

            return count;

        }
    }
}
