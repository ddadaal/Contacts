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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
            DatabaseStatus status =  db.Login(tbUsername.Text, tbPassword.Text);
            if (status == DatabaseStatus.WrongPassword)
            {
                MessageBox.Show("密码错误！");

            }
            if (status == DatabaseStatus.Success)
            {
                ContactList formContact = new Contacts.ContactList(db);
                formContact.Show();
                this.Hide();

            }
            if (status == DatabaseStatus.UserNotExists)
            {
                MessageBox.Show("用户不存在");
            }
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            (new Register()).Show();
        }

    }
}
