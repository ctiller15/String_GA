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
        public int Fitness { get; set; } = 0;
        public List<string> Genes { get; set; } = new List<string>();

        public void GetLength(int maxLength)
        {
            Length = Services.Rand.Next(maxLength);
            //Console.WriteLine(Length);
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
            //Console.WriteLine(string.Join("", Genes));
        }

        public void CalculateFitness()
        {
            Console.WriteLine(string.Join("", Genes));
            // First calculate based on length.
            int LengthFitness = (Population.Matchedstring.Count() - Math.Abs(Population.Matchedstring.Count() - Length));
            Console.WriteLine($"Length fitness: {LengthFitness}");
            // Then calculate based on having correct letters.


            int StringFitness = 0;
            // If it has the right letter, add to the fitness...
            var strBucket = Services.CreateBucket(Genes);

            foreach (var unichar in Population.MatchedStringBucket)
            {
                if (strBucket.ContainsKey(unichar.Key))
                {
                    // match it ONLY for every character that it has.
                    if (strBucket[unichar.Key] <= unichar.Value)
                    {
                        StringFitness += strBucket[unichar.Key];
                    }
                    else
                    {
                        StringFitness += unichar.Value;
                    }
                }
            }

            Console.WriteLine($"String fitness: {StringFitness}");



            // Then calculate based on having letters in correct position.
        }

        public Individual()
        {
            GetLength(Population.Matchedstring.Count() * 2);
            PopulateGenes();
            CalculateFitness();

        }
    }
}
