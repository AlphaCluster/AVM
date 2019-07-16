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
    public partial class Importer : Form
    {
        public Database db;
        public MainForm main;
        public int currentSelected = 1;
        private bool _restoreTookPlace = false;

        #region Properties
        /// <summary>
        /// Tells if a restore has taken place so the main window can react.
        /// </summary>
        public bool RestoreTookPlace
        {
            get { return _restoreTookPlace; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Importer form passing the mainform, database and currently
        /// selected group.
        /// </summary>
        /// <param name="avm">The mainFrom for the program.</param>
        /// <param name="database">This is the main database.</param>
        /// <param name="selectedGroup">This is the currently selected group.</param>
        public Importer(MainForm avm,
                        Database database,
                        int selectedGroup)
        {
            InitializeComponent();
            db = database;
            main = avm;
            currentSelected = selectedGroup;
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// Runs on load. Populates comboBox on the folder tab and populates the
        /// patternTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importer_Load(object sender,
                                   EventArgs e)
        {
            db.refreshComboBoxGroups(groupComboBox);
            groupComboBox.SelectedIndex = currentSelected;
            if (db.ParentGroup == 0)
                groupBackButton.Enabled = false;
            if (groupComboBox.Items.Count == 0)
                groupForwardButton.Enabled = false;
            folderPatternTextBox.Text = Properties.Settings.Default.FileNamePattern;
        }
        #endregion

        #region Group Methods
        /// <summary>
        /// Moves the comboBox forward in the groups list based
        /// off the currently selected group in the ComboBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupForwardButton_Click(object sender,
                                              EventArgs e)
        {
            db.ParentGroup = ((AVM.Types.Group)groupComboBox.SelectedItem).Id;
            groupComboBox.SelectedIndex = -1; // So it doesn't break
            db.refreshComboBoxGroups(groupComboBox);
            if (groupComboBox.Items.Count > 0)
                groupComboBox.SelectedIndex = 0;
            else
                groupForwardButton.Enabled = false;
            groupBackButton.Enabled = true;
        }

        /// <summary>
        /// Moves the comboBox back in the groups list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBackButton_Click(object sender,
                                           EventArgs e)
        {
            db.gotoParent();
            if (db.ParentGroup == 0)
                groupBackButton.Enabled = false;
            groupComboBox.SelectedIndex = -1; // So it doesn't break
            db.refreshComboBoxGroups(groupComboBox);
            groupComboBox.SelectedIndex = 0;
            groupForwardButton.Enabled = true;
        }
        #endregion

        #region Restore Methods
        /// <summary>
        /// Opens up the openFileDialog so that the backup xml file can be selected
        /// to restore data from.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreBrowseButton_Click(object sender,
                                              EventArgs e)
        {
            restoreOpenFileDialog.ShowDialog();
            restoreFileTextBox.Text = restoreOpenFileDialog.FileName;
        }

        /// <summary>
        /// Enables and disables the restoreAddButton based on if there is
        /// currently a file selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topTextBox_TextChanged(object sender,
                                            EventArgs e)
        {
            if (restoreFileTextBox.Text == "")
                restoreAddButton.Enabled = false;
            else
                restoreAddButton.Enabled = true;
        }

        /// <summary>
        /// Runs the backup xml parser and restores the database from it.
        /// Also refreshes the group list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreAddButton_Click(object sender,
                                           EventArgs e)
        {
            if (restoreFileTextBox.Text != "")
            {
                AVM.Parsers.BackupParser parser = new AVM.Parsers.BackupParser(db);
                parser.ReadXmlBackup(restoreFileTextBox.Text);
                _restoreTookPlace = true;
                this.Close();
            }
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreCancelButton_Click(object sender,
                                              EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Folder Methods
        /// <summary>
        /// Displays the folderBrowserDialog and selects the folder to be added.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderBrowseButton_Click(object sender,
                                              EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            folderFileTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// Runs the FolderParser based on the selected folder and refreshes
        /// the group list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderAddButton_Click(object sender,
                                           EventArgs e)
        {
            if (folderFileTextBox.Text != "")
            {
                AVM.Parsers.FolderParser parser = new AVM.Parsers.FolderParser(folderFileTextBox.Text, db);
                parser.Pattern = folderPatternTextBox.Text;
                if (groupComboBox.SelectedIndex > -1)
                    parser.parse(((AVM.Types.Group)groupComboBox.SelectedItem).Id);
                else
                    parser.parse(0);
                main.refreshGroups();
                this.Close();
            }
        }

        /// <summary>
        /// Enables and disables the folderAddButton based on if there is
        /// currently a folder selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderFileTextBox_TextChanged(object sender,
                                                   EventArgs e)
        {
            if (folderFileTextBox.Text == "")
                folderAddButton.Enabled = false;
            else
                folderAddButton.Enabled = true;
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderCancelButton_Click(object sender,
                                              EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
