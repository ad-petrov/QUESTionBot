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
            teamList.Add("kronva228", new Team(1, "1"));
            teamList.Add("good_job_oleg", new Team(2, "1"));
            teamList.Add("chaikagopka", new Team(3, "1"));
            teamList.Add("pk2020", new Team(4, "1"));
            teamList.Add("mne_nujen_beliash", new Team(5, "1"));
            teamList.Add("ilovetiktok", new Team(6, "1"));
            teamList.Add("badboy14let", new Team(7, "1"));
            teamList.Add("rovesnik8", new Team(8, "1"));
            teamList.Add("ktopridumal7", new Team(9, "1"));
            teamList.Add("belissimo_gracia", new Team(10, "1"));
            teamList.Add("rechka_donetck", new Team(11, "1"));
            teamList.Add("minecraft_moya_zizn", new Team(12, "1"));
            teamList.Add("bokal_polon_xo", new Team(13, "1"));
            teamList.Add("djip_v_moskve", new Team(14, "1"));
            teamList.Add("emae_kruto_uril", new Team(15, "1"));
            teamList.Add("its_m_to_the_b", new Team(16, "1"));
            teamList.Add("shoping_modni_look", new Team(17, "1"));
            teamList.Add("korol_drifta", new Team(18, "1"));
            teamList.Add("mama_ya_hochu_na_more", new Team(19, "1"));
            teamList.Add("gucci_flips_flops", new Team(20, "1"));
            return teamList;
        }
    }
}
