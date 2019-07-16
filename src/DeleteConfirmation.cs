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

        #region Properties
        /// <summary>
        /// This returns true if the user wants to delete the selected video.
        /// </summary>
        public bool Delete
        {
            get { return _delete; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates the DeleteConfirmation form.
        /// </summary>
        public DeleteConfirmation()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is called when the "Delete" button is pressed.
        /// It sets _delete to true and then closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender,
                                        EventArgs e)
        {
            _delete = true;
            Properties.Settings.Default.PromptOnDelete = !dontPromptCheckBox.Checked;
            this.Close();
        }

        /// <summary>
        /// This is called when the "Cancel" button is pressed.
        /// It sets _delete to false and then closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender,
                                        EventArgs e)
        {
            _delete = false;
            Properties.Settings.Default.PromptOnDelete = !dontPromptCheckBox.Checked;
            this.Close();
        }
        #endregion
    }
}
