using System;

namespace Szakdolgozat.Persistence.Structures
{
    public class AlgorithmSaveVisitorParam : IAlgorithmSaveVisitor
    {
        private Action<GaleShapleyAlgorithmSave> _galeShapleyMethod;

        private Action<GeneticAlgorithmSave> _geneticMethod;

        public AlgorithmSaveVisitorParam(Action<GaleShapleyAlgorithmSave> galeShapleyMethod,
            Action<GeneticAlgorithmSave> geneticMethod)
        {
            _galeShapleyMethod = galeShapleyMethod;
            _geneticMethod = geneticMethod;
        }

        public void Visit(IAlgorithmSaveElement element)
        {
            element.Accept(this);
        }

        public void Visit(GaleShapleyAlgorithmSave element)
        {
            _galeShapleyMethod?.Invoke(element);
        }

        public void Visit(GeneticAlgorithmSave element)
        {
            _geneticMethod?.Invoke(element);
        }
    }
}
