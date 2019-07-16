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
using System.Data.SQLite;

namespace AVM
{
    public class Database
    {
        private SQLiteConnection _database;
        private string _databasePath;
        private long parentOfCurrentGroup = 0;
        private long currentGroup = 0;

        public long ParentGroup
        {
            get { return parentOfCurrentGroup; }
            set { parentOfCurrentGroup = value; }
        }

        public long CurrentGroup
        {
            get { return currentGroup; }
            set 
            // Figure out new ParentGroup
            {
                currentGroup = value;
                if (currentGroup != 0)
                {
                    _database.Open();
                    SQLiteTransaction trans = _database.BeginTransaction();
                    SQLiteCommand command = new SQLiteCommand(
                                        "SELECT parent_id FROM groups " +
                                        "WHERE group_id = " + currentGroup + ";", _database, trans);
                    SQLiteDataReader reader = command.ExecuteReader();
                    reader.Read();
                    parentOfCurrentGroup = reader.GetInt16(0);
                    reader.Close();
                    trans.Rollback();
                    _database.Close();
                }
                else
                    parentOfCurrentGroup = 0;
            }
        }

		public Database(string databasePath)
        {
            if (!System.IO.File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
                SQLiteConnectionStringBuilder temp = new SQLiteConnectionStringBuilder();
                temp.DataSource = databasePath;
                _database = new SQLiteConnection(temp.ConnectionString);
                _database.Open();
                SQLiteTransaction trans = _database.BeginTransaction();
                #region create groups table
                SQLiteCommand com = new SQLiteCommand();
                com.Connection = _database;
                com.Transaction = trans;
                com.CommandText = "CREATE TABLE groups (" +
                    "group_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "name TEXT," +
                    "parent_id INTEGER DEFAULT 0);";
                com.ExecuteNonQuery();
                #endregion
                #region create nodes table
                com = new SQLiteCommand();
                com.Connection = _database;
                com.Transaction = trans;
                com.CommandText = "CREATE TABLE nodes (" +
                    "node_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "name TEXT," +
                    "watched INTEGER DEFAULT 0," + // number of times watched
                    "type INTEGER," +    // 0 means nothing 1:youtube 2: hulu
                    "url TEXT," +        // original url for hulu or youtube video
                    "embeded TEXT," +    // embeded info for hulu or youtube
                    "comment TEXT," +    // stores the comment section for DVDs
                    "episode_number INTEGER," +
                    "season_number INTEGER," +
                    "last_watched INTEGER," + //true 1 false 0
                    "episode_name TEXT," +
                    "uri TEXT," + // if null not a file
                    "video_encoding TEXT," +
                    "audio_encoding TEXT," +
                    "container TEXT," +
                    "parent_group_id INTEGER NOT NULL);";
                com.ExecuteNonQuery();
                #endregion
                trans.Commit();
                _database.Close();
            }
            SQLiteConnectionStringBuilder dbBuilder = new SQLiteConnectionStringBuilder();
            dbBuilder.DataSource = databasePath;
            _database = new SQLiteConnection(dbBuilder.ConnectionString);
            _databasePath = databasePath;
		}

