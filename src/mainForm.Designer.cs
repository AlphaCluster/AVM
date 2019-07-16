namespace AVM
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mainTopGroupTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.backButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.searchGroupTextBox = new System.Windows.Forms.TextBox();
            this.searchGroupsLabel = new System.Windows.Forms.Label();
            this.groupListBox = new System.Windows.Forms.ListBox();
            this.groupBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGroupMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.aboutButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.newGroupButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupTitleLabel = new System.Windows.Forms.Label();
            this.nodeListView = new System.Windows.Forms.ListView();
            this.nodeBoxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNodeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.newButton = new System.Windows.Forms.Button();
            this.nodeSearchAllCheckBox = new System.Windows.Forms.CheckBox();
            this.nodeSearchTextBox = new System.Windows.Forms.TextBox();
            this.nodeSearchLabel = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.mainTopGroupTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxMenu.SuspendLayout();
            this.leftTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.nodeBoxMenu.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.mainTopGroupTableLayoutPanel);
            this.mainSplitContainer.Panel1.Controls.Add(this.leftTableLayoutPanel);
            this.mainSplitContainer.Panel1MinSize = 100;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.mainSplitContainer.Panel2.Controls.Add(this.mainTableLayoutPanel);
            this.mainSplitContainer.Panel2MinSize = 100;
            this.mainSplitContainer.Size = new System.Drawing.Size(681, 432);
            this.mainSplitContainer.SplitterDistance = 227;
            this.mainSplitContainer.TabIndex = 2;
            // 
            // mainTopGroupTableLayoutPanel
            // 
            this.mainTopGroupTableLayoutPanel.ColumnCount = 3;
            this.mainTopGroupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.mainTopGroupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTopGroupTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.mainTopGroupTableLayoutPanel.Controls.Add(this.backButton, 0, 0);
            this.mainTopGroupTableLayoutPanel.Controls.Add(this.forwardButton, 2, 0);
            this.mainTopGroupTableLayoutPanel.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.mainTopGroupTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTopGroupTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTopGroupTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mainTopGroupTableLayoutPanel.Name = "mainTopGroupTableLayoutPanel";
            this.mainTopGroupTableLayoutPanel.RowCount = 1;
            this.mainTopGroupTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTopGroupTableLayoutPanel.Size = new System.Drawing.Size(227, 368);
            this.mainTopGroupTableLayoutPanel.TabIndex = 7;
            // 
            // backButton
            // 
            this.backButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backButton.Location = new System.Drawing.Point(3, 3);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(16, 362);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "<";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backOneGroup);
            // 
            // forwardButton
            // 
            this.forwardButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forwardButton.Location = new System.Drawing.Point(208, 3);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(16, 362);
            this.forwardButton.TabIndex = 3;
            this.forwardButton.Text = ">";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.switchGroupToSelected);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupListBox, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(22, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(183, 368);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.searchGroupTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchGroupsLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(183, 28);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // searchGroupTextBox
            // 
            this.searchGroupTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchGroupTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchGroupTextBox.Location = new System.Drawing.Point(101, 3);
            this.searchGroupTextBox.Name = "searchGroupTextBox";
            this.searchGroupTextBox.Size = new System.Drawing.Size(79, 22);
            this.searchGroupTextBox.TabIndex = 6;
            this.searchGroupTextBox.TextChanged += new System.EventHandler(this.searchGroupTextBox_TextChanged);
            // 
            // searchGroupsLabel
            // 
            this.searchGroupsLabel.AutoSize = true;
            this.searchGroupsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchGroupsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchGroupsLabel.Location = new System.Drawing.Point(0, 0);
            this.searchGroupsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.searchGroupsLabel.Name = "searchGroupsLabel";
            this.searchGroupsLabel.Size = new System.Drawing.Size(98, 28);
            this.searchGroupsLabel.TabIndex = 7;
            this.searchGroupsLabel.Text = "Search Groups";
            this.searchGroupsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupListBox
            // 
            this.groupListBox.ContextMenuStrip = this.groupBoxMenu;
            this.groupListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupListBox.FormattingEnabled = true;
            this.groupListBox.ItemHeight = 16;
            this.groupListBox.Location = new System.Drawing.Point(3, 31);
            this.groupListBox.Name = "groupListBox";
            this.groupListBox.Size = new System.Drawing.Size(177, 324);
            this.groupListBox.TabIndex = 5;
            this.groupListBox.SelectedIndexChanged += new System.EventHandler(this.groupListBox_SelectedIndexChanged);
            this.groupListBox.DoubleClick += new System.EventHandler(this.switchGroupToSelected);
            // 
            // groupBoxMenu
            // 
            this.groupBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewGroupMenuItem,
            this.renameGroupMenuItem,
            this.deleteGroupMenuItem});
            this.groupBoxMenu.Name = "groupBoxMenu";
            this.groupBoxMenu.Size = new System.Drawing.Size(160, 70);
            this.groupBoxMenu.Opening += new System.ComponentModel.CancelEventHandler(this.groupBoxMenu_Opening);
            // 
            // addNewGroupMenuItem
            // 
            this.addNewGroupMenuItem.Name = "addNewGroupMenuItem";
            this.addNewGroupMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addNewGroupMenuItem.Text = "Add New Group";
            this.addNewGroupMenuItem.Visible = false;
            this.addNewGroupMenuItem.Click += new System.EventHandler(this.addNewGroupMenuItem_Click);
            // 
            // renameGroupMenuItem
            // 
            this.renameGroupMenuItem.Enabled = false;
            this.renameGroupMenuItem.Name = "renameGroupMenuItem";
            this.renameGroupMenuItem.Size = new System.Drawing.Size(159, 22);
            this.renameGroupMenuItem.Text = "Rename";
            this.renameGroupMenuItem.Click += new System.EventHandler(this.renameMenuItem_Click);
            // 
            // deleteGroupMenuItem
            // 
            this.deleteGroupMenuItem.Enabled = false;
            this.deleteGroupMenuItem.Name = "deleteGroupMenuItem";
            this.deleteGroupMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteGroupMenuItem.Text = "Delete";
            this.deleteGroupMenuItem.Click += new System.EventHandler(this.deleteGroupMenuItem_Click);
            // 
            // leftTableLayoutPanel
            // 
            this.leftTableLayoutPanel.ColumnCount = 2;
            this.leftTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.Controls.Add(this.aboutButton, 0, 1);
            this.leftTableLayoutPanel.Controls.Add(this.importButton, 1, 0);
            this.leftTableLayoutPanel.Controls.Add(this.newGroupButton, 0, 0);
            this.leftTableLayoutPanel.Controls.Add(this.settingsButton, 1, 1);
            this.leftTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.leftTableLayoutPanel.Location = new System.Drawing.Point(0, 368);
            this.leftTableLayoutPanel.Name = "leftTableLayoutPanel";
            this.leftTableLayoutPanel.RowCount = 2;
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.leftTableLayoutPanel.Size = new System.Drawing.Size(227, 64);
            this.leftTableLayoutPanel.TabIndex = 1;
            // 
            // aboutButton
            // 
            this.aboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutButton.AutoSize = true;
            this.aboutButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.aboutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutButton.Location = new System.Drawing.Point(3, 35);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(107, 26);
            this.aboutButton.TabIndex = 3;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.importButton.AutoSize = true;
            this.importButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.importButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importButton.Location = new System.Drawing.Point(116, 3);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(108, 26);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // newGroupButton
            // 
            this.newGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newGroupButton.AutoSize = true;
            this.newGroupButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newGroupButton.Location = new System.Drawing.Point(3, 3);
            this.newGroupButton.Name = "newGroupButton";
            this.newGroupButton.Size = new System.Drawing.Size(107, 26);
            this.newGroupButton.TabIndex = 0;
            this.newGroupButton.Text = "New Group";
            this.newGroupButton.UseVisualStyleBackColor = true;
            this.newGroupButton.Click += new System.EventHandler(this.newGroupButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.AutoSize = true;
            this.settingsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsButton.Location = new System.Drawing.Point(116, 35);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(108, 26);
            this.settingsButton.TabIndex = 2;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupTitleLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nodeListView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(450, 400);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupTitleLabel
            // 
            this.groupTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.groupTitleLabel.Name = "groupTitleLabel";
            this.groupTitleLabel.Size = new System.Drawing.Size(444, 27);
            this.groupTitleLabel.TabIndex = 2;
            this.groupTitleLabel.Text = "GROUP";
            this.groupTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeListView
            // 
            this.nodeListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.nodeListView.AllowColumnReorder = true;
            this.nodeListView.ContextMenuStrip = this.nodeBoxMenu;
            this.nodeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeListView.FullRowSelect = true;
            this.nodeListView.GridLines = true;
            this.nodeListView.Location = new System.Drawing.Point(3, 30);
            this.nodeListView.MultiSelect = false;
            this.nodeListView.Name = "nodeListView";
            this.nodeListView.Size = new System.Drawing.Size(444, 367);
            this.nodeListView.TabIndex = 1;
            this.nodeListView.UseCompatibleStateImageBehavior = false;
            this.nodeListView.View = System.Windows.Forms.View.Details;
            this.nodeListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.nodeListView_ColumnWidthChanged);
            this.nodeListView.DoubleClick += new System.EventHandler(this.nodeListView_DoubleClick);
            this.nodeListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nodeListView_KeyPress);
            // 
            // nodeBoxMenu
            // 
            this.nodeBoxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playNodeMenuItem,
            this.infoNodeMenuItem,
            this.editNodeMenuItem,
            this.deleteNodeMenuItem});
            this.nodeBoxMenu.Name = "nodeBoxMenu";
            this.nodeBoxMenu.Size = new System.Drawing.Size(108, 92);
            this.nodeBoxMenu.Opening += new System.ComponentModel.CancelEventHandler(this.nodeBoxMenu_Opening);
            // 
            // playNodeMenuItem
            // 
            this.playNodeMenuItem.Enabled = false;
            this.playNodeMenuItem.Name = "playNodeMenuItem";
            this.playNodeMenuItem.Size = new System.Drawing.Size(107, 22);
            this.playNodeMenuItem.Text = "Play";
            this.playNodeMenuItem.Click += new System.EventHandler(this.playNodeMenuItem_Click);
            // 
            // infoNodeMenuItem
            // 
            this.infoNodeMenuItem.Enabled = false;
            this.infoNodeMenuItem.Name = "infoNodeMenuItem";
            this.infoNodeMenuItem.Size = new System.Drawing.Size(107, 22);
            this.infoNodeMenuItem.Text = "Info";
            this.infoNodeMenuItem.Click += new System.EventHandler(this.infoNodeMenuItem_Click);
            // 
            // editNodeMenuItem
            // 
            this.editNodeMenuItem.Enabled = false;
            this.editNodeMenuItem.Name = "editNodeMenuItem";
            this.editNodeMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editNodeMenuItem.Text = "Edit";
            this.editNodeMenuItem.Click += new System.EventHandler(this.editNodeMenuItem_Click);
            // 
            // deleteNodeMenuItem
            // 
            this.deleteNodeMenuItem.Enabled = false;
            this.deleteNodeMenuItem.Name = "deleteNodeMenuItem";
            this.deleteNodeMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteNodeMenuItem.Text = "Delete";
            this.deleteNodeMenuItem.Click += new System.EventHandler(this.deleteNodeMenuItem_Click);
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.ColumnCount = 4;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.newButton, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.nodeSearchAllCheckBox, 2, 0);
            this.mainTableLayoutPanel.Controls.Add(this.nodeSearchTextBox, 3, 0);
            this.mainTableLayoutPanel.Controls.Add(this.nodeSearchLabel, 1, 0);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 400);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 1;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(450, 32);
            this.mainTableLayoutPanel.TabIndex = 0;
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newButton.AutoSize = true;
            this.newButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.newButton.Enabled = false;
            this.newButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newButton.Location = new System.Drawing.Point(3, 3);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(84, 26);
            this.newButton.TabIndex = 3;
            this.newButton.Text = "New Video";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // nodeSearchAllCheckBox
            // 
            this.nodeSearchAllCheckBox.AutoSize = true;
            this.nodeSearchAllCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSearchAllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeSearchAllCheckBox.Location = new System.Drawing.Point(190, 0);
            this.nodeSearchAllCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.nodeSearchAllCheckBox.Name = "nodeSearchAllCheckBox";
            this.nodeSearchAllCheckBox.Size = new System.Drawing.Size(42, 32);
            this.nodeSearchAllCheckBox.TabIndex = 4;
            this.nodeSearchAllCheckBox.Text = "All";
            this.nodeSearchAllCheckBox.UseVisualStyleBackColor = true;
            this.nodeSearchAllCheckBox.CheckedChanged += new System.EventHandler(this.nodeSearchAllCheckBox_CheckedChanged);
            // 
            // nodeSearchTextBox
            // 
            this.nodeSearchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeSearchTextBox.Location = new System.Drawing.Point(235, 3);
            this.nodeSearchTextBox.Name = "nodeSearchTextBox";
            this.nodeSearchTextBox.Size = new System.Drawing.Size(212, 22);
            this.nodeSearchTextBox.TabIndex = 6;
            this.nodeSearchTextBox.TextChanged += new System.EventHandler(this.nodeSearchTextBox_TextChanged);
            // 
            // nodeSearchLabel
            // 
            this.nodeSearchLabel.AutoSize = true;
            this.nodeSearchLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeSearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeSearchLabel.Location = new System.Drawing.Point(93, 0);
            this.nodeSearchLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.nodeSearchLabel.Name = "nodeSearchLabel";
            this.nodeSearchLabel.Size = new System.Drawing.Size(97, 32);
            this.nodeSearchLabel.TabIndex = 5;
            this.nodeSearchLabel.Text = "Search Videos";
            this.nodeSearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(144, 3);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(135, 29);
            this.button.TabIndex = 3;
            this.button.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 432);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "mainForm";
            this.Text = "A Video Manager";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.ResumeLayout(false);
            this.mainTopGroupTableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxMenu.ResumeLayout(false);
            this.leftTableLayoutPanel.ResumeLayout(false);
            this.leftTableLayoutPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.nodeBoxMenu.ResumeLayout(false);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TableLayoutPanel leftTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.ListView nodeListView;
        private System.Windows.Forms.Button newGroupButton;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.ContextMenuStrip groupBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewGroupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameGroupMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGroupMenuItem;
        private System.Windows.Forms.ListBox groupListBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.TextBox searchGroupTextBox;
        private System.Windows.Forms.Label groupTitleLabel;
        private System.Windows.Forms.ContextMenuStrip nodeBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editNodeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playNodeMenuItem;
        private System.Windows.Forms.CheckBox nodeSearchAllCheckBox;
        private System.Windows.Forms.TextBox nodeSearchTextBox;
        private System.Windows.Forms.Label nodeSearchLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTopGroupTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label searchGroupsLabel;
        private System.Windows.Forms.ToolStripMenuItem infoNodeMenuItem;
    }
}

