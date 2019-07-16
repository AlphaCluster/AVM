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

namespace AVM.Types
{
    public class EpisodeInfo
    {
        private int _episodeNumber;
        private int _seasonNumber;
        private bool _lastWatched;
        private string _episodeName;

        #region Properties
        /// <summary>
        /// Episode number for the node.
        /// </summary>
        public int EpisodeNumber
        {
            get { return _episodeNumber; }
            set { _episodeNumber = value; }
        }

        /// <summary>
        /// Season number for the node.
        /// </summary>
        public int SeasonNumber
        {
            get { return _seasonNumber; }
            set { _seasonNumber = value; }
        }

        /// <summary>
        /// Name of the episode for the node.
        /// </summary>
        public string EpisodeName
        {
            get { return _episodeName; }
            set { _episodeName = value; }
        }

        /// <summary>
        /// Whether or not the episode was the last one watched.
        /// </summary>
        public bool LastWatched
        {
            get { return _lastWatched; }
            set { _lastWatched = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a basic version of EpisodeInfo.
        /// </summary>
        public EpisodeInfo()
        {
            _episodeName = "";
            _lastWatched = false;
            _episodeNumber = -1;
            _seasonNumber = -1;
        }

        /// <summary>
        /// Creates EpisodeInfo with a populated episode number.
        /// </summary>
        /// <param name="episodeNumber">The nodes episode number.</param>
        public EpisodeInfo(int episodeNumber)
        {
            _episodeName = "";
            _lastWatched = false;
            _episodeNumber = episodeNumber;
            _seasonNumber = -1;
        }
        #endregion
    }
}
