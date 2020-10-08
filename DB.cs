using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUESTionBot
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=questionbase1");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public static void UpdateTeamNote(Team team)
        {
            try
            {
                DB database = new DB();

                MySqlCommand command = new MySqlCommand("UPDATE `teams` SET `currentTask`=@cT, `currentQuestion`=@cQ, `points`=@pO, `hintsUsed`=@hU " +
                    "WHERE `teamId`=@tId", database.GetConnection());
                command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;
                //command.Parameters.Add("@tK", MySqlDbType.VarChar).Value = e.Message.Text.Trim().ToLower();
                //command.Parameters.Add("@lCh", MySqlDbType.Double).Value = team.LinkedChat.Id;
                //command.Parameters.Add("@sAt", MySqlDbType.DateTime).Value = teamList[e.Message.Chat.Id].QuestStartedAt;
                command.Parameters.Add("@cT", MySqlDbType.Int64).Value = team.CurrentStation;
                command.Parameters.Add("@cQ", MySqlDbType.Int64).Value = team.CurrentQuestion;
                command.Parameters.Add("@pO", MySqlDbType.Int64).Value = team.Points;
                command.Parameters.Add("@hU", MySqlDbType.Int64).Value = team.HintsUsed;

                if (team.QuestFinishedAt != null)
                {
                    MySqlCommand command2 = new MySqlCommand("UPDATE `teams` SET `finishedAt`=@fAt " +
                    "WHERE `teamId`=@tId", database.GetConnection());
                    command2.Parameters.Add("@fAt", MySqlDbType.DateTime).Value = team.QuestFinishedAt;
                    database.OpenConnection();
                    command2.ExecuteNonQuery();
                    database.CloseConnection();
                }

                database.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    database.CloseConnection();
                    return;
                }

                throw new Exception();
            }
            catch
            {
                return;
            }
            }

        public static bool TeamAdd(Team team, string teamkey)
        {
            DB database = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `teams`(`teamId`, `teamKey`, `linkedChatId`, `startedAt`" +
                ", `currentTask`, `currentQuestion`, `points`, `hintsUsed`) VALUES (@tId, @tK, @lCh, @sAt, @cT, @cQ, @pO, @hU)", database.GetConnection());
            command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;
            command.Parameters.Add("@tK", MySqlDbType.VarChar).Value = teamkey;
            command.Parameters.Add("@lCh", MySqlDbType.Double).Value = team.LinkedChat;
            command.Parameters.Add("@sAt", MySqlDbType.DateTime).Value = team.QuestStartedAt;
            command.Parameters.Add("@cT", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@cQ", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@pO", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@hU", MySqlDbType.Int64).Value = 0;

            database.OpenConnection();
            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    database.CloseConnection();
                    return true;
                }
                else
                {
                    database.CloseConnection();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        } 

        public static Dictionary<long, Team> LoadData()
        {
            try
            {
                Dictionary<long, Team> result = new Dictionary<long, Team>();

                DB database = new DB();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable dataTable = new DataTable();

                MySqlCommand command = new MySqlCommand("SELECT `teamId` AS 'ID', `teamKey` AS `Key`, `linkedChatId` AS `ChatId`," +
                    "`startedAt` as `start`, `currentTask` AS `Task`, `currentQuestion` AS `question`, `points` AS `points`, `hintsUsed` AS `hints`" +
                    " FROM `teams`", database.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

                foreach (var row in dataTable.Rows)
                {
                    Team team = (Team)Activator.CreateInstance(typeof(Team), row);
                    result.Add(team.LinkedChat, team);
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public static void AddAnswer(Team team, string answer)
        {
            try {
                int tasknumber=0;
                if (team.CurrentStation == 1)
                {
                    if (team.CurrentQuestion < 3)
                    {
                        tasknumber = team.CurrentQuestion;
                    }
                    if (team.CurrentQuestion == 3)
                    {
                        return;
                    }
                    else
                    {
                        tasknumber = team.CurrentQuestion-1;
                    }
                }
                if (team.CurrentStation == 2)
                {
                    tasknumber = team.CurrentQuestion + 6;
                }
                if (team.CurrentStation == 3)
                {
                    tasknumber = team.CurrentQuestion + 11;
                }
                if (team.CurrentStation == 4)
                {
                    tasknumber = team.CurrentQuestion + 14;
                }
                if (team.CurrentStation == 5)
                {
                    tasknumber = team.CurrentQuestion + 18;
                }
                if (team.CurrentStation == 6)
                {
                    tasknumber = team.CurrentQuestion + 23;
                }
                if (team.CurrentStation == 7)
                {
                    tasknumber = team.CurrentQuestion + 24;
                }
                if (team.CurrentStation == 8)
                {
                    tasknumber = team.CurrentQuestion + 25;
                }
                if (team.CurrentStation == 9)
                {
                    tasknumber = team.CurrentQuestion + 27;
                }
                if (team.CurrentStation == 10)
                {
                    tasknumber = team.CurrentQuestion + 33;
                }
                if (tasknumber == 1)
                {
                    DB database = new DB();

                    MySqlCommand command = new MySqlCommand("INSERT INTO `answers` (`teamId`, `task1`, `task2`, `task3`, `task4`, " +
                        "`task5`, `task6`, `task7`, `task8`, `task9`, `task10`, `task11`, `task12`, `task13`, `task14`," +
                        " `task15`, `task16`, `task17`, `task18`, `task19`, `task20`, `task21`, `task22`, `task23`, `task24`," +
                        " `task25`, `task26`, `task27`, `task28`, `task29`, `task30`, `task31`, `task32`, `task33`, `task34`," +
                        $" `task35`, `task36`, `task37`) VALUES (@tId, \"{answer}\", 0, 0, 0, 0, 0, 0, 0, 0, 0," +
                    " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)", database.GetConnection());
                    command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;

                    database.OpenConnection();


                    if (command.ExecuteNonQuery() == 1)
                    {
                        database.CloseConnection();

                    }
                    else
                    {
                        database.CloseConnection();

                    }


                }
                else
                {
                    DB database = new DB();

                    MySqlCommand command = new MySqlCommand($"UPDATE `answers` SET `{"task" + tasknumber.ToString()}`=\"{answer}\"" +
                    " WHERE `teamId`=@tId", database.GetConnection());
                    command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;
                    database.OpenConnection();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        database.CloseConnection();

                    }
                    else
                    {
                        database.CloseConnection();

                    }


                }
            }
            catch
            {

            }
            }
    }
    }

