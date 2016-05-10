using System;
using System.Collections.Generic;
using System.Linq;

namespace CJUWPExtension.Common
{

    public static class UriExtensions
    {

        private static Dictionary<string, string> GetQueryDictionary(this Uri uri, Func<string, string> decodeFunc)
        {
            return uri.Query.Split(new[] { "&", "?" }, StringSplitOptions.RemoveEmptyEntries).Where(exp => exp.Contains('=') && exp[0] != '=').Select(exp => new Tuple<string, int>(exp, exp.IndexOf('='))).ToDictionary(experssion => decodeFunc(experssion.Item1.Substring(0, experssion.Item2)), experssion => experssion.Item1.Length > experssion.Item2 + 1 ?  decodeFunc(experssion.Item1.Substring(experssion.Item2 + 1)) : string.Empty);
        }

        public static Dictionary<string, string> GetUnescapedQueryDictionary(this Uri uri)
        {
            return uri.GetQueryDictionary(Uri.UnescapeDataString);
        }

        public static Dictionary<string, string> GetOriginalQueryDictionary(this Uri uri)
        {
            return uri.GetQueryDictionary(value => value);
        }

    }

}
