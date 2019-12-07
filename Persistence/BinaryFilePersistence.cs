using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Szakdolgozat.Persistence
{
    public class BinaryFilePersistence : PersistenceBase
    {
        public BinaryFilePersistence() : base() { }

        public BinaryFilePersistence(string path) : base(path) { }

        protected override void SaveFile(SaveData data)
        {
            //string path = Path.Combine(FilePath, ".xml");
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Create(SavePath);
                binaryFormatter.Serialize(fileStream, data);
                fileStream.Close();
            }
            catch(Exception)
            {
            }
        }

        protected override SaveData LoadFile()
        {
            SaveData data = null;
            try
            {
                if(File.Exists(SavePath))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(SavePath, FileMode.Open);
                    data = (SaveData)bf.Deserialize(file);
                    file.Close();
                }
            }
            catch(Exception)
            {

            }

            return data;
        }
    }
}
