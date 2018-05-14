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
        }

        public void CalculateFitness()
        {
            //Console.WriteLine(string.Join("", Genes));
            // First calculate based on length.
            int LengthFitness = (Population.Matchedstring.Count() - Math.Abs(Population.Matchedstring.Count() - Length));

            // Then calculate based on having correct letters.
            // If it has the right letter, add to the fitness...
            var strBucket = Services.CreateBucket(Genes);

            // Probably weight a little heavier.
            int StringFitness = CalculateStringFitness(strBucket);

            // Then calculate based on having letters in correct position.
            int MatchFitness = 0;

            MatchFitness = CalculateMatchFitness();

            int TotalFitness = CalculateTotalFitness(LengthFitness, StringFitness, MatchFitness);

            //Console.WriteLine($"Total Fitness: {TotalFitness}");

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

        public void MutateSelf()
        {
            //int numChars = 94;
            List<string> MutatedGenes = new List<string>();

            foreach(var gene in Genes)
                if(Services.Rand.Next() % (Population.Matchedstring.Count()) <= 1)
                {
                    MutatedGenes.Add(Convert.ToString(GetRandomCharacter()));
                } else
                {
                    MutatedGenes.Add(gene);
                }

            //Console.WriteLine($"{String.Join("", Genes)} , {Fitness}");
            Genes = MutatedGenes;

            CalculateFitness();

            //Console.WriteLine($"{String.Join("", MutatedGenes)} , {Fitness}");
        }

        static int CalculateTotalFitness(int lenFit, int strFit, int matchFit)
        {
            int TotalFitness = (lenFit * 2) + (strFit * 5) + (matchFit * 10);
            return TotalFitness;
        }

        public Individual()
        {
            GetLength(Population.Matchedstring.Count() * 2);
            PopulateGenes();
            CalculateFitness();
        }

        public Individual(List<string> genes)
        {
            Genes = genes;
            Length = Genes.Count();
            CalculateFitness();
        }
    }
}
