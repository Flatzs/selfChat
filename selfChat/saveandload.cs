using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace selfChat
{
    class saveandload
    {

        public void Save(List<messageClass> messages)
        {
            XmlSerializer serializer = new
                XmlSerializer(typeof(List<messageClass>));

            StreamWriter writer = new StreamWriter("chatlog.xml");

            serializer.Serialize(writer, messages);
            writer.Close();

        }

        public List<messageClass> Load()
        {
            List<messageClass> messages;

            XmlSerializer serializer = new
                XmlSerializer(typeof(List<messageClass>));


            try {
                FileStream iFileStream = new
                    FileStream("chatlog.xml", FileMode.Open);

                messages = (List<messageClass>)serializer.Deserialize(iFileStream);

                iFileStream.Close();
            }
            catch (Exception) {
                // first time using program.. create a chat object to use
                messages = new List<messageClass>();
            }


            return messages;

        }


    }
}
