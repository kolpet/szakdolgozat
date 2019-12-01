using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Persistence
{
    public abstract class IPersistence
    {
        public abstract void Save(SaveData data, string path);

        public abstract SaveData Load(string path);
    }
}
