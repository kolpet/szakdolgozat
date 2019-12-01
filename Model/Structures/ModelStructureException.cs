using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Model.Structures
{
    class ModelStructureException : Exception
    {
        public ModelStructureException() : base() { }

        public ModelStructureException(string message) : base(message) { }
    }
}
