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
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVM
{
    public partial class MainForm : Form
    {
        public Database db;
        private List<AVM.Types.Group> groups = new List<AVM.Types.Group>();
        private List<AVM.Types.Node> nodes = new List<AVM.Types.Node>();

        #region Constructors
        /// <summary>
        /// Basic class constructor. Literally nothing beyond InitializeComponets takes place here.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Group Button Panel
        /// <summary>
        /// Prompts the user for a group name and if it gets a name it adds it to
        /// database and then refreshes the groupListBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newGroupButton_Click(object sender,
                                          EventArgs e)
        {
            GroupEditor editor = new GroupEditor("", "New Group", "Add");
            editor.ShowDialog();
            if (editor.NewName != "")
            {
                db.addGroup(editor.NewName);
                refreshGroups();
                enableForwardBackButtons();
            }
        }

        /// <summary>
        /// Displays the Settings dialog for the user. If the columns are changed then
        /// it reformats the columns and reloads the nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsButton_Click(object sender,
                                          EventArgs e)
        {
            AVM.Settings settingsWindow = new AVM.Settings(db);
            settingsWindow.ShowDialog();
            if (settingsWindow.Success)
            {
                formatColumns();
                loadNodes();
            }
        }
        
        /// <summary>
        /// Displays the About dialog for the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutButton_Click(object sender,
                                       EventArgs e)
        {
            AboutBox win = new AboutBox();
            win.ShowDialog();
        }

        /// <summary>
        /// Displays the Importer dialog for the user which can restore backed up 
        /// databases and can also cause both group and node refreshes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importButton_Click(object sender, EventArgs e)
        {
            Importer importDialog = new Importer(this, db, groupListBox.SelectedIndex);
            importDialog.ShowDialog();

            if (importDialog.RestoreTookPlace)
            {
                // Refresh nodes and move to root level.
                db.CurrentGroup = 0;
                groupTitleLabel.Text = "";
                refreshGroups();
                db.refreshNodes(ref nodes);
                loadNodes();
            }
        }
        #endregion

        #region Node Panel
        /// <summary>
        /// Creates a new node by running the NodeEditor in "New" mode.
        /// Also refreshes nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender,
                                     EventArgs e)
        {
            NodeEditor editor = new NodeEditor("New", null);
            editor.ShowDialog();
            if (editor.Successful)
            {
                db.addNode(editor.Node);
                db.refreshNodes(ref nodes);
                loadNodes();
            }
        }

        /// <summary>
        /// If there is a current search on the nodes then it will redo the
        /// search based off of the new status of if it should be searching
        /// all nodes or just nodes in the current group.
        /// May refresh nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeSearchAllCheckBox_CheckedChanged(object sender,
                                                          EventArgs e)
        {
            if (nodeSearchTextBox.Text != "")
            {
                db.searchNodes(ref nodes,
                               nodeSearchTextBox.Text,
                               nodeSearchAllCheckBox.Checked);
                groupTitleLabel.Text = "Search Results";
                loadNodes();
            }
        }

        /// <summary>
        /// If search is entered make sure to change current nodes listed to
        /// the search results. Different searches take place based off of
        /// the checkbox for searching all nodes.
        /// Refreshes nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeSearchTextBox_TextChanged(object sender,
                                                   EventArgs e)
        {
            if (nodeSearchTextBox.Text != "")
            {
                db.searchNodes(ref nodes,
                               nodeSearchTextBox.Text,
                               nodeSearchAllCheckBox.Checked);
                groupTitleLabel.Text = "Search Results";
            }
            else
            {
                db.refreshNodes(ref nodes);

                if (db.CurrentGroup != 0)
                    groupTitleLabel.Text = db.findBreadcrumbs(db.CurrentGroup); //((AVM.Types.Group)groupListBox.SelectedItem).Name;
                else
                    groupTitleLabel.Text = "";
            }

            loadNodes();
        }
        #endregion

        #region Group ContextMenu Methods
        /// <summary>
        /// Enables and disables the options in the group right-click menu based
        /// on whether or not a group is currently selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBoxMenu_Opening(object sender,
                                          CancelEventArgs e)
        {
            if (groupListBox.SelectedIndex >= 0)
            {
                renameGroupMenuItem.Enabled = true;
                deleteGroupMenuItem.Enabled = true;
            }
            else
            {
                renameGroupMenuItem.Enabled = false;
                deleteGroupMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Creates a new group by running the GroupEditor.
        /// Refreshes groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNewGroupMenuItem_Click(object sender,
                                               EventArgs e)
        {
            GroupEditor editor = new GroupEditor("", "New Group", "Add");
            editor.ShowDialog();
            if (editor.NewName != "")
            {
                db.addGroup(editor.NewName);
                refreshGroups();
                enableForwardBackButtons();
            }
        }

        /// <summary>
        /// Renames currently selected group using the GroupEditor.
        /// Refreshes groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameMenuItem_Click(object sender,
                                          EventArgs e)
        {
            if (groupListBox.SelectedIndex >= 0)
            {
                GroupEditor messageBox = new GroupEditor(groupListBox.SelectedItem.ToString(), "Rename Group", "Rename");
                messageBox.ShowDialog();
                string tempName = messageBox.NewName;
                if (tempName != "")
                {
                    db.renameGroup((AVM.Types.Group)groupListBox.SelectedItem, tempName);
                    refreshGroups();
                }
            }
        }

        /// <summary>
        /// Deletes currently selected group. If PromptOnDelete is true
        /// it will prompt the users using DeleteConfirmation.
        /// Refreshes groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteGroupMenuItem_Click(object sender, EventArgs e)
        {
            if (groupListBox.SelectedIndex >= 0)
            {
                bool delete = false; // This decides if the group should actually be deleted.

                if (Properties.Settings.Default.PromptOnDelete)
                {
                    DeleteConfirmation prompt = new DeleteConfirmation();
                    prompt.ShowDialog();
                    delete = prompt.Delete;
                }
                else
                    delete = true; // If not prompted just delete it.

                if (delete)
                {
                    db.removeGroup((AVM.Types.Group)groupListBox.SelectedItem);
                    refreshGroups();
                    db.CurrentGroup = db.ParentGroup;
                    if (groupListBox.Items.Count == 0)
                        forwardButton.Enabled = false;
                    db.refreshNodes(ref nodes);
                    loadNodes();
                    // See if a group is selected
                    if (db.CurrentGroup != 0)
                        groupTitleLabel.Text = db.findBreadcrumbs(db.CurrentGroup); //((AVM.Types.Group)groupListBox.SelectedItem).Name;
                    else
                        groupTitleLabel.Text = "";
                }
            }
        }
        #endregion

        #region Node ContextMenu Methods
        /// <summary>
        /// Enables and disabled the options in the node right-click menu based
        /// on whether or not a node is currently selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeBoxMenu_Opening(object sender,
                                         CancelEventArgs e)
        {
            if (nodeListView.SelectedItems.Count > 0)
            {
                playNodeMenuItem.Enabled = true;
                infoNodeMenuItem.Enabled = true;
                editNodeMenuItem.Enabled = true;
                deleteNodeMenuItem.Enabled = true;
            }
            else
            {
                playNodeMenuItem.Enabled = true;
                infoNodeMenuItem.Enabled = true;
                editNodeMenuItem.Enabled = true;
                deleteNodeMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Runs playSelectedNode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playNodeMenuItem_Click(object sender,
                                            EventArgs e)
        {
            playSelectedNode();
        }

        /// <summary>
        /// Opens the NodeViewer for the currently selected node. Runs play
        /// if the viewer wants the currently selected node to be played.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoNodeMenuItem_Click(object sender,
                                            EventArgs e)
        {
            if (nodeListView.SelectedIndices.Count > 0)
            {
                AVM.NodeViewer viewer = new AVM.NodeViewer(
                        nodes[nodeListView.SelectedIndices[0]]);
                viewer.ShowDialog();
                if (viewer.Play)
                    playNodeMenuItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Edits the currently selected node using NodeEditor.
        /// Refreshes nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editNodeMenuItem_Click(object sender,
                                            EventArgs e)
        {
            if (nodeListView.SelectedIndices.Count > 0)
            {
                NodeEditor editor = new NodeEditor("Edit " + nodes[nodeListView.SelectedIndices[0]].Name,
                                                   nodes[nodeListView.SelectedIndices[0]]);
                editor.ShowDialog();
                if (editor.Successful)
                {
                    db.updateNode(nodes[nodeListView.SelectedIndices[0]], editor.Node);
                    db.refreshNodes(ref nodes);
                    loadNodes();
                }
            }
        }
        
        /// <summary>
        /// Deletes the currently selected node. If PromptOnDelete is selected
        /// then DeleteConfirmation will ask for confirmation.
        /// Refreshes nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteNodeMenuItem_Click(object sender, EventArgs e)
        {
            if (nodeListView.SelectedIndices.Count > 0)
            {
                bool delete = false; // this is set to true if delete should take place

                // If PromptOnDelete then run the confirmation dialog
                if (Properties.Settings.Default.PromptOnDelete)
                {
                    DeleteConfirmation prompt = new DeleteConfirmation();
                    prompt.ShowDialog();
                    delete = prompt.Delete;
                }
                // else just set to delete
                else
                    delete = true;

                if (delete)
                {
                    ListView.SelectedIndexCollection indexes = nodeListView.SelectedIndices;
                    foreach (int index in indexes)
                        db.removeNode(nodes[index]);
                    db.refreshNodes(ref nodes);
                    loadNodes();
                }
            }
        }
        #endregion

        #region Group Methods
        /// <summary>
        /// Switches the current group to the last currently selected one.
        /// Traverses into the group that is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchGroupToSelected(object sender,
                                           EventArgs e)
        {
            if (groupListBox.SelectedIndex >= 0)
            {
                db.ParentGroup = ((AVM.Types.Group)groupListBox.SelectedItem).Id;
                refreshGroups();
                enableForwardBackButtons();
            }
        }

        /// <summary>
        /// Switches the current group to its parent.
        /// Traverses backward into the parent group.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backOneGroup(object sender,
                                  EventArgs e)
        {
            if (db.CurrentGroup != 0)
            {
                db.CurrentGroup = db.ParentGroup;
                refreshGroups();

                //Reload nodes to the new group.
                db.refreshNodes(ref nodes);
                loadNodes();
                //Select the currently opened group.
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].Id == db.CurrentGroup)
                        groupListBox.SelectedIndex = i;
                }

                enableForwardBackButtons();
            }

            if (db.ParentGroup == 0)
                newButton.Enabled = false;
        }

        /// <summary>
        /// Makes sure that the "<" and ">" buttons are correctly enabled.
        /// </summary>
        private void enableForwardBackButtons()
        {
            if (db.ParentGroup == 0)
                backButton.Enabled = false;
            else
                backButton.Enabled = true;
            if (groupListBox.Items.Count == 0)
                forwardButton.Enabled = false;
            else
                forwardButton.Enabled = true;
        }

        /// <summary>
        /// If text changes then it does a search based on what is in the textbox and
        /// places the results in the group list.
        /// Refreshes groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchGroupTextBox_TextChanged(object sender,
                                                    EventArgs e)
        {
            if (searchGroupTextBox.Text != "")
            {
                db.searchGroups(groupListBox, searchGroupTextBox.Text);
                backButton.Enabled = false;
                forwardButton.Enabled = false;
            }
            else
            {
                refreshGroups();
                enableForwardBackButtons();
            }
        }

        /// <summary>
        /// When a different group is selected refresh the node list based on what is
        /// in the newly selected group.
        /// Refreshes nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupListBox.SelectedItem != null)
            {
                newButton.Enabled = true;
                db.CurrentGroup = ((AVM.Types.Group)groupListBox.SelectedItem).Id;
                groupTitleLabel.Text = db.findBreadcrumbs(db.CurrentGroup);
            }
            else
            {
                db.CurrentGroup = db.ParentGroup;
                groupTitleLabel.Text = db.findBreadcrumbs(db.CurrentGroup);
            }
            db.refreshNodes(ref nodes);
            loadNodes();
        }

        /// <summary>
        /// Refreshes the groups from the database.
        /// </summary>
        public void refreshGroups()
        {
            db.refreshGroups(groupListBox, ref groups);
        }
        #endregion

        #region Node Methods
        /// <summary>
        /// Populates the nodeListView based on what is supposed to be shown.
        /// </summary>
        private void loadNodes()
        {
            nodeListView.Items.Clear();
            foreach (AVM.Types.Node node in nodes)
            {
                ListViewItem tempItem = new ListViewItem(node.Name);

                if (Properties.Settings.Default.EpisodeNameColumnEnabled)
                {
                    if (node.IsEpisode)
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, node.Episode.EpisodeName));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.SeasonNumberColumnEnabled)
                {
                    if (node.IsEpisode)
                        if (node.Episode.SeasonNumber != -1)
                            tempItem.SubItems.Add(
                                new ListViewItem.ListViewSubItem(tempItem, node.Episode.SeasonNumber.ToString()));
                        else
                            tempItem.SubItems.Add(
                                new ListViewItem.ListViewSubItem(tempItem, ""));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.EpisodeNumberColumnEnabled)
                {
                    if (node.IsEpisode)
                        if (node.Episode.EpisodeNumber != -1)
                            tempItem.SubItems.Add(
                                new ListViewItem.ListViewSubItem(tempItem, node.Episode.EpisodeNumber.ToString()));
                        else
                            tempItem.SubItems.Add(
                                new ListViewItem.ListViewSubItem(tempItem, ""));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.VideoCodecColumnEnabled)
                {
                    if (node.IsFile)
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, node.File.Video_Encoding));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.AudioCodecColumnEnabled)
                {
                    if (node.IsFile)
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, node.File.Audio_Encoding));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.ContainerColumnEnabled)
                {
                    if (node.IsFile)
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, node.File.Container));
                    else
                        tempItem.SubItems.Add(
                            new ListViewItem.ListViewSubItem(tempItem, ""));
                }

                if (Properties.Settings.Default.TimesPlayedColumnEnabled)
                {
                    tempItem.SubItems.Add(new ListViewItem.ListViewSubItem(tempItem, node.TimesPlayed.ToString()));
                }

                // Change color if it was the last video watched
                if ((node.IsEpisode) && (node.Episode.LastWatched))
                    tempItem.BackColor = Properties.Settings.Default.LastWatchedColor;
                nodeListView.Items.Add(tempItem);
            }
        }

        /// <summary>
        /// Saves a changed ColumnWidth to the settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeListView_ColumnWidthChanged(object sender,
                                                     ColumnWidthChangedEventArgs e)
        {
            if (nodeListView.Columns.Count == Properties.Settings.Default.NumberOfColumns)
            {
                int index = 0;

                if (Properties.Settings.Default.NameColumnEnabled)
                {
                    Properties.Settings.Default.NameColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.EpisodeNameColumnEnabled)
                {
                    Properties.Settings.Default.EpisodeNameColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.SeasonNumberColumnEnabled)
                {
                    Properties.Settings.Default.SeasonNumberColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.EpisodeNumberColumnEnabled)
                {
                    Properties.Settings.Default.EpisodeNumberColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.VideoCodecColumnEnabled)
                {
                    Properties.Settings.Default.VideoCodecColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.AudioCodecColumnEnabled)
                {
                    Properties.Settings.Default.AudioCodecColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.ContainerColumnEnabled)
                {
                    Properties.Settings.Default.ContainerColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }

                if (Properties.Settings.Default.TimesPlayedColumnEnabled)
                {
                    Properties.Settings.Default.TimesPlayedColumnWidth = nodeListView.Columns[index].Width;
                    index++;
                }
            }
        }

        /// <summary>
        /// If DoublClickPlay is enabled then this will just play a video.
        /// If not then it will just display a NodeViewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeListView_DoubleClick(object sender,
                                              EventArgs e)
        {
            if (Properties.Settings.Default.DoubleClickPlay)
                playSelectedNode();
            else
                if (nodeListView.SelectedIndices.Count > 0)
                {
                    AVM.NodeViewer viewer = new AVM.NodeViewer(
                        nodes[nodeListView.SelectedIndices[0]]);
                    viewer.ShowDialog();
                    if (viewer.Play)
                        playSelectedNode();
                }
        }

        /// <summary>
        /// Plays the selected node.
        /// If it is an episode it will set it as lastWatched.
        /// Refreshes nodes. (because of possible lastWatched change)
        /// </summary>
        public void playSelectedNode()
        {
            if (nodeListView.SelectedIndices.Count > 0)
            {
                // Increment the watched list by one
                nodes[nodeListView.SelectedIndices[0]].TimesPlayed++;
                db.incrementPlayed(nodes[nodeListView.SelectedIndices[0]]);
                // Mark as lastPlayed
                if (nodes[nodeListView.SelectedIndices[0]].IsEpisode)
                {
                    db.lastWatched(nodes[nodeListView.SelectedIndices[0]]);
                    nodes[nodeListView.SelectedIndices[0]].Episode.LastWatched = true;
                }

                // Check if what is selected can be played just incase something got through
                if (nodes[nodeListView.SelectedIndices[0]].IsYouTube ||
                    nodes[nodeListView.SelectedIndices[0]].IsHulu ||
                    nodes[nodeListView.SelectedIndices[0]].IsFile)
                {
                    // If YouTube video
                    if (nodes[nodeListView.SelectedIndices[0]].IsYouTube)
                    {
                        // If set to use the WebPlayer use it else use default web browser
                        if ((Properties.Settings.Default.YouTubeWebPlayer == true) &&
                            (nodes[nodeListView.SelectedIndices[0]].embedded != null))
                        {
                            this.WindowState = FormWindowState.Minimized;
                            nodes[nodeListView.SelectedIndices[0]].Play();
                        }
                        else
                        {
                            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(nodes[nodeListView.SelectedIndices[0]].Url);
                            System.Diagnostics.Process.Start(psi);
                        }
                    }

                    // If Hulu video
                    if (nodes[nodeListView.SelectedIndices[0]].IsHulu)
                    {
                        // If set to use the WebPlayer use it else use default web browser
                        if ((Properties.Settings.Default.HuluWebPlayer == true) &&
                            (nodes[nodeListView.SelectedIndices[0]].embedded != null))
                        {
                            this.WindowState = FormWindowState.Minimized;
                            nodes[nodeListView.SelectedIndices[0]].Play();
                        }
                        else
                        {
                            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(nodes[nodeListView.SelectedIndices[0]].Url);
                            System.Diagnostics.Process.Start(psi);
                        }
                    }

                    // Plays files using whatever program has been set as a default for that file format.
                    // This should be the most user friendly way but maybe could be setup to always run
                    //  a specific application to play it. The benefits from this I dont know.
                    if (nodes[nodeListView.SelectedIndices[0]].IsFile)
                    {
                        this.WindowState = FormWindowState.Minimized;
                        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(nodes[nodeListView.SelectedIndices[0]].File.Uri.LocalPath);
                        System.Diagnostics.Process tempPlayer;
                        tempPlayer = System.Diagnostics.Process.Start(psi);
                        // Look into this a bit more as it makes teh minimize look odd
                        // WMP doesn't do this right
                        if (tempPlayer != null)
                            tempPlayer.WaitForExit();
                    }
                    this.WindowState = FormWindowState.Normal;
                    db.refreshNodes(ref nodes);
                    loadNodes();
                }

                // If there is a search dont do a normal refresh.
                if (nodeSearchTextBox.Text != "")
                {
                    db.refreshNodes(ref nodes);
                    loadNodes();
                }
                else
                    nodeSearchAllCheckBox_CheckedChanged(null, null);
            }
        }

        /// <summary>
        /// Checks for the user pressing the backspace key when a node is selected.
        /// If PromptOnDelete is enabled will prompt user with DeleteConfirmation otherwise
        /// will just delete the video when backspace is pressed and BackspaceDelete is
        /// enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nodeListView_KeyPress(object sender,
                                           KeyPressEventArgs e)
        {
            if (Properties.Settings.Default.BackspaceDelete)
                if ((e.KeyChar == '\b') && (nodeListView.SelectedItems.Count > 0))
                {
                    bool delete = false; // this is set to true if delete should take place

                    // If PromptOnDelete then run the confirmation dialog
                    if (Properties.Settings.Default.PromptOnDelete)
                    {
                        DeleteConfirmation prompt = new DeleteConfirmation();
                        prompt.ShowDialog();
                        delete = prompt.Delete;
                    }
                    // else just set to delete
                    else
                        delete = true;

                    if (delete)
                    {
                        ListView.SelectedIndexCollection indexes = nodeListView.SelectedIndices;
                        foreach (int index in indexes)
                            db.removeNode(nodes[index]);
                        db.refreshNodes(ref nodes);
                        loadNodes();
                    }
                }
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// Creates the MainForm and also loads or creates the database.
        /// Refreshes both nodes and groups to their last selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender,
                                   EventArgs e)
        {
            if (Properties.Settings.Default.LastListView == null)
                formatColumns();
            else
                nodeListView = Properties.Settings.Default.LastListView;

            //db = new AVM.Database(System.IO.Directory.GetCurrentDirectory().ToString() + "\\VideoManager.db3");
            // Create the folder if it doesn't exist since Installer wont.
            if (!System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AVM"))
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AVM");
            db = new AVM.Database(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AVM\\VideoManager.db3");

            db.ParentGroup = Properties.Settings.Default.LastParentGroup;
            refreshGroups();

            if (groupListBox.Items.Count > Properties.Settings.Default.LastSelectedIndex)
                groupListBox.SelectedIndex = Properties.Settings.Default.LastSelectedIndex;
            else
                groupListBox.SelectedIndex = -1;

            // See if a group is selected
            if (db.CurrentGroup != 0)
                groupTitleLabel.Text = db.findBreadcrumbs(db.CurrentGroup); //((AVM.Types.Group)groupListBox.SelectedItem).Name;
            else
                groupTitleLabel.Text = "";

            // Enable and disable buttons as needed
            enableForwardBackButtons();
        }

        /// <summary>
        /// Saves current settings and positions for group and nodes. Also kills
        /// the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender,
                                          FormClosingEventArgs e)
        {
            Properties.Settings.Default.LastListView = nodeListView;
            Properties.Settings.Default.LastParentGroup = db.ParentGroup;
            Properties.Settings.Default.LastSelectedIndex = groupListBox.SelectedIndex;
            Properties.Settings.Default.Save();
            db.Kill();
        }

        /// <summary>
        /// Sets up all of the columnWidths and columnNames.
        /// </summary>
        private void formatColumns()
        {
            nodeListView.Columns.Clear();

            if (Properties.Settings.Default.NameColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.NameColumnLabel,
                                         Properties.Settings.Default.NameColumnWidth);

            if (Properties.Settings.Default.EpisodeNameColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.EpisodeNameColumnLabel,
                                         Properties.Settings.Default.EpisodeNameColumnWidth);

            if (Properties.Settings.Default.SeasonNumberColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.SeasonNumberColumnLabel,
                                         Properties.Settings.Default.SeasonNumberColumnWidth);

            if (Properties.Settings.Default.EpisodeNumberColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.EpisodeNumberColumnLabel,
                                         Properties.Settings.Default.EpisodeNumberColumnWidth);

            if (Properties.Settings.Default.VideoCodecColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.VideoCodecColumnLabel,
                                         Properties.Settings.Default.VideoCodecColumnWidth);

            if (Properties.Settings.Default.AudioCodecColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.AudioCodecColumnLabel,
                                         Properties.Settings.Default.AudioCodecColumnWidth);

            if (Properties.Settings.Default.ContainerColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.ContainerColumnLabel,
                                         Properties.Settings.Default.ContainerColumnWidth);

            if (Properties.Settings.Default.TimesPlayedColumnEnabled)
                nodeListView.Columns.Add(Properties.Settings.Default.TimesPlayedColumnLabel,
                                         Properties.Settings.Default.TimesPlayedColumnWidth);
        }
        #endregion
    }
}
