using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Utilities;
using Utilities.ExMethod;
using VisionControl;

namespace VisionApplication
{
    public partial class FLogin : Sunny.UI.UIEditForm
    {
        ILog log = LogManager.GetLogger(typeof(FLogin));
        internal LoginUser User = MyAppConfig.User;
        public FLogin()
        {
            base.Font = new Font("宋体", 9);
            InitializeComponent();

            base.Font = new Font("宋体", 9);
            if (User != null && User.Level == AccessLevel.Administrator)
            {
                rbAdmin.Checked = true;
            }
            else
            {
                rbOperator.Checked = true;
            }
            uiCheckBox1.Checked = MyAppConfig.AutoLogin;
            Panel1_VisibleChanged(panel1, EventArgs.Empty);
            panel1.VisibleChanged += Panel1_VisibleChanged;
        }

        private void Panel1_VisibleChanged(object sender, EventArgs e)
        {
            this.Height = panel1.Visible ? panel1.Bottom + 80 : panel1.Top + 80; 
        }

        bool checkPwd()
        {
            var level = rbAdmin.Checked ? AccessLevel.Administrator : AccessLevel.Operator;
            var user = MyAppConfig.GetUser(level);
            return string.IsNullOrEmpty(tbPwd.Text) && string.IsNullOrEmpty(user?.Password) || tbPwd.Text == user?.Password;
        }
        private void btnOK_Click(object sender, EventArgs e)
        { 
            if (checkPwd())
            {
                try
                {
                    if (User == null)
                        User = new LoginUser();
                    User.Level = rbAdmin.Checked ? AccessLevel.Administrator : AccessLevel.Operator;
                    User.Password = tbPwd.Text;
                    MyAppConfig.User = User; 
                    MyAppConfig.SaveLoginSet(User, uiCheckBox1.Checked);
                    this.DialogResult = DialogResult.OK;
                    
                }
                catch (Exception ex)
                {
                    MessageBoxE.Show(this, "密码修改失败：" + ex.Message);
                }
            }
            else
            {
                MessageBoxE.Show(this, "输入密码错误");
                DialogResult = DialogResult.None;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            var user = User?.DeepClone() ?? new LoginUser();

            if (!checkPwd())
            {
                MessageBoxE.Show(this, "密码错误");
                return;
            }
            if (tbNewPass1.Text != tbNewPass2.Text)
            {
                MessageBoxE.Show(this, "两次密码输入不一致");
                return;
            }
            try
            {
                user.Password = tbNewPass1.Text;
                user.Level = rbAdmin.Checked ? AccessLevel.Administrator : AccessLevel.Operator;
                MyAppConfig.Save(user);
                if (User == null || user.Level == User.Level)
                {
                    User = user;
                    MyAppConfig.User = user;
                    MyAppConfig.SaveLoginSet(User, MyAppConfig.AutoLogin);
                }
                log.Info(user.Level.GetDescription() + "修改密码");
                MessageBoxE.Show(this, "密码修改成功");
                panel1.Visible = false;
                tbNewPass1.Clear();
                tbNewPass2.Clear();

            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, "密码修改失败：" + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }
    }
}
