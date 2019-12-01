using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Persistence
{
    public class TextFilePersistence : IPersistence
    {
        private string _lastPath;

        public bool Saved { get; private set; }

        public TextFilePersistence()
        {
            Saved = false;
        }

        public TextFilePersistence(string path)
        {
            Saved = true;
        }

        public override void Save(SaveData data, string path)
        {
            if (path != null)
            {
                _lastPath = path;
            }
            if (_lastPath == null)
            {
                throw new PersistenceException();
            }
            throw new NotImplementedException();
        }

        public override SaveData Load(string path)
        {
            if (path != null)
            {
                throw new PersistenceException();
            }
            throw new NotImplementedException();
        }
    }
}
