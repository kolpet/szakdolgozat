using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class AlgorithmOptionGaleShapley : AlgorithmOptionBase, IAlgorithmOptionElement
    {
        public AlgorithmOptionGaleShapley(string name, int index) : base(name, index)
        {

        }

        public void Accept(IAlgorithmOptionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
