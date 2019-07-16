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
    public partial class WebPlayer : Form
    {
        private bool _youTube = false;
        private string original_embedded;

        #region Properties
        /// <summary>
        /// Returns the title of the form.
        /// </summary>
        public string Title
        {
            get { return this.Title; }
            set { this.Text = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Makes a new WebPlayer nothing fancy here.
        /// </summary>
        public WebPlayer()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Plays a YouTube video using hte embedded string.
        /// </summary>
        /// <param name="embedded">The YouTube embedded string.</param>
        public void PlayYouTube(string embedded)
        {
            _youTube = true;
            original_embedded = embedded;
            embedded = embedded.Replace("WIDTH", videoWebBrowser.Width.ToString());
            embedded = embedded.Replace("HEIGHT", videoWebBrowser.Height.ToString());
            videoWebBrowser.DocumentText = "<body style=\"margin:0px\">" + embedded + "</body>";
        }

        /// <summary>
        /// Plays a Hulu video by using the embedded url.
        /// </summary>
        /// <param name="url">embedded url to play video via.</param>
        public void PlayHulu(string url)
        {
            // TODO get this working since its for some reason gives scripting errors?
            videoWebBrowser.Url = new Uri(url);
        }

        /// <summary>
        /// When a YouTube video is playing refresh the YouTube video to the new size.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebPlayer_Resize(object sender,
                                      EventArgs e)
        {
            if (_youTube)
            {
                string embedded = original_embedded;
                embedded = embedded.Replace("WIDTH", videoWebBrowser.Width.ToString());
                embedded = embedded.Replace("HEIGHT", videoWebBrowser.Height.ToString());
                videoWebBrowser.DocumentText = "<body style=\"margin:0px\">" + embedded + "</body>";
            }
        }

        /// <summary>
        /// Prevent errors from showing up when the form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoWebBrowser.ScriptErrorsSuppressed = true;
        }
        #endregion
    }
}
