using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public abstract class AlgorithmSaveBase : IAlgorithmSaveElement
    {
        public string AlgorithmName;

        public virtual void Accept(IAlgorithmSaveVisitor visitor) { }
    }
}
