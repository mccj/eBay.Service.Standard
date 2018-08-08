using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WSDLTool
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //eeeee.eeeefff();
            //return;





            var stream = @"eBaySvc.wsdl";
            var description = System.Web.Services.Description.ServiceDescription.Read(stream);//创建和格式化WSDL文档  

            //var eere = Newtonsoft.Json.JsonConvert.SerializeObject(description, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings { MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore, ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            //System.IO.File.WriteAllText("aaa.json", eere);
            ////textBox1.Text = eere;
            ////return;

            var operations = description.PortTypes.OfType<System.Web.Services.Description.PortType>().Select(f => new
            {
                f.Name,
                QualifiedNamespace = f.ServiceDescription.TargetNamespace,
                Operations = f.Operations.OfType<System.Web.Services.Description.Operation>().Select(ff => new
                {
                    ff.Name,
                    Input = ff.Messages.OfType<System.Web.Services.Description.OperationInput>().Select(f1 => f1.Message.Name).FirstOrDefault(),
                    Output = ff.Messages.OfType<System.Web.Services.Description.OperationOutput>().Select(f1 => f1.Message.Name).FirstOrDefault()
                }).ToArray()
            }).ToArray();
            Annotation getAnnotation(System.Xml.Schema.XmlSchemaAnnotation annotation)
            {
                //var xmlSchemaObject = annotation?.Items.OfType<System.Xml.Schema.XmlSchemaObject>().Select(f1 => f1.GetType().FullName).Distinct().ToArray();
                return new Annotation
                {
                    Documentation = annotation?.Items.OfType<System.Xml.Schema.XmlSchemaDocumentation>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => f2.InnerText)).ToArray(),
                    AppInfo = annotation?.Items.OfType<System.Xml.Schema.XmlSchemaAppInfo>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => new AppInfo { Name = f2.Name, InnerText = f2.InnerText })).ToArray()
                };
            }
            var names = description.Types.Schemas.Select(f => new
            {
                XmlSchemaElement = f.Items.OfType<System.Xml.Schema.XmlSchemaElement>()/*.Select(f1 => f1.Name)*/.ToArray(),
                XmlSchemaComplexType = f.Items.OfType<System.Xml.Schema.XmlSchemaComplexType>()/*.Select(f1 => f1.Name)*/.ToArray(),
                XmlSchemaSimpleType = f.Items.OfType<System.Xml.Schema.XmlSchemaSimpleType>()/*.Select(f1 => f1.Name)*/.ToArray(),
                XmlSchemaAnnotation = f.Items.OfType<System.Xml.Schema.XmlSchemaAnnotation>().ToArray()
            })
            .Select(f => new
            {
                XmlSchemaComplexType = f.XmlSchemaComplexType.Select(ff => new
                {
                    Item = ff,
                    QualifiedNamespace = ff.QualifiedName.Namespace,
                    QualifiedName = ff.QualifiedName.Name,
                    Name = ff.Name,
                    Annotation = getAnnotation(ff.Annotation),
                    IsAbstract = ff.IsAbstract,
                    //new
                    //{
                    //    //XmlSchemaObject = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaObject>().Select(f1 => f1.GetType().FullName).Distinct().ToArray(),
                    //    Documentation = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaDocumentation>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => f2.InnerText)).ToArray(),
                    //    AppInfo = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaAppInfo>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => new { f2.Name, f2.InnerText })).ToArray()
                    //}
                    BaseTypeName = ((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaComplexContentExtension)?.BaseTypeName?.Name) ?? (((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaSimpleContentExtension)?.BaseTypeName?.Namespace != "http://www.w3.org/2001/XMLSchema") ? (ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaSimpleContentExtension)?.BaseTypeName?.Name : ""),
                    Properties = ((((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaComplexContentExtension)?.Particle ?? ff.Particle) as System.Xml.Schema.XmlSchemaSequence)?.Items?.OfType<System.Xml.Schema.XmlSchemaElement>() ?? new System.Xml.Schema.XmlSchemaElement[] { })?.Select(f2 => new
                    {
                        Use = System.Xml.Schema.XmlSchemaUse.None,
                        Attribute = "",
                        Annotation = getAnnotation(f2.Annotation),
                        Name = f2.Name,
                        TypeName = f2.SchemaTypeName.Name,
                        IsArray = f2.MaxOccursString == "unbounded"
                    })
                    .Concat((((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaSimpleContentExtension)?.BaseTypeName?.Namespace != "http://www.w3.org/2001/XMLSchema") ? new System.Xml.Schema.XmlSchemaSimpleContentExtension[] { } : new[] { (ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaSimpleContentExtension) }).Select(f2 => new
                    {
                        Use = System.Xml.Schema.XmlSchemaUse.Required,
                        Attribute = "System.Xml.Serialization.XmlText" + (f2.BaseTypeName.Namespace == "http://www.w3.org/2001/XMLSchema" ? "(DataType=\"" + f2.BaseTypeName.Name + "\")" : ""),//
                        Annotation = getAnnotation(f2.Annotation),
                        Name = "Value",
                        TypeName = getType(f2.BaseTypeName.Name),
                        IsArray = false// f2.MaxOccursString == "unbounded"
                    }))
                    .Concat(((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaSimpleContentExtension)?.Attributes?.OfType<System.Xml.Schema.XmlSchemaAttribute>() ?? new System.Xml.Schema.XmlSchemaAttribute[] { }).Select(f2 => new
                    {
                        Use = System.Xml.Schema.XmlSchemaUse.Required/*f2.Use*/,
                        Attribute = "System.Xml.Serialization.XmlAttribute" + (f2.SchemaTypeName.Namespace == "http://www.w3.org/2001/XMLSchema" ? "(DataType=\"" + f2.SchemaTypeName.Name + "\")" : ""),//
                        Annotation = getAnnotation(f2.Annotation),
                        Name = f2.Name,
                        TypeName = getType(f2.SchemaTypeName.Name),
                        IsArray = false// f2.MaxOccursString == "unbounded",

                    }))
                    .Concat((ff?.Attributes?.OfType<System.Xml.Schema.XmlSchemaAttribute>() ?? new System.Xml.Schema.XmlSchemaAttribute[] { }).Select(f2 => new
                    {
                        Use = f2.Use,
                        Attribute = "",
                        Annotation = getAnnotation(f2.Annotation),
                        Name = f2.Name,
                        TypeName = f2.SchemaTypeName.Name,
                        IsArray = false// f2.MaxOccursString == "unbounded"
                    }))

                    .ToArray(),
                    PropertieAnys = (((ff.ContentModel?.Content as System.Xml.Schema.XmlSchemaComplexContentExtension)?.Particle ?? ff.Particle) as System.Xml.Schema.XmlSchemaSequence)?.Items?.OfType<System.Xml.Schema.XmlSchemaAny>()?.Select(f2 => new
                    {
                        Annotation = getAnnotation(f2.Annotation)
                    }).ToArray()
                }).ToArray(),
                XmlSchemaSimpleType = f.XmlSchemaSimpleType.Select(ff => new
                {
                    Item = ff,
                    QualifiedNamespace = ff.QualifiedName.Namespace,
                    QualifiedName = ff.QualifiedName.Name,
                    Name = ff.Name,
                    Annotation = getAnnotation(ff.Annotation),
                    //new
                    //{
                    //    XmlSchemaObject = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaObject>().Select(f1 => f1.GetType().FullName).Distinct().ToArray(),
                    //    Documentation = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaDocumentation>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => f2.InnerText)).ToArray(),
                    //    AppInfo = ff.Annotation?.Items.OfType<System.Xml.Schema.XmlSchemaAppInfo>().Where(f1 => f1.Markup != null).SelectMany(f1 => f1.Markup.Select(f2 => new { f2.Name, f2.InnerText })).ToArray()
                    //},
                    TypeName = (ff.Content as System.Xml.Schema.XmlSchemaSimpleTypeRestriction).BaseTypeName.Name,
                    Items = (ff.Content as System.Xml.Schema.XmlSchemaSimpleTypeRestriction).Facets.OfType<System.Xml.Schema.XmlSchemaEnumerationFacet>().Select(f2 => new { Annotation = getAnnotation(f2.Annotation), f2.Value }).ToArray()
                }).ToArray(),
                XmlSchemaAnnotation = f.XmlSchemaAnnotation.Select(ff => getAnnotation(ff)).ToArray(),
                XmlSchemaElement = f.XmlSchemaElement.Select(ff => new
                {
                    Item = ff,
                    Name = ff.Name,
                    Annotation = getAnnotation(ff.Annotation),
                    QualifiedNamespace = ff.QualifiedName.Namespace,
                    QualifiedName = ff.QualifiedName.Name,
                    SchemaTypeName = ff.SchemaTypeName.Name
                }).ToArray()
            }
            )
            .ToArray();
            //var dddd = names.SelectMany(f => f.XmlSchemaSimpleType).SelectMany(f => f.Annotation.XmlSchemaObject ?? new string[] { }).Distinct().ToArray();

            var aaa1 = names.SelectMany(f => f.XmlSchemaComplexType).FirstOrDefault(f => f.Name == "AmountType");
            //var aaa1 = names.SelectMany(f => f.XmlSchemaComplexType).Where(f => f?.Properties?.Any() != true).ToArray();
            //var aaa1 = names.SelectMany(f => f.XmlSchemaComplexType).Where(f => f?.PropertieAnys?.Any() == true).ToArray();



            var 代码生成器 = new 代码生成器();
            var 枚举 = names.SelectMany(f => f.XmlSchemaSimpleType).Where(f =>/* !(new[] { "UUIDType", "DisputeIDType", "ItemIDType" }.Contains(f.Name))*/f.Items.Length > 0).Select(f => new 枚举
            {
                TypeAccessModifier = 访问修饰符.Public,
                Namespace = "eBay.Service.Core.Soap",
                Name = f.Name,
                Attributes = new[] { "System.Xml.Serialization.XmlType(Namespace = \"" + f.QualifiedNamespace + "\")" },
                Description = new 注释
                {
                    Summary = String.Join("\r\n", f.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" + */f1?.InnerText) ?? new string[] { }),
                    Remarks = String.Join("\r\n", f.Annotation?.Documentation ?? new string[] { })
                },
                Enums = f.Items.Select(ff => new 枚举Item
                {
                    Name = ff.Value,
                    Description = new 注释
                    {
                        Summary = String.Join("\r\n", ff.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" +*/ f1?.InnerText) ?? new string[] { }),
                        Remarks = String.Join("\r\n", ff.Annotation?.Documentation ?? new string[] { })
                    }
                }).ToArray()
            }).ToArray();
            //var ddfsdfds=     枚举.Where(f => f.Name == "UUIDType").ToArray();
            var code枚举 = 代码生成器.创建代码(枚举);
            var 枚举toStr = names.SelectMany(f => f.XmlSchemaSimpleType).Where(f => f.Items.Length == 0).Select(f => f.Name).ToArray();


            //匹配数组
            var dfsdfsdsdf = names.SelectMany(f => f.XmlSchemaComplexType).Where(f => /*f.Name.EndsWith("ArrayType") &&*/ f.Properties.Length == 1 && f.Properties.FirstOrDefault().IsArray && f.PropertieAnys?.Any() == false && String.IsNullOrWhiteSpace(f.BaseTypeName)).Select(f => new
            {
                Item = f,
                Name = "System.Collections.Generic.List<" + getType(f.Properties.FirstOrDefault().TypeName, 枚举.Select(fff => fff.Name).ToArray(), false, 枚举toStr/*, ddddd*/) + ">",
                Attribute = "System.Xml.Serialization.XmlArrayItem(\"" + f.Properties.FirstOrDefault().Name + "\", IsNullable = false)"
            }).ToArray();
            var ddddd = names.SelectMany(f => f.XmlSchemaComplexType).Where(f => !dfsdfsdsdf.Select(ff => ff.Item).Contains(f)).Where(f => !String.IsNullOrWhiteSpace(f.BaseTypeName)).GroupBy(f => f.BaseTypeName).ToDictionary(f => f.Key, f => f.Select(ff => ff.Name).ToArray());

            var 类 = names.SelectMany(f => f.XmlSchemaComplexType).Where(f => !dfsdfsdsdf.Select(ff => ff.Item).Contains(f)).Select(f => new 类
            {
                TypeAccessModifier = 访问修饰符.Public,
                Namespace = "eBay.Service.Core.Soap",
                Name = f.Name,
                BaseTypeName = f.BaseTypeName,
                IsAbstract = f.IsAbstract,
                Attributes = new[] { "System.ComponentModel.DesignerCategory(\"code\")", "System.Xml.Serialization.XmlType(Namespace = \"" + f.QualifiedNamespace + "\")" }
                   .Concat(ddddd.ContainsKey(f.Name) ? ddddd[f.Name].Select(ffff => "System.Xml.Serialization.XmlInclude(typeof(" + ffff + "))") : new string[] { }).ToArray(),
                Description = new 注释
                {
                    Summary = String.Join("\r\n", f.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" + */f1?.InnerText) ?? new string[] { }),
                    Remarks = String.Join("\r\n", f.Annotation?.Documentation ?? new string[] { })
                },
                Properties = (f.Properties?.Select(ff => new 属性
                {
                    Name = ff?.Name,
                    Type = ((dfsdfsdsdf.FirstOrDefault(fff => fff.Item.Name == ff?.TypeName))?.Name) ?? (ff.IsArray ? ("System.Collections.Generic.List<" + getType(ff?.TypeName, 枚举.Select(fff => fff.Name).ToArray(), false, 枚举toStr/*, ddddd*/) + ">") : getType(ff?.TypeName, 枚举.Select(fff => fff.Name).ToArray(), ff?.Use != System.Xml.Schema.XmlSchemaUse.Required, 枚举toStr/*, ddddd*/)),
                    Attributes = new[] { ff?.Attribute, (dfsdfsdsdf.FirstOrDefault(fff => fff.Item.Name == ff?.TypeName))?.Attribute }.Concat((ff.IsArray ? new[] { "System.Xml.Serialization.XmlElement(\"" + ff?.Name + "\")" } : new string[] { })).Where(fff => !string.IsNullOrWhiteSpace(fff)).ToArray(),
                    Description = new 注释
                    {
                        Summary = String.Join("\r\n", ff?.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" +*/ f1?.InnerText) ?? new string[] { }),
                        Remarks = String.Join("\r\n", ff?.Annotation?.Documentation ?? new string[] { })
                    }
                }) ?? new 属性[] { })
                   .Concat(f.PropertieAnys?.Select(ff => new 属性
                   {
                       Attributes = new[] { "System.Xml.Serialization.XmlAnyElement()" },
                       Description = new 注释
                       {
                           Summary = String.Join("\r\n", ff?.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" +*/ f1?.InnerText) ?? new string[] { }),
                           Remarks = String.Join("\r\n", ff?.Annotation?.Documentation ?? new string[] { })
                       },
                       Name = "Any",
                       Type = "System.Xml.XmlElement[]"
                   }) ?? new 属性[] { })
                   .ToArray()
            }).ToArray();


            var code类 = 代码生成器.创建代码(类);


            var 类2 = names.SelectMany(f => f.XmlSchemaElement).Where(f => f.Name != "RequesterCredentials").Select(f => new 类
            {
                TypeAccessModifier = 访问修饰符.Public,
                Namespace = "eBay.Service.Core.Soap",
                Name = f.Name,
                Attributes = new[] { "System.ServiceModel.MessageContract(IsWrapped = false)" },
                Description = new 注释
                {
                    Summary = String.Join("\r\n", f.Annotation?.AppInfo?.Select(f1 => /*f1?.Name + "\r\n" + */f1?.InnerText) ?? new string[] { }),
                    Remarks = String.Join("\r\n", f.Annotation?.Documentation ?? new string[] { })
                },
                Properties = new[]{ new 属性{
                    Attributes=new[]{
                        "System.ServiceModel.MessageBodyMember(Name = \"" +f.Name+"\", Namespace = \""+f.QualifiedNamespace + "\", Order = 0)",
                        (dfsdfsdsdf.FirstOrDefault(fff => fff.Item.Name == f?.SchemaTypeName))?.Attribute
                    },
                    Name = f?.SchemaTypeName,
                    Type =((dfsdfsdsdf.FirstOrDefault(fff => fff.Item.Name == f?.SchemaTypeName))?.Name)??  f?.SchemaTypeName
                },new 属性{
                     Name="RequesterCredentials", Type="CustomSecurityHeaderType", Attributes=new[]{ "System.ServiceModel.MessageHeader(Namespace = \"" + f.QualifiedNamespace + "\")" }
                } }
            }).ToArray();


            var code类2 = 代码生成器.创建代码(类2);

            var 接口 = operations.SelectMany(f => new[] {
                new 接口 {
                    Name = f.Name,
                    Namespace= "eBay.Service.Core.Soap",
                    Attributes=new[]{ "System.ServiceModel.ServiceContractAttribute(Namespace = \""+f.QualifiedNamespace + "\", ConfigurationName = \"eBayApi.eBayAPIInterface\")"},
                    Methods = f.Operations.Select(f1 => new 方法 { Name = f1.Name, Parameters = new[]{ new 参数 {  Type= f1.Input ,Name= "request" } }, ResultType = f1.Output,Attributes=new []{
                        "System.ServiceModel.OperationContract(Action = \"\", ReplyAction = \"*\")",
                        "System.ServiceModel.XmlSerializerFormat(SupportFaults = true)",
                        "System.ServiceModel.ServiceKnownType(typeof(AbstractResponseType))",
                        "System.ServiceModel.ServiceKnownType(typeof(AbstractRequestType))"
                    } })
                    .Concat(f.Operations.Select(f1 => new 方法 { Name = f1.Name+"Async", Parameters = new[]{ new 参数 {  Type= f1.Input ,Name= "request" } }, ResultType =  "System.Threading.Tasks.Task<"+f1.Output+">" , Attributes=new[]{
                        "System.ServiceModel.OperationContract(Action = \"\", ReplyAction = \"*\")"
                    } }))
                    .ToArray() }
            }).ToArray();

            var code接口 = 代码生成器.创建代码(接口);

            var 类4 = operations.SelectMany(f => new[] {
                new 类 {
                    Namespace= "eBay.Service",
                    Name = /*f.Name*/"eBay"+"Client",
                    //BaseTypeName="System.ServiceModel.ClientBase<"+f.Name+">, "+f.Name
                    //Attributes=new[]{ "System.ServiceModel.ServiceContractAttribute(Namespace = \""+f.QualifiedNamespace + "\", ConfigurationName = \"eBayApi.eBayAPIInterface\")"},
                    Methods = f.Operations.Select(f1 => new 方法 { Name = f1.Name+"Async", Parameters = new[]{ new 参数 {  Type= "Core.Soap." + f1.Input + "Type", Name= "request" } }, ResultType ="async System.Threading.Tasks.Task<Core.Soap."+ f1.Output+"Type>" ,
                        Content =
                        $"var apiName = \"{f1.Name}\";\r\n" +
                        $"var url = getUrl(apiName);\r\n" +
                        $"var client = this.GeteBayAPIClient(url);\r\n" +
                        $"var requestInfo = new Core.Soap.{f1.Name}Request\r\n" +
                        $"{{\r\n" +
                        $"  {f1.Name}RequestType = handleRequest(request),\r\n" +
                        $"  RequesterCredentials = this.securityHeader\r\n" +
                        $"}};\r\n" +
                        $"var response = await client.{f1.Name}Async(requestInfo);\r\n" +
                        $"return response.{f1.Name}ResponseType;"})
                    .Concat(f.Operations.Select(f1 => new 方法 { Name = f1.Name, Parameters = new[]{ new 参数 {  Type = "Core.Soap." + f1.Input + "Type", Name= "request" } }, ResultType =  "Core.Soap."+ f1.Output+"Type",
                     Content=$"return this.{f1.Name}Async(request).Result;"
                    }))
                    .ToArray()
                }
            }).ToArray();

            var code类4 = 代码生成器.创建代码(类4);

            var code = code类4 + code接口 + code类2 + code类 + code枚举;


            System.IO.File.WriteAllText(@"Reference.cs", code);
        }
        //static List<string> GetVs = new List<string>();
        private static string getType(string type, string[] enums = null, bool isnull = false, string[] 枚举toStr = null/*, string[] classNames*/)
        {
            //if (type== "AddressAttributeCodeType") {
            //}
            //if (classNames.Contains(type))
            //{
            //    return type;
            //}
            if (枚举toStr?.Contains(type) == true)
            {
                return "string";
            }
            var valueType = new[] { "int", "long", "double", "decimal", "float" };
            var value转换 = new Dictionary<string, string> {
                { "time", "System.DateTime" },
                { "dateTime", "System.DateTime" },
                { "boolean", "bool"}
            };
            var 引用转换 = new Dictionary<string, string> {
                { "anyURI","string"},
                { "duration", "string" },
                { "token", "string" },
                { "base64Binary", "byte[]" },
            };

            if (引用转换.ContainsKey(type))
            {
                return 引用转换[type];
            }
            if (value转换.ContainsKey(type))
            {
                return value转换[type] + (isnull ? "?" : "");
            }
            if (enums?.Contains(type) == true || valueType?.Contains(type) == true)
            {
                return type + (isnull ? "?" : "");
            }
            var nt = Type.GetType(type);
            if (nt != null && nt.IsValueType)
            {
                return type + (isnull ? "?" : "");
            }

            //if (!GetVs.Contains(type))
            //{
            //    GetVs.Add(type);
            //}
            return type;
        }
    }

    //public static class ssssss
    //{
    //    public static IEnumerable<object> ToEnumerable(this System.Collections.CollectionBase collectionBase)
    //    {
    //        foreach (var item in collectionBase)
    //        {
    //            yield return item;
    //        }
    //    }
    //}

    public class Annotation
    {
        public string[] Documentation { get; set; }
        public AppInfo[] AppInfo { get; set; }

    }
    public class AppInfo
    {
        public string Name { get; set; }
        public string InnerText { get; set; }
    }
}
