using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Structures
{
    public class AlgorithmOptionGenetic : AlgorithmOptionBase<GeneticSettings>, IAlgorithmOptionElement
    {
        public double AbsoluteSelection { get; set; }

        public double SelectionRate { get; set; }

        public double MutationChance { get; set; }

        public double StablePairWeight { get; set; }

        public double GroupHappinessWeight { get; set; } 

        public double EgalitarianHappinessWeight { get; set; }

        public int Size { get; set; }

        public int Generations { get; set; }

        public AlgorithmOptionGenetic()
        {
            Name = "Genetikus Algoritmus";
        }

        public GeneticSettings AcceptGetGeneticSettings(IAlgorithmOptionVisitor visitor)
        {
            return visitor.GetGeneticSettings(this);
        }

        public void AcceptSetGeneticSettings(IAlgorithmOptionVisitor visitor, GeneticSettings settings)
        {
            visitor.SetGeneticSettings(this, settings);
        }
    }
}
