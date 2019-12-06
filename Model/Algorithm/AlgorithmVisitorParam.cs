using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Algorithm
{
    public class AlgorithmVisitorParam : IAlgorithmVisitor
    {
        private Action<GaleShapleyAlgorithm> _galeShapleyMethod;

        private Action<GeneticAlgorithm> _geneticMethod;

        public AlgorithmVisitorParam(Action<GaleShapleyAlgorithm> galeShapleyMethod,
            Action<GeneticAlgorithm> geneticMethod)
        {
            _galeShapleyMethod = galeShapleyMethod;
            _geneticMethod = geneticMethod;
        }

        public void Visit(IAlgorithmElement element)
        {
            element.Accept(this);
        }

        public void Visit(GaleShapleyAlgorithm element)
        {
            _galeShapleyMethod.Invoke(element);
        }

        public void Visit(GeneticAlgorithm element)
        {
            _geneticMethod.Invoke(element);
        }
    }
}
