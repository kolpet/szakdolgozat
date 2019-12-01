using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Structures;

namespace Szakdolgozat.ViewModel.Structures
{
    public abstract class AlgorithmOptionBase
    {
        public string Name { get; set; }
    }

    public abstract class AlgorithmOptionBase<T> : AlgorithmOptionBase where T : ISettings
    {
        public T Settings { get; set; }
    }
}
