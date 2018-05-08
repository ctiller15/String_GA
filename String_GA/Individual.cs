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
        public List<string> Genes { get; set; } = new List<string>();

        public void GetLength(int maxLength)
        {
            Length = Services.Rand.Next(maxLength);
            Console.WriteLine(Length);
        }

        public char GetRandomCharacter()
        {
            // 32 = min, 32 + 94 = max.
            int minVal = 32;
            int maxVal = 126;
            int num = Services.Rand.Next(minVal, maxVal + 1);
            char letter = Convert.ToChar(num);
            return letter;
        }

        public void PopulateGenes()
        {
            for(int i = 0; i < Length; i++)
            {
                var strChar = GetRandomCharacter();

                Genes.Add(Convert.ToString(GetRandomCharacter()));
            }
            Console.WriteLine(string.Join("", Genes));
        }

        public Individual()
        {
            GetLength(Population.Matchedstring.Count() * 2);
            PopulateGenes();

        }
    }
}
