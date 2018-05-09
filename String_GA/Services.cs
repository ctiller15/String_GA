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

        static public void CreateBucket(List<string> str)
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

            Console.WriteLine(string.Join("",str));
            foreach(var item in groupDict)
            {
                Console.WriteLine($"{string.Join(",",item.Key)}, {item.Value}");
            }
            Console.WriteLine("\n");
        }
    }
}
