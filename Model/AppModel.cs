using System;
using System.Collections.Generic;
using System.Linq;
using Szakdolgozat.Model.Algorithm;
using Szakdolgozat.Model.Evaluation;
using Szakdolgozat.Persistence;
using Szakdolgozat.Model.Structures;
using Szakdolgozat.Model.Events;

namespace Szakdolgozat.Model
{
    public partial class AppModel : ModelBase
    {
        public AppModel(PersistenceBase persistence)
        {
            Context.Persistence = persistence;
        }

        public void SaveData()
        {
            SaveAsData(null);
        }

        public void SaveAsData(string path)
        {
            SaveData data = new SaveData();


            Context.Persistence.Save(data, path);
        }

        public void LoadData(string path)
        {
            SaveData data = Context.Persistence.Load(path);
        }
    }
}
