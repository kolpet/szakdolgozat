using Szakdolgozat.Common;
using Szakdolgozat.Model.Algorithm;

namespace Szakdolgozat.Model.Structures
{
    public class AlgorithmData : IAlgorithmData
    {
        public string Name { get; set; }

        public AlgorithmBase Algorithm { get; set; }

        public IAlgorithmElement Element { get; set; }
    }
}
