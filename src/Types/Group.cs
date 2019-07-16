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


namespace AVM.Types
{
    public class Group
    {
        private string _name;
        private long _id;
        private long _parentId;

        #region Properties
        /// <summary>
        /// This is the name of the group.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// This is the group_id for the group.
        /// Used to identify the group in database lookups.
        /// </summary>
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// This is the group_id for the parent of the current group.
        /// Used to identify parent group in database lookups.
        /// </summary>
        public long ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Makes a basic group with no name.
        /// </summary>
        public Group()
        {
            _name = "";
        }

        /// <summary>
        /// Creates a group with a name and id.
        /// </summary>
        /// <param name="name">Name of the group.</param>
        /// <param name="id">The group_id number.</param>
        public Group(string name,
                     long id)
        {
            _name = name;
            _id = id;
        }

        /// <summary>
        /// Creates a group with a name, id and id of its parent group.
        /// </summary>
        /// <param name="name">Name of the group.</param>
        /// <param name="id">The group_id number.</param>
        /// <param name="parentId">The group_id of this groups parent group.</param>
        public Group(string name,
                     long id,
                     long parentId)
        {
            _name = name;
            _id = id;
            _parentId = parentId;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the name of the group.
        /// </summary>
        /// <returns>Name of the group.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion
    }
}
