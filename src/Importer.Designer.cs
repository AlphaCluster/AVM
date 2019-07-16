namespace AVM
{
    partial class Importer
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
            this.restoreBrowseButton = new System.Windows.Forms.Button();
            this.restoreCancelButton = new System.Windows.Forms.Button();
            this.restoreFileTextBox = new System.Windows.Forms.TextBox();
            this.restoreFileLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.restoreOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.folderTabPage = new System.Windows.Forms.TabPage();
            this.folderPatternTextBox = new System.Windows.Forms.TextBox();
            this.patternLabel = new System.Windows.Forms.Label();
            this.folderAddButton = new System.Windows.Forms.Button();
            this.folderFileTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowseButton = new System.Windows.Forms.Button();
            this.folderFileLabel = new System.Windows.Forms.Label();
            this.folderCancelButton = new System.Windows.Forms.Button();
            this.groupLabel = new System.Windows.Forms.Label();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.groupForwardButton = new System.Windows.Forms.Button();
            this.groupBackButton = new System.Windows.Forms.Button();
            this.restoreTabPage = new System.Windows.Forms.TabPage();
            this.restoreAddButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.folderTabPage.SuspendLayout();
            this.restoreTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // restoreBrowseButton
            // 
            this.restoreBrowseButton.Location = new System.Drawing.Point(313, 38);
            this.restoreBrowseButton.Margin = new System.Windows.Forms.Padding(4);
            this.restoreBrowseButton.Name = "restoreBrowseButton";
            this.restoreBrowseButton.Size = new System.Drawing.Size(100, 26);
            this.restoreBrowseButton.TabIndex = 10;
            this.restoreBrowseButton.Text = "Browse";
            this.restoreBrowseButton.UseVisualStyleBackColor = true;
            this.restoreBrowseButton.Click += new System.EventHandler(this.restoreBrowseButton_Click);
            // 
            // restoreCancelButton
            // 
            this.restoreCancelButton.Location = new System.Drawing.Point(313, 99);
            this.restoreCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.restoreCancelButton.Name = "restoreCancelButton";
            this.restoreCancelButton.Size = new System.Drawing.Size(100, 26);
            this.restoreCancelButton.TabIndex = 9;
            this.restoreCancelButton.Text = "Cancel";
            this.restoreCancelButton.UseVisualStyleBackColor = true;
            this.restoreCancelButton.Click += new System.EventHandler(this.restoreCancelButton_Click);
            // 
            // restoreFileTextBox
            // 
            this.restoreFileTextBox.Location = new System.Drawing.Point(47, 40);
            this.restoreFileTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.restoreFileTextBox.Name = "restoreFileTextBox";
            this.restoreFileTextBox.Size = new System.Drawing.Size(258, 22);
            this.restoreFileTextBox.TabIndex = 5;
            this.restoreFileTextBox.TextChanged += new System.EventHandler(this.topTextBox_TextChanged);
            // 
            // restoreFileLabel
            // 
            this.restoreFileLabel.AutoSize = true;
            this.restoreFileLabel.Location = new System.Drawing.Point(9, 43);
            this.restoreFileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.restoreFileLabel.Name = "restoreFileLabel";
            this.restoreFileLabel.Size = new System.Drawing.Size(30, 16);
            this.restoreFileLabel.TabIndex = 4;
            this.restoreFileLabel.Text = "File";
            // 
            // restoreOpenFileDialog
            // 
            this.restoreOpenFileDialog.DefaultExt = "xml";
            this.restoreOpenFileDialog.Filter = "xml files|*.xml|All files|*.*";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.folderTabPage);
            this.tabControl1.Controls.Add(this.restoreTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(425, 163);
            this.tabControl1.TabIndex = 11;
            // 
            // folderTabPage
            // 
            this.folderTabPage.Controls.Add(this.folderPatternTextBox);
            this.folderTabPage.Controls.Add(this.patternLabel);
            this.folderTabPage.Controls.Add(this.folderAddButton);
            this.folderTabPage.Controls.Add(this.folderFileTextBox);
            this.folderTabPage.Controls.Add(this.folderBrowseButton);
            this.folderTabPage.Controls.Add(this.folderFileLabel);
            this.folderTabPage.Controls.Add(this.folderCancelButton);
            this.folderTabPage.Controls.Add(this.groupLabel);
            this.folderTabPage.Controls.Add(this.groupComboBox);
            this.folderTabPage.Controls.Add(this.groupForwardButton);
            this.folderTabPage.Controls.Add(this.groupBackButton);
            this.folderTabPage.Location = new System.Drawing.Point(4, 25);
            this.folderTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.folderTabPage.Name = "folderTabPage";
            this.folderTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.folderTabPage.Size = new System.Drawing.Size(417, 134);
            this.folderTabPage.TabIndex = 0;
            this.folderTabPage.Text = "Folder";
            this.folderTabPage.UseVisualStyleBackColor = true;
            // 
            // folderPatternTextBox
            // 
            this.folderPatternTextBox.Location = new System.Drawing.Point(67, 70);
            this.folderPatternTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.folderPatternTextBox.Name = "folderPatternTextBox";
            this.folderPatternTextBox.Size = new System.Drawing.Size(238, 22);
            this.folderPatternTextBox.TabIndex = 17;
            // 
            // patternLabel
            // 
            this.patternLabel.AutoSize = true;
            this.patternLabel.Location = new System.Drawing.Point(9, 73);
            this.patternLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.patternLabel.Name = "patternLabel";
            this.patternLabel.Size = new System.Drawing.Size(50, 16);
            this.patternLabel.TabIndex = 16;
            this.patternLabel.Text = "Pattern";
            // 
            // folderAddButton
            // 
            this.folderAddButton.Enabled = false;
            this.folderAddButton.Location = new System.Drawing.Point(205, 99);
            this.folderAddButton.Margin = new System.Windows.Forms.Padding(4);
            this.folderAddButton.Name = "folderAddButton";
            this.folderAddButton.Size = new System.Drawing.Size(100, 26);
            this.folderAddButton.TabIndex = 15;
            this.folderAddButton.Text = "Add";
            this.folderAddButton.UseVisualStyleBackColor = true;
            this.folderAddButton.Click += new System.EventHandler(this.folderAddButton_Click);
            // 
            // folderFileTextBox
            // 
            this.folderFileTextBox.Location = new System.Drawing.Point(64, 40);
            this.folderFileTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.folderFileTextBox.Name = "folderFileTextBox";
            this.folderFileTextBox.Size = new System.Drawing.Size(241, 22);
            this.folderFileTextBox.TabIndex = 12;
            this.folderFileTextBox.TextChanged += new System.EventHandler(this.folderFileTextBox_TextChanged);
            // 
            // folderBrowseButton
            // 
            this.folderBrowseButton.Location = new System.Drawing.Point(313, 38);
            this.folderBrowseButton.Margin = new System.Windows.Forms.Padding(4);
            this.folderBrowseButton.Name = "folderBrowseButton";
            this.folderBrowseButton.Size = new System.Drawing.Size(100, 26);
            this.folderBrowseButton.TabIndex = 14;
            this.folderBrowseButton.Text = "Browse";
            this.folderBrowseButton.UseVisualStyleBackColor = true;
            this.folderBrowseButton.Click += new System.EventHandler(this.folderBrowseButton_Click);
            // 
            // folderFileLabel
            // 
            this.folderFileLabel.AutoSize = true;
            this.folderFileLabel.Location = new System.Drawing.Point(9, 43);
            this.folderFileLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.folderFileLabel.Name = "folderFileLabel";
            this.folderFileLabel.Size = new System.Drawing.Size(47, 16);
            this.folderFileLabel.TabIndex = 11;
            this.folderFileLabel.Text = "Folder";
            // 
            // folderCancelButton
            // 
            this.folderCancelButton.Location = new System.Drawing.Point(313, 99);
            this.folderCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.folderCancelButton.Name = "folderCancelButton";
            this.folderCancelButton.Size = new System.Drawing.Size(100, 26);
            this.folderCancelButton.TabIndex = 13;
            this.folderCancelButton.Text = "Cancel";
            this.folderCancelButton.UseVisualStyleBackColor = true;
            this.folderCancelButton.Click += new System.EventHandler(this.folderCancelButton_Click);
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.Location = new System.Drawing.Point(9, 11);
            this.groupLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(101, 16);
            this.groupLabel.TabIndex = 6;
            this.groupLabel.Text = "Group to Add to";
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(146, 8);
            this.groupComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(159, 24);
            this.groupComboBox.Sorted = true;
            this.groupComboBox.TabIndex = 4;
            // 
            // groupForwardButton
            // 
            this.groupForwardButton.Location = new System.Drawing.Point(313, 6);
            this.groupForwardButton.Margin = new System.Windows.Forms.Padding(4);
            this.groupForwardButton.Name = "groupForwardButton";
            this.groupForwardButton.Size = new System.Drawing.Size(20, 26);
            this.groupForwardButton.TabIndex = 5;
            this.groupForwardButton.Text = ">";
            this.groupForwardButton.UseVisualStyleBackColor = true;
            this.groupForwardButton.Click += new System.EventHandler(this.groupForwardButton_Click);
            // 
            // groupBackButton
            // 
            this.groupBackButton.Location = new System.Drawing.Point(118, 6);
            this.groupBackButton.Margin = new System.Windows.Forms.Padding(4);
            this.groupBackButton.Name = "groupBackButton";
            this.groupBackButton.Size = new System.Drawing.Size(20, 26);
            this.groupBackButton.TabIndex = 7;
            this.groupBackButton.Text = "<";
            this.groupBackButton.UseVisualStyleBackColor = true;
            this.groupBackButton.Click += new System.EventHandler(this.groupBackButton_Click);
            // 
            // restoreTabPage
            // 
            this.restoreTabPage.Controls.Add(this.restoreAddButton);
            this.restoreTabPage.Controls.Add(this.restoreFileTextBox);
            this.restoreTabPage.Controls.Add(this.restoreBrowseButton);
            this.restoreTabPage.Controls.Add(this.restoreFileLabel);
            this.restoreTabPage.Controls.Add(this.restoreCancelButton);
            this.restoreTabPage.Location = new System.Drawing.Point(4, 25);
            this.restoreTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.restoreTabPage.Name = "restoreTabPage";
            this.restoreTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.restoreTabPage.Size = new System.Drawing.Size(417, 134);
            this.restoreTabPage.TabIndex = 1;
            this.restoreTabPage.Text = "Restore";
            this.restoreTabPage.UseVisualStyleBackColor = true;
            // 
            // restoreAddButton
            // 
            this.restoreAddButton.Enabled = false;
            this.restoreAddButton.Location = new System.Drawing.Point(205, 99);
            this.restoreAddButton.Margin = new System.Windows.Forms.Padding(4);
            this.restoreAddButton.Name = "restoreAddButton";
            this.restoreAddButton.Size = new System.Drawing.Size(100, 26);
            this.restoreAddButton.TabIndex = 11;
            this.restoreAddButton.Text = "Restore";
            this.restoreAddButton.UseVisualStyleBackColor = true;
            this.restoreAddButton.Click += new System.EventHandler(this.restoreAddButton_Click);
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 163);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Importer";
            this.Text = "Importer";
            this.Load += new System.EventHandler(this.Importer_Load);
            this.tabControl1.ResumeLayout(false);
            this.folderTabPage.ResumeLayout(false);
            this.folderTabPage.PerformLayout();
            this.restoreTabPage.ResumeLayout(false);
            this.restoreTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox restoreFileTextBox;
        private System.Windows.Forms.Label restoreFileLabel;
        private System.Windows.Forms.Button restoreCancelButton;
        private System.Windows.Forms.Button restoreBrowseButton;
        private System.Windows.Forms.OpenFileDialog restoreOpenFileDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage folderTabPage;
        private System.Windows.Forms.Label groupLabel;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.Button groupForwardButton;
        private System.Windows.Forms.Button groupBackButton;
        private System.Windows.Forms.TabPage restoreTabPage;
        private System.Windows.Forms.TextBox folderFileTextBox;
        private System.Windows.Forms.Button folderBrowseButton;
        private System.Windows.Forms.Label folderFileLabel;
        private System.Windows.Forms.Button folderCancelButton;
        private System.Windows.Forms.Button folderAddButton;
        private System.Windows.Forms.Button restoreAddButton;
        private System.Windows.Forms.TextBox folderPatternTextBox;
        private System.Windows.Forms.Label patternLabel;
    }
}