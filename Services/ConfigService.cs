﻿using HappyBrowser.Config;
using HappyBrowser.Entity;
using HappyBrowser.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace HappyBrowser.Services
{
    public class ConfigService
    {
        #region 根配置文件操作
        private static string configSyncPath="";
        private const string ROOT_CONFIG_PATH_FILENAME = "root.json";

        /// <summary>
        /// 读取配置文件保存位置
        /// 主要考虑使用同步盘进行配置同步
        /// </summary>
        /// <returns></returns>
        public static string GetSyncConfigPath()
        {
            if (!string.IsNullOrEmpty(configSyncPath))
            {
                return configSyncPath;
            }
            RootConfig rootConfig = GetRootConfig();
            configSyncPath = rootConfig.RootPath;
            return configSyncPath;
        }

        /// <summary>
        /// 修改配置文件保存位置
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static bool ModifySyncConfigPath(string rootPath)
        {
            if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath))
            {
                return false;
            }

            if (rootPath.ToLower() == configSyncPath.ToLower())
            {
                return false;
            }

            string? oldRootConfigPath = configSyncPath;
            string[] configFileList = new string[] { CONFIG_FILE_NAME, BookMarksService.BOOK_MARKS_FILE_NAME };

            RootConfig rootConfig = GetRootConfig();
            rootConfig.RootPath = rootPath;
            WriteRootConfig(rootConfig);
            configSyncPath = rootPath;

            foreach (string itm in configFileList)
            {
                string fromPath = Path.Combine(oldRootConfigPath, itm);
                string toPath = Path.Combine(configSyncPath, itm);

                if (File.Exists(fromPath) && !File.Exists(toPath))
                {
                    File.Copy(fromPath, toPath, true);
                }
            }
            return true;
        }


        /// <summary>
        /// 读取下载文件保存位置
        /// </summary>
        /// <returns></returns>
        public static string GetDownloadPath()
        {
            RootConfig rootConfig = GetRootConfig();
            return rootConfig.DownloadPath;
        }

        /// <summary>
        /// 修改配置文件保存位置
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static bool ModifyDownloadPath(string newPath)
        {
            if (string.IsNullOrEmpty(newPath) || !Directory.Exists(newPath))
            {
                return false;
            }
            RootConfig rootConfig = GetRootConfig();

            if (newPath.ToLower() == rootConfig.DownloadPath.ToLower())
            {
                return false;
            }
            rootConfig.DownloadPath = newPath;
            WriteRootConfig(rootConfig);
            return true;
        }

        /// <summary>
        /// 读取根配置文件
        /// 该文件和执行程序同路径
        /// </summary>
        /// <returns></returns>
        public static RootConfig GetRootConfig()
        {
            RootConfig rootConfig = new();

            string appPath = Application.StartupPath;
            
            rootConfig.RootPath = Path.Combine(appPath, "Config");
            rootConfig.DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string configFileName = Path.Combine(appPath,ROOT_CONFIG_PATH_FILENAME);

            if (File.Exists(configFileName))
            {
                string content = File.ReadAllText(configFileName);
                if (string.IsNullOrEmpty(content))
                {
                    WriteRootConfig(rootConfig);
                }
                else
                {
                    rootConfig = JToken.Parse(content).ToObject<RootConfig>()!;
                }
            }
            else
            {
                WriteRootConfig(rootConfig);
            }
            configSyncPath = rootConfig.RootPath;
            return rootConfig;
        }

        private static void WriteRootConfig(RootConfig rootConfig)
        {
            string appPath = Application.StartupPath;
            string configFileName = Path.Combine(appPath, ROOT_CONFIG_PATH_FILENAME);
            File.WriteAllText(configFileName, JToken.FromObject(rootConfig).ToString());
        }

        #endregion 根配置文件操作

        #region 基本配置信息
        private static string configInfo = "{}";
        private const string CONFIG_FILE_NAME = "config.json";

        private ConfigService() { }

        /// <summary>
        /// 修改保存时清空configInfo，使用时再去读取
        /// </summary>
        /// <param name="config"></param>
        private static void SaveConfig(JObject config)
        {
            if (config == null || config is not JObject)
            {
                return;
            }
                
            Thread thread = new(() =>
            {
                try
                {
                    string configPath = Path.Combine(GetSyncConfigPath(), CONFIG_FILE_NAME);
                    string strConfig = config.ToString();
                    File.WriteAllText(configPath, strConfig);
                    configInfo = strConfig;
                }
                catch (Exception ex)
                {
                    LogUtil.Error(ex);
                }
            });
            thread.Start();
            
        }

        /// <summary>
        /// 将配置信息读取到内存中
        /// 每个方法都必须在第一行调用
        /// </summary>
        private static void ReadConfigInfo(bool isForce = false)
        {
            try
            {
                if (string.IsNullOrEmpty(configInfo) || configInfo == "{}" || isForce)
                {
                    string configPath = Path.Combine(GetSyncConfigPath(), CONFIG_FILE_NAME);
                    if (!File.Exists(configPath))
                    {
                        File.WriteAllText(configPath, "{}", Encoding.UTF8);
                        return;
                    }

                    using StreamReader file = File.OpenText(configPath);
                    configInfo = file.ReadToEnd();
                }
                
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
        }

        #endregion 基本配置信息

        #region 记录未关闭的浏览
        /// <summary>
        /// 处理浏览历史URL
        /// </summary>
        public class BrowsingHis
        {
            private const string URL_HISTORY_KEY = "urlHistory";
            public static void WriteHis(string tabKey,string hisUrl,string workgroup,bool locked=false)
            {
                if (string.IsNullOrEmpty(tabKey) || string.IsNullOrEmpty(hisUrl) || hisUrl.ToLower() == BrowserConfig.NewTabURL.ToLower())
                {
                    return;
                }
                var (hisData, configAll) = GetUrlHis(true);
                tabKey = tabKey.ToLower();
                JObject tabNode;
                if (hisData.ContainsKey(tabKey))
                {
                    tabNode = hisData.Value<JObject>(tabKey)!;
                    tabNode["url"] = hisUrl;
                    tabNode["locked"] = locked;
                    tabNode["group"] = workgroup;
                }
                else
                {
                    tabNode = new JObject();
                    hisData.Add(tabKey, tabNode);
                    tabNode.Add("url", hisUrl);
                    tabNode.Add("locked", locked);
                    tabNode.Add("group", workgroup);
                }
                SaveConfig(configAll);
            }

            public static void ModifyLocked(string tabKey, bool locked)
            {
                if (string.IsNullOrEmpty(tabKey))
                {
                    return;
                }
                var (hisData, configAll) = GetUrlHis(true);
                tabKey = tabKey.ToLower();
                if (!hisData.ContainsKey(tabKey)) return;

                JObject tabNode = hisData.Value<JObject>(tabKey)!;
                tabNode["locked"] = locked;
                SaveConfig(configAll);
            }

            public static void ModifyWorkgroup(string tabKey, string workgroup)
            {
                if (string.IsNullOrEmpty(tabKey))
                {
                    return;
                }
                var (hisData, configAll) = GetUrlHis(true);
                tabKey = tabKey.ToLower();
                if (!hisData.ContainsKey(tabKey)) return;

                JObject tabNode = hisData.Value<JObject>(tabKey)!;
                tabNode["group"] = workgroup;
                SaveConfig(configAll);
            }

            public static void RemoveKey(string tabKey)
            {
                if (string.IsNullOrEmpty(tabKey))
                {
                    return;
                }
                var (hisData, configAll) = GetUrlHis(true);
                tabKey = tabKey.ToLower();
                if (hisData.ContainsKey(tabKey))
                {
                    hisData.Remove(tabKey);
                    SaveConfig(configAll);
                }
            }

            public static List<HistoryUrl> ReadAll()
            {
                JObject data = GetUrlHis().hisData;
                List<HistoryUrl> urls = new();
                foreach (JProperty item in data.Properties())
                {
                    JObject node = (JObject)item.Value;
                    urls.Add(new()
                    {
                        Key = item.Name,
                        Url = node.Value<string>("url"),
                        Locked = node.Value<bool>("locked"),
                        WorkGroup = node.Value<string>("group")
                    });
                }
                return urls;
            }

            private static (JObject hisData, JObject configData) GetUrlHis(bool isForce = false)
            {
                ReadConfigInfo(isForce);
                JObject? hisData = new();
                JObject? configAll = (JObject)JToken.Parse(configInfo);

                if (configAll.ContainsKey(URL_HISTORY_KEY))
                {
                    hisData = (JObject)configAll.GetValue(URL_HISTORY_KEY);
                    if (hisData == null)
                    {
                        hisData = new JObject();
                        configAll.Add(URL_HISTORY_KEY, hisData);
                    }
                }
                else
                {
                    configAll.Add(URL_HISTORY_KEY, hisData);
                }

                return (hisData, configAll);
            }
        }
        #endregion 记录未关闭的浏览

        #region 登录账号密码信息
        /// <summary>
        /// 登录账号密码信息
        /// </summary>
        public class LoginInfoHis
        {
            private const string LOGIN_HISTORY_KEY = "loginHistory";

            public static void Save(LoginWebInfoEntity loginWeb)
            {
                if (loginWeb == null || string.IsNullOrEmpty(loginWeb.Url) || string.IsNullOrEmpty(loginWeb.AccountValue))
                {
                    return;
                }                
                var (hisData, configAll) = GetLoginHis(true);

                JObject oldLoginInfo = GetLoginInfo(loginWeb.Url, hisData);
                string newSecretPwd = AESUtil.Encrypt(loginWeb.passwordValue);

                if (oldLoginInfo == null)
                {
                    if (NotifyUtil.Confirm("保存密码吗？", "确认", null, 3000) == DialogResult.OK)
                    {
                        JObject newLoginInfo = new JObject();
                        newLoginInfo.Add("url", loginWeb.Url);
                        newLoginInfo.Add("account", loginWeb.AccountValue);

                        newLoginInfo.Add("password", newSecretPwd);
                        newLoginInfo.Add("add-time", DateUtil.GetDateTime());
                        newLoginInfo.Add("modify-time", DateUtil.GetDateTime());
                        hisData.Add(newLoginInfo);
                        SaveConfig(configAll);
                    }
                }
                else
                {
                    string account = oldLoginInfo.ContainsKey("account") ? oldLoginInfo.GetValue("account").ToString() : "";
                    string oldSecretPwd = oldLoginInfo.ContainsKey("password") ? oldLoginInfo.GetValue("password").ToString() : "";

                    if(account != loginWeb.AccountValue || oldSecretPwd != newSecretPwd)
                    {
                        if (NotifyUtil.Confirm("是否修改已保存的密码？", "确认", null, 3000) == DialogResult.OK)
                        {
                            if (oldLoginInfo.ContainsKey("account"))
                            {
                                oldLoginInfo["account"] = loginWeb.AccountValue;
                            }
                            else
                            {
                                oldLoginInfo.Add("account", loginWeb.AccountValue);
                            }
                            if (oldLoginInfo.ContainsKey("password"))
                            {
                                oldLoginInfo["password"] = newSecretPwd;
                            }
                            else
                            {
                                oldLoginInfo.Add("password", newSecretPwd);
                            }
                            if (oldLoginInfo.ContainsKey("modify-time"))
                            {
                                oldLoginInfo["modify-time"] = DateUtil.GetDateTime();
                            }
                            else
                            {
                                oldLoginInfo.Add("modify-time", DateUtil.GetDateTime());
                            }
                            SaveConfig(configAll);
                        }
                    }
                }

                
            }

            public static (string account, string password) Load(string url)
            {
                JObject loginIfo = GetLoginInfo(url, GetLoginHis().hisData);
                if (loginIfo == null)
                {
                    return ("","");
                }

                string account = loginIfo.ContainsKey("account") ? loginIfo.GetValue("account").ToString() : "";
                string secretPwd = loginIfo.ContainsKey("password") ? loginIfo.GetValue("password").ToString() : "";
                string password = string.IsNullOrEmpty(secretPwd) ? "" : AESUtil.Decrypt(secretPwd);
                return (account, password);
            }

            private static JObject GetLoginInfo(string url,JArray loginDatas)
            {
                if(loginDatas == null)
                {
                    loginDatas = GetLoginHis().hisData;
                }
                foreach (JObject item in loginDatas)
                {
                    if(item.GetValue("url").ToString().ToLower().StartsWith(url.ToLower()))
                    {
                        return item;
                    }
                }
                return null;
            }

            private static (JArray hisData, JObject configData) GetLoginHis(bool isForce = false)
            {
                ReadConfigInfo(isForce);
                JArray hisData = new();
                JObject configAll = (JObject)JToken.Parse(configInfo);

                if (configAll.ContainsKey(LOGIN_HISTORY_KEY))
                {
                    hisData = (JArray)configAll.GetValue(LOGIN_HISTORY_KEY);
                    if (hisData == null)
                    {
                        hisData = new();
                        configAll.Add(LOGIN_HISTORY_KEY, hisData);
                    }
                }
                else
                {
                    configAll.Add(LOGIN_HISTORY_KEY, hisData);
                }

                return (hisData, configAll);
            }
        }
        #endregion 登录账号密码信息

    }

    #region 子类
    public class HistoryUrl
    {
        public String? Key;
        public String? Url;
        /// <summary>
        /// 是否锁定标签
        /// </summary>
        public bool Locked = false;
        /// <summary>
        /// 工作组
        /// </summary>
        public string WorkGroup = "";
    }

    public class RootConfig
    {
        /// <summary>
        /// 根配置文件路径
        /// 也就是需要同步的配置文件保存路径
        /// </summary>
        public string RootPath = "";

        /// <summary>
        /// 下载文件保存路径
        /// </summary>
        public string DownloadPath = "";

    }
    #endregion 子类
}
