using System;
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

        /// <summary>
        /// Remove the 'THE' from the end string and add it to beggin
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FixDiscogsName(this string value)
        {
            const string mark = ", the";

            if (!value.EndsWith(mark, StringComparison.CurrentCultureIgnoreCase)) return value;

            value = value.Substring(0, value.Length - mark.Length);
            value = "The " + value;

            return value;
        }

        public static int ToInt(this string value, bool throwException = false)
        {
            int num;

            if (int.TryParse(value, out num))
                return num;

            if(throwException)
                throw new InvalidOperationException("Cannot parse to int");

            return -1;
        }
    }
}
