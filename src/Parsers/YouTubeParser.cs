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
using System.Text;
using System.Net;

namespace AVM.Parsers
{
    class YouTubeParser
    {
        private Uri _link = null;
        private string _title = null;
        private string _embeded = null;

        public YouTubeParser(string input)
        {
            if (Uri.IsWellFormedUriString(input, UriKind.Absolute))
                _link = new Uri(input);
            else
                _link = null;
        }

        public bool parse()
        {
            if (_link != null)
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string doc = client.DownloadString(_link);
                int temp = doc.IndexOf("<meta name=\"title\" content=\"");
                _title = doc.Substring(temp + 28);
                temp = _title.IndexOf("\">");
                _title = _title.Substring(0, temp);
                // Only pull out embedded data if it will be able to play it
                if (!doc.Contains("Embedding disabled by request"))
                {
                    temp = doc.IndexOf("var embedUrl = '");
                    _embeded = doc.Substring(temp + 16);
                    temp = _embeded.IndexOf("';");
                    _embeded = _embeded.Substring(0, temp);
                }
                return true;
            }
            else
                return false;
        }

        public string Title
        {
            get { return _title; }
        }

        public string Embeded
        {
            get { return _embeded; }
        }

        public string Url
        {
            get { return _link.OriginalString; }
        }
    }
}
