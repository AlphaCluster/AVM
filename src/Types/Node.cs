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
using System.IO;

namespace AVM.Types
{
    public class Node
    {
        
        private string _name;
        private long _node_id;
        private long _parent_id;
        private AVM.Types.FileData _file;
        private AVM.Types.EpisodeInfo _episodeInfo;
        
        //This is the base for making a embedded YouTube video. Fill in WIDTH, HEIGHT and LINK to use.
        public const string YOUTUBE_embedded_BASE = "<object width=\"WIDTH\" height=\"HEIGHT\"><param name=\"movie\" value=\"LINK\"></param><param name=\"allowFullScreen\" value=\"true\"></param><embed src=\"LINK\" type=\"application/x-shockwave-flash\" allowfullscreen=\"true\" width=\"WIDTH\" height=\"HEIGHT\"></embed></object>";
        private string _embedded = null;
        private string _url = null;

        private bool _youTube = false;
        private bool _hulu = false;

        private string _comment = null;
        private int _played = 0;

        #region Properties
        /// <summary>
        /// The node_id for the node.
        /// Used as an identifier for database lookups.
        /// </summary>
        public long Id
        {
            get { return _node_id; }
            set { _node_id = value; }
        }

        /// <summary>
        /// The nodes name that is seen by the user.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Checks if there is FileData in this node.
        /// </summary>
        public bool IsFile
        {
            get
            {
                if (_file == null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Checks if the node is a YouTube video.
        /// </summary>
        public bool IsYouTube
        {
            get
            {
                if (_youTube == true)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Checks if the node is a Hulu video.
        /// </summary>
        public bool IsHulu
        {
            get
            {
                if (_hulu == true)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Checks if the node has EpisodeInfo.
        /// </summary>
        public bool IsEpisode
        {
            get
            {
                if (_episodeInfo == null)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Returns or sets the EpisodeInfo for this node.
        /// </summary>
        public AVM.Types.EpisodeInfo Episode
        {
            get { return _episodeInfo; }
            set { _episodeInfo = value; }
        }

        /// <summary>
        /// Returns or sets the FileData for this node.
        /// </summary>
        public AVM.Types.FileData File
        {
            get { return _file; }
            set { _file = value; }
        }

        /// <summary>
        /// Used to set a YouTube video by sending the embedded link.
        /// If an embedded link is sent then it creates the right html code for
        /// an embedded player and if it is null it doesn't.
        /// </summary>
        public string YouTube
        {
            // This should be gotten from embedded
            //get { return _embedded; }
            set
            {
                if (value == null)
                {
                    _embedded = null;
                    _youTube = true;
                }
                else
                {
                    _embedded = YOUTUBE_embedded_BASE.Replace("LINK", value);
                    _youTube = true;
                }
            }
        }

        /// <summary>
        /// Used to set a Hulu video by sending the embedded link.
        /// If an embedded link is sent then it creates the right html code for
        /// an embedded player (currently not working) and if it is null it doesn't.
        /// </summary>
        public string Hulu
        {
            set
            {
                if (value == null)
                {
                    _embedded = null;
                    _hulu = true;
                }
                else
                {
                    // TODO this can be fixed to save an embedded Hulu video like YouTube
                    // not a very importent feature as Hulu embedded player sucks.
                    _embedded = value;
                    _hulu = true;
                }
            }
        }

        /// <summary>
        /// Returns what type of Url is stored in Url.
        /// 0: nothing
        /// 1: youtube
        /// 2: hulu
        /// </summary>
        public short UrlType
        {
            get
            {
                if (_youTube)
                    return 1;
                if (_hulu)
                    return 2;
                return 0;
            }
            set
            {
                if (value == 1)
                    _youTube = true;
                else
                    _youTube = false;
                if (value == 2)
                    _hulu = true;
                else
                    _hulu = false;
            }
        }

        /// <summary>
        /// Returns or sets the url for the YouTube or Hulu video.
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// Gets and sets the embedded string.
        /// NOTE: Do not use this to set the embedded string for
        /// a new YouTube or Hulu video.
        /// </summary>
        public string embedded
        {
            get { return _embedded; }
            // used when not creating a new embedded set
            set { _embedded = value; }
        }

        /// <summary>
        /// Gets or sets the comment. This if for DVDs.
        /// </summary>
        public string Comment
        {
            get
            {
                if (_comment == null)
                    return "";
                else
                    return _comment;
            }
            set
            {
                if (value == "")
                    _comment = null;
                else
                    _comment = value;
            }
        }

        /// <summary>
        /// Gets and sets the number of times played.
        /// </summary>
        public int TimesPlayed
        {
            get { return _played; }
            set { _played = value; }
        }

        /// <summary>
        /// The group_id of the parent group for the node.
        /// </summary>
        public long ParentId
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a basic node with no information and a blank name.
        /// </summary>
        public Node()
        {
            _name = "";
            _file = null;
            _embedded = null;
            _episodeInfo = null;
        }

        /// <summary>
        /// Creates a node based on a given path to a file.
        /// </summary>
        /// <param name="path">Path to the file the node is for.</param>
        public Node(string path)
        {
            _file = new FileData(path);

            AVM.Parsers.FolderParser parser = new AVM.Parsers.FolderParser();

            // -1 signifies that the parser should only run once
            Node tempNode = parser.parseFile(-1, new FileInfo(_file.Uri.OriginalString));

            if (tempNode != null)
            {
                _name = tempNode._name;
                if (tempNode.IsEpisode)
                {
                    _episodeInfo.EpisodeName = tempNode.Episode.EpisodeName;
                    _episodeInfo.EpisodeNumber = tempNode.Episode.EpisodeNumber;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Used to play Hulu and YouTube videos.
        /// </summary>
        public void Play()
        {
            if (_youTube == true)
            {
                if ((_embedded != null) &&
                    (Properties.Settings.Default.YouTubeWebPlayer))
                {
                    WebPlayer player = new WebPlayer();
                    player.PlayYouTube(_embedded);
                    player.Title = _name;
                    player.ShowDialog();
                }
                else
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(_url);
                    System.Diagnostics.Process.Start(psi);
                }
            }
            if (_hulu == true)
            {
                if ((_embedded != null) &&
                    (Properties.Settings.Default.HuluWebPlayer))
                {
                    AVM.WebPlayer player = new AVM.WebPlayer();
                    player.PlayHulu(_embedded);
                    player.Title = _name;
                    player.ShowDialog();
                }
                else
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(_url);
                    System.Diagnostics.Process.Start(psi);
                }
            }
        }
        #endregion
    }
}
