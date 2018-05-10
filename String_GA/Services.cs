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
