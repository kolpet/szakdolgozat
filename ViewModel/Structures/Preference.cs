using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.ViewModel.Structures
{
    public class Preference
    {
        public Preference(int id, List<int> preferences)
        {
            ID = id;
            Preferences = preferences;
        }

        public int ID { get; set; }

        public List<int> Preferences { get; set; }
    }
}
