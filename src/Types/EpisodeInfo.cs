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

namespace AVM.Types
{
    public class EpisodeInfo
    {
        private int _episodeNumber;
        private int _seasonNumber;
        private bool _lastWatched;
        private string _episodeName;

        public EpisodeInfo()
        {
            _episodeName = "";
            _lastWatched = false;
            _episodeNumber = -1;
            _seasonNumber = -1;
        }

        public EpisodeInfo(int episodeNumber)
        {
            _episodeName = "";
            _lastWatched = false;
            _episodeNumber = episodeNumber;
            _seasonNumber = -1;
        }

        public int EpisodeNumber
        {
            get { return _episodeNumber; }
            set { _episodeNumber = value; }
        }

        public int SeasonNumber
        {
            get { return _seasonNumber; }
            set { _seasonNumber = value; }
        }

        public string EpisodeName
        {
            get { return _episodeName; }
            set { _episodeName = value; }
        }

        public bool LastWatched
        {
            get { return _lastWatched; }
            set { _lastWatched = value; }
        }

    }
}
