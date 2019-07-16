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


namespace AVM.Types
{
    public class Group
    {
        private string _name;
        private long _id;
        private long _parentId;

        // Constructors
        public Group()
        {
            _name = "";
        }

        public Group(string name, long id)
        {
            _name = name;
            _id = id;
        }

        public Group(string name, long id, long parentId)
        {
            _name = name;
            _id = id;
            _parentId = parentId;
        }

        // Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public long ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        // Overrides
        public override string ToString()
        {
            return _name;
        }
    }
}
