using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "")
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            if (tbPassword.Text == "")
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            Database db = new Database();
            DatabaseStatus status = db.Register(tbUsername.Text, tbPassword.Text);
            if (status == DatabaseStatus.UserExists)
            {
                MessageBox.Show("用户已存在！");
            }
            if (status == DatabaseStatus.Success)
            {

                MessageBox.Show("用户注册成功！");
            }
        }
    }
}
