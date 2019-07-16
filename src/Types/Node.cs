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
using System.IO;

namespace AVM.Types
{
    public class Node
    {
        // Last played for group T/F
        private string _name;
        private long _node_id;
        private long _parent_id;
        private AVM.Types.FileData _file;
        private AVM.Types.EpisodeInfo _episodeInfo;
        
        public const string YOUTUBE_EMBEDED_BASE = "<object width=\"WIDTH\" height=\"HEIGHT\"><param name=\"movie\" value=\"LINK\"></param><param name=\"allowFullScreen\" value=\"true\"></param><embed src=\"LINK\" type=\"application/x-shockwave-flash\" allowfullscreen=\"true\" width=\"WIDTH\" height=\"HEIGHT\"></embed></object>";
        private string _embeded = null;
        private string _url = null;

        private bool _youTube = false;
        private bool _hulu = false;

        private string _comment = null;
        private int _played = 0;

        private string ReplaceBrackets(string original)
        {
            string new_string = "";
            bool can_copy = true;
            foreach (char character in original)
            {
                if (character == '[')
                    can_copy = false;
                if (can_copy)
                    new_string += character;
                if (character == ']')
                    can_copy = true;
            }
            return new_string;
        }

        public Node()
        {
            _name = "";
            _file = null;
            _embeded = null;
            _episodeInfo = null;
        }

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

        public long Id
        {
            get { return _node_id; }
            set { _node_id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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

        public AVM.Types.EpisodeInfo Episode
        {
            get { return _episodeInfo; }
            set { _episodeInfo = value; }
        }

        public AVM.Types.FileData File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string YouTube
        {
            // This should be gotten from Embeded
            //get { return _embeded; }
            set
            {
                if (value == null)
                {
                    _embeded = null;
                    _youTube = true;
                }
                else
                {
                    _embeded = YOUTUBE_EMBEDED_BASE.Replace("LINK", value);
                    _youTube = true;
                }
            }
        }

        public string Hulu
        {
            set
            {
                if (value == null)
                {
                    _embeded = null;
                    _hulu = true;
                }
                else
                {
                    // FIXME fix this so it does everything i needs to do
                    _embeded = value;
                    _hulu = true;
                }
            }
        }

        /// <summary>
        /// This is used to test Hulu and YouTube videos.
        /// </summary>
        public void Play()
        {
            if (_youTube == true)
            {
                if ((_embeded != null) &&
                    (Properties.Settings.Default.YouTubeWebPlayer))
                {
                    WebPlayer player = new WebPlayer();
                    player.PlayYouTube(_embeded);
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
                if ((_embeded != null) &&
                    (Properties.Settings.Default.HuluWebPlayer))
                {
                    AVM.WebPlayer player = new AVM.WebPlayer();
                    player.PlayHulu(_embeded);
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

        public short UrlType
        /* returns what type of Url is stored in Url
         * 0: nothing
         * 1: youtube
         * 2: hulu
         */
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

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Embeded
        {
            get { return _embeded; }
            // used when not creating a new embeded set
            set { _embeded = value; }
        }

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

        public int TimesPlayed
        {
            get { return _played; }
            set { _played = value; }
        }

        public long ParentId
        {
            get { return _parent_id; }
            set { _parent_id = value; }
        }

        // TODO write this to do name parsing instead! move this to FileParser
        public void NameParser()
        {
        }
    }
}
