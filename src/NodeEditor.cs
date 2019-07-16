//  Video Manager - Appliction used to manage Video Files and DVD's
//
//  Copyright (c) 2008 Nicholas Omann
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

        public NodeEditor(string title, AVM.Types.Node node)
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

        #region Properties
        public bool Successful
        {
            get { return _success; }
        }

        public AVM.Types.Node Node
        {
            get { return _node; }
        }
        #endregion

        #region File Functions
        private void fileBrowseButton_Click(object sender, EventArgs e)
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

        private void fileVideoCodecComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _node.File.Video_Encoding = fileVideoCodecComboBox.Text;
        }

        private void fileAudioCodecComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _node.File.Audio_Encoding = fileAudioCodecComboBox.Text;
        }

        private void fileContainerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _node.File.Container = fileContainerComboBox.Text;
        }
        #endregion

        #region YouTube Functions
        private void youTubeLinkButton_Click(object sender, EventArgs e)
        {
            fileAddButton.Enabled = true;
            youTubeAddButton.Enabled = true;
            huluAddButton.Enabled = true;
            dvdAddButton.Enabled = true;
            // TODO: Check what data is saved when this is run but never tested good
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
                    _node.YouTube = parser.Embeded;
                    _node.Url = parser.Url;
                }
                else
                {
                    youTubeStatusLabel.ForeColor = Color.Red;
                    youTubeStatusLabel.Text = "BAD";
                }

            }
        }

        private void youTubeLinkTextBox_TextChanged(object sender, EventArgs e)
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
        private void huluLinkButton_Click(object sender, EventArgs e)
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
                    _node.Hulu = parser.Embeded;
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
        
        private void huluLinkTextBox_TextChanged(object sender, EventArgs e)
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
        /// <param name="name"></param>
        private void updateNodeName(string name)
        {
            fileNameTextBox.Text = name;
            youTubeNameTextBox.Text = name;
            huluNameTextBox.Text = name;
            dvdNameTextBox.Text = name;
            _node.Name = name;
        }

        private void updateEpisodeName(string name)
        {
            fileEpisodeNameTextBox.Text = name;
            youTubeEpisodeNameTextBox.Text = name;
            huluEpisodeNameTextBox.Text = name;
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.EpisodeName = name;
        }

        private void updateLastWatched(bool value)
        {
            fileLastWatchedCheckBox.Checked = value;
            youTubeLastWatchedCheckBox.Checked = value;
            huluLastWatchedCheckBox.Checked = value;
            if (!_node.IsEpisode)
                _node.Episode = new AVM.Types.EpisodeInfo();
            _node.Episode.LastWatched = value;
        }

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
        
        #region General TextChanged Functions
        private void episodeOrSeasonNumber_TextChanged(object sender, EventArgs e)
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
        
        private void name_TextChanged(object sender, EventArgs e)
        {
            updateNodeName(((TextBox)sender).Text);
        }

        private void episodeName_TextChanged(object sender, EventArgs e)
        {
            updateEpisodeName(((TextBox)sender).Text);
        }
        #endregion

        #region General Button_Click Functions
        private void playButton_Click(object sender, EventArgs e)
        {
            _node.Play();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Make sure all changes are in _node


            // finalize the successful changes
            _success = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _success = false;
            this.Close();
        }
        #endregion

        private void lastWatched_CheckedChanged(object sender, EventArgs e)
        {
            updateLastWatched(((CheckBox)sender).Checked);
        }

        private void dvdCommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            _node.Comment = dvdCommentsTextBox.Text;
        }

        
    }
}
