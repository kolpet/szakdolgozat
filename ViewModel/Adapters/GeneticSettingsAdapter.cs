using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace ViewModel.Adapters
{
    public class GeneticSettingsAdapter : AlgorithmOptionGenetic
    {
        public GeneticSettingsAdapter(GeneticSettings settings) : base()
        {
            SelectionRate = settings.SelectionRate * 100;
            AbsoluteSelection = settings.AbsoluteSelection * 100;
            MutationChance = settings.MutationChance * 100;
            StablePairWeight = settings.StablePairWeight;
            GroupHappinessWeight = settings.GroupHappinessWeight;
            EgalitarianHappinessWeight = settings.EgalitarianHappinessWeight;
            Size = settings.Size;
            Generations = settings.Generations;
        }
    }
}
