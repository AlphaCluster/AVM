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
using System.Text.RegularExpressions;
using System.IO;

namespace AVM.Parsers
{
    class FolderParser
    {
        private string path = "";
        private List<AVM.Types.Node> nodes;
        private List<AVM.Types.Group> groups;
        private long last_node = 0;
        private long last_group = 0;
        private string _pattern = Properties.Settings.Default.FileNamePattern;
        private AVM.Database _db;

        #region Properties
        /// <summary>
        /// Sets the pattern used to parse the filename.
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a basic FolderParser.
        /// </summary>
        public FolderParser()
        {
        }

        /// <summary>
        /// Creates a Folder parser for a given path and using the passed
        /// in database.
        /// </summary>
        /// <param name="path">The path to the folder that is to be parsed.</param>
        /// <param name="db">The database to load groups and nodes into.</param>
        public FolderParser(string path,
                            AVM.Database db)
        {
            this.path = path;
            _db = db;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parses a single folder for files and additional folders.
        /// </summary>
        /// <param name="folder">The folder to parse.</param>
        /// <param name="parent_id">The parent_id of the groups parient.</param>
        private void parseFolder(DirectoryInfo folder,
                                 long parent_id)
        {
            AVM.Types.Group group = new AVM.Types.Group();
            group.Id = ++last_group;
            group.ParentId = parent_id;
            group.Name = cleanString(folder.Name);
            Console.WriteLine(group.Id + ": " + group.Name);

            groups.Add(group);

            foreach (FileInfo file in folder.GetFiles())
            {
                parseFile(group.Id, file);
            }

            foreach (DirectoryInfo temp in folder.GetDirectories())
            {
                parseFolder(temp, group.Id);
            }
        }

        /// <summary>
        /// Parses a single file using the pattern.
        /// </summary>
        /// <param name="parent_group">Group_id of the parent group.</param>
        /// <param name="file">The file to be parsed.</param>
        /// <returns>A node with all the data that could be parsed from the file.</returns>
        public AVM.Types.Node parseFile(long parent_group,
                                        FileInfo file)
        {
            string ext = file.Extension.ToLower();
            string name;
            string dil;
            string tempName;
            string pattern = _pattern;
            string tempPattern;

            AVM.Types.Node node = new AVM.Types.Node();
            node.Id = ++last_node;
            node.ParentId = parent_group;
            node.File = new AVM.Types.FileData();
            node.File.Uri = new Uri(file.FullName);

            if ((_pattern.Contains(" - ")) ||
                (_pattern.Contains("_-_")))
                dil = " - ";
            else
                dil = " ";

            if ((ext == ".flv") || (ext == ".avi") || (ext == ".mkv") ||
                (ext == ".ogm") || (ext == ".mp4") || (ext == ".wmv"))
            {
                // Parse filename for information
                name = cleanString(file.Name.Replace("_", " "));
                name = name.Replace(ext, "");
                while ((pattern != "") &&
                       (name != ""))
                {
                    if (name.Contains(dil))
                        tempName = name.Remove(name.IndexOf(dil));
                    else
                        tempName = name;

                    if (pattern.Contains(dil))
                        tempPattern = pattern.Remove(_pattern.IndexOf(dil));
                    else
                        tempPattern = pattern;

                    // This is the pattern keyword section.
                    // Adding an if statement here for the
                    // keyword you want to add will allow
                    // you to add your own keywords to the
                    // pattern.

                    if (tempPattern.Replace(" ", "") == "Name")
                    {
                        tempName = tempName.TrimStart(' ');
                        tempName = tempName.TrimEnd(' ');
                        node.Name = tempName;
                    }

                    if (tempPattern.Replace(" ", "") == "EpisodeNumber")
                    {
                        int number;
                        if (Int32.TryParse(tempName, out number))
                        {
                            if (!node.IsEpisode)
                                node.Episode = new AVM.Types.EpisodeInfo();
                            node.Episode.EpisodeNumber = number;
                        }
                    }

                    if (name.Contains(dil))
                        name = name.Replace(tempName + dil, "");
                    else
                        name = name.Replace(tempName, "");

                    if (pattern.Contains(dil))
                        pattern = pattern.Replace(tempPattern + dil, "");
                    else
                        pattern = pattern.Replace(tempPattern, "");
                }
                //Console.WriteLine("-" + last_node++ + " - " + cleanString(file.Name.Replace("_", " ")));
                Console.Write("--" + node.Id + ": " + node.Name);
                if (node.IsEpisode)
                    Console.WriteLine(" #" + node.Episode.EpisodeNumber);
                else
                    Console.WriteLine();

                if (parent_group != -1)
                    nodes.Add(node);
                return node;
            }
            else
                return null;
        }

        /// <summary>
        /// Parses through the path it's configured to parse.
        /// </summary>
        /// <param name="parent_id">This is the group_id for the 
        /// group it is going to be inside of.</param>
        public void parse(long parent_id)
        {
            groups = new List<AVM.Types.Group>();
            nodes = new List<AVM.Types.Node>();
            
            //parseFolder(start);
            DirectoryInfo direct = new DirectoryInfo(path);

            //Get starting group_id
            last_group = _db.getLastGroupId();
            //Get starting node_id
            last_node = _db.getLastNodeId();

            if (direct.Exists)
            {
                Console.WriteLine("Path Exists");
                parseFolder(direct, parent_id);

                _db.fillGroups(groups);
                _db.fillNodes(nodes);
            }
            else
                Console.WriteLine("Path: " + path + "\nDoes not work!");
        }

        /// <summary>
        /// Removes everything between a () and [] in the string.
        /// </summary>
        /// <param name="original">The original string to be cleaned.</param>
        /// <returns>The clean string.</returns>
        private string cleanString(string original)
        {
            string clean = original;

            while ((clean.Contains("(") && clean.Contains(")")) ||
                   (clean.Contains("[") && clean.Contains("]")))
            {
                if (clean.Contains("(") && clean.Contains(")"))
                {
                    string front = clean.Remove(clean.IndexOf("("));
                    string back = clean.Substring(clean.IndexOf("("));
                    back = back.Substring(back.IndexOf(")") + 1);
                    clean = front + back;
                }

                if (clean.Contains("[") && clean.Contains("]"))
                {
                    string front = clean.Remove(clean.IndexOf("["));
                    // Do this incase there are braces that do not match up
                    string back = clean.Substring(clean.IndexOf("["));
                    back = back.Substring(back.IndexOf("]") + 1);
                    clean = front + back;
                }
            }

            return clean;
        }
        #endregion
    }
}
