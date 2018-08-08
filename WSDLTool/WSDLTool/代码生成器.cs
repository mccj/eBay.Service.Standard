using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotLiquid;

namespace WSDLTool
{
    public class 代码生成器
    {
        public 代码生成器()
        {
            DotLiquid.Condition.Operators.Add("is", (left, right) =>
            {
                if ("Number".Equals(right as string, StringComparison.OrdinalIgnoreCase))
                {
                    return NJsonSchema.Utilities.IsNumber(left as string);
                }
                return false;
            });
            //DotLiquid.Template.RegisterTag<IsNumberTag>("isNumber");
        }
        public string 创建代码(params 类[] 类型)
        {
            var code = 创建代码("类型", 类型);
            return code;
        }
        public string 创建代码(params 枚举[] 类型)
        {
            var code = 创建代码("枚举", 类型);
            return code;
        }
        public string 创建代码(params 接口[] 类型)
        {
            var code = 创建代码("接口", 类型);
            return code;
        }
        private string 创建代码(string file, params object[] vs)
        {
            var setting = new 设置();
            //var templateDirectory = "Templates";
            var data = System.IO.File.ReadAllText("Templates/" + file + ".liquid");

            data = Regex.Replace(data, "(\n( )*)([^\n]*?) \\| csharpdocs }}", m =>
                  m.Groups[1].Value + m.Groups[3].Value + " | csharpdocs: " + m.Groups[1].Value.Length / 4 + " }}",
                  RegexOptions.Singleline);

            var template = DotLiquid.Template.Parse(data); // Parses and compiles the template
            var code = vs.Select(f => template.Render(new DotLiquid.RenderParameters(System.Globalization.CultureInfo.InvariantCulture)
            {
                LocalVariables = NJsonSchema.Utilities.FromAnonymousObject(new { setting = setting, it = f }),
                Filters = new[] { typeof(LiquidFilters) }
            })).ToArray();
            return string.Join("\r\n", code);
        }
    }
    public class 设置 : LiquidizableAbstract
    {
        /// <summary>
        /// 代码生成器版本
        /// </summary>
        public string ToolchainVersion { get; set; } = "4.7.3056.0";
        /// <summary>
        /// 代码生成器名称
        /// </summary>
        public string ToolchainName { get; set; } = "System.Xml";
    }
    //public class IsNumberTag : DotLiquid.Tag
    //{
    //    public override void Initialize(string tagName, string markup, List<string> tokens)
    //    {
    //        base.Initialize(tagName, markup, tokens);
    //        base.Initialize(tagName, markup, tokens);
    //    }
    //    public override void Render(Context context, TextWriter result)
    //    {
    //        base.Render(context, result);
    //    }
    //}
    internal static class LiquidFilters
    {
        public static string Csharpdocs(string input, int tabCount)
        {
            return NJsonSchema.ConversionUtilities.ConvertCSharpDocBreaks(input, tabCount);
        }
    }
    public class 枚举 : LiquidizableAbstract
    {
        public string Namespace { get; set; }
        public 访问修饰符 TypeAccessModifier { get; set; }
        public string Name { get; set; }
        //public string 继承 { get; set; }
        public bool IsEnumAsBitFlags { get; set; } = false;

        public 注释 Description { get; set; }

        public 枚举Item[] Enums { get; set; }
        public string[] Attributes { get; set; }
    }
    public class 枚举Item : LiquidizableAbstract
    {
        public string Name { get; set; }
        public 注释 Description { get; set; }
    }
    public class 类 : LiquidizableAbstract
    {
        public string Namespace { get; set; }
        public 访问修饰符 TypeAccessModifier { get; set; }
        public string Name { get; set; }
        public 注释 Description { get; set; }
        public bool IsAbstract { get; set; } = false;
        public string[] Attributes { get; set; }
        public 属性[] Properties { get; set; }
        public 方法[] Methods { get; set; }

        public string BaseTypeName { get; set; }
    }
    public class 属性 : LiquidizableAbstract
    {
        public 访问修饰符 TypeAccessModifier { get; set; }
        public string Name { get; set; }
        public 注释 Description { get; set; }
        public string Type { get; set; }
        public string[] Attributes { get; set; }
    }
    public class 接口 : LiquidizableAbstract
    {
        public string Namespace { get; set; }
        public 访问修饰符 TypeAccessModifier { get; set; }
        public string Name { get; set; }
        public 注释 Description { get; set; }
        public string[] Attributes { get; set; }
        public 方法[] Methods { get; set; }
        public bool Sync { get; set; }

    }
    public class 方法 : LiquidizableAbstract
    {
        public 访问修饰符 TypeAccessModifier { get; set; }
        public string Name { get; set; }
        public 注释 Description { get; set; }
        public string[] Attributes { get; set; }
        public string ResultType { get;  set; }
        public 参数[] Parameters { get; set; }
        public string Content { get; set; }
    }
    public class 参数: LiquidizableAbstract
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        //public bool IsOptional { get; set; }
    }
    public enum 访问修饰符
    {
        Public,
        Internal,
    }
    public abstract class LiquidizableAbstract : DotLiquid.ILiquidizable
    {
        public object ToLiquid()
        {
            return NJsonSchema.Utilities.FromAnonymousObject(this);
        }
    }
    public class 注释 : LiquidizableAbstract
    {
        public string Summary { get; set; }
        public string Remarks { get; set; }


    }
}
