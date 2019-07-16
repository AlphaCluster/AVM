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
using System.IO;
using System.Xml;

namespace AVM.Parsers
{
    class BackupParser
    {
        private Database _db;

        #region Constructor
        /// <summary>
        /// Creates a basic parser with a database if needed.
        /// </summary>
        /// <param name="db"></param>
        public BackupParser(Database db)
        {
            _db = db;
        }
        #endregion

        #region Writing Backup
        /// <summary>
        /// Writes an xml backup to the file passed in.
        /// </summary>
        /// <param name="file">File where you want to save the xml backup to.</param>
        public void WriteXmlBackup(string file)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(file, Encoding.UTF8);
            //StreamWriter writer = new StreamWriter(file);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("backup");
            
            // Write nodes section
            xmlWriter.WriteStartElement("nodes");
            writeNodes(xmlWriter);
            xmlWriter.WriteEndElement(); // close the nodes element

            // Write groups section
            xmlWriter.WriteStartElement("groups");
            writeGroups(xmlWriter);
            xmlWriter.WriteEndElement();

            // Finalize the xml document
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

        /// <summary>
        /// Writes all the nodes to the xmlWriter.
        /// </summary>
        /// <param name="xmlWriter">The writer for the xml backup.</param>
        private void writeNodes(XmlTextWriter xmlWriter)
        {
            List<AVM.Types.Node> list = _db.getAllNodes();

            foreach (AVM.Types.Node node in list)
                writeNode(node, xmlWriter);
        }

