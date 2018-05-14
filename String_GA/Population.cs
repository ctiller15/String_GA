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
        public int PopSize { get; set; } = 500;
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
                //Console.WriteLine($"{string.Join("", item.Genes)}, fitness: {item.Fitness}");
            }
        }

        // Crosses by using fittest of population.
        public void CrossPopulation()
        {
            NextPop = new List<Individual>();

            for(int i = 0; i < PopSize; i++)
            {
                //Console.WriteLine("\n");

                var SplitGenesA = SliceDice();
                var SplitGenesB = SliceDice();

                //Console.WriteLine($"{String.Join("", SplitGenesA[0])} , { String.Join("", SplitGenesA[1])}");
                //Console.WriteLine($"{String.Join("", SplitGenesB[0])} , { String.Join("", SplitGenesB[1])}");

                List<string> genesa = SplitGenesA[0].Concat(SplitGenesB[1]).ToList();

                List<string> genesb = SplitGenesB[0].Concat(SplitGenesA[1]).ToList();

                //Console.WriteLine(string.Join("", genesa));
                //Console.WriteLine(string.Join("", genesb));
                Individual childa = new Individual(genesa);
                Individual childb = new Individual(genesb);

                //Console.WriteLine(childa.Fitness);
                //Console.WriteLine(childb.Fitness);

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

        public IEnumerable<string>[] SliceDice()
        {
            var randIndividual = FittestPop[Services.Rand.Next(FittestPop.Count())];
            var randGauss = Services.RandGaussian();
            var clamped = randGauss > 1.00 ? 1.00 : randGauss < 0 ? 0 : randGauss;

            // Create the slice point, more weighted towards the center via a gaussian distribution.
            int slicePoint = (int)Math.Floor(clamped * randIndividual.Genes.Count());

            // Create the individual cuts for each gene.
            var second = randIndividual.Genes.Skip(slicePoint);
            var first = randIndividual.Genes.Take(slicePoint);
            return new IEnumerable<string>[] { first, second };
        }



        public void ReplacePopulation()
        {
            Populus = NextPop;
            NextPop = null;
        }

        public void MutateGenes()
        {
            foreach (var ind in Populus)
            {
                ind.MutateSelf();
            }
        }

    }
}
