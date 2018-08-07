using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
//using System.Runtime.Serialization.Formatters.Soap;
namespace Orchard.Utility
{
    /// <summary>
    /// 序列化操作类
    /// </summary>
    public class SerializationHelper
    {
        /// <summary>
        /// 对象是否能序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsSerializable(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Type type = obj.GetType();
            Type attributeType = typeof(SerializableAttribute);
            SerializableAttribute attribute = null;
            if (!(attributeType.IsSubclassOf(typeof(Attribute))))
            {
                return false;
            }
            if (type.IsDefined(attributeType, false))
            {
                object[] attributes = type.GetCustomAttributes(attributeType, false);
                if (attributes.Length > 0)
                {
                    attribute = (SerializableAttribute)attributes[0];
                }
            }
            if (attribute != null)
            {
                return true;
            }
            return false;
        }
        #region Serialize

        /// <summary>
        /// 序列化成XML
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml(Object obj, bool indent = false, System.Text.Encoding encoding = null)
        {
            try
            {
                if (obj == null)
                {
                    return "";
                }
                Type type = obj.GetType();
                string text;
                XmlSerializer serializer = new XmlSerializer(type);
                //var serializer = new System.Runtime.Serialization.NetDataContractSerializer();
                using (StringWriter writer = new StringWriter(CultureInfo.CurrentCulture))
                {

                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings { Encoding = encoding == null ? System.Text.Encoding.UTF8 : encoding };
                    XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                    if (indent)
                    {
                        settings.Indent = false;
                        settings.OmitXmlDeclaration = true;
                        n.Add("", "");
                    }
                    using (var stream = System.Xml.XmlWriter.Create(writer, settings))
                    {
                        serializer.Serialize(stream, obj, n);
                        stream.Close();
                    }
                    text = writer.ToString();
                }

                return text;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
            //return null;
        }
        /// <summary>
        /// 序列化成XML文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SerializeToXmlFile(Object obj, string fileName, bool indent = false, System.Text.Encoding encoding = null)
        {
            bool result = false;
            try
            {
                if (obj == null)
                {
                    return result;
                }
                Type type = obj.GetType();
                //    fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings { Encoding = encoding == null ? System.Text.Encoding.UTF8 : encoding };
                    XmlSerializerNamespaces n = new XmlSerializerNamespaces();
                    if (indent)
                    {
                        settings.Indent = false;
                        settings.OmitXmlDeclaration = true;
                        n.Add("", "");
                    }
                    using (var stream = System.Xml.XmlWriter.Create(fs, settings))
                    {
                        serializer.Serialize(stream, obj, n);
                        stream.Close();
                    }
                }
                result = true;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// 序列化成Binary 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToBinary(Object obj)
        {
            try
            {
                if (obj == null)
                {
                    return "";
                }
                if (!IsSerializable(obj))
                {
                    return "";
                }
                BinaryFormatter serializer = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.Serialize(ms, obj);
                    if (ms.Length != 0)
                    {
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
                return "";
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
        /// <summary>
        /// 序列化Binary 为File
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SerializeToBinaryFile(Object obj, string fileName)
        {
            bool result = false;
            try
            {
                if (obj == null)
                {
                    return result;
                }
                if (!IsSerializable(obj))
                {
                    return result;
                }
                BinaryFormatter serializer = new BinaryFormatter();
                using (FileStream fs = File.Create(fileName, 1024, FileOptions.WriteThrough))
                {
                    serializer.Serialize(fs, obj);
                }
                result = true;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        #endregion Serialize
        #region Deserialize
        /// <summary>
        /// 从xml字符串反序列化
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(string xml, Type type)
        {
            try
            {
                if (String.IsNullOrEmpty(xml))
                {
                    return null;
                }
                XmlSerializer deserializer = new XmlSerializer(type);
                using (var reader = new System.Xml.XmlTextReader(new StringReader(xml)))
                {
                    reader.Namespaces = false;
                    object obj = deserializer.Deserialize(reader);
                    return obj;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 从xml字符串反序列化
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string xml)
        {
            return (T)DeserializeFromXml(xml, typeof(T));
        }

        /// <summary>
        /// 从xml字符串反序列化
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFromXmlFile(string fileName, Type type)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                {
                    return null;
                }
                XmlSerializer deserializer = new XmlSerializer(type);
                using (var reader = new System.Xml.XmlTextReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    reader.Namespaces = false;
                    object obj = deserializer.Deserialize(reader);
                    return obj;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 从xml字符串反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromXmlFile<T>(string fileName)
        {
            return (T)DeserializeFromXmlFile(fileName, typeof(T));
        }
        /// <summary>
        /// 从base64字符串反序列
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        public static object DeserializeFromBinary(string base64Str)
        {
            try
            {
                if (String.IsNullOrEmpty(base64Str))
                {
                    return null;
                }
                BinaryFormatter deserializer = new BinaryFormatter();
                byte[] data = Convert.FromBase64String(base64Str);
                if ((data != null) && (data.Length > 0))
                {
                    MemoryStream ms = new MemoryStream(data);
                    return deserializer.Deserialize(ms);
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 从base64字符串反序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static T DeserializeFromBinary<T>(string base64Str)
        {
            return (T)DeserializeFromBinary(base64Str);
        }
        /// <summary>
        /// 从文件反序列
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object DeserializeFromBinaryFile(string fileName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                {
                    return null;
                }
                BinaryFormatter deserializer = new BinaryFormatter();
                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Delete))
                {
                    return deserializer.Deserialize(fs);
                }
                //return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 从文件反序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromBinaryFile<T>(string fileName)
        {
            return (T)DeserializeFromBinaryFile(fileName);
        }
        #endregion Deserialize


        //string Serialize<T>(System.Net.Http.Formatting.MediaTypeFormatter formatter, T value) {
        //    // Create a dummy HTTP Content.
        //    // 创建一个HTTP内容的哑元
        //    var stream = new MemoryStream();
        //    var content = new System.Net.Http.StreamContent(stream);
        //    // Serialize the object.
        //    // 序列化对象
        //    formatter.WriteToStreamAsync(typeof(T), value, stream, content, null).Wait();
        //    // Read the serialized string.
        //    // 读取序列化的字符串
        //    stream.Position = 0;
        //    return content.ReadAsStringAsync().Result;
        //}
        //T Deserialize<T>(System.Net.Http.Formatting.MediaTypeFormatter formatter, string str) where T : class {
        //    // Write the serialized string to a memory stream.
        //    // 将序列化的字符器写入内涵流
        //    Stream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(str);
        //    writer.Flush();
        //    stream.Position = 0;
        //    // Deserialize to an object of type T
        //    // 解序列化成类型为T的对象
        //    return formatter.ReadFromStreamAsync(typeof(T), stream,null, null).Result as T;
        //}
    }
}