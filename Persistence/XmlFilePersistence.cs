using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Szakdolgozat.Persistence
{
    public class XmlFilePersistence : PersistenceBase
    {
        public XmlFilePersistence() : base() { }

        public XmlFilePersistence(string path) : base(path) { }

        protected override void SaveFile(SaveData data)
        {
            string path = Path.Combine(FilePath, ".xml");

            try
            {
                if(!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(data.GetType());

                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, data);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(path);
                }
            }
            catch(Exception e)
            {
            }
        }

        protected override SaveData LoadFile()
        {
            string path = Path.Combine(FilePath, ".xml");
            if(string.IsNullOrEmpty(path)) { return null; }

            SaveData data = new SaveData();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                string xmlString = xmlDocument.OuterXml;

                using(StringReader read = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    using(XmlReader reader = new XmlTextReader(read))
                    {
                        data = (SaveData)serializer.Deserialize(reader);
                    }
                }
            }
            catch(Exception e)
            {
                //Log exception here
            }

            return data;
        }
    }
}
