using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_GA
{
    class Population
    {
        public static string Matchedstring = "string";
        public static Dictionary<char, int> MatchedStringBucket { get; set; } = Services.CreateBucket(Matchedstring.ToCharArray().Select(s => s.ToString()).ToList());
        public int PopSize { get; set; } = 10;
        public List<Individual> Populus { get; set; } = new List<Individual>();
        public List<Individual> NextPop { get; set; } = new List<Individual>();
        public List<Individual> FittestPop { get; set; }

        public void CreatePopulation()
        {
            for(int i = 0; i < PopSize; i++)
            {
                Populus.Add(new Individual());
            }
        }

        public void GetFittest(int percentageNum)
        {
            int percentageCount = (percentageNum * PopSize) / 100;
            FittestPop = Populus.OrderByDescending(o => o.Fitness).Take(percentageCount).ToList();
            foreach(var item in FittestPop)
            {
                Console.WriteLine($"{string.Join("", item.Genes)}, fitness: {item.Fitness}");
            }
        }

        // Crosses by using fittest of population.
        public void CrossPopulation()
        {
            NextPop = new List<Individual>();

            for(int i = 0; i < PopSize; i++)
            {
                Console.WriteLine("\n");
                var randIndividuala = FittestPop[Services.Rand.Next(FittestPop.Count())];
                var randIndividualb = FittestPop[Services.Rand.Next(FittestPop.Count())];
                Console.WriteLine($"{string.Join("", randIndividuala.Genes)} , {string.Join("", randIndividualb.Genes)}");

                // Create the slice points, more weighted towards the center via a gaussian distribution.
                var randGaussA = Services.RandGaussian();
                var randGaussB = Services.RandGaussian();

                var clampedA = randGaussA > 1.00 ? 1.00 : randGaussA < 0 ? 0 : randGaussA;
                var clampedB = randGaussB > 1.00 ? 1.00 : randGaussB < 0 ? 0 : randGaussB;

                int slicePointa = (int) Math.Floor(clampedA * randIndividuala.Genes.Count());
                int slicePointb = (int) Math.Floor(clampedB * randIndividualb.Genes.Count());

                Console.WriteLine($"a:{slicePointa} b:{slicePointb}");

                var seconda = randIndividuala.Genes
                            .Skip(slicePointa);

                var secondb = randIndividualb.Genes
                            .Skip(slicePointb);

                Console.WriteLine($"{string.Join("", randIndividuala.Genes.Take(slicePointa))} , {string.Join("", randIndividualb.Genes.Take(slicePointb))}");
                Console.WriteLine($"{string.Join("", seconda)} , {string.Join("", secondb)}");


                List<string> genesa = randIndividuala.Genes
                                        .Take(slicePointa)
                                        .Concat(secondb)
                                        .ToList();

                List<string> genesb = randIndividualb.Genes
                                        .Take(slicePointb)
                                        .Concat(seconda)
                                        .ToList();

                Console.WriteLine(string.Join("", genesa));
                Console.WriteLine(string.Join("", genesb));
                Individual childa = new Individual(genesa);
                Individual childb = new Individual(genesb);

                Console.WriteLine(childa.Fitness);
                Console.WriteLine(childb.Fitness);

                if(childa.Fitness >= childb.Fitness)
                {
                    NextPop.Add(childa);
                } else
                {
                    NextPop.Add(childb);
                }


            }

            //return NextPop;

        }

        public void ReplacePopulation()
        {
            Populus = NextPop;
            NextPop = null;
        }

    }
}
