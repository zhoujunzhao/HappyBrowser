using CefSharp;

namespace HappyBrowser.Config
{
    internal static class BrowserConfig
    {
        public static string Branding = "WendaBrowser";
        public static string AcceptLanguage = "zh-CN,zh;q=0.9";//"en-US,en;q=0.9";
        //public static string UserAgent = $"Chrome/{Cef.ChromiumVersion} (Windows NT 10.0; Win64; x64) /CefSharp Browser" + Cef.CefSharpVersion;
        public static string UserAgent = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{Cef.ChromiumVersion} Safari/537.36 ";
        public static string HomepageURL = "https://www.google.com";
        public static string NewTabURL = "about:blank";
        public static string InternalURL = "wendaBrowser";
        public static string DownloadsURL = "wendaBrowser://storage/downloads.html";
        public static string FileNotFoundURL = "wendaBrowser://storage/errors/notFound.html";
        public static string CannotConnectURL = "wendaBrowser://storage/errors/cannotConnect.html";

        public static bool WebSecurity = true;
        public static bool CrossDomainSecurity = true;
        public static bool WebGL = true;
        public static bool ApplicationCache = true;

        public static bool Proxy = false;
        public static string ProxyIP = "123.123.123.123";
        public static int ProxyPort = 123;
        public static string ProxyUsername = "username";
        public static string ProxyPassword = "pass";
        public static string ProxyBypassList = "";
    }
}
