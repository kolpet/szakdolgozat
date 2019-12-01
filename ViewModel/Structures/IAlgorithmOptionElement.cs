using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionElement
    {
        GeneticSettings AcceptGetGeneticSettings(IAlgorithmOptionVisitor visitor);

        void AcceptSetGeneticSettings(IAlgorithmOptionVisitor visitor, GeneticSettings settings);
    }
}
