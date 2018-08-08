using System.Linq;
using System.Text;

namespace NJsonSchema
{
    public class Utilities
    {
        public static DotLiquid.Hash FromAnonymousObject(object anonymousObject)
        {
            //return DotLiquid.Hash.FromAnonymousObject(anonymousObject);
            var result = new DotLiquid.Hash();
            if (anonymousObject != null)
                foreach (var property in anonymousObject.GetType().GetProperties())
                {
                    if (property.PropertyType.IsEnum)
                    {
                        result[property.Name] = property.GetValue(anonymousObject, null).ToString();
                    }
                    else
                    {
                        result[property.Name] = property.GetValue(anonymousObject, null);
                    }
                }
            return result;
        }

        /// <summary>
        /// ÅÐ¶Ï×Ö·û´®ÊÇ·ñÊÇÊý×Ö
        /// </summary>
        public static bool IsNumber(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return false;
            const string pattern = "^[0-9]*$";
            var rx = new System.Text.RegularExpressions.Regex(pattern);
            return rx.IsMatch(s);
        }
    }
}