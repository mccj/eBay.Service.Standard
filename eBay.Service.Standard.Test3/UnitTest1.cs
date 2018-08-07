using System;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eBay.Service.Standard.Test3
{
    [TestClass]
    public class UnitTest1
    {
        string eBayToken = "";
        [TestInitialize]
        public void Initialize()
        {
        }
        [TestMethod]
        public void TestMethod1()
        {
            var context = new ApiContext { ApiCredential = new ApiCredential { eBayToken = eBayToken }, CallRetry = new CallRetry { MaximumRetries = 5 } };
            //GetApiAccessRulesCall apicall = new GetApiAccessRulesCall(context);
            //var rules = apicall.GetApiAccessRules();


            GetItemCall apical2 = new GetItemCall(context);
            var rules11 = apical2.GetItem("323269291886");


        }
        //[TestMethod]
        //public void ssssss()
        //{
        //    var xml = System.IO.File.ReadAllText(@"C:\Users\mccj\source\repos\eBay.Service.Standard\eBay.Service.Standard.Test3\test.xml");
        //    //var xml = XmlSerializeUtil.Serializer(typeof(Core.Soap.GetApiAccessRulesResponseType),new Core.Soap.GetApiAccessRulesResponseType { ApiAccessRule=new Core.Soap.ApiAccessRuleTypeCollection { new Core.Soap.ApiAccessRuleType() {  CallName="555"} } });
        //    var dfdfsdf = Orchard.Utility.SerializationHelper.DeserializeFromXml<Core.Soap.GetApiAccessRulesResponseType>(xml);

        //    var cccc = System.Xml.Linq.XElement.Parse(xml).ToString(System.Xml.Linq.SaveOptions.OmitDuplicateNamespaces);
        //    var xmldoc = new System.Xml.XmlDocument();
        //    xmldoc.LoadXml(xml);
        //    var xxx = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmldoc, Newtonsoft.Json.Formatting.Indented);
        //    //GetApiAccessRulesResponseType
        //    var ddddf = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(xxx);
        //    var eeeee = Newtonsoft.Json.JsonConvert.DeserializeObject<Core.Soap.GetApiAccessRulesResponseType>(xxx);
        //}
        //public void dddd()
        //{

        //}
        //private void ssss()
        //{
        //    string strobj = "test string for serialization";
        //    FileStream stream = new FileStream("C:\\StrObj.txt", FileMode.Create, FileAccess.Write,
        //    FileShare.None);
        //    var formatter = new SoapFormatter();
        //    formatter.Serialize(stream, strobj);
        //    stream.Close();
        //    //Deserialization of String Object
        //    FileStream readstream = new FileStream("C:\\StrObj.txt", FileMode.Open, FileAccess.Read,
        //    FileShare.Read);
        //    string readdata = (string)formatter.Deserialize(readstream);
        //    readstream.Close();
        //    Console.WriteLine(readdata);
        //    Console.ReadLine();
        //}
    }
}
