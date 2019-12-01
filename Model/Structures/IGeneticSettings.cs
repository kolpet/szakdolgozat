using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Structures
{
    public interface IGeneticSettings
    {
        double AbsoluteSelection { get; set; }

        double SelectionRate { get; set; }

        double MutationChance { get; set; }

        double StablePairWeight { get; set; }

        double GroupHappinessWeight { get; set; }

        double EgalitarianHappinessWeight { get; set; }

        int Size { get; set; }

        int Generations { get; set; }
    }
}
