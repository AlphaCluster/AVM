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
        public mainForm main;
        public int currentSelected = 1;

        public Importer(mainForm VideoManager, Database database, int selectedGroup)
        {
            InitializeComponent();
            db = database;
            main = VideoManager;
            currentSelected = selectedGroup;
        }

        private void Importer_Load(object sender, EventArgs e)
        {
            db.refreshComboBoxGroups(groupComboBox);
            groupComboBox.SelectedIndex = currentSelected;
            if (db.ParentGroup == 0)
                groupBackButton.Enabled = false;
            folderPatternTextBox.Text = Properties.Settings.Default.FileNamePattern;
        }

        private void groupForwardButton_Click(object sender, EventArgs e)
        {
            db.ParentGroup = ((AVM.Types.Group)groupComboBox.SelectedItem).Id;
            groupComboBox.SelectedIndex = -1; // So it doesn't break
            db.refreshComboBoxGroups(groupComboBox);
            if (groupComboBox.Items.Count > 0)
                groupComboBox.SelectedIndex = 1;
        }

        private void groupBackButton_Click(object sender, EventArgs e)
        {
            if (((AVM.Types.Group)groupComboBox.SelectedItem).ParentId != 0)
            {
                db.ParentGroup = ((AVM.Types.Group)groupComboBox.SelectedItem).ParentId;
                if (db.ParentGroup == 0)
                    groupBackButton.Enabled = false;
                groupComboBox.SelectedIndex = -1; // So it doesn't break
                db.refreshComboBoxGroups(groupComboBox);
                groupComboBox.SelectedIndex = 1;
            }
        }

        private void backupBrowseButton_Click(object sender, EventArgs e)
        {
            backupOpenFileDialog.ShowDialog();
            backupFileTextBox.Text = backupOpenFileDialog.FileName;
        }

        private void topTextBox_TextChanged(object sender, EventArgs e)
        {
            if (backupFileTextBox.Text == "")
                backupCancelButton.Enabled = false;
            else
                backupCancelButton.Enabled = true;
        }

        private void backupAddButton_Click(object sender, EventArgs e)
        {
            if (backupFileTextBox.Text != "")
            {
                AVM.Parsers.BackupParser parser = new AVM.Parsers.BackupParser(db);
                parser.ReadXmlBackup(backupFileTextBox.Text);
                main.refreshGroups();
                this.Close();
            }
        }

        private void backupCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void folderBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            folderFileTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void folderAddButton_Click(object sender, EventArgs e)
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

        private void folderCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
