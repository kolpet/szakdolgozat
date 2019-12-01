using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public class AlgorithmOptionVisitor : IAlgorithmOptionVisitor
    {
        public GeneticSettings GetGeneticSettings(AlgorithmOptionPanel panel)
        {
            return panel.AlgorithmOption.AcceptGetGeneticSettings(this);
        }

        public GeneticSettings GetGeneticSettings(AlgorithmOptionGenetic element)
        {
            return element.Settings;
        }

        public GeneticSettings GetGeneticSettings(AlgorithmOptionGaleShapley element)
        {
            return null;
        }

        public void SetGeneticSettings(AlgorithmOptionPanel panel, GeneticSettings settings)
        {
            panel.AlgorithmOption.AcceptSetGeneticSettings(this, settings);
        }

        public void SetGeneticSettings(AlgorithmOptionGenetic element, GeneticSettings settings)
        {
            element.Settings = settings;
        }

        public void SetGeneticSettings(AlgorithmOptionGaleShapley element, GeneticSettings settings)
        {
            //Nothing
        }
    }
}
