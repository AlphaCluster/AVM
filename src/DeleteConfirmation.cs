using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVM
{
    public partial class DeleteConfirmation : Form
    {
        private bool _delete = false;

        public DeleteConfirmation()
        {
            InitializeComponent();
        }

        public bool Delete
        {
            get { return _delete; }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            _delete = true;
            Properties.Settings.Default.PromptOnDelete = !dontPromptCheckBox.Checked;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PromptOnDelete = !dontPromptCheckBox.Checked;
            this.Close();
        }
    }
}
