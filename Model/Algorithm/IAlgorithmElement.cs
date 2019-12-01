using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Model.Algorithm
{
    public interface IAlgorithmElement
    {
        void Accept(IAlgorithmVisitor visitor);
    }
}
