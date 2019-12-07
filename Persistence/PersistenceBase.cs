using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szakdolgozat.Persistence
{
    public abstract class PersistenceBase
    {
        public SaveData Data { get; private set; }

        public string Directory { get; private set; }

        public string SavePath { get; private set; }

        public bool Saved { get; private set; }

        public PersistenceBase()
        {
            Saved = false;
            CreateDefaultDirectory("Saves");
        }

        public PersistenceBase(string path)
        {
            SavePath = path;
            Saved = true;
            CreateDefaultDirectory("Saves");
        }

        public void Save(SaveData data, string path)
        {
            if(path != null)
            {
                SavePath = path;
            }
            if(SavePath == null)
            {
                throw new PersistenceException();
            }

            SaveFile(data);
            Saved = true;
        }

        public void Load(string path)
        {
            SavePath = path ?? throw new PersistenceException();
            Data = LoadFile();
            Saved = true;
        }

        protected abstract void SaveFile(SaveData data);

        protected abstract SaveData LoadFile();

        private void CreateDefaultDirectory(string directoryName)
        {
            Directory = Path.Combine(System.IO.Directory.GetCurrentDirectory(), directoryName);

            if(!System.IO.Directory.Exists(Directory))
            {
                System.IO.Directory.CreateDirectory(Directory);
            }
        }
    }
}
