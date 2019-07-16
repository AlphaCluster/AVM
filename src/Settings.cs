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
    public partial class Settings : Form
    {
        private Database _db;
        private bool _success = false;

        public Settings(Database db)
        {
            InitializeComponent();
            _db = db;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            // Save the Column Properties
            Properties.Settings.Default.NameColumnEnabled =
                    nameColumnCheckBox.Checked;
            Properties.Settings.Default.NameColumnLabel =
                    nameColumnLabelTextBox.Text;

            Properties.Settings.Default.EpisodeNameColumnEnabled =
                    episodeNameColumnCheckBox.Checked;
            Properties.Settings.Default.EpisodeNameColumnLabel =
                    episodeNameColumnLabelTextBox.Text;

            Properties.Settings.Default.SeasonNumberColumnEnabled =
                    seasonNumberColumnCheckBox.Checked;
            Properties.Settings.Default.SeasonNumberColumnLabel =
                    seasonNumberColumnLabelTextBox.Text;

            Properties.Settings.Default.EpisodeNumberColumnEnabled =
                    episodeNumberColumnCheckBox.Checked;
            Properties.Settings.Default.EpisodeNumberColumnLabel =
                    episodeNumberColumnLabelTextBox.Text;

            Properties.Settings.Default.VideoCodecColumnEnabled =
                    videoCodecColumnCheckBox.Checked;
            Properties.Settings.Default.VideoCodecColumnLabel =
                    videoCodecColumnLabelTextBox.Text;

            Properties.Settings.Default.AudioCodecColumnEnabled =
                    audioCodecColumnCheckBox.Checked;
            Properties.Settings.Default.AudioCodecColumnLabel =
                    audioCodecColumnLabelTextBox.Text;

            Properties.Settings.Default.ContainerColumnEnabled =
                    containerColumnCheckBox.Checked;
            Properties.Settings.Default.ContainerColumnLabel =
                    containerColumnLabelTextBox.Text;

            Properties.Settings.Default.TimesPlayedColumnEnabled =
                    timesPlayedColumnCheckBox.Checked;
            Properties.Settings.Default.TimesPlayedColumnLabel =
                    timesPlayedColumnLabelTextBox.Text;

            // Save the Web Properties
            Properties.Settings.Default.YouTubeWebPlayer = youTubeWebPlayerCheckBox.Checked;
            Properties.Settings.Default.HuluWebPlayer = huluWebPlayerCheckBox.Checked;

            Properties.Settings.Default.FileNamePattern = patternTextBox.Text;
            Properties.Settings.Default.MoreInfoService = moreInfoComboBox.Text;
            _success = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backupButton_Click(object sender, EventArgs e)
        {
            backupSaveFileDialog.ShowDialog();

            if (backupSaveFileDialog.FileName != "")
            {
                AVM.Parsers.BackupParser parser;
                parser = new AVM.Parsers.BackupParser(_db);
                parser.WriteXmlBackup(backupSaveFileDialog.FileName);
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            loadProperties();
        }

        public bool Success
        {
            get { return _success; }
        }

        private void loadProperties()
        {
            nameColumnCheckBox.Checked =
                    Properties.Settings.Default.NameColumnEnabled;
            nameColumnLabelTextBox.Text =
                    Properties.Settings.Default.NameColumnLabel;

            episodeNameColumnCheckBox.Checked =
                    Properties.Settings.Default.EpisodeNameColumnEnabled;
            episodeNameColumnLabelTextBox.Text =
                    Properties.Settings.Default.EpisodeNameColumnLabel;

            seasonNumberColumnCheckBox.Checked =
                    Properties.Settings.Default.SeasonNumberColumnEnabled;
            seasonNumberColumnLabelTextBox.Text =
                    Properties.Settings.Default.SeasonNumberColumnLabel;

            episodeNumberColumnCheckBox.Checked =
                    Properties.Settings.Default.EpisodeNumberColumnEnabled;
            episodeNumberColumnLabelTextBox.Text =
                    Properties.Settings.Default.EpisodeNumberColumnLabel;

            videoCodecColumnCheckBox.Checked =
                    Properties.Settings.Default.VideoCodecColumnEnabled;
            videoCodecColumnLabelTextBox.Text =
                    Properties.Settings.Default.VideoCodecColumnLabel;

            audioCodecColumnCheckBox.Checked =
                    Properties.Settings.Default.AudioCodecColumnEnabled;
            audioCodecColumnLabelTextBox.Text =
                    Properties.Settings.Default.AudioCodecColumnLabel;

            containerColumnCheckBox.Checked =
                    Properties.Settings.Default.ContainerColumnEnabled;
            containerColumnLabelTextBox.Text =
                    Properties.Settings.Default.ContainerColumnLabel;

            timesPlayedColumnCheckBox.Checked =
                    Properties.Settings.Default.TimesPlayedColumnEnabled;
            timesPlayedColumnLabelTextBox.Text =
                    Properties.Settings.Default.TimesPlayedColumnLabel;

            youTubeWebPlayerCheckBox.Checked = Properties.Settings.Default.YouTubeWebPlayer;
            huluWebPlayerCheckBox.Checked = Properties.Settings.Default.HuluWebPlayer;
            patternTextBox.Text = Properties.Settings.Default.FileNamePattern;
            moreInfoComboBox.Text = Properties.Settings.Default.MoreInfoService;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            loadProperties();
        }
    }
}
