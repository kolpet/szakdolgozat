using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public interface IAlgorithmOptionElement
    {
        void Accept(IAlgorithmOptionVisitor visitor);
    }
}
