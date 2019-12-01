using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Model.Algorithm
{
    public class AlgorithmException : Exception
    {
        public AlgorithmException() : base() { }

        public AlgorithmException(string message) : base(message) { }
    }
}
