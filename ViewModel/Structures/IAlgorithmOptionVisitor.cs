using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionVisitor
    {
        void Visit(IAlgorithmOptionElement element);

        void Visit(AlgorithmOptionGenetic element);

        void Visit(AlgorithmOptionGaleShapley element);
    }
}
