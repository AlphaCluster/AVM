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
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace AVM.Parsers
{
    class HuluParser
    {
        private Uri link = null;
        private string _title = null;
        private string _episodeTitle = null;
        private int _episodeNumber = -1;
        private int _seasonNumber = -1;
        private string _embedded = null;
        private string _videoType = null; // movie, video, tv, hd, excerpt

        #region Properties
        /// <summary>
        /// This is the title for the Hulu video.
        /// </summary>
        public string Title
        {
            get { return _title; }
        }

        /// <summary>
        /// This is the episode title for the Hulu video.
        /// </summary>
        public string EpisodeTitle
        {
            get
            {
                if (_episodeTitle == null)
                    return "";
                else
                    return _episodeTitle;
            }
        }

        /// <summary>
        /// This is the episode number for the Hulu video.
        /// </summary>
        public int EpisodeNumber
        {
            get { return _episodeNumber; }
        }

        /// <summary>
        /// This is the season number for the Hulu video.
        /// </summary>
        public int SeasonNumber
        {
            get { return _seasonNumber; }
        }

        /// <summary>
        /// This is the embedded string for the Hulu video.
        /// </summary>
        public string Embedded
        {
            get { return _embedded; }
        }

        /// <summary>
        /// This is the url to the Hulu video.
        /// </summary>
        public string Url
        {
            get { return link.OriginalString; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// This creates a HuluParser with the url that is to be parsed.
        /// </summary>
        /// <param name="input">Url that is to be parse.</param>
        public HuluParser(string input)
        {
            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
                link = new Uri(input);
            else
                link = null;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the parser on the url that it was created with.
        /// Returns false if parse fails.
        /// </summary>
        /// <returns>Returns true if it parses successfully.</returns>
        public bool parse()
        {
            if (link != null)
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string doc = client.DownloadString(link);
                int index;

                // Figure out what type of hulu video it is
                index = doc.IndexOf("<div class=\"description\">");
                string disc = doc.Substring(index + 25);
                index = disc.IndexOf("<span");
                disc = disc.Substring(0, index);

                // Currently all HD is tv but does not parse the same
                if (Regex.IsMatch(disc, ".*Full Episode.*"))
                    _videoType = "hd";
                if (Regex.IsMatch(disc, ".*Season.*"))
                    _videoType = "tv";
                if (Regex.IsMatch(disc, ".*Feature Film.*"))
                    _videoType = "movie";
                if (Regex.IsMatch(disc, ".*Excerpt.*"))
                    _videoType = "excerpt";
                if (Regex.IsMatch(disc, ".*Video.*"))
                    _videoType = "video";

                switch ((string)_videoType)
                {
                    case "hd":
                        index = doc.IndexOf("<h2 class=\"show-name\" style=\"margin-bottom:3px;\">");
                        string hold = doc.Substring(index + 49);
                        index = hold.IndexOf("</h2>");
                        hold = hold.Substring(0, index);

                        string[] holdArr = hold.Split('-');
                        if (holdArr.Length > 1)
                        {
                            _title = holdArr[0];
                            _episodeTitle = holdArr[1];
                            _episodeTitle = _episodeTitle.TrimStart(' ');
                        }
                        else
                            _title = holdArr[0];
                        break;

                    case "movie":
                        index = doc.IndexOf("<title>Hulu - ");
                        _title = doc.Substring(index + 14);
                        index = _title.IndexOf(" - ");
                        _title = _title.Substring(0, index);
                        break;
                    case "video":
                    case "excerpt":
                        index = doc.IndexOf("<title>Hulu - ");
                        _title = doc.Substring(index + 14);
                        index = _title.IndexOf("</title>");
                        _title = _title.Substring(0, index);
                        break;

                    case "tv":
                        index = doc.IndexOf("<title>Hulu - ");
                        string holdTV = doc.Substring(index + 14);
                        index = holdTV.IndexOf(" - ");
                        holdTV = holdTV.Substring(0, index);

                        // Get the title and episode title if it exists
                        string[] holdTVArray = holdTV.Split(':');
                        if (holdTVArray.Length > 1)
                        {
                            _title = holdTVArray[0];
                            _episodeTitle = holdTVArray[1];
                            _episodeTitle = _episodeTitle.TrimStart(' ');
                        }
                        else
                            _title = holdTVArray[0];

                        // get season number
                        index = disc.IndexOf("Season ");
                        string holderSeasonNumber = disc.Substring(index + 7);
                        index = holderSeasonNumber.IndexOf(":");
                        holderSeasonNumber = holderSeasonNumber.Substring(0, index);
                        index = holderSeasonNumber.IndexOf(" ");
                        holderSeasonNumber = holderSeasonNumber.Substring(0, index);
                        if (!int.TryParse(holderSeasonNumber, out _seasonNumber))
                            System.Windows.Forms.MessageBox.Show(
                                "Warning: Could not read season number",
                                "Warning",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Exclamation);

                        // get episode number
                        index = disc.IndexOf("Ep. ");
                        string holderEpisodeNumber = disc.Substring(index + 4);
                        if (!int.TryParse(holderEpisodeNumber, out _episodeNumber))
                            System.Windows.Forms.MessageBox.Show(
                                "Warning: Could not read episode number",
                                "Warning",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Exclamation);
                        break;

                    default:
                        System.Windows.Forms.MessageBox.Show(
                                "Warning: Link could not be parsed",
                                "Warning",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Exclamation);
                        break;
                }

                // If the video is embedable then save the embedded link.
                index = doc.IndexOf("<link rel=\"video_src\" href=\"");
                if (index > -1) // if not embedable temp will be null
                {
                    _embedded = doc.Substring(index + 28);
                    index = _embedded.IndexOf("\" />");
                    _embedded = _embedded.Substring(0, index);
                }
            }
            else
                return false;

            return true;
        }
        #endregion
    }
}
