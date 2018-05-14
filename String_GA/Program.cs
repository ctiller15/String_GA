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
            Console.WriteLine(Population.Matchedstring);
            var pop = new Population();

            pop.CreatePopulation();

            pop.GetFittest(40);

            pop.CrossPopulation();

            pop.ReplacePopulation();

            // Mutate the population.
            pop.MutateGenes();

            Console.ReadLine();
        }
    }
}
