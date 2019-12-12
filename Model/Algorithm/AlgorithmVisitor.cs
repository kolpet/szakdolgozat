using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Algorithm
{
    public class AlgorithmVisitor : IAlgorithmVisitor
    {
        private Action _galeShapleyMethod;

        private Action _geneticMethod;

        public AlgorithmVisitor(Action galeShapleyMethod, 
            Action geneticMethod)
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
            _galeShapleyMethod?.Invoke();
        }

        public void Visit(GeneticAlgorithm element)
        {
            _geneticMethod?.Invoke();
        }
    }
}
