﻿using System;
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

        public void CreatePopulation()
        {
            for(int i = 0; i < PopSize; i++)
            {
                Populus.Add(new Individual());
            }
        }
    }
}
