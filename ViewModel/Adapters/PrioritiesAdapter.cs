using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Common;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace ViewModel.Adapters
{
    public class PrioritiesAdapter : List<Preference>
    {
        private IPriorities _priorities;

        public PrioritiesAdapter(IPriorities priorities) : base(priorities.GetDictionary().Select(x => new Preference(x.Key, x.Value)))
        {
            _priorities = priorities;
        }
    }
}
