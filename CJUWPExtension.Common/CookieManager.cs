using System;
using System.Linq;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace CJUWPExtension.Common
{

    public class CookieManager
    {

        private static readonly Lazy<CookieManager> _sharedManager = new Lazy<CookieManager>();

        public static CookieManager SharedManger => _sharedManager.Value;


        private readonly Lazy<HttpBaseProtocolFilter> Filter = new Lazy<HttpBaseProtocolFilter>();
        
        public void DeleteCookie(HttpCookie cookie)
        {
            Filter.Value.CookieManager.DeleteCookie(cookie);
        }

        public HttpCookieCollection GetCookies(Uri uri)
        {
            return Filter.Value.CookieManager.GetCookies(uri);
        }

        public HttpCookie GetCookie(Uri uri, string key)
        {
            return GetCookies(uri).FirstOrDefault(cookie => string.Equals(cookie.Name, key));
        }

        public HttpCookie GetCookie(Uri uri, string key, StringComparison stringComparison)
        {
            return GetCookies(uri).FirstOrDefault(cookie => string.Equals(cookie.Name, key, stringComparison));
        }

        public bool SetCookie(HttpCookie cookie)
        {
            return Filter.Value.CookieManager.SetCookie(cookie);
        }

        public bool SetCookie(HttpCookie cookie, bool thirdParty)
        {
            return Filter.Value.CookieManager.SetCookie(cookie, thirdParty);
        }

    }

}