        // GROUP DATABASE FUNCTIONS
        /// <summary>
        /// Fills listBox with the group that is currently selected.
        /// </summary>
        /// <param name="listBox"></param>
        public void refreshGroups(System.Windows.Forms.ListBox listBox)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            listBox.Items.Clear();
            SQLiteCommand command = new SQLiteCommand(
                            "SELECT name, group_id FROM groups " +
                            "WHERE parent_id = " + parentOfCurrentGroup +
                            " AND group_id > 0;",
                            _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                reader.GetInt64(1),
                                                parentOfCurrentGroup);
                    listBox.Items.Add(tempGroup);
                }
            trans.Rollback();
            _database.Close();
        }

        public List<AVM.Types.Group> getGroups(int groupID)
        {
            List<AVM.Types.Group> list = new List<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                            "SELECT name, group_id FROM groups " +
                            "WHERE parent_id = " + groupID +
                            " AND group_id > 0;",
                            _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                reader.GetInt64(1),
                                                parentOfCurrentGroup);
                    list.Add(tempGroup);
                }
            trans.Rollback();
            _database.Close();
            return list;
        }

        public List<AVM.Types.Group> getAllGroups()
        {
            List<AVM.Types.Group> list = new List<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand("SELECT * from groups;", _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group newGroup = new AVM.Types.Group(reader.GetString(1),
                                               reader.GetInt64(0),
                                               reader.GetInt64(2));
                    list.Add(newGroup);
                }
            trans.Rollback();
            _database.Close();
            return list;
        }

        /// <summary>
        /// This will clear and populate a ComboBox with the Groups
        /// that are in the current ParentGroup.
        /// </summary>
        /// <param name="comboBox"></param>
        public void refreshComboBoxGroups(System.Windows.Forms.ComboBox comboBox)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            comboBox.Items.Clear();
            SQLiteCommand command = new SQLiteCommand(
                            "SELECT name, group_id FROM groups " +
                            "WHERE parent_id = " + parentOfCurrentGroup +
                            " AND group_id > 0;",
                            _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                reader.GetInt16(1),
                                                parentOfCurrentGroup);
                    comboBox.Items.Add(tempGroup);
                }
            trans.Rollback();
            _database.Close();
        }

        /// <summary>
        /// Fills listBox with the groups found using the passed in query string.
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="query"></param>
        public void searchGroups(System.Windows.Forms.ListBox listBox,
                                 string query)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            listBox.Items.Clear();
            SQLiteCommand command = new SQLiteCommand(
                            "SELECT name, group_id FROM groups " +
                            "WHERE name like '%" + query + "%'" +
                            " AND group_id > 0;",
                            _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                reader.GetInt16(1),
                                                parentOfCurrentGroup);
                    listBox.Items.Add(tempGroup);
                }
            trans.Rollback();
            _database.Close();
        }

        public void fillGroups(List<AVM.Types.Group> groups)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            foreach (AVM.Types.Group group in groups)
            {
                // Create the adapter
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                adapter.InsertCommand = new SQLiteCommand(
                    "INSERT INTO groups (name, group_id, parent_id) " +
                    "VALUES (@name, @group_id, @parent_id);",
                    trans.Connection);

                // Fill the adapter with values
                adapter.InsertCommand.Parameters.AddWithValue("@name", group.Name);
                adapter.InsertCommand.Parameters.AddWithValue("@group_id", group.Id);
                adapter.InsertCommand.Parameters.AddWithValue("@parent_id", group.ParentId);

                adapter.InsertCommand.Transaction = trans;
                adapter.InsertCommand.ExecuteNonQuery();

            }

            trans.Commit();
            _database.Close();
        }
        
        /// <summary>
        /// This will add a group using the name provovided as its name
        /// and the current group as its parent.
        /// </summary>
        /// <param name="name"></param>
        public void addGroup(string name)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                            "INSERT INTO groups" +
                            "(name, parent_id) VALUES (" +
                            "'" + name + "'," + parentOfCurrentGroup + ");",
                            _database, trans);
            command.ExecuteNonQuery();
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Renames the group passed in from the database using the string that is passed in.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="newName"></param>
        public void renameGroup(AVM.Types.Group group,
                                string newName)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                            "UPDATE groups " +
                            "SET name = '" + newName + "'" +
                            "WHERE group_id = " + group.Id + ";",
                            _database, trans);
            command.ExecuteNonQuery();
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// This function will recursivly delete a whole group,
        /// its nodes and sub-groups.NOTE: This requires an open 
        /// transaction and needs to be commited or rolledback
        /// after.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="trans"></param>
        private void recursiveRemoveGroup(AVM.Types.Group group,
                                          SQLiteTransaction trans)
        {
            SQLiteCommand command = new SQLiteCommand(
                            "DELETE FROM groups " +
                            "WHERE group_id = " + group.Id +
                            " AND parent_id = " + group.ParentId +
                            " AND name = '" + group.Name + "';",
                            _database, trans);
            command.ExecuteNonQuery();
            removeNodesFromGroup(group.Id, trans);
            // find all sub-groups to run this against
            command = new SQLiteCommand(
                            "SELECT name, group_id FROM groups " +
                            "WHERE parent_id = " + group.Id +
                            " AND group_id > 0;",
                            _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            List<AVM.Types.Group> groupList = new List<AVM.Types.Group>();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                reader.GetInt64(1),
                                                parentOfCurrentGroup);
                    recursiveRemoveGroup(tempGroup, trans);
                }
            reader.Close();
        }

        public List<AVM.Types.Node> getAllNodes()
        {
            List<AVM.Types.Node> list = new List<AVM.Types.Node>();

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM nodes;", _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Node tempNode = new AVM.Types.Node();
                    tempNode.Id = reader.GetInt64(0);
                    tempNode.Name = reader.GetString(1);
                    tempNode.TimesPlayed = reader.GetInt32(2);
                    tempNode.UrlType = reader.GetInt16(3);
                    if (tempNode.UrlType > 0)
                    {
                        tempNode.Url = reader.GetString(4);
                        if (reader.IsDBNull(5))
                            tempNode.Embeded = null;
                        else
                            tempNode.Embeded = reader.GetString(5);
                    }

                    if (!reader.IsDBNull(6))
                        tempNode.Comment = reader.GetString(6);

                    if (!reader.IsDBNull(7))
                        if ((reader.GetInt16(8) > -1) ||
                            (reader.GetInt16(9) > -1) ||
                            (!reader.IsDBNull(7)))
                        {
                            tempNode.Episode = new AVM.Types.EpisodeInfo();
                            tempNode.Episode.EpisodeNumber = reader.GetInt16(7);
                            tempNode.Episode.SeasonNumber = reader.GetInt16(8);
                            tempNode.Episode.LastWatched = (reader.GetInt16(9) != 0);
                            if (!reader.IsDBNull(10))
                                tempNode.Episode.EpisodeName = reader.GetString(10);
                        }
                    if (!reader.IsDBNull(11))
                    {
                        tempNode.File = new AVM.Types.FileData();
                        tempNode.File.Uri = new Uri(reader.GetString(11));
                        if (!reader.IsDBNull(12))
                            tempNode.File.Video_Encoding = reader.GetString(12);
                        if (!reader.IsDBNull(13))
                            tempNode.File.Audio_Encoding = reader.GetString(13);
                        if (!reader.IsDBNull(14))
                            tempNode.File.Container = reader.GetString(14);
                    }
                    list.Add(tempNode);
                }
            trans.Rollback();
            _database.Close();
            return list;
        }

        /// <summary>
        /// This will return a List of Nodes that are in the group
        /// specified by groupId. NOTE: This requires an open transaction
        /// and needs to be commited or rolledback after.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private List<AVM.Types.Node> selectNodesInGroup(long groupId,
                                                            SQLiteTransaction trans)
        {
            List<AVM.Types.Node> tempList = new List<AVM.Types.Node>();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM nodes " +
                   "WHERE parent_group_id = " + groupId +
                   " ORDER BY name, season_number, episode_number, episode_name;",
                   _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Node tempNode = new AVM.Types.Node();
                    tempNode.Id = reader.GetInt64(0);
                    tempNode.Name = reader.GetString(1);
                    tempNode.TimesPlayed = reader.GetInt32(2);
                    tempNode.UrlType = reader.GetInt16(3);
                    if (tempNode.UrlType > 0)
                    {
                        tempNode.Url = reader.GetString(4);
                        if (reader.IsDBNull(5))
                            tempNode.Embeded = null;
                        else
                            tempNode.Embeded = reader.GetString(5);
                    }

                    if (!reader.IsDBNull(6))
                        tempNode.Comment = reader.GetString(6);

                    if (!reader.IsDBNull(7))
                        if ((reader.GetInt16(8) > -1) ||
                            (reader.GetInt16(9) > -1) ||
                            (!reader.IsDBNull(7)))
                        {
                            tempNode.Episode = new AVM.Types.EpisodeInfo();
                            tempNode.Episode.EpisodeNumber = reader.GetInt16(7);
                            tempNode.Episode.SeasonNumber = reader.GetInt16(8);
                            tempNode.Episode.LastWatched = (reader.GetInt16(9) != 0);
                            if (!reader.IsDBNull(10))
                                tempNode.Episode.EpisodeName = reader.GetString(10);
                        }

                    if (!reader.IsDBNull(11))
                    {
                        tempNode.File = new AVM.Types.FileData();
                        tempNode.File.Uri = new Uri(reader.GetString(11));
                        if (!reader.IsDBNull(12))
                            tempNode.File.Video_Encoding = reader.GetString(12);
                        if (!reader.IsDBNull(13))
                            tempNode.File.Audio_Encoding = reader.GetString(13);
                        if (!reader.IsDBNull(14))
                            tempNode.File.Container = reader.GetString(14);
                    }
                    tempList.Add(tempNode);
                }
            return tempList;
        }

        /// <summary>
        /// This will remove all Nodes from the groupId sent in.
        /// NOTE: This requires an open transaction and needs to
        /// be commited or rolledback after.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="trans"></param>
        private void removeNodesFromGroup(long groupId,
                                          SQLiteTransaction trans)
        {
            List<AVM.Types.Node> nodeList = selectNodesInGroup(groupId, trans);

            foreach (AVM.Types.Node node in nodeList)
                removeSingleNode(node, trans);
        }

        public void removeGroup(AVM.Types.Group group)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                            "DELETE FROM groups " +
                            "WHERE group_id = " + group.Id +
                            " AND parent_id = " + group.ParentId +
                            " AND name = '" + group.Name + "';",
                            _database, trans);
            command.ExecuteNonQuery();
            
            // Delete sub-nodes
            command = new SQLiteCommand(
                            "SELECT FROM nodes " +
                            "WHERE parent_group_id = " + group.Id + ";",
                            _database, trans);
            //FINISH
            trans.Commit();
            _database.Close();
        }

        public void gotoParent()
        {
            if (parentOfCurrentGroup != 0)
            {
                _database.Open();
                SQLiteTransaction trans = _database.BeginTransaction();
                SQLiteCommand command = new SQLiteCommand(
                                    "SELECT parent_id FROM groups " +
                                    "WHERE group_id = " + parentOfCurrentGroup + ";", _database, trans);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                parentOfCurrentGroup = reader.GetInt16(0);
                reader.Close();
                trans.Rollback();
                _database.Close();
            }
        }

        public void refreshNodes(ref System.Collections.Generic.List<AVM.Types.Node> list)
        {
            list.Clear();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            list = selectNodesInGroup(currentGroup, trans);
            trans.Rollback();
            _database.Close();
        }

        public void fillNodes(List<AVM.Types.Node> nodes)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            foreach (AVM.Types.Node node in nodes)
            {
                // Create strings to make the data adapter
                string identifiers = "name, node_id, parent_group_id, type, url, embeded, comment";
                string data = "@name, @node_id, @parent_group_id, @type, @url, @embeded, @comment";
                if (node.IsFile)
                {
                    identifiers += ", uri, video_encoding, audio_encoding, container";
                    data += ", @uri, @video_encoding, @audio_encoding, @container";
                }
                if (node.IsEpisode)
                {
                    identifiers += ", episode_number, season_number, last_watched, episode_name";
                    data += ", @episode_number, @season_number, @last_watched, @episode_name";
                }

                // Create the adapter
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                adapter.InsertCommand = new SQLiteCommand(
                    "INSERT INTO nodes (" + identifiers + ") " +
                    "VALUES (" + data + ");",
                    trans.Connection);

                // Fill the adapter with values
                adapter.InsertCommand.Parameters.AddWithValue("@name", node.Name);
                adapter.InsertCommand.Parameters.AddWithValue("@node_id", node.Id);
                adapter.InsertCommand.Parameters.AddWithValue("@parent_group_id", node.ParentId);
                adapter.InsertCommand.Parameters.AddWithValue("@type", node.UrlType);
                adapter.InsertCommand.Parameters.AddWithValue("@url", node.Url);
                adapter.InsertCommand.Parameters.AddWithValue("@embeded", node.Embeded);
                adapter.InsertCommand.Parameters.AddWithValue("@comment", node.Comment);
                if (node.IsFile)
                {
                    adapter.InsertCommand.Parameters.AddWithValue("@uri", node.File.Uri.ToString());
                    adapter.InsertCommand.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                    adapter.InsertCommand.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                    adapter.InsertCommand.Parameters.AddWithValue("@container", node.File.Container);
                }
                if (node.IsEpisode)
                {
                    adapter.InsertCommand.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                    adapter.InsertCommand.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                    adapter.InsertCommand.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                    adapter.InsertCommand.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
                }

                adapter.InsertCommand.Transaction = trans;
                adapter.InsertCommand.ExecuteNonQuery();
            
            }

            trans.Commit();
            _database.Close();
        }

        public void addNode(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            
            // Create strings to make the data adapter
            string identifiers = "name, parent_group_id, type, url, embeded, comment";
            string data = "@name, @parent_group_id, @type, @url, @embeded, @comment";
            if (node.IsFile)
            {
                identifiers += ", uri, video_encoding, audio_encoding, container";
                data += ", @uri, @video_encoding, @audio_encoding, @container";
            }
            if (node.IsEpisode)
            {
                identifiers += ", episode_number, season_number, last_watched, episode_name";
                data += ", @episode_number, @season_number, @last_watched, @episode_name";
            }

            // Create the adapter
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.InsertCommand = new SQLiteCommand(
                "INSERT INTO nodes (" + identifiers + ") " +
                "VALUES (" + data + ");",
                trans.Connection);

            // Fill the adapter with values
            adapter.InsertCommand.Parameters.AddWithValue("@name", node.Name);
            adapter.InsertCommand.Parameters.AddWithValue("@parent_group_id", currentGroup);
            adapter.InsertCommand.Parameters.AddWithValue("@type", node.UrlType);
            adapter.InsertCommand.Parameters.AddWithValue("@url", node.Url);
            adapter.InsertCommand.Parameters.AddWithValue("@embeded", node.Embeded);
            adapter.InsertCommand.Parameters.AddWithValue("@comment", node.Comment);
            if (node.IsFile)
            {
                adapter.InsertCommand.Parameters.AddWithValue("@uri", node.File.Uri.ToString());
                adapter.InsertCommand.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                adapter.InsertCommand.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                adapter.InsertCommand.Parameters.AddWithValue("@container", node.File.Container);
            }
            if (node.IsEpisode)
            {
                adapter.InsertCommand.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                adapter.InsertCommand.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                adapter.InsertCommand.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                adapter.InsertCommand.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
            }

            adapter.InsertCommand.Transaction = trans;
            adapter.InsertCommand.ExecuteNonQuery();
            
            trans.Commit();
            _database.Close();
        }

        public void updateNode(AVM.Types.Node old_node, AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            // fills node
            string updateString = "name = @name" +
                                  ", type = @type" +
                                  ", url = @url" +
                                  ", embeded = @embeded" +
                                  ", comment = @comment";
            if (node.IsFile)
            {
                updateString += ", uri = @uri" +
                                ", video_encoding = @video_encoding" +
                                ", audio_encoding = @audio_encoding" +
                                ", container = @container";
            }
            // If episode populate the updateString with episode data
            if (node.IsEpisode)
            {
                updateString += ", episode_number = @episode_number" +
                                ", season_number = @season_number" +
                                ", last_watched = @last_watched" +
                                ", episode_name = @episode_name";
            }

            // Create the adapter
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.UpdateCommand = new SQLiteCommand(
                "UPDATE nodes SET " + updateString +
                " WHERE node_id = " + old_node.Id + ";",
                trans.Connection);

            adapter.UpdateCommand.Parameters.AddWithValue("@name", node.Name);
            adapter.UpdateCommand.Parameters.AddWithValue("@type", node.UrlType);
            adapter.UpdateCommand.Parameters.AddWithValue("@url", node.Url);
            adapter.UpdateCommand.Parameters.AddWithValue("@embeded", node.Embeded);
            adapter.UpdateCommand.Parameters.AddWithValue("@comment", node.Comment);
            // If file populate the SQLiteDataAdapter with file data
            if (node.IsFile)
            {
                adapter.UpdateCommand.Parameters.AddWithValue("@uri", node.File.Uri);
                adapter.UpdateCommand.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                adapter.UpdateCommand.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                adapter.UpdateCommand.Parameters.AddWithValue("@container", node.File.Container);
            }
            // If episode populate the SQLiteDataAdapter with episode data
            if (node.IsEpisode)
            {
                adapter.UpdateCommand.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                adapter.UpdateCommand.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                adapter.UpdateCommand.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                adapter.UpdateCommand.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
            }
            adapter.UpdateCommand.Transaction = trans;
            adapter.UpdateCommand.ExecuteNonQuery();
            trans.Commit();
            _database.Close();
        }

        private void removeSingleNode(AVM.Types.Node node,
                                      SQLiteTransaction trans)
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM nodes WHERE node_id = " + node.Id + ";",
                   _database, trans);
            command.ExecuteNonQuery();
        }

        public void removeNode(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            removeSingleNode(node, trans);
            trans.Commit();
            _database.Close();
        }

        public void incrementPlayed(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                    "UPDATE nodes SET watched = " + node.TimesPlayed +
                    " WHERE node_id = " + node.Id + ";",
                    _database, trans);
            command.ExecuteNonQuery();
            trans.Commit();
            _database.Close();
        }

        public void searchNodes(ref List<AVM.Types.Node> nodes,
                                string query,
                                bool searchAll)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            nodes.Clear();
            string commandString = "SELECT * FROM nodes " +
                            "WHERE (name LIKE '%" + query + "%'" +
                            " OR episode_name LIKE '%" + query + "%')";
            // If no parent then parent should be sent as -1
            if (searchAll)
                commandString += ";";
            else
                commandString += " AND parent_group_id = " + currentGroup + ";";
            
            SQLiteCommand command = new SQLiteCommand(commandString, _database, trans);
            
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    AVM.Types.Node tempNode = new AVM.Types.Node();
                    tempNode.Id = reader.GetInt64(0);
                    tempNode.Name = reader.GetString(1);
                    tempNode.TimesPlayed = reader.GetInt32(2);
                    tempNode.UrlType = reader.GetInt16(3);
                    if (tempNode.UrlType > 0)
                    {
                        tempNode.Url = reader.GetString(4);
                        if (reader.IsDBNull(5))
                            tempNode.Embeded = null;
                        else
                            tempNode.Embeded = reader.GetString(5);
                    }

                    if (!reader.IsDBNull(6))
                        tempNode.Comment = reader.GetString(6);

                    if (!reader.IsDBNull(7))
                        if ((reader.GetInt16(8) > -1) ||
                            (reader.GetInt16(9) > -1) ||
                            (!reader.IsDBNull(7)))
                        {
                            tempNode.Episode = new AVM.Types.EpisodeInfo();
                            tempNode.Episode.EpisodeNumber = reader.GetInt16(7);
                            tempNode.Episode.SeasonNumber = reader.GetInt16(8);
                            tempNode.Episode.LastWatched = (reader.GetInt16(9) != 0);
                            if (!reader.IsDBNull(10))
                                tempNode.Episode.EpisodeName = reader.GetString(10);
                        }

                    if (!reader.IsDBNull(11))
                    {
                        tempNode.File = new AVM.Types.FileData();
                        tempNode.File.Uri = new Uri(reader.GetString(11));
                        if (!reader.IsDBNull(12))
                            tempNode.File.Video_Encoding = reader.GetString(12);
                        if (!reader.IsDBNull(13))
                            tempNode.File.Audio_Encoding = reader.GetString(13);
                        if (!reader.IsDBNull(14))
                            tempNode.File.Container = reader.GetString(14);
                    }
                    nodes.Add(tempNode);
                }
            trans.Rollback();
            _database.Close();
        }

        public void Clear()
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                    "DELETE FROM nodes;",
                    _database, trans);
            command.ExecuteNonQuery();
            command = new SQLiteCommand(
                    "DELETE FROM groups;",
                    _database, trans);
            command.ExecuteNonQuery();
            trans.Commit();
            _database.Close();
        }

        public string FindBreadcrumbs(long starting_id)
        {
            string crums = "";
            Stack<AVM.Types.Group> groups = new Stack<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            long group_id = starting_id;
            while (group_id > 0)
            {
                SQLiteCommand command = new SQLiteCommand(
                    "SELECT * FROM groups WHERE group_id = " + group_id.ToString() + ";",
                    _database, trans);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                AVM.Types.Group group = new AVM.Types.Group();
                group.Id = reader.GetInt64(0);
                group.Name = reader.GetString(1);
                group.ParentId = reader.GetInt64(2);
                group_id = group.ParentId;
                groups.Push(group);
            }

            while (groups.Count > 0)
            {
                AVM.Types.Group group = groups.Pop();
                crums += "> " + group.Name + " ";
            }

            _database.Close();
            return crums;
        }

        public long getLastNodeId()
        {
            long last_id;

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT max(node_id) FROM nodes;",
                _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            if (reader.IsDBNull(0))
                last_id = 0;
            else
                last_id = reader.GetInt64(0);
            _database.Close();

            return last_id;
        }

        public long getLastGroupId()
        {
            long last_id;

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(
                "SELECT max(group_id) FROM groups;",
                _database, trans);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            last_id = reader.GetInt64(0);
            _database.Close();

            return last_id;
        }

        public void Kill()
        {
            _database.Close();
        }
    }
}
