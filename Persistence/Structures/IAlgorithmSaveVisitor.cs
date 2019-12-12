using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Persistence.Structures
{
    public interface IAlgorithmSaveVisitor
    {
        void Visit(IAlgorithmSaveElement element);

        void Visit(GaleShapleyAlgorithmSave element);

        void Visit(GeneticAlgorithmSave element);
    }
}
