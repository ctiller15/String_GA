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
            //Console.WriteLine($"Length fitness: {LengthFitness}");
            // Then calculate based on having correct letters.

            // If it has the right letter, add to the fitness...
            var strBucket = Services.CreateBucket(Genes);

            // Probably weight a little heavier.
            int StringFitness = CalculateStringFitness(strBucket);

            //Console.WriteLine($"String fitness: {StringFitness}");

            // Then calculate based on having letters in correct position.
            int MatchFitness = 0;

            // If the length of the genestring is greater than the length of the matching string...

            MatchFitness = CalculateMatchFitness();

            //Console.WriteLine($"Match Fitness: {MatchFitness}");

            int TotalFitness = CalculateTotalFitness(LengthFitness, StringFitness, MatchFitness);

            Console.WriteLine($"Total Fitness: {TotalFitness}");

            Fitness = TotalFitness;
        }

        public int CalculateStringFitness(Dictionary<char,int> bucket)
        {
            int StringFitness = 0;
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

            return StringFitness;
        }

        public int CalculateMatchFitness()
        {
            int MatchFitness = 0;

            var temp = Genes.Select(s => Convert.ToChar(s)).ToList();

            if (Genes.Count() >= Population.Matchedstring.Count())
            {
                MatchFitness = Services.CountMatchedChars(temp, new List<char>(Population.Matchedstring));
            }
            else
            {
                MatchFitness = Services.CountMatchedChars(new List<char>(Population.Matchedstring), temp);
            }
            return MatchFitness;
        }

        static int CalculateTotalFitness(int lenFit, int strFit, int matchFit)
        {
            int TotalFitness = lenFit + (strFit * 3) + (matchFit * 5);
            return TotalFitness;
        }

        public Individual()
        {
            GetLength(Population.Matchedstring.Count() * 2);
            PopulateGenes();
            CalculateFitness();
        }
    }
}
