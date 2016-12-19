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
    public partial class AddContactDialog : Form
    {
        private Database db = null;
        public AddContactDialog(Database db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Contact newContact = new Contact()
            {
                Name = tbName.Text,
                Phone = tbPhone.Text
            };
            db.AddContact(newContact);
            this.Close();
        }
    }
}
