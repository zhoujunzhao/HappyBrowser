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
    public partial class BrowserSet : Form
    {
        public BrowserSet()
        {
            InitializeComponent();

        }

        private void BrowserSet_Load(object sender, EventArgs e)
        {
            ReadRootConfigPath();
        }

        private bool SaveConfig()
        {
            if (!ConfigModifyFlg.IsModify)
            {
                NotifyUtil.Warn("所有配置项目没有变化，不用保存");
                return false;
            }

            if (ConfigModifyFlg.RootConfigPath)
            {
                ConfigService.ModifyRootConfigPath(this.txtRootConfigPath.Text);
                ConfigModifyFlg.RootConfigPath = false;
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

        #region 根配置文件设置
        private void ReadRootConfigPath()
        {
            this.txtRootConfigPath.Text = ConfigService.GetRootConfigPath();
        }
        private void BtnSelRootConfigPath_Click(object sender, EventArgs e)
        {
            this.fbdSelectPath.Description="选择根配置文件保存路径";
            this.fbdSelectPath.ShowNewFolderButton =true;
            this.fbdSelectPath.SelectedPath = ConfigService.GetRootConfigPath();
            if (this.fbdSelectPath.ShowDialog() == DialogResult.OK)
            {
                string newConfigPath = this.fbdSelectPath.SelectedPath;
                if (!System.IO.Directory.Exists(newConfigPath))
                {
                    NotifyUtil.Warn($"【{newConfigPath}】不存在。请重新选择。");
                    return;
                }
                this.txtRootConfigPath.Text = newConfigPath;
                ConfigModifyFlg.RootConfigPath = true;
            }
        }

        #endregion 根配置文件设置


        #region 内部类
        public static class ConfigModifyFlg
        {
            private static bool rootConfigPath = false;

            public static bool RootConfigPath { get { return rootConfigPath; } set { rootConfigPath=value; } }

            /// <summary>
            /// 所有配置项目是否有修改
            /// </summary>
            public static bool IsModify
            {
                get
                {
                    // 用或连接所有配置项
                    return rootConfigPath;
                }
            }
        }
        #endregion 内部类


        private void BrowserSet_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(SystemColors.ControlDark, 1.0f);
            e.Graphics.DrawLine(p, 10, 400, 670, 400);
        }
    }
}
