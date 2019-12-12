using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.ViewModel.Structures
{
    public class AlgorithmOptionVisitorParam : IAlgorithmOptionVisitor
    {
        private Action<AlgorithmOptionGaleShapley> _galeShapleyMethod;

        private Action<AlgorithmOptionGenetic> _geneticMethod;

        public AlgorithmOptionVisitorParam(Action<AlgorithmOptionGaleShapley> galeShapleyMethod,
            Action<AlgorithmOptionGenetic> geneticMethod)
        {
            _galeShapleyMethod = galeShapleyMethod;
            _geneticMethod = geneticMethod;
        }

        public void Visit(IAlgorithmOptionElement element)
        {
            element.Accept(this);
        }

        public void Visit(AlgorithmOptionGaleShapley element)
        {
            _galeShapleyMethod?.Invoke(element);
        }

        public void Visit(AlgorithmOptionGenetic element)
        {
            _geneticMethod?.Invoke(element);
        }
    }
}
