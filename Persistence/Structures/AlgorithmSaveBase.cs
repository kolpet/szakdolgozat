using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public abstract class AlgorithmSaveBase
    {
        public int AlgorithmType;

        public string AlgorithmName;
    }
}
