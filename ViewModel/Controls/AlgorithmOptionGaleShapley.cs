using Szakdolgozat.ViewModel.Structures;

namespace Szakdolgozat.ViewModel.Controls
{
    public class AlgorithmOptionGaleShapley : AlgorithmOptionBase, IAlgorithmOptionElement
    {
        public AlgorithmOptionGaleShapley(string name, int index) : base(name, index)
        {

        }

        public AlgorithmOptionGenetic AcceptGetGeneticOption(IAlgorithmOptionVisitor visitor)
        {
            return visitor.GetGeneticOption(this);
        }

        public void AcceptReduceIndex(IAlgorithmOptionVisitor visitor)
        {
            Index--;
        }
    }
}
