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
    public partial class ContactList : Form
    {
        private Database db = null;
        public ContactList(Database logedInDatabase)
        {
            InitializeComponent();
            db = logedInDatabase;
        }

        private void ContactList_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        public void UpdateList()
        {
            
            listviewContactList.BeginUpdate();
            listviewContactList.Items.Clear();
            foreach (Contact contact in db.AcquireContacts())
            {
                ListViewItem item = new ListViewItem();
                item.Text = contact.ID.ToString();
                item.SubItems.Add(contact.Name);

                item.SubItems.Add(contact.Phone);
                listviewContactList.Items.Add(item);
            }
            listviewContactList.EndUpdate();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            AddContactDialog dialog = new AddContactDialog(db);
            dialog.Show();
            dialog.FormClosed += (s, ev) =>
            {
                UpdateList();
            };
        }

        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            var selectedItem = listviewContactList.SelectedItems[0];
        }

    }
}
