//using System;
//using System.IO;
//using System.Xml.Serialization;
//using eBay.Service.Call;
//using eBay.Service.Core.Sdk;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace eBay.Service.Standard.Test3
//{
//    /// <summary>  
//    /// <remarks>Xml序列化与反序列化</remarks>  
//    /// <creator>zhangdapeng</creator>  
//    /// </summary>  
//    public class SoapFormatterUtil
//    {
//        #region 反序列化  
//        /// <summary>  
//        /// 反序列化  
//        /// </summary>  
//        /// <param name="type">类型</param>  
//        /// <param name="xml">XML字符串</param>  
//        /// <returns></returns>  
//        public static object Deserialize(Type type, string xml)
//        {
//            using (StringReader sr = new StringReader(xml))
//            {
//                XmlSerializer xmldes = new SoapFormatter(type);
//                return xmldes.Deserialize(sr);
//            }
//        }
//        #endregion

//        #region 序列化  
//        /// <summary>  
//        /// 序列化  
//        /// </summary>  
//        /// <param name="type">类型</param>  
//        /// <param name="obj">对象</param>  
//        /// <returns></returns>  
//        public static string Serializer(Type type, object obj)
//        {
//            MemoryStream Stream = new MemoryStream();
//            XmlSerializer xml = new XmlSerializer(type);
//            //序列化对象  
//            xml.Serialize(Stream, obj);
//            Stream.Position = 0;
//            StreamReader sr = new StreamReader(Stream);
//            string str = sr.ReadToEnd();

//            sr.Dispose();
//            Stream.Dispose();

//            return str;
//        }

//        #endregion
//    }
//}
