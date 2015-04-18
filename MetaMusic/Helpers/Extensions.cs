using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MetaMusic.Helpers
{
    public static class Extensions
    {
        public static bool HasProperty(this JToken token, string propName)
        {
            return token.Value<object>(propName) != null;
        }

        /// <summary>
        /// Gets property value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <param name="propName">Property</param>
        /// <returns></returns>
        public static string GetStringValue(this JToken token, string propName)
        {
            try
            {
                return token[propName].ToString();
            }
            catch
            {
                return "Undefined";
            }
        }
    }
}
