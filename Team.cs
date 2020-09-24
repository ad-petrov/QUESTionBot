using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QUESTionBot
{
    class Team
    {
        public int teamID { get; set; }
        public Chat linkedChat { get; set; }
        public Location teamCurrentLocation { get; set; }
        public Stack<int> taskList { get; set; } = new Stack<int>();
        public DateTime questStartedAt { get; set; }
        public DateTime questFinishedAt { get; set; }

        public Team(int id, string tasks)
        {
            teamID = id;
            foreach (var task in tasks.Trim().Split(',').Reverse())
            {
                taskList.Push(Int32.Parse(task));
            }
        }

        public static Dictionary<string, Team> CreateTeamList()
        {
            Dictionary<string, Team> teamList = new Dictionary<string, Team>();
            teamList.Add("question123", new Team(1, "1"));
            return teamList;
        }
    }
}
