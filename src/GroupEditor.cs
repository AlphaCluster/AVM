//  AVM - Appliction used to manage Web Videos, Video Files, and DVD's
//
//  Copyright (c) 2008-2009 Nicholas Omann
//
//  Permission is hereby granted, free of charge, to any person
//  obtaining a copy of this software and associated documentation
//  files (the "Software"), to deal in the Software without
//  restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the
//  Software is furnished to do so, subject to the following
//  conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//  WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//  OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVM
{
    public partial class GroupEditor : Form
    {
        private string _name;

        #region Properties
        /// <summary>
        /// Returns the new name for the group.
        /// Returns "" if no new name is created.
        /// </summary>
        public string NewName
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a GroupEditorDialog using the originalName passed in as the
        /// base name. Also the title can be configured using formName and the
        /// button can be titled using buttonName so that the Editor can be used
        /// both for addition and editing of groups.
        /// </summary>
        /// <param name="originalName">This is used to populate the name TextBox.</param>
        /// <param name="formName">This string will be used as the forms title.</param>
        /// <param name="buttonName">This will be the name on the acceptence button.</param>
        public GroupEditor(string originalName,
                           string formName,
                           string buttonName)
        {
            InitializeComponent();
            _name = originalName;
            renameTextBox.Text = originalName;
            this.Text = formName;
            okButton.Text = buttonName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This merely closes the dialog so that the mainForm can get the NewName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender,
                                    EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This closes the form and makes it so NewName will return "".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender,
                                        EventArgs e)
        {
            _name = "";
            this.Close();
        }
        
        /// <summary>
        /// This saves the text in the TextBox to _name every time it's changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameTextBox_TextChanged(object sender,
                                               EventArgs e)
        {
            _name = renameTextBox.Text;
        }

        /// <summary>
        /// This method allows for the user to press "enter" in order to finish
        /// using this dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameTextBox_KeyPress(object sender,
                                            KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.Close();
        }
        #endregion
    }
}
