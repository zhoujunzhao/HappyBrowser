using CefSharp;
using CefSharp.Handler;
using CefSharp.Web;
using HappyBrowser.Entity;
using HappyBrowser.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HappyBrowser.Handler
{
    public class CefRequestHandler : RequestHandler
    {
        private bool isIntercept = false;
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture,
         bool isRedirect)
        {
            // 先调用基类的实现，断点调试
            return base.OnBeforeBrowse(chromiumWebBrowser, browser, frame, request, userGesture, isRedirect);
        }

        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
            IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            if(this.isIntercept 
                && this.LoginWebInfo!=null 
                && this.LoginWebInfo.IsLogin
                && this.LoginWebInfo.Url.ToLower().Trim() == request.ReferrerUrl.ToLower().Trim()
                && request.Method.ToLower() == "post")
            {
                this.isIntercept = false;
                IPostDataElement itm = request.PostData.Elements[0];
                string charSet = request.GetCharSet();
                ResolvingLoginInfo(itm.GetBody(charSet));

                if(!(string.IsNullOrEmpty(LoginWebInfo.AccountValue) || string.IsNullOrEmpty(LoginWebInfo.passwordValue)))
                {
                    ConfigService.LoginInfoHis.Save(LoginWebInfo);
                }
            }

            // 先调用基类的实现，断点调试
            return base.GetResourceRequestHandler(
                chromiumWebBrowser, browser, frame, request, isNavigation,
                isDownload, requestInitiator, ref disableDefaultHandling);
        }

        public bool IsIntercept
        {
            get { return this.isIntercept; }
            set { this.isIntercept = value; }
        }

        public LoginWebInfoEntity? LoginWebInfo
        {
            get;set;
        }
    
        private void ResolvingLoginInfo(string source)
        {
            bool flgParse = false;
            #region 使用C#原生解析
            try
            {
                NameValueCollection nameValues = System.Web.HttpUtility.ParseQueryString(source);

                if (nameValues != null && nameValues.Count>0)
                {
                    if (!string.IsNullOrEmpty(nameValues.Get(LoginWebInfo!.AccountId)))
                    {
                        LoginWebInfo.AccountValue = nameValues.Get(LoginWebInfo!.AccountId)!;
                    }
                    else if (!string.IsNullOrEmpty(nameValues.Get(LoginWebInfo!.AccountName)))
                    {
                        LoginWebInfo.AccountValue = nameValues.Get(LoginWebInfo!.AccountName)!;
                    }

                    if (!string.IsNullOrEmpty(nameValues.Get(LoginWebInfo!.passwordId)))
                    {
                        LoginWebInfo.passwordValue = nameValues.Get(LoginWebInfo!.passwordId)!;
                    }
                    else if (!string.IsNullOrEmpty(nameValues.Get(LoginWebInfo!.passwordName)))
                    {
                        LoginWebInfo.passwordValue = nameValues.Get(LoginWebInfo!.passwordName)!;
                    }
                }
                else
                {
                    flgParse = false;
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                flgParse = false;
            }
            #endregion 使用C#原生解析

            // 自己解析
            if (!flgParse)
            {
                var (isVali, loginInfo) = GetLoginINfo(source);

                if (!isVali) return;

                if (loginInfo == null) return;

                if (loginInfo.ContainsKey(LoginWebInfo!.AccountId))
                {
                    LoginWebInfo.AccountValue = loginInfo.GetValue(LoginWebInfo.AccountId)!.ToString();
                }
                else if (loginInfo.ContainsKey(LoginWebInfo.AccountName))
                {
                    LoginWebInfo.AccountValue = loginInfo.GetValue(LoginWebInfo.AccountName)!.ToString();
                }

                if (loginInfo.ContainsKey(LoginWebInfo.passwordId))
                {
                    LoginWebInfo.passwordValue = loginInfo.GetValue(LoginWebInfo.passwordId)!.ToString();
                }
                else if (loginInfo.ContainsKey(LoginWebInfo.passwordName))
                {
                    LoginWebInfo.passwordValue = loginInfo.GetValue(LoginWebInfo.passwordName)!.ToString();
                }
            }
        }

        private (bool,JObject?) GetLoginINfo(string source) 
        {
            JObject loginInfo;
            try
            {
                loginInfo = (JObject)JToken.Parse(source);
                return (true, loginInfo);
            }
            catch (JsonReaderException)
            {
                if (source.IndexOf("&")>=0)
                {
                    loginInfo = new JObject();

                    string[] ary = source.Split("&");
                    foreach (string itm in ary)
                    {
                        int idx = itm.IndexOf("=");
                        if (idx>=0)
                        {
                            string key = Util.URLUtils.DecodeURL(itm.Substring(0, idx));
                            string val = Util.URLUtils.DecodeURL(itm.Substring(idx+1));
                            loginInfo.Add(key, val);
                        }
                    }
                    return (true, loginInfo);
                }
                else
                {
                    return (false, null);
                }
            }

        }
    }
}
