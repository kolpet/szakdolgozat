using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionElement
    {
        AlgorithmOptionGenetic AcceptGetGeneticOption(IAlgorithmOptionVisitor visitor);

        void AcceptReduceIndex(IAlgorithmOptionVisitor visitor);
    }
}
