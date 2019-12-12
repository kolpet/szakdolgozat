using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szakdolgozat.Persistence;

namespace Unit_Test.Model
{
    public class MockPersistence : PersistenceBase
    {
        public SaveData MockData { get; set; }

        protected override SaveData LoadFile()
        {
            return MockData;
        }

        protected override void SaveFile(SaveData data)
        {
            MockData = data;
        }
    }
}
