using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public class GaleShapleyAlgorithmSave : AlgorithmSaveBase, IAlgorithmSaveElement
    {
        public override void Accept(IAlgorithmSaveVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
