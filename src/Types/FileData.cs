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

    public class FileData
    {
        private Uri _uri;
        private string _videoEncoding;
        private string _audioEncoding;
        private string _container;

        #region Properties
        /// <summary>
        /// The file's uri (filename).
        /// </summary>
        public Uri Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        /// <summary>
        /// Stores and returns valid video encoding names.
        /// </summary>
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

        /// <summary>
        /// Stores and returns valid audio encoding names.
        /// </summary>
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

        /// <summary>
        /// Stores and returns valid container names.
        /// </summary>
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
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a basic version of FileData.
        /// </summary>
        public FileData()
        {
        }

        /// <summary>
        /// Creates FileData using the full path to the file.
        /// </summary>
        /// <param name="fullPath">The full path to the file.</param>
        public FileData(string fullPath)
        {
            _uri = new Uri(fullPath);
        }
        
        /// <summary>
        /// Creates FileData using the path and name of the file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="name">The name of the file.</param>
        public FileData(string path, string name)
        {
            _uri = new Uri(path + name);
        }
        #endregion
    }
}
