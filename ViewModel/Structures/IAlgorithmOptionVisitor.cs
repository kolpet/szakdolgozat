using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionVisitor
    {
        GeneticSettings GetGeneticSettings(AlgorithmOptionPanel panel);

        GeneticSettings GetGeneticSettings(AlgorithmOptionGenetic element);

        GeneticSettings GetGeneticSettings(AlgorithmOptionGaleShapley element);

        void SetGeneticSettings(AlgorithmOptionPanel panel, GeneticSettings settings);

        void SetGeneticSettings(AlgorithmOptionGenetic element, GeneticSettings settings);

        void SetGeneticSettings(AlgorithmOptionGaleShapley element, GeneticSettings settings);
    }
}
