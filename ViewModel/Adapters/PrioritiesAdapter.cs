using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.ViewModel.Structures;

namespace ViewModel.Adapters
{
    public class PrioritiesAdapter : List<Preference>
    {
        private Priorities _priorities;

        public PrioritiesAdapter(Priorities priorities) : base(priorities.Select(x => new Preference(x.Key, x.Value)))
        {
            _priorities = priorities;
        }
    }
}
