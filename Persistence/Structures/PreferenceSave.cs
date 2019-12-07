using System;
using System.Collections.Generic;
using System.Text;

namespace Szakdolgozat.Persistence.Structures
{
    [Serializable]
    public class PreferenceSave
    {
        public int Id;

        public List<int> Preferences;
    }
}
