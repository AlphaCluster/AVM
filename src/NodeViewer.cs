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

        public NodeViewer(AVM.Types.Node node)
        {
            InitializeComponent();
            _node = node;
        }

        private void NodeViewer_Load(object sender, EventArgs e)
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

        private void playButton_Click(object sender, EventArgs e)
        {
            _play = true;
            this.Close();
        }

        public bool Play
        {
            get { return _play; }
        }

        private void moreInfoButton_Click(object sender, EventArgs e)
        {
            string searchString = "";
            switch (Properties.Settings.Default.MoreInfoService)
            {
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
    }
}
