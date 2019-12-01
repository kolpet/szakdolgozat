using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Algorithm
{
    public interface IAlgorithmVisitor
    {
        void Visit(GaleShapleyAlgorithm element);

        void Visit(GeneticAlgorithm element);
    }
}
