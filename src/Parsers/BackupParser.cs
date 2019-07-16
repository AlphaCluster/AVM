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
using System.IO;
using System.Xml;

namespace AVM.Parsers
{
    class BackupParser
    {
        private Database _db;

        public BackupParser(Database db)
        {
            _db = db;
        }

        #region Writing Backup
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

        private void writeNodes(XmlTextWriter xmlWriter)
        {
            List<AVM.Types.Node> list = _db.getAllNodes();

            foreach (AVM.Types.Node node in list)
                writeNode(node, xmlWriter);
        }

        private void writeNode(AVM.Types.Node node, XmlTextWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("node");
            xmlWriter.WriteAttributeString("id", node.Id.ToString());
            xmlWriter.WriteAttributeString("name", node.Name);
            xmlWriter.WriteStartElement("url");
            xmlWriter.WriteAttributeString("type", node.UrlType.ToString());
            xmlWriter.WriteString(node.Url);
            xmlWriter.WriteEndElement();
            // this should be able to be easily remade by choping off the last 
            // part of the url and placeing it in http://www.youtube.com/v/HERE&hl=en&fs=1
            // this should be looked into to save space since its taking up over 50% of a \
            // node in xml currently problem is hulu
            xmlWriter.WriteElementString("embeded", node.Embeded);
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

        private void writeGroups(XmlTextWriter xmlWriter)
        {
            List<AVM.Types.Group> groups = _db.getAllGroups();

            foreach (AVM.Types.Group group in groups)
                writeGroup(group, xmlWriter);
        }

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
        public string ReadXmlBackup(string file)
        {
            _db.Clear();

            XmlTextReader xmlReader = new XmlTextReader(file);
            string debug = "";
            xmlReader.Read();
            xmlReader.ReadStartElement("backup");
            xmlReader.ReadStartElement("nodes");
            readNodes(xmlReader, debug);
            xmlReader.ReadEndElement();
            xmlReader.ReadStartElement("groups");
            readGroups(xmlReader);
            xmlReader.ReadEndElement();
            xmlReader.ReadEndElement();
            xmlReader.Close();
            return debug;
        }

        private void readNodes(XmlTextReader xmlReader, string debug)
        {
            List<AVM.Types.Node> nodes = new List<AVM.Types.Node>();

            while (xmlReader.IsStartElement("node"))
            {
                nodes.Add(readNode(xmlReader));
            }

            _db.fillNodes(nodes);
        }

        private AVM.Types.Node readNode(XmlTextReader xmlReader)
        {
            AVM.Types.Node node = new AVM.Types.Node();

            node.Id = Int64.Parse(xmlReader[0]);
            node.Name = xmlReader[1];

            xmlReader.ReadToDescendant("url");
            
            node.UrlType = Int16.Parse(xmlReader[0]);
            xmlReader.ReadStartElement("url");
            if (node.UrlType != 0)
            {
                node.Url = xmlReader.ReadString();
                xmlReader.ReadEndElement();
            }

            node.Embeded = xmlReader.ReadElementString("embeded");
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

        private void readGroups(XmlTextReader xmlReader)
        {
            List<AVM.Types.Group> groups = new List<AVM.Types.Group>();

            while (xmlReader.IsStartElement("group"))
            {
                groups.Add(readGroup(xmlReader));
            }

            _db.fillGroups(groups);
        }

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