        /// <summary>
        /// Writes out an individual node to the xmlWriter.
        /// </summary>
        /// <param name="node">The node which is to be written.</param>
        /// <param name="xmlWriter">The writer for the xml backup.</param>
        private void writeNode(AVM.Types.Node node,
                               XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("node");
            xmlWriter.WriteAttributeString("id", node.Id.ToString());
            xmlWriter.WriteAttributeString("parent_id", node.ParentId.ToString());
            xmlWriter.WriteAttributeString("name", node.Name);
            xmlWriter.WriteStartElement("url");
            xmlWriter.WriteAttributeString("type", node.UrlType.ToString());
            xmlWriter.WriteString(node.Url);
            xmlWriter.WriteEndElement();
            // this should be able to be easily remade by choping off the last 
            // part of the url and placeing it in http://www.youtube.com/v/HERE&hl=en&fs=1
            // this should be looked into to save space since its taking up over 50% of a \
            // node in xml currently problem is hulu
            xmlWriter.WriteElementString("embedded", node.embedded);
            xmlWriter.WriteElementString("comment", node.Comment);

            // If its an episode write the Episode element
            if (node.IsEpisode)
            {
                xmlWriter.WriteStartElement("episode");
                xmlWriter.WriteAttributeString("name", node.Episode.EpisodeName);
                xmlWriter.WriteAttributeString("number", node.Episode.EpisodeNumber.ToString());
                xmlWriter.WriteAttributeString("season", node.Episode.SeasonNumber.ToString());
                xmlWriter.WriteAttributeString("last", node.Episode.LastWatched.ToString());
                xmlWriter.WriteEndElement();
            }
            else
                xmlWriter.WriteElementString("episode", null);

            if (node.IsFile)
            {
                xmlWriter.WriteStartElement("file");
                xmlWriter.WriteAttributeString("uri", node.File.Uri.ToString());
                xmlWriter.WriteAttributeString("audio", node.File.Audio_Encoding);
                xmlWriter.WriteAttributeString("video", node.File.Video_Encoding);
                xmlWriter.WriteAttributeString("container", node.File.Container);
                xmlWriter.WriteEndElement();
            }
            else
                xmlWriter.WriteElementString("file", null);

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Writes all the groups to the xmlWriter.
        /// </summary>
        /// <param name="xmlWriter">The writer for the xml backup.</param>
        private void writeGroups(XmlTextWriter xmlWriter)
        {
            List<AVM.Types.Group> groups = _db.getAllGroups();

            foreach (AVM.Types.Group group in groups)
                writeGroup(group, xmlWriter);
        }

        /// <summary>
        /// Writes out an individual group to xmlWriter.
        /// </summary>
        /// <param name="group">The group which is to be written.</param>
        /// <param name="xmlWriter">The writer for the xml backup.</param>
        private void writeGroup(AVM.Types.Group group, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("group");
            xmlWriter.WriteAttributeString("name", group.Name);
            xmlWriter.WriteAttributeString("id", group.Id.ToString());
            xmlWriter.WriteAttributeString("parent", group.ParentId.ToString());
            xmlWriter.WriteEndElement();
        }
        #endregion

        #region Reading Backup
        /// <summary>
        /// Reads an xml backup from the file passed in.
        /// </summary>
        /// <param name="file">File where you want to read the xml backup from.</param>
        public void ReadXmlBackup(string file)
        {
            _db.Clear();

            XmlTextReader xmlReader = new XmlTextReader(file);
            xmlReader.Read();
            xmlReader.ReadStartElement("backup");
            xmlReader.ReadStartElement("nodes");
            readNodes(xmlReader);
            xmlReader.ReadEndElement();
            xmlReader.ReadStartElement("groups");
            readGroups(xmlReader);
            xmlReader.ReadEndElement();
            xmlReader.ReadEndElement();
            xmlReader.Close();
        }

        /// <summary>
        /// Reads all the nodes from the xmlReader and adds them to the database.
        /// </summary>
        /// <param name="xmlReader">The reader for the xml backup.</param>
        private void readNodes(XmlTextReader xmlReader)
        {
            List<AVM.Types.Node> nodes = new List<AVM.Types.Node>();

            while (xmlReader.IsStartElement("node"))
            {
                nodes.Add(readNode(xmlReader));
            }

            _db.fillNodes(nodes);
        }

        /// <summary>
        /// Reads in an individual node from the xmlReader and returns a node.
        /// </summary>
        /// <param name="xmlReader">The reader for the xml backup.</param>
        /// <returns>The node that was read in.</returns>
        private AVM.Types.Node readNode(XmlTextReader xmlReader)
        {
            AVM.Types.Node node = new AVM.Types.Node();

            node.Id = Int64.Parse(xmlReader[0]);
            node.ParentId = Int64.Parse(xmlReader[1]);
            node.Name = xmlReader[2];

            xmlReader.ReadToDescendant("url");
            
            node.UrlType = Int16.Parse(xmlReader[0]);
            xmlReader.ReadStartElement("url");
            if (node.UrlType != 0)
            {
                node.Url = xmlReader.ReadString();
                xmlReader.ReadEndElement();
            }

            node.embedded = xmlReader.ReadElementString("embedded");
            node.Comment = xmlReader.ReadElementString("comment");

            // if it has attributes read it
            if ((xmlReader.AttributeCount > 0) && 
                (xmlReader.Name == "episode"))
            {
                node.Episode = new AVM.Types.EpisodeInfo();
                node.Episode.EpisodeName = xmlReader[0];
                node.Episode.EpisodeNumber = Int32.Parse(xmlReader[1]);
                node.Episode.SeasonNumber = Int32.Parse(xmlReader[2]);
                node.Episode.LastWatched = bool.Parse(xmlReader[3]);
            }
            xmlReader.ReadStartElement("episode");

            // if it has attributes then read it
            if ((xmlReader.AttributeCount > 0) &&
                (xmlReader.Name == "file"))
            {
                node.File = new AVM.Types.FileData();
                node.File.Uri = new Uri(xmlReader[0]);
                node.File.Audio_Encoding = xmlReader[1];
                node.File.Video_Encoding = xmlReader[2];
                node.File.Container = xmlReader[3];
            }
            xmlReader.ReadStartElement("file");

            xmlReader.ReadEndElement();
            return node;
        }

        /// <summary>
        /// Reads all the groups from the xmlReader and adds them to the database.
        /// </summary>
        /// <param name="xmlReader">The reader for the xml backup.</param>
        private void readGroups(XmlTextReader xmlReader)
        {
            List<AVM.Types.Group> groups = new List<AVM.Types.Group>();

            while (xmlReader.IsStartElement("group"))
            {
                groups.Add(readGroup(xmlReader));
            }

            _db.fillGroups(groups);
        }

        /// <summary>
        /// Reads in an individual group from the xmlReader and returns a group.
        /// </summary>
        /// <param name="xmlReader">The reader for the xml backup.</param>
        /// <returns>The group that was read in.</returns>
        private AVM.Types.Group readGroup(XmlTextReader xmlReader)
        {
            AVM.Types.Group group = new AVM.Types.Group();
            group.Name = xmlReader[0];
            group.Id = Int64.Parse(xmlReader[1]);
            group.ParentId = Int64.Parse(xmlReader[2]);
            xmlReader.ReadToNextSibling("group");
            return group;
        }
        #endregion
    }
}