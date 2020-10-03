using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
            DB database = new DB();

            MySqlCommand command = new MySqlCommand("UPDATE `teams` SET `currentTask`=@cT, `currentQuestion`=@cQ, `points`=@pO, `hintsUsed`=@hU " +
                "WHERE `teamId`=@tId", database.GetConnection());
            command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;
            //command.Parameters.Add("@tK", MySqlDbType.VarChar).Value = e.Message.Text.Trim().ToLower();
            //command.Parameters.Add("@lCh", MySqlDbType.Double).Value = team.LinkedChat.Id;
            //command.Parameters.Add("@sAt", MySqlDbType.DateTime).Value = teamList[e.Message.Chat.Id].QuestStartedAt;
            command.Parameters.Add("@cT", MySqlDbType.Int64).Value = team.CurrentTask;
            command.Parameters.Add("@cQ", MySqlDbType.Int64).Value = team.CurrentQuestion;
            command.Parameters.Add("@pO", MySqlDbType.Int64).Value = team.Points;
            command.Parameters.Add("@hU", MySqlDbType.Int64).Value = team.HintsUsed;

            database.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                database.CloseConnection();
                return;
            }

            throw new Exception();
            }

        public static bool TeamAdd(Team team, string teamkey)
        {
            DB database = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `teams`(`teamId`, `teamKey`, `linkedChatId`, `startedAt`" +
                ", `currentTask`, `currentQuestion`, `points`, `hintsUsed`) VALUES (@tId, @tK, @lCh, @sAt, @cT, @cQ, @pO, @hU)", database.GetConnection());
            command.Parameters.Add("@tId", MySqlDbType.Int64).Value = team.TeamID;
            command.Parameters.Add("@tK", MySqlDbType.VarChar).Value = teamkey;
            command.Parameters.Add("@lCh", MySqlDbType.Double).Value = team.LinkedChat.Id;
            command.Parameters.Add("@sAt", MySqlDbType.DateTime).Value = team.QuestStartedAt;
            command.Parameters.Add("@cT", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@cQ", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@pO", MySqlDbType.Int64).Value = 0;
            command.Parameters.Add("@hU", MySqlDbType.Int64).Value = 0;

            database.OpenConnection();

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

    
    }
    }

