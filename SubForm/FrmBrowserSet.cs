using HappyBrowser.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HappyBrowser.SubForm
{
    public partial class FrmBrowserSet : Form
    {
        public FrmBrowserSet()
        {
            InitializeComponent();

        }

        private void BrowserSet_Load(object sender, EventArgs e)
        {
            RootConfig rootConfig = ConfigService.GetRootConfig();
            this.txtRootConfigPath.Text = rootConfig.RootPath;
            this.txtDownloadPath.Text = rootConfig.DownloadPath;
        }

        private bool SaveConfig()
        {
            if (!ConfigModifyFlg.IsModify)
            {
                NotifyUtil.Warn("所有配置项目没有变化，不用保存");
                return false;
            }

            if (ConfigModifyFlg.SyncConfigPath)
            {
                ConfigService.ModifySyncConfigPath(this.txtRootConfigPath.Text);
                ConfigModifyFlg.SyncConfigPath = false;
            }

            if (ConfigModifyFlg.DownloadPath)
            {
                ConfigService.ModifyDownloadPath(this.txtDownloadPath.Text);
                ConfigModifyFlg.DownloadPath = false;
            }

            return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void BtnSaveClose_Click(object sender, EventArgs e)
        {
            if (SaveConfig())
            {
                this.Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (ConfigModifyFlg.IsModify && NotifyUtil.Confirm("配置项目有修改，确实保存就退出吗？", "确认", this, 3000) == DialogResult.Cancel)
            {
                return;
            }
            this.Close();

        }

        #region 同步配置文件设置

        private void BtnSelRootConfigPath_Click(object sender, EventArgs e)
        {
            this.fbdSelectPath.Description="选择根配置文件保存路径";
            this.fbdSelectPath.ShowNewFolderButton =true;
            this.fbdSelectPath.SelectedPath = ConfigService.GetSyncConfigPath();
            if (this.fbdSelectPath.ShowDialog() == DialogResult.OK)
            {
                string newConfigPath = this.fbdSelectPath.SelectedPath;
                if (!System.IO.Directory.Exists(newConfigPath))
                {
                    NotifyUtil.Warn($"【{newConfigPath}】不存在。请重新选择。");
                    return;
                }
                this.txtRootConfigPath.Text = newConfigPath;
                ConfigModifyFlg.SyncConfigPath = true;
            }
        }

        #endregion 同步配置文件设置

        #region 下载文件保存路径设置
        private void BtnSelDownloadPath_Click(object sender, EventArgs e)
        {
            this.fbdSelectPath.Description="选择下载文件保存路径";
            this.fbdSelectPath.ShowNewFolderButton =true;
            this.fbdSelectPath.SelectedPath = ConfigService.GetDownloadPath();
            if (this.fbdSelectPath.ShowDialog() == DialogResult.OK)
            {
                string newPath = this.fbdSelectPath.SelectedPath;
                if (!System.IO.Directory.Exists(newPath))
                {
                    NotifyUtil.Warn($"【{newPath}】不存在。请重新选择。");
                    return;
                }
                this.txtDownloadPath.Text = newPath;
                ConfigModifyFlg.DownloadPath = true;
            }
        }

        #endregion 下载文件保存路径设置

        private void BrowserSet_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(SystemColors.ControlDark, 1.0f);
            e.Graphics.DrawLine(p, 10, 400, 670, 400);
        }

        #region 内部类
        public static class ConfigModifyFlg
        {
            /// <summary>
            /// 同步配置是否有变动
            /// true=有变动，需要保存
            /// false=无变动，无需保存
            /// </summary>
            private static bool syncConfigPath = false;
            /// <summary>
            /// 下载路径配置是否有变动
            /// true=有变动，需要保存
            /// false=无变动，无需保存
            /// </summary>
            private static bool downloadPath = false;

            public static bool SyncConfigPath { get { return syncConfigPath; } set { syncConfigPath=value; } }

            public static bool DownloadPath { get { return downloadPath; } set { downloadPath=value; } }

            /// <summary>
            /// 所有配置项目是否有修改
            /// </summary>
            public static bool IsModify
            {
                get
                {
                    // 用或连接所有配置项
                    return syncConfigPath || downloadPath;
                }
            }
        }
        #endregion 内部类

    }
}
