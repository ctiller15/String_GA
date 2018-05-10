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
            //Console.WriteLine($"{string.Join(",",str.Distinct())} , {string.Join(",",str.Distinct().Count())}");
            var groupBucket = str
                .GroupBy(g => g)
                .Select(s => new
                {
                    Letter = s.Distinct().First(),
                    Count = s.Count()
                });

            Dictionary<char, int> groupDict = groupBucket.ToDictionary(TD => Convert.ToChar(TD.Letter), TD => TD.Count);

            //Console.WriteLine(string.Join("",str));
            //foreach(var item in groupDict)
            //{
            //    Console.WriteLine($"{string.Join(",",item.Key)}, {item.Value}");
            //}
            //Console.WriteLine("\n");

            return groupDict;
        }

        static public int CountMatchedChars(List<char> let1, List<char> let2)
        {
            int count = 0;
            //if (Genes.Count() >= Population.Matchedstring.Count())
            //{
            //    for (int i = 0; i < Population.Matchedstring.Count(); i++)
            //    {
            //        if (temp[i] == Population.Matchedstring[i])
            //        {
            //            MatchFitness += 1;
            //        }
            //    }
            //}

            //if(let1.Count() >= let2.Count())
            //{
            for(int i = 0; i < let2.Count(); i++)
            {
                if(let1[i] == let2[i])
                {
                    count += 1;
                }
            }
            //} else
            //{
            //    for (int i = 0; i < let1.Count(); i++)
            //    {
            //        if (let2[i] == let1[i])
            //        {
            //            count += 1;
            //        }
            //    }
            //}

            return count;

        }
    }
}
