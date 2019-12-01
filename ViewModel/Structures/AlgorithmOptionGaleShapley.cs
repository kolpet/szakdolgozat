using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.ViewModel.Structures
{
    public class AlgorithmOptionGaleShapley : AlgorithmOptionBase, IAlgorithmOptionElement
    {
        public AlgorithmOptionGaleShapley()
        {
            Name = "Gale-Shapley Algoritmus";
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
