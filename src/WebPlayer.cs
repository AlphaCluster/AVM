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
        private string original_embeded;

        public WebPlayer()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return this.Title; }
            set { this.Text = value; }
        }

        public void PlayYouTube(string embeded)
        {
            _youTube = true;
            original_embeded = embeded;
            embeded = embeded.Replace("WIDTH", videoWebBrowser.Width.ToString());
            embeded = embeded.Replace("HEIGHT", videoWebBrowser.Height.ToString());
            videoWebBrowser.DocumentText = "<body style=\"margin:0px\">" + embeded + "</body>";
        }

        public void PlayHulu(string url)
        {
            // TODO get this working since its for some reason gives scripting errors?
            videoWebBrowser.Url = new Uri(url);
        }

        private void WebPlayer_Resize(object sender, EventArgs e)
        {
            if (_youTube)
            {
                string embeded = original_embeded;
                embeded = embeded.Replace("WIDTH", videoWebBrowser.Width.ToString());
                embeded = embeded.Replace("HEIGHT", videoWebBrowser.Height.ToString());
                videoWebBrowser.DocumentText = "<body style=\"margin:0px\">" + embeded + "</body>";
            }
        }

        private void videoWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //if (_youTube)
            //{
            //    Video_Manager.Parsers.YouTubeParser parser =
            //            new Video_Manager.Parsers.YouTubeParser(videoWebBrowser.Url.ToString());
            //    parser.parse();
            //    Text = parser.Title;
            //    PlayYouTube(Video_Manager.Node.YOUTUBE_EMBEDED_BASE.Replace("LINK", parser.Embeded));
            //    videoWebBrowser
            //}
        }

        private void WebPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoWebBrowser.ScriptErrorsSuppressed = true;
        }
    }
}
