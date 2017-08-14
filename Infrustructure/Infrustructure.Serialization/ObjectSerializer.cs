using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Validation;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization
{
    public class ObjectSerializer : IObjectSerializer
    {
        public void Serialize(Type serializableType, object type)
        {
            Validator.CheckReferenceTypeForNull(type, nameof(serializableType), MethodBase.GetCurrentMethod());

            XmlSerializer xmlSerializer = new XmlSerializer(serializableType);
            var path = Environment.CurrentDirectory + "//ApplicationFunctionsConfiguration.xml";

            FileStream fileStream = File.Create(path);

            xmlSerializer.Serialize(fileStream, type);
            fileStream.Close();
        }

        public T Deserialize<T>(string xmlString) where T: class 
        {
            Validator.IsNullEmptyOrWhitespace(xmlString, nameof(xmlString));

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            T type = null;

            using (StringReader reader = new StringReader(xmlString))
            {
                type = (T) xmlSerializer.Deserialize(new XmlTextReader(reader));
            }
            return type;
        }
    }
}
