using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_GA
{
    class Program
    {
        static void Main(string[] args)
        {
            int generationCount = 0;

            Console.WriteLine(Population.Matchedstring);
            var pop = new Population();

            // Creates population and calculates fitness for every individual.
            pop.CreatePopulation();



            // Gets the top % of fittest individuals.
            pop.GetFittest(40);

            Console.WriteLine($"Generation: {generationCount} , Fittest: {String.Join("",pop.FittestPop[0].Genes)} , Fitness: {pop.FittestPop[0].Fitness}");

            while((String.Join("", pop.FittestPop[0].Genes) != Population.Matchedstring && generationCount < 500)){
                generationCount++;
                // Crosses entire population.
                pop.CrossPopulation();

                pop.GetFittest(40);
                //foreach(var ind in pop.Populus)
                //{
                //    Console.WriteLine(String.Join(",",ind.Genes));
                //}

                // Replaces entire population.
                pop.ReplacePopulation();

                // Mutate the population.
                pop.MutateGenes();
                Console.WriteLine($"Generation: {generationCount} , Fittest: {String.Join("", pop.FittestPop[0].Genes)} , Fitness: {pop.FittestPop[0].Fitness}");
            }


            Console.ReadLine();
        }
    }
}
