namespace Contacts
{
    partial class ContactList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listviewContactList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnAddContact = new System.Windows.Forms.Button();
            this.btnDeleteContact = new System.Windows.Forms.Button();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listviewContactList
            // 
            this.listviewContactList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.listviewContactList.FullRowSelect = true;
            this.listviewContactList.Location = new System.Drawing.Point(23, 68);
            this.listviewContactList.Name = "listviewContactList";
            this.listviewContactList.Size = new System.Drawing.Size(283, 414);
            this.listviewContactList.TabIndex = 0;
            this.listviewContactList.UseCompatibleStateImageBehavior = false;
            this.listviewContactList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "姓名";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "电话";
            // 
            // btnAddContact
            // 
            this.btnAddContact.Location = new System.Drawing.Point(327, 82);
            this.btnAddContact.Name = "btnAddContact";
            this.btnAddContact.Size = new System.Drawing.Size(85, 39);
            this.btnAddContact.TabIndex = 1;
            this.btnAddContact.Text = "添加联系人";
            this.btnAddContact.UseVisualStyleBackColor = true;
            this.btnAddContact.Click += new System.EventHandler(this.btnAddContact_Click);
            // 
            // btnDeleteContact
            // 
            this.btnDeleteContact.Location = new System.Drawing.Point(327, 297);
            this.btnDeleteContact.Name = "btnDeleteContact";
            this.btnDeleteContact.Size = new System.Drawing.Size(85, 39);
            this.btnDeleteContact.TabIndex = 2;
            this.btnDeleteContact.Text = "删除联系人";
            this.btnDeleteContact.UseVisualStyleBackColor = true;
            this.btnDeleteContact.Click += new System.EventHandler(this.btnDeleteContact_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            // 
            // ContactList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 571);
            this.Controls.Add(this.btnDeleteContact);
            this.Controls.Add(this.btnAddContact);
            this.Controls.Add(this.listviewContactList);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ContactList";
            this.Text = "联系人界面";
            this.Load += new System.EventHandler(this.ContactList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listviewContactList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnAddContact;
        private System.Windows.Forms.Button btnDeleteContact;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}