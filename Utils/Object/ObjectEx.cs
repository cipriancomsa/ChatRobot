namespace Utils.Object
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public static class ObjectEx
    {
        public static object Clone(this Type objType)
        {
            return Activator.CreateInstance(objType);
        }

        public static object DeserializeObject<T>(this string toDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            var textReader = new StringReader(toDeserialize);
            return xmlSerializer.Deserialize(textReader);
        }

        public static string SerializeObject<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, toSerialize);
            return textWriter.ToString();
        }
    }
}