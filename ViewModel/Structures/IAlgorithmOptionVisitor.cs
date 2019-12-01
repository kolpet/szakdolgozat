using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionVisitor
    {
        AlgorithmOptionGenetic GetGeneticOption(IAlgorithmOptionElement element);

        AlgorithmOptionGenetic GetGeneticOption(AlgorithmOptionGenetic element);

        AlgorithmOptionGenetic GetGeneticOption(AlgorithmOptionGaleShapley element);

        void ReduceIndex(IAlgorithmOptionElement element);
    }
}
