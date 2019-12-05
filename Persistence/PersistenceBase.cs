using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Persistence
{
    public abstract class PersistenceBase
    {
        public string FilePath { get; private set; }

        public bool Saved { get; private set; }

        public PersistenceBase()
        {
            Saved = false;
        }

        public PersistenceBase(string path)
        {
            Saved = true;
        }

        public void Save(SaveData data, string path)
        {
            if(path != null)
            {
                FilePath = path;
            }
            if(FilePath == null)
            {
                throw new PersistenceException();
            }

            SaveFile(data);
            Saved = true;
        }

        public SaveData Load(string path)
        {
            if(path != null)
            {
                throw new PersistenceException();
            }

            FilePath = path;
            SaveData data = LoadFile();
            Saved = true;
            return data;
        }

        protected abstract void SaveFile(SaveData data);

        protected abstract SaveData LoadFile();
    }
}
