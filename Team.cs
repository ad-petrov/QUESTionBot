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

        public static Dictionary<string, Team> CreateTeamList()
        {
            Dictionary<string, Team> teamList = new Dictionary<string, Team>();
            teamList.Add("276425", new Team() { teamID=1});
            return teamList;
        }
    }
}
