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
    public partial class NodeViewer : Form
    {
        private AVM.Types.Node _node = null;
        private bool _play = false;

        #region Properties
        /// <summary>
        /// Tells whether or not the video should be played after the
        /// viewer is closed.
        /// </summary>
        public bool Play
        {
            get { return _play; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a NodeViewer form populated with the information from
        /// the passed in node.
        /// </summary>
        /// <param name="node"></param>
        public NodeViewer(AVM.Types.Node node)
        {
            InitializeComponent();
            _node = node;
        }
        #endregion

        #region Button Methods
        /// <summary>
        /// Sets play to true and closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playButton_Click(object sender,
                                      EventArgs e)
        {
            _play = true;
            this.Close();
        }

        /// <summary>
        /// Opens a web browser to a search page for the chosen service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moreInfoButton_Click(object sender,
                                          EventArgs e)
        {
            string searchString = "";
            switch (Properties.Settings.Default.MoreInfoService)
            {
                // Add to this list if you want to add another search.
                case "imdb":
                    searchString = "http://www.imdb.com/find?s=all&q=$SEARCH&x=0&y=0".Replace("$SEARCH", _node.Name.Replace(" ", "%20"));
                    break;
                case "Anime News Network":
                    searchString = "http://www.animenewsnetwork.com/search?cx=016604166282602569737%3Aznd1ysjewre&q=$SEARCH".Replace("$SEARCH", _node.Name.Replace(" ", "%20"));
                    break;
                case "Wikipedia":
                    searchString = "http://en.wikipedia.org/wiki/Special:Search?search=$SEARCH".Replace("$SEARCH", _node.Name.Replace(" ", "%20"));
                    break;
                default:
                    searchString = "http://www.google.com/search?q=$SEARCH".Replace("$SEARCH", _node.Name.Replace(" ", "%20"));
                    break;
            }
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(searchString);
            System.Diagnostics.Process tempPlayer;
            tempPlayer = System.Diagnostics.Process.Start(psi);
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Loads all of the information stored in _node.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeViewer_Load(object sender,
                                     EventArgs e)
        {
            nameLabel.Text = "Name: " + _node.Name;
            if (_node.IsEpisode)
            {
                episodeNameLabel.Text = "Episode Name: " + _node.Episode.EpisodeName;
                
                if (_node.Episode.SeasonNumber > -1)
                    seasonNumberLabel.Text = "Season: " + _node.Episode.SeasonNumber.ToString();
                else
                    seasonNumberLabel.Text = "Season: ";

                if (_node.Episode.EpisodeNumber > -1)
                    episodeNumberLabel.Text = "Episode: " + _node.Episode.EpisodeNumber.ToString();
                else
                    episodeNumberLabel.Text = "Episode: ";
            }
            if (_node.IsFile)
            {
                videoCodecLabel.Text = "Video Codec: " + _node.File.Video_Encoding;
                audioCodecLabel.Text = "Audio Codec: " + _node.File.Audio_Encoding;
                containerLabel.Text = "Container Type: " + _node.File.Container;
            }
        }
        #endregion
    }
}
