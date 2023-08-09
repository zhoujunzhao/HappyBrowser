using HappyBrowser.Entity;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HappyBrowser.Services
{
    public class HtmlAnalysisService
    {
        private static HtmlAnalysisService? htmlServcie;

        private HtmlAnalysisService() { }

        public static HtmlAnalysisService Instance
        {
            get {
                htmlServcie ??= new();
                return htmlServcie;
            }
        }

        /// <summary>
        /// 分析网页是不是登录页面
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public LoginWebInfoEntity CheckISLoginWebPage(string source)
        {
            try
            {

                LoginWebInfoEntity loginWebInfo = new(false, "");

                if (string.IsNullOrEmpty(source)) return loginWebInfo;

                HtmlDocument doc = new();
                doc.LoadHtml(source);

                HtmlNodeCollection pwdNodes = doc.DocumentNode.SelectNodes(".//input[@type='password']");
                
                if (pwdNodes==null || pwdNodes.Count <= 0) return loginWebInfo;

                HtmlNode pwdNode = pwdNodes[0];

                if (pwdNodes.Count > 1)
                {
                    HtmlNodeCollection allInputNodes = doc.DocumentNode.SelectNodes(".//input");
                    int idx = allInputNodes.IndexOf(pwdNode);
                    // 这是修改密码
                    if (idx + 1 < allInputNodes.Count && allInputNodes[idx + 1].GetAttributeValue("type", "").ToLower() == "password")
                    {
                        return loginWebInfo;
                    }
                    else if (idx + 2 < allInputNodes.Count && allInputNodes[idx + 2].GetAttributeValue("type", "").ToLower() == "password")
                    {
                        return loginWebInfo;
                    }
                    // 有些网页会有两个登录输入框，一个在页面最上面，一个在中间
                    pwdNode = pwdNodes[1];
                }

                HtmlNode? parentNode = FindFormNode(pwdNode);

                if (parentNode == null)
                {
                    parentNode = FindFirstNode(pwdNode);
                }

                if (parentNode == null)
                { 
                    return loginWebInfo; 
                }

                HtmlNode? accountNode = GetAccountNode(parentNode, pwdNode);

                if (accountNode == null) return loginWebInfo;

                loginWebInfo.IsLogin = true;
                loginWebInfo.AccountId = accountNode.GetAttributeValue("id", "");
                loginWebInfo.AccountName = accountNode.GetAttributeValue("name", "");
                loginWebInfo.passwordId = pwdNode.GetAttributeValue("id", "");
                loginWebInfo.passwordName = pwdNode.GetAttributeValue("name", "");
                return loginWebInfo;
            }
            catch(Exception e)
            {
                LogUtil.Error(e);
                return new LoginWebInfoEntity();
            }
        }

        private HtmlNode? FindFormNode(HtmlNode pwdNode)
        {
            HtmlNode pNode = pwdNode.ParentNode;
            int i = 0;
            while (pNode != null && pNode.Name.ToLower() != "body" && i < 1000)
            {
                if (pNode.Name.ToLower() == "form")
                {
                    return pNode;
                }
                pNode = pNode.ParentNode;
                if (pNode == null)
                {
                    return null;
                }
                // 预防死循环
                i++;
            }
            return null;
        }

        private HtmlNode? FindFirstNode(HtmlNode pwdNode)
        {
            HtmlNode pNode = pwdNode.ParentNode;
            int i = 0;
            while (pNode != null && pNode.Name.ToLower() != "body" && i < 1000)
            {
                HtmlNodeCollection allNodes = pNode.SelectNodes(".//input");
                if (allNodes.Count > 1)
                {
                    return pNode;
                }

                pNode = pNode.ParentNode;
                if (pNode == null)
                {
                    return null;
                }
                // 预防死循环
                i++;
            }
            return null;
        }

        private HtmlNode? GetAccountNode(HtmlNode parentNode, HtmlNode pwdNode)
        {
            HtmlNodeCollection inputNodes = parentNode.SelectNodes(".//input[@type='text']");

            if (inputNodes == null || inputNodes.Count < 1)
            {
                return null;
            }
            HtmlNodeCollection allNodes = parentNode.SelectNodes(".//input");

            for (int i = 0; i<allNodes.Count; i++)
            {
                HtmlNode itm = allNodes[i];
                if (i+1 < allNodes.Count && allNodes[i+1] == pwdNode)
                {
                    return itm;
                }
            }
            return null;
        }

    }
}
