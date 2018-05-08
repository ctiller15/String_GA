using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_GA
{
    class Individual
    {
        public int Length { get; set; }
        public int Fitness { get; set; }
        public List<string> Genes { get; set; }

        public void GetLength(int maxLength)
        {
            Length = Services.Rand.Next(maxLength);
            Console.WriteLine(Length);
        }

        public Individual()
        {
            GetLength(Population.Matchedstring.Count() * 2);
        }
    }
}
