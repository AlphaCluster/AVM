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

    public class FileData
    {
        private Uri _uri;
        private string _videoEncoding;
        private string _audioEncoding;
        private string _container;

        public Uri Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public string Video_Encoding
        {
            get { return _videoEncoding; }
            set
            {
                // Test to make sure its actually in a valid format
                if ((value == "Xvid") || (value == "x264") || (value == "h264") ||
                    (value == "DivX") || (value == "mpeg2") || (value == "VC-1") ||
                    (value == "Theora"))
                    _videoEncoding = value;
                else
                    _videoEncoding = "";
            }
        }

        public string Audio_Encoding
        {
            get { return _audioEncoding; }
            set
            {
                // Test to make sure its actually a valid format
                if ((value == "MP3") || (value == "AAC") || (value == "Vorbis") ||
                    (value == "AC-3"))
                    _audioEncoding = value;
                else
                    _audioEncoding = "";
            }
        }

        public string Container
        {
            get { return _container; }
            set
            {
                // Test to make sure its a valid container
                if ((value == "flv") || (value == "avi") || (value == "mkv") || 
                    (value == "ogm") || (value == "mp4") || (value == "wmv") ||
                    (value == "mpeg") || (value == "mpg"))
                    _container = value;
                else
                    _container = "";
            }
        }

        // Constructors
        public FileData()
        {
        }

        public FileData(string fullPath)
        {
            _uri = new Uri(fullPath);
        }
        
        public FileData(string path, string name)
        {
            _uri = new Uri(path + name);
        }
    }
}
