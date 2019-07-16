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
    public partial class NodeEditor : Form
    {
        //(value == "flv") || (value == "avi") || (value == "mkv") || 
        //(value == "ogm") || (value == "mp4") || (value == "wmv") ||
        //(value == "mpeg") || (value == "mpg"))
        public const string VIDEO_FILTER = "Video files (*.flv;*.avi;*.mkv;*.ogm;*.mp4;*.wmv;*.mpeg;*.mpg)" +
                                           "|*.flv;*.avi;*.mkv;*.ogm;*.mp4;*.wmv;*.mpeg;*.mpg|All files (*.*)|*.*";
        private bool _success = false;
        private AVM.Types.Node _node = new AVM.Types.Node();

        #region Properties
        /// <summary>
        /// Returns true if the information in NodeEditor is supposed to be used
        /// to populate/update the database. If false does nothing with it.
        /// </summary>
        public bool Successful
        {
            get { return _success; }
        }

        /// <summary>
        /// Returns the Node with the edited or new data in it.
        /// </summary>
        public AVM.Types.Node Node
        {
            get { return _node; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Populates the editor with data if a node is provided.
        /// Changes the text on buttons based on if the title is "new" or not.
        /// </summary>
        /// <param name="title">The title of the form. "New" means your adding a new node.</param>
        /// <param name="node">The node to populate the editor weith. Null means new node.</param>
        public NodeEditor(string title,
                          AVM.Types.Node node)
        {
            InitializeComponent();
            this.Text = title;
            if (title != "New")
            {
                fileAddButton.Text = "Save";
                youTubeAddButton.Text = "Save";
                huluAddButton.Text = "Save";
                dvdAddButton.Text = "Save";
            }
            if (node != null)
            {
                _node = node;
                if (_node.IsFile)
                {
                    fileFileTextBox.Text = _node.File.Uri.OriginalString;
                    fileVideoCodecComboBox.Text = _node.File.Video_Encoding;
                    fileAudioCodecComboBox.Text = _node.File.Audio_Encoding;
                    fileContainerComboBox.Text = _node.File.Container;
                }
                if (_node.IsEpisode)
                {
                    // Fill EpisodeName in for each tab
                    fileEpisodeNameTextBox.Text = _node.Episode.EpisodeName;
                    youTubeEpisodeNameTextBox.Text = _node.Episode.EpisodeName;
                    huluEpisodeNameTextBox.Text = _node.Episode.EpisodeName;
                    
                    // Fill LastWatched for each tab
                    fileLastWatchedCheckBox.Checked = _node.Episode.LastWatched;
                    youTubeLastWatchedCheckBox.Checked = _node.Episode.LastWatched;
                    huluLastWatchedCheckBox.Checked = _node.Episode.LastWatched;
                    
                    // Fill EpisodeNumber in for each tab
                    if (_node.Episode.EpisodeNumber > -1)
                    {
                        fileEpisodeNumberTextBox.Text = _node.Episode.EpisodeNumber.ToString();
                        youTubeEpisodeNumberTextBox.Text = _node.Episode.EpisodeNumber.ToString();
                        huluEpisodeNumberTextBox.Text = _node.Episode.EpisodeNumber.ToString();
                    }

                    // Fill SeasonNumber in for each tab
                    if (_node.Episode.SeasonNumber > -1)
                    {
                        fileSeasonNumberTextBox.Text = _node.Episode.SeasonNumber.ToString();
                        youTubeSeasonNumberTextBox.Text = _node.Episode.SeasonNumber.ToString();
                        huluSeasonNumberTextBox.Text = _node.Episode.SeasonNumber.ToString();
                    }
                }
                fileNameTextBox.Text = _node.Name;
                youTubeNameTextBox.Text = _node.Name;
                huluNameTextBox.Text = _node.Name;
                dvdNameTextBox.Text = _node.Name;

                if (_node.Comment != "")
                {
                    dvdCommentsTextBox.Text = _node.Comment;
                    nodeTabControl.SelectedIndex = 3;
                }

                if (_node.IsYouTube)
                {
                    youTubeLinkTextBox.Text = _node.Url;

                    youTubeStatusLabel.ForeColor = Color.Green;
                    youTubeStatusLabel.Text = "GOOD";

                    fileAddButton.Enabled = true;
                    youTubeAddButton.Enabled = true;
                    huluAddButton.Enabled = true;
                    dvdAddButton.Enabled = true;

                    nodeTabControl.SelectedIndex = 1;
                }
                if (_node.IsHulu)
                {
                    huluLinkTextBox.Text = _node.Url;
                    huluStatusLabel.ForeColor = Color.Green;
                    huluStatusLabel.Text = "GOOD";
                    fileAddButton.Enabled = true;
                    youTubeAddButton.Enabled = true;
                    huluAddButton.Enabled = true;
                    dvdAddButton.Enabled = true;

                    nodeTabControl.SelectedIndex = 2;
                }
            }
            #region Populating File Tab
            // Populate Video Codec ComboBox
            fileVideoCodecComboBox.Items.Add("");
            fileVideoCodecComboBox.Items.Add("Xvid");
            fileVideoCodecComboBox.Items.Add("x264");
            fileVideoCodecComboBox.Items.Add("h.286");
            fileVideoCodecComboBox.Items.Add("DivX");
            fileVideoCodecComboBox.Items.Add("mpeg2");
            fileVideoCodecComboBox.Items.Add("VC-1");
            fileVideoCodecComboBox.Items.Add("Theora");
            // Populate Audio Codec ComboxBox
            fileAudioCodecComboBox.Items.Add("");
            fileAudioCodecComboBox.Items.Add("mp3");
            fileAudioCodecComboBox.Items.Add("AAC");
            fileAudioCodecComboBox.Items.Add("Vorbis");
            fileAudioCodecComboBox.Items.Add("VC-1");
            fileAudioCodecComboBox.Items.Add("AC-3");
            // Populate Container ComboBox
            fileContainerComboBox.Items.Add("");
            fileContainerComboBox.Items.Add("flv");
            fileContainerComboBox.Items.Add("avi");
            fileContainerComboBox.Items.Add("mkv");
            fileContainerComboBox.Items.Add("ogm");
            fileContainerComboBox.Items.Add("ogg");
            fileContainerComboBox.Items.Add("mp4");
            fileContainerComboBox.Items.Add("wmv");
            #endregion
        }
        #endregion

        #region File Functions
        /// <summary>
        /// Shows the file selection dialog in order to prompt the user for the file
        /// they want to add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileBrowseButton_Click(object sender,
                                            EventArgs e)
        {
            openFileDialog.Filter = VIDEO_FILTER;
            bool first_time = fileFileTextBox.Text == "" ? true : false;
            if (first_time)
                openFileDialog.ShowDialog();
            else
            {
                openFileDialog.FileName = fileFileTextBox.Text;
                openFileDialog.InitialDirectory = fileFileTextBox.Text.Remove(fileFileTextBox.Text.LastIndexOf('\\'));
                openFileDialog.ShowDialog();
            }
            fileFileTextBox.Text = "";
            fileFileTextBox.Text = openFileDialog.FileName;
            // Need to check that its "" incase nothing is returned
            if (first_time && openFileDialog.FileName != "")
            {
                _node = new AVM.Types.Node(fileFileTextBox.Text);
                if (_node.Name != "")
                    fileNameTextBox.Text = _node.Name;
                if (_node.IsEpisode)
                    if (_node.Episode.EpisodeNumber != -1)
                        fileEpisodeNumberTextBox.Text = _node.Episode.EpisodeNumber.ToString();
                if (_node.File.Video_Encoding != "")
                    fileVideoCodecComboBox.Text = _node.File.Video_Encoding;
                if (_node.File.Container != "")
                    fileContainerComboBox.Text = _node.File.Container;
            }
        }

        /// <summary>
        /// Stores the new Video Encoding in the new node and propagates it to all tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileVideoCodecComboBox_SelectedIndexChanged(object sender,
                                                                 EventArgs e)
        {
            _node.File.Video_Encoding = fileVideoCodecComboBox.Text;
        }

        /// <summary>
        /// Stores the new Audio Encoding in the new node and propagates it to all tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileAudioCodecComboBox_SelectedIndexChanged(object sender,
                                                                 EventArgs e)
        {
            _node.File.Audio_Encoding = fileAudioCodecComboBox.Text;
        }

        /// <summary>
        /// Stores the new Container in the new node and propagates it to all tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileContainerComboBox_SelectedIndexChanged(object sender,
                                                                EventArgs e)
        {
            _node.File.Container = fileContainerComboBox.Text;
        }
        #endregion

        #region YouTube Functions
        /// <summary>
        /// Parses the YouTube link in the YouTube textbox.
        /// If valid displays GOOD else displays BAD.
        /// If valid will enable the Add buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void youTubeLinkButton_Click(object sender,
                                             EventArgs e)
        {
            fileAddButton.Enabled = true;
            youTubeAddButton.Enabled = true;
            huluAddButton.Enabled = true;
            dvdAddButton.Enabled = true;
            if (youTubeLinkTextBox.Text != "")
            {
                AVM.Parsers.YouTubeParser parser;
                parser = new AVM.Parsers.YouTubeParser(youTubeLinkTextBox.Text);
                bool status = parser.parse();
                if (status)
                {
                    youTubeStatusLabel.ForeColor = Color.Green;
                    youTubeStatusLabel.Text = "GOOD";
                    youTubeNameTextBox.Text = parser.Title;
                    fileNameTextBox.Text = parser.Title;
                    _node = new AVM.Types.Node();
                    _node.Name = youTubeNameTextBox.Text;
                    _node.YouTube = parser.Embedded;
                    _node.Url = parser.Url;
                }
                else
                {
                    youTubeStatusLabel.ForeColor = Color.Red;
                    youTubeStatusLabel.Text = "BAD";
                }

            }
        }

        /// <summary>
        /// Run when user changes the YouTube link.
        /// Changes the test label to Test.
        /// Disables the Add buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void youTubeLinkTextBox_TextChanged(object sender,
                                                    EventArgs e)
        {
            youTubeStatusLabel.ForeColor = Color.Red;
            youTubeStatusLabel.Text = "Test";
            fileAddButton.Enabled = false;
            youTubeAddButton.Enabled = false;
            huluAddButton.Enabled = false;
            dvdAddButton.Enabled = false;
        }
        #endregion

        #region Hulu Functions
        /// <summary>
        /// Parses the YouTube link in the Hulu textbox.
        /// If valid displays GOOD else displays BAD.
        /// If valid will enable the Add buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void huluLinkButton_Click(object sender,
                                          EventArgs e)
        {
            fileAddButton.Enabled = true;
            youTubeAddButton.Enabled = true;
            huluAddButton.Enabled = true;
            dvdAddButton.Enabled = true;

            if (huluLinkTextBox.Text != "")
            {
                AVM.Parsers.HuluParser parser;
                parser = new AVM.Parsers.HuluParser(huluLinkTextBox.Text);
                bool status = parser.parse();
                if (status)
                {
                    huluStatusLabel.ForeColor = Color.Green;
                    huluStatusLabel.Text = "GOOD";
                    _node = new AVM.Types.Node();
                    _node.Name = youTubeNameTextBox.Text;
                    _node.Hulu = parser.Embedded;
                    _node.Url = parser.Url;
                    updateNodeName(parser.Title);
                    if (parser.EpisodeTitle != "")
                        updateEpisodeName(parser.EpisodeTitle);
                    updateEpisodeNumber(parser.EpisodeNumber);
                    updateSeasonNumber(parser.SeasonNumber);
                }
                else
                {
                    huluStatusLabel.ForeColor = Color.Red;
                    huluStatusLabel.Text = "BAD";
                }
            }
        }
        
        /// <summary>
        /// Runs when user changes the Hulu link.
        /// Changes the test label to Test.
        /// Disables the Add buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void huluLinkTextBox_TextChanged(object sender,
                                                 EventArgs e)
        {
            huluStatusLabel.ForeColor = Color.Red;
            huluStatusLabel.Text = "Test";
            fileAddButton.Enabled = false;
            youTubeAddButton.Enabled = false;
            huluAddButton.Enabled = false;
            dvdAddButton.Enabled = false;
        }
        #endregion

        #region Update Funcitons
        /// <summary>
        /// Updates the NodeName and every textbox that is related to it.
        /// </summary>
        /// <param name="name">The new name taht is going to be updated.</param>
        private void updateNodeName(string name)
        {
            fileNameTextBox.Text = name;
            youTubeNameTextBox.Text = name;
            huluNameTextBox.Text = name;
            dvdNameTextBox.Text = name;
            _node.Name = name;
        }

        /// <summary>
        /// Updates the EpisodeName and every textbox that is related to it.
        /// </summary>
        /// <param name="name">The new Episode Name that is going to be updated.</param>
        private void updateEpisodeName(string name)
        {
            fileEpisodeNameTextBox.Text = name;
            youTubeEpisodeNameTextBox.Text = name;
            huluEpisodeNameTextBox.Text = name;
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.EpisodeName = name;
        }

        /// <summary>
        /// Updates the LastWatched status and every checkbox related to it.
        /// </summary>
        /// <param name="value">Boolean value representing if it was the last watched episode.</param>
        private void updateLastWatched(bool value)
        {
            fileLastWatchedCheckBox.Checked = value;
            youTubeLastWatchedCheckBox.Checked = value;
            huluLastWatchedCheckBox.Checked = value;
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.LastWatched = value;
        }

        /// <summary>
        /// Updates the EpisodeNumber and every textbox related to it.
        /// </summary>
        /// <param name="value">The new Episode Number that is going to be updated.</param>
        private void updateEpisodeNumber(int value)
        {
            if (value != -1)
            {
                fileEpisodeNumberTextBox.Text = value.ToString();
                youTubeEpisodeNumberTextBox.Text = value.ToString();
                huluEpisodeNumberTextBox.Text = value.ToString();
            }
            else
            {
                fileSeasonNumberTextBox.Text = "";
                youTubeSeasonNumberTextBox.Text = "";
                huluSeasonNumberTextBox.Text = "";
            }
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.EpisodeNumber = value;
        }

        /// <summary>
        /// Updates the SeasonNumber and every textbox related to it.
        /// </summary>
        /// <param name="value">The new Season Number that is going to be updated.</param>
        private void updateSeasonNumber(int value)
        {
            if (value != -1)
            {
                fileSeasonNumberTextBox.Text = value.ToString();
                youTubeSeasonNumberTextBox.Text = value.ToString();
                huluSeasonNumberTextBox.Text = value.ToString();
            }
            else
            {
                fileSeasonNumberTextBox.Text = "";
                youTubeSeasonNumberTextBox.Text = "";
                huluSeasonNumberTextBox.Text = "";
            }
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.SeasonNumber = value;
        }
        #endregion
        
        #region General Field Functions
        /// <summary>
        /// Updates either the season or the episode depending on which textbox was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void episodeOrSeasonNumber_TextChanged(object sender,
                                                       EventArgs e)
        {
            TextBox source = (TextBox)sender;

            int number;
            if (source.Text == "")
            {
                switch ((string)source.Tag)
                {
                    case "episode":
                        updateEpisodeNumber(-1);
                        break;
                    case "season":
                        updateSeasonNumber(-1);
                        break;
                    default:
                        MessageBox.Show("Error: Cannot figuring out if episode or season changed?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
                if (Int32.TryParse(source.Text, out number))
                {
                    // check if the episode data exists yet
                    if (!_node.IsEpisode)
                        _node.Episode = new AVM.Types.EpisodeInfo();

                    switch ((string)source.Tag)
                    {
                        case "episode":
                            updateEpisodeNumber(number);
                            break;
                        case "season":
                            updateSeasonNumber(number);
                            break;
                        default:
                            MessageBox.Show("Error: Cannot figuring out if episode or season changed?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                    MessageBox.Show("Warning: Please enter an integer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
        /// <summary>
        /// Runs updateNodeName when a textbox related to the name is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void name_TextChanged(object sender,
                                      EventArgs e)
        {
            updateNodeName(((TextBox)sender).Text);
        }

        /// <summary>
        /// Runs updateEpisodeName when a textbox related to the name is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void episodeName_TextChanged(object sender,
                                             EventArgs e)
        {
            updateEpisodeName(((TextBox)sender).Text);
        }

        /// <summary>
        /// Updates the comment in the node when the comment textbox was changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dvdCommentsTextBox_TextChanged(object sender,
                                                    EventArgs e)
        {
            _node.Comment = dvdCommentsTextBox.Text;
        }

        /// <summary>
        /// Runs the updateLastWatched when a checkbox related to it is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastWatched_CheckedChanged(object sender,
                                                EventArgs e)
        {
            updateLastWatched(((CheckBox)sender).Checked);
        }
        #endregion

        #region General Button Functions
        /// <summary>
        /// Plays the web video to test the link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender,
                                      EventArgs e)
        {
            _node.Play();
        }

        /// <summary>
        /// Marks as success and closes form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender,
                                     EventArgs e)
        {
            // finalize the successful changes
            _success = true;
            this.Close();
        }

        /// <summary>
        /// Marks success as false and closes form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender,
                                        EventArgs e)
        {
            _success = false;
            this.Close();
        }
        #endregion
    }
}
