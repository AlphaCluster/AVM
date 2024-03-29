﻿//  AVM - Appliction used to manage Web Videos, Video Files, and DVD's
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
using System.Data.SQLite;
using System.Text;

namespace AVM
{
    public class Database : IDisposable
    {
        private SQLiteConnection _database;
        private long parentOfCurrentGroup = 0;
        private long currentGroup = 0;

        #region Properties
        /// <summary>
        /// This is used to set or get the parent of the CurrentGroup.
        /// </summary>
        public long ParentGroup
        {
            get { return parentOfCurrentGroup; }
            set { parentOfCurrentGroup = value; }
        }
        
        /// <summary>
        /// This is used to set or get the Current group being used by
        /// the SQLite database. If it is set it will automatically
        /// change the ParentGroup.
        /// </summary>
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
                    using (SQLiteCommand command = new SQLiteCommand(_database))
                    {
                        command.CommandText = "SELECT parent_id FROM groups " +
                                              "WHERE group_id = @currentGroup;";
                        command.Parameters.AddWithValue("@currentGroup", currentGroup);
                        SQLiteDataReader reader = command.ExecuteReader();
                        reader.Read();
                        parentOfCurrentGroup = reader.GetInt16(0);
                        reader.Close();
                    }
                    trans.Rollback();
                    _database.Close();
                }
                else
                    parentOfCurrentGroup = 0;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// This creates a Database at the path sent via databasePath.
        /// If it doesn't exist then it will create it.
        /// </summary>
        /// <param name="databasePath">This is the path to the SQLite database</param>
        public Database(string databasePath)
        {
            if (!System.IO.File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
                SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
                connectionStringBuilder.DataSource = databasePath;
                _database = new SQLiteConnection(connectionStringBuilder.ConnectionString);
                _database.Open();
                SQLiteTransaction trans = _database.BeginTransaction();
                #region create groups table
                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = "CREATE TABLE groups (" +
                        "group_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "name TEXT," +
                        "parent_id INTEGER DEFAULT 0);";
                    command.ExecuteNonQuery();
                }
                #endregion
                #region create nodes table
                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = "CREATE TABLE nodes (" +
                        "node_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "name TEXT," +
                        "watched INTEGER DEFAULT 0," + // number of times watched
                        "type INTEGER," +    // 0 means nothing 1:youtube 2: hulu
                        "url TEXT," +        // original url for hulu or youtube video
                        "embedded TEXT," +    // embedded info for hulu or youtube
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
                    command.ExecuteNonQuery();
                }
                #endregion
                trans.Commit();
                _database.Close();
            }
            SQLiteConnectionStringBuilder dbBuilder = new SQLiteConnectionStringBuilder();
            dbBuilder.DataSource = databasePath;
            _database = new SQLiteConnection(dbBuilder.ConnectionString);
        }
        #endregion

        #region Group Functions
        /// <summary>
        /// Fills listBox with the group that is currently selected.
        /// </summary>
        /// <param name="listBox">ListBox to be filled with groups.</param>
        public void refreshGroups(System.Windows.Forms.ListBox listBox,
                                  ref List<AVM.Types.Group> groups)
        {
            groups.Clear();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            listBox.Items.Clear();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT name, group_id FROM groups " +
                                      "WHERE parent_id = @parentOfCurrentGroup" +
                                      " AND group_id > 0 " +
                                      "ORDER BY name;";
                command.Parameters.AddWithValue("@parentOfCurrentGroup", parentOfCurrentGroup);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                        reader.GetInt64(1),
                                                        parentOfCurrentGroup);
                            listBox.Items.Add(tempGroup);
                            groups.Add(tempGroup);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
        }

        /// <summary>
        /// Returns a List of all the groups inside the specified group.
        /// </summary>
        /// <param name="groupId">The id of the base group.</param>
        /// <returns></returns>
        public List<AVM.Types.Group> getGroups(int groupId)
        {
            List<AVM.Types.Group> list = new List<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT name, group_id FROM groups WHERE parent_id = @groupId AND group_id > 0;";
                command.Parameters.AddWithValue("@groupId", groupId);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                        reader.GetInt64(1),
                                                        parentOfCurrentGroup);
                            list.Add(tempGroup);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
            return list;
        }

        /// <summary>
        /// Returns a List of all the groups in the database.
        /// </summary>
        /// <returns></returns>
        public List<AVM.Types.Group> getAllGroups()
        {
            List<AVM.Types.Group> list = new List<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT * from groups;";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group newGroup = new AVM.Types.Group(reader.GetString(1),
                                                       reader.GetInt64(0),
                                                       reader.GetInt64(2));
                            list.Add(newGroup);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
            return list;
        }

        /// <summary>
        /// This will clear and populate a ComboBox with the Groups
        /// that are in the current ParentGroup.
        /// </summary>
        /// <param name="comboBox">ComboBox to be populated.</param>
        public void refreshComboBoxGroups(System.Windows.Forms.ComboBox comboBox)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            comboBox.Items.Clear();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT name, group_id FROM groups " +
                                      "WHERE parent_id = @parentOfCurrentGroup" +
                                      " AND group_id > 0;";
                command.Parameters.AddWithValue("@parentOfCurrentGroup", parentOfCurrentGroup);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                        reader.GetInt16(1),
                                                        parentOfCurrentGroup);
                            comboBox.Items.Add(tempGroup);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
        }

        /// <summary>
        /// Fills listBox with the groups found using the passed in query string.
        /// </summary>
        /// <param name="listBox">ListBox to be filled.</param>
        /// <param name="query">Serach string.</param>
        public void searchGroups(System.Windows.Forms.ListBox listBox,
                                 string query)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            listBox.Items.Clear();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT name, group_id FROM groups " +
                                      "WHERE name like @query" +
                                      " AND group_id > 0;";
                command.Parameters.AddWithValue("@query", "%" + query + "%");
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                        reader.GetInt16(1),
                                                        parentOfCurrentGroup);
                            listBox.Items.Add(tempGroup);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
        }

        /// <summary>
        /// Takes a List of groups and fills the database with them.
        /// </summary>
        /// <param name="groups">List of groups to fill database with.</param>
        public void fillGroups(List<AVM.Types.Group> groups)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            foreach (AVM.Types.Group group in groups)
            {
                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = "INSERT INTO groups (name, group_id, parent_id) " +
                                          "VALUES (@name, @group_id, @parent_id);";
                    command.Parameters.AddWithValue("@name", group.Name);
                    command.Parameters.AddWithValue("@group_id", group.Id);
                    command.Parameters.AddWithValue("@parent_id", group.ParentId);
                    command.ExecuteNonQuery();
                }
            }

            trans.Commit();
            _database.Close();
        }
        
        /// <summary>
        /// This will add a group using the name provided as its name
        /// and the current group as its parent.
        /// </summary>
        /// <param name="name">Name of the new group.</param>
        public void addGroup(string name)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "INSERT INTO groups" +
                                      "(name, parent_id) VALUES" +
                                      "(@name, @parent_id);";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@parent_id", parentOfCurrentGroup);
                command.ExecuteNonQuery();
            }
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Renames the group passed in from the database using the string that is passed in.
        /// </summary>
        /// <param name="group">The group that is being renamed.</param>
        /// <param name="newName">The new name for the group.</param>
        public void renameGroup(AVM.Types.Group group,
                                string newName)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "UPDATE groups " +
                                       "SET name = @name " +
                                       "WHERE group_id = @group_id;";
                command.Parameters.AddWithValue("@name", newName);
                command.Parameters.AddWithValue("@group_id", group.Id);
                command.ExecuteNonQuery();
            }
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// This function will recursively delete a whole group,
        /// its nodes and sub-groups. NOTE: This requires an open 
        /// transaction and needs to be committed or rolled back
        /// after.
        /// </summary>
        /// <param name="group">The group you want to remove.</param>
        /// <param name="trans">The current running transation.</param>
        private void recursiveRemoveGroup(AVM.Types.Group group,
                                          SQLiteTransaction trans)
        {
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "DELETE FROM groups " +
                                      "WHERE group_id = @group_id" +
                                      " AND parent_id = @parent_id" +
                                      " AND name = @name;";
                command.Parameters.AddWithValue("@group_id", group.Id);
                command.Parameters.AddWithValue("@parent_id", group.ParentId);
                command.Parameters.AddWithValue("@name", group.Name);
                command.ExecuteNonQuery();
            }
            removeNodesFromGroup(group.Id, trans);
            
            // find all sub-groups to run this against
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT name, group_id, parent_id FROM groups " +
                                      "WHERE parent_id = @parent_id" +
                                      " AND group_id > 0;";
                command.Parameters.AddWithValue("@parent_id", group.Id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AVM.Types.Group tempGroup = new AVM.Types.Group(reader.GetString(0),
                                                        reader.GetInt64(1),
                                                        reader.GetInt64(2));
                            recursiveRemoveGroup(tempGroup, trans);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove the group sent to the function from the database.
        /// </summary>
        /// <param name="group">The group to be removed.</param>
        public void removeGroup(AVM.Types.Group group)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            recursiveRemoveGroup(group, trans);

            //FINISH
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Returns the last group id which is also the largest.
        /// </summary>
        /// <returns></returns>
        public long getLastGroupId()
        {
            long last_id;

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT max(group_id) FROM groups;";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    // If there is nothing in groups yet use default.
                    if (reader.IsDBNull(0))
                        last_id = 0;
                    else
                        last_id = reader.GetInt64(0);
                }
            }
            
            _database.Close();
            return last_id;
        }

        /// <summary>
        /// This moves the current group back to its parent.
        /// </summary>
        public void gotoParent()
        {
            if (parentOfCurrentGroup != 0)
            {
                _database.Open();
                SQLiteTransaction trans = _database.BeginTransaction();
                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = "SELECT parent_id FROM groups " +
                                          "WHERE group_id = @parentOfCurrentGroup;";
                    command.Parameters.AddWithValue("@parentOfCurrentGroup", parentOfCurrentGroup);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        parentOfCurrentGroup = reader.GetInt16(0);
                    }
                }
                trans.Rollback();
                _database.Close();
            }
        }
        #endregion

        #region Node Functions
        /// <summary>
        /// This function returns a List of all the nodes in the database.
        /// </summary>
        /// <returns></returns>
        public List<AVM.Types.Node> getAllNodes()
        {
            List<AVM.Types.Node> list = new List<AVM.Types.Node>();

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM nodes;", _database, trans))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
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
                                    tempNode.embedded = null;
                                else
                                    tempNode.embedded = reader.GetString(5);
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
                            tempNode.ParentId = reader.GetInt64(15);
                            list.Add(tempNode);
                        }
                    }
                }
            }
            trans.Rollback();
            _database.Close();
            return list;
        }

        /// <summary>
        /// This will return a List of Nodes that are in the group
        /// specified by groupId. NOTE: This requires an open transaction
        /// and needs to be committed or rolled back after.
        /// </summary>
        /// <param name="groupId">The id of the group to select nodes from.</param>
        /// <param name="trans">The current transaction that is to be used.</param>
        /// <returns></returns>
        private List<AVM.Types.Node> selectNodesInGroup(long groupId,
                                                        SQLiteTransaction trans)
        {
            List<AVM.Types.Node> tempList = new List<AVM.Types.Node>();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT * FROM nodes WHERE parent_group_id = @groupId ORDER BY name, season_number, episode_number, episode_name;";
                command.Parameters.AddWithValue("@groupId", groupId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
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
                                    tempNode.embedded = null;
                                else
                                    tempNode.embedded = reader.GetString(5);
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
                }
            }
            return tempList;
        }

        /// <summary>
        /// This will remove all Nodes from the groupId sent in.
        /// NOTE: This requires an open transaction and needs to
        /// be committed or rolled back after.
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
        
        /// <summary>
        /// Fill the List passed with all the nodes in the current group.
        /// </summary>
        /// <param name="list">The List that is to be filled.</param>
        public void refreshNodes(ref System.Collections.Generic.List<AVM.Types.Node> list)
        {
            list.Clear();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            list = selectNodesInGroup(currentGroup, trans);
            trans.Rollback();
            _database.Close();
        }

        /// <summary>
        /// Adds all the nodes in the supplied List to the database.
        /// </summary>
        /// <param name="nodes">List of all the nodes to be added.</param>
        public void fillNodes(List<AVM.Types.Node> nodes)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            foreach (AVM.Types.Node node in nodes)
            {
                // Create strings to make the data adapter
                StringBuilder insertBuilder = new StringBuilder("INSERT INTO nodes (name, node_id, parent_group_id, type, url, embedded, comment");
                StringBuilder dataBuilder = new StringBuilder("@name, @node_id, @parent_group_id, @type, @url, @embedded, @comment");
                if (node.IsFile)
                {
                    insertBuilder.Append(", uri, video_encoding, audio_encoding, container");
                    dataBuilder.Append(", @uri, @video_encoding, @audio_encoding, @container");
                }
                if (node.IsEpisode)
                {
                    insertBuilder.Append(", episode_number, season_number, last_watched, episode_name");
                    dataBuilder.Append(", @episode_number, @season_number, @last_watched, @episode_name");
                }
                insertBuilder.Append(") VALUES (");
                insertBuilder.Append(dataBuilder.ToString());
                insertBuilder.Append(");");

                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = insertBuilder.ToString();

                    command.Parameters.AddWithValue("@name", node.Name);
                    command.Parameters.AddWithValue("@node_id", node.Id);
                    command.Parameters.AddWithValue("@parent_group_id", node.ParentId);
                    command.Parameters.AddWithValue("@type", node.UrlType);
                    command.Parameters.AddWithValue("@url", node.Url);
                    command.Parameters.AddWithValue("@embedded", node.embedded);
                    command.Parameters.AddWithValue("@comment", node.Comment);
                    if (node.IsFile)
                    {
                        command.Parameters.AddWithValue("@uri", node.File.Uri.ToString());
                        command.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                        command.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                        command.Parameters.AddWithValue("@container", node.File.Container);
                    }
                    if (node.IsEpisode)
                    {
                        command.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                        command.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                        command.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                        command.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
                    }

                    command.Transaction = trans;
                    command.ExecuteNonQuery();
                }
            }

            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Add the supplied node to the database.
        /// </summary>
        /// <param name="node">Node to added to the database.</param>
        public void addNode(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            
            // Create strings to make the data adapter.
            StringBuilder insertBuilder = new StringBuilder("INSERT INTO nodes (name, parent_group_id, type, url, embedded, comment");
            StringBuilder dataBuilder = new StringBuilder("@name, @parent_group_id, @type, @url, @embedded, @comment");
            if (node.IsFile)
            {
                insertBuilder.Append(", uri, video_encoding, audio_encoding, container");
                dataBuilder.Append(", @uri, @video_encoding, @audio_encoding, @container");
            }
            if (node.IsEpisode)
            {
                insertBuilder.Append(", episode_number, season_number, last_watched, episode_name");
                dataBuilder.Append(", @episode_number, @season_number, @last_watched, @episode_name");
            }
            insertBuilder.Append(") VALUES (");
            insertBuilder.Append(dataBuilder.ToString());
            insertBuilder.Append(");");

            // Create the adapter.
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = insertBuilder.ToString();

                // Fill the adapter with values.
                command.Parameters.AddWithValue("@name", node.Name);
                command.Parameters.AddWithValue("@parent_group_id", currentGroup);
                command.Parameters.AddWithValue("@type", node.UrlType);
                command.Parameters.AddWithValue("@url", node.Url);
                command.Parameters.AddWithValue("@embedded", node.embedded);
                command.Parameters.AddWithValue("@comment", node.Comment);
                // If file populate the SQLiteDataAdapter with file data.
                if (node.IsFile)
                {
                    command.Parameters.AddWithValue("@uri", node.File.Uri.ToString());
                    command.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                    command.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                    command.Parameters.AddWithValue("@container", node.File.Container);
                }
                // If episode populate the SQLiteDataAdapter with episode data.
                if (node.IsEpisode)
                {
                    command.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                    command.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                    command.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                    command.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
                }

                command.Transaction = trans;
                command.ExecuteNonQuery();
            }
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Update the supplied node with the new nodes information.
        /// </summary>
        /// <param name="old_node">Node to be updated.</param>
        /// <param name="node">Node with the new information.</param>
        public void updateNode(AVM.Types.Node old_node,
                               AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            // Fills node.
            StringBuilder updateString = new StringBuilder("UPDATE nodes SET name = @name, type = @type, url = @url, embedded = @embedded, comment = @comment");

            if (node.IsFile)
            {
                updateString.Append(", uri = @uri, video_encoding = @video_encoding, audio_encoding = @audio_encoding, container = @container");
            }
            // If episode populate the updateString with episode data.
            if (node.IsEpisode)
            {
                updateString.Append(", episode_number = @episode_number, season_number = @season_number, last_watched = @last_watched, episode_name = @episode_name");
            }

            updateString.Append(" WHERE node_id = @old_node_id;");

            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = updateString.ToString();

                command.Parameters.AddWithValue("@name", node.Name);
                command.Parameters.AddWithValue("@type", node.UrlType);
                command.Parameters.AddWithValue("@url", node.Url);
                command.Parameters.AddWithValue("@embedded", node.embedded);
                command.Parameters.AddWithValue("@comment", node.Comment);
                command.Parameters.AddWithValue("@old_node_id", old_node.Id);
                // If file populate the SQLiteDataAdapter with file data.
                if (node.IsFile)
                {
                    command.Parameters.AddWithValue("@uri", node.File.Uri);
                    command.Parameters.AddWithValue("@video_encoding", node.File.Video_Encoding);
                    command.Parameters.AddWithValue("@audio_encoding", node.File.Audio_Encoding);
                    command.Parameters.AddWithValue("@container", node.File.Container);
                }
                // If episode populate the SQLiteDataAdapter with episode data.
                if (node.IsEpisode)
                {
                    command.Parameters.AddWithValue("@episode_number", node.Episode.EpisodeNumber);
                    command.Parameters.AddWithValue("@season_number", node.Episode.SeasonNumber);
                    command.Parameters.AddWithValue("@last_watched", node.Episode.LastWatched);
                    command.Parameters.AddWithValue("@episode_name", node.Episode.EpisodeName);
                }
                command.Transaction = trans;
                command.ExecuteNonQuery();
            }

            trans.Commit();
            _database.Close();

            if (node.IsEpisode)
                if (node.Episode.LastWatched)
                    lastWatched(node);
        }

        /// <summary>
        /// Removes a single node from the database.
        /// NOTE: A connection must be open and pass in a transaction
        /// and after this is run it must be committed or rolled back.
        /// </summary>
        /// <param name="node">Node that is being removed.</param>
        /// <param name="trans">Transaction that is being used.</param>
        private void removeSingleNode(AVM.Types.Node node,
                                      SQLiteTransaction trans)
        {
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "DELETE FROM nodes WHERE node_id = @nodeId;";
                command.Parameters.AddWithValue("@nodeId", node.Id);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Remove the node that is supplied.
        /// </summary>
        /// <param name="node">Node to be removed.</param>
        public void removeNode(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            removeSingleNode(node, trans);
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Increment the times played for a node.
        /// </summary>
        /// <param name="node">Node to be incremented.</param>
        public void incrementPlayed(AVM.Types.Node node)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "UPDATE nodes SET watched = @nodeTimesPlayed WHERE node_id = @nodeId;";
                command.Parameters.AddWithValue("@nodeTimesPlayed", node.TimesPlayed);
                command.Parameters.AddWithValue("@nodeId", node.Id);
                command.ExecuteNonQuery();
            }
            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// Search the database using the query and return it in the List.
        /// NOTE: This can search only the group or in all groups.
        /// </summary>
        /// <param name="nodes">The list of nodes to be filled.</param>
        /// <param name="query">String that is used as the query.</param>
        /// <param name="searchAll">True: Search all groups.
        ///                         False: Search just current group.</param>
        public void searchNodes(ref List<AVM.Types.Node> nodes,
                                string query,
                                bool searchAll)
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            nodes.Clear();
            StringBuilder commandString = new StringBuilder("SELECT * FROM nodes WHERE (name LIKE @query OR episode_name LIKE @query)");
            // If no parent then parent should be sent as -1
            if (searchAll)
                commandString.Append(";");
            else
                commandString.Append("AND parent_group_id = @parent_group_id;");

            using (SQLiteCommand command = new SQLiteCommand(commandString.ToString(), _database, trans))
            {
                command.Parameters.AddWithValue("@query", "%" + query + "%");
                command.Parameters.AddWithValue("@parent_group_id", currentGroup);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
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
                                    tempNode.embedded = null;
                                else
                                    tempNode.embedded = reader.GetString(5);
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
                    }
                }
            }
            trans.Rollback();
            _database.Close();
        }
        
        /// <summary>
        /// Gets the last node id which is also the largest node id.
        /// </summary>
        /// <returns></returns>
        public long getLastNodeId()
        {
            long last_id;

            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "SELECT max(node_id) FROM nodes;";
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    if (reader.IsDBNull(0))
                        last_id = 0;
                    else
                        last_id = reader.GetInt64(0);
                }
            }
            
            _database.Close();
            return last_id;
        }

        /// <summary>
        /// Sets the node passed in as the lastWatched.
        /// </summary>
        /// <param name="node">Node that just started playing.</param>
        public void lastWatched(AVM.Types.Node node)
        {
            // Make it so no Nodes in the same group are marked as last_watched
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "UPDATE nodes SET last_watched = 0 WHERE name = @name;";
                command.Parameters.AddWithValue("@name", node.Name);
                command.ExecuteNonQuery();
            }

            // Mark current node as last_watched
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "UPDATE nodes SET last_watched = 1 WHERE node_id = @node_id;";
                command.Parameters.AddWithValue("@node_id", node.Id);
                command.ExecuteNonQuery();
            }    

            trans.Commit();
            _database.Close();
        }
        #endregion

        #region Misc Functions
        
        /// <summary>
        /// This function empties the database.
        /// </summary>
        public void Clear()
        {
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();
            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "DELETE FROM nodes;";
                command.ExecuteNonQuery();
            }

            using (SQLiteCommand command = new SQLiteCommand(_database))
            {
                command.CommandText = "DELETE FROM groups;";
                command.ExecuteNonQuery();
            }

            trans.Commit();
            _database.Close();
        }

        /// <summary>
        /// This returns a breadcrumb list of all the groups going
        /// back to the root group from the group whose id is supplied.
        /// </summary>
        /// <param name="starting_id">Group to make breadcrumbs from.</param>
        /// <returns></returns>
        public string findBreadcrumbs(long startingId)
        {
            string crums = "";
            Stack<AVM.Types.Group> groups = new Stack<AVM.Types.Group>();
            _database.Open();
            SQLiteTransaction trans = _database.BeginTransaction();

            long groupId = startingId;
            while (groupId > 0)
            {
                using (SQLiteCommand command = new SQLiteCommand(_database))
                {
                    command.CommandText = "SELECT * FROM groups WHERE group_id = @groupId;";
                    command.Parameters.AddWithValue("@groupId", groupId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        AVM.Types.Group group = new AVM.Types.Group();
                        group.Id = reader.GetInt64(0);
                        group.Name = reader.GetString(1);
                        group.ParentId = reader.GetInt64(2);
                        groupId = group.ParentId;
                        groups.Push(group);
                    }
                }
            }

            while (groups.Count > 0)
            {
                AVM.Types.Group group = groups.Pop();
                crums += "> " + group.Name + " ";
            }

            _database.Close();
            return crums;
        }
        
        /// <summary>
        /// This closes the database.
        /// </summary>
        public void Kill()
        {
            _database.Close();
        }
        #endregion

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
