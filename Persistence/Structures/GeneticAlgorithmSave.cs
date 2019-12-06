using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Common;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public class GeneticAlgorithmSave : AlgorithmSaveBase, IGeneticSettings
    {
        public double AbsoluteSelection { get; set; }

        public double SelectionRate { get; set; }

        public double MutationChance { get; set; }

        public double StablePairWeight { get; set; }

        public double GroupHappinessWeight { get; set; }

        public double EgalitarianHappinessWeight { get; set; }

        public int Size { get; set; }

        public int Generations { get; set; }
    }
}
