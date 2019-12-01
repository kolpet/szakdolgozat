using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public class AlgorithmOptionVisitor : IAlgorithmOptionVisitor
    {
        public AlgorithmOptionGenetic GetGeneticOption(IAlgorithmOptionElement element)
        {
            return element.AcceptGetGeneticOption(this);
        }

        public AlgorithmOptionGenetic GetGeneticOption(AlgorithmOptionGenetic element)
        {
            return element;
        }

        public AlgorithmOptionGenetic GetGeneticOption(AlgorithmOptionGaleShapley element)
        {
            return null;
        }

        public void ReduceIndex(IAlgorithmOptionElement element)
        {
            element.AcceptReduceIndex(this);
        }
    }
}
