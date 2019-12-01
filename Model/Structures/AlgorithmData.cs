using System;
using System.Collections.Generic;
using System.Text;
using Szakdolgozat.Model.Algorithm;

namespace Szakdolgozat.Model.Structures
{
    public class AlgorithmData
    {
        public string Name { get; set; }

        public AlgorithmBase Algorithm { get; set; }

        public IAlgorithmElement Element { get; set; }
    }
}
