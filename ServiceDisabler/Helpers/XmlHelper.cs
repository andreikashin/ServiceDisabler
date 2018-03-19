using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ServiceDisabler.Properties;

namespace ServiceDisabler.Helpers
{
    public static class XmlHelper
    {
        public static string ToXml(object obj)
        {
            var T = obj.GetType();

            var serializer = new XmlSerializer(T);

            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                serializer.Serialize(writer, obj);
            }
            return builder.ToString();
        }

        public static void ToXmlFile(object obj, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            using (var writer = XmlWriter.Create(fs))
            {
                new XmlSerializer(obj.GetType())
                    .Serialize(writer, obj);
            }

        }

        public static T FromXml<T>(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                return (T)new XmlSerializer(typeof(T))
                    .Deserialize(reader);
            }
        }

        public static T FromXmlFile<T>(string path)
        {
            var reader = new StreamReader(path);
            try
            {
                return FromXml<T>(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(
                    Resources.XmlFileReadError, path) +
                    ex.InnerException?.Message);
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
