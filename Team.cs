using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QUESTionBot
{
    class Team
    {
        public int TeamID { get; set; }
        public Chat LinkedChat { get; set; }
        public DateTime QuestStartedAt { get; set; }
        public DateTime QuestFinishedAt { get; set; }
        public int CurrentTask { get; set; }
        public int CurrentQuestion { get; set; }
        public int Points { get; set; }

        public bool hint1used { get; set; } = false;
        public bool hint2used { get; set; } = false;

        public static string[] KeyWordsList = new string[] { "kronva228", "good_job_oleg", "chaikagopka", "pk2020", "mne_nujen_beliash", "ilovetiktok", "badboy14let", 
            "rovesnik8", "ktopridumal7", "belissimo_gracia", "rechka_donetck", "minecraft_moya_zizn", "bokal_polon_xo", "djip_v_moskve", "emae_kruto_uril",
            "its_m_to_the_b", "shoping_modni_look", "korol_drifta", "mama_ya_hochu_na_more", "gucci_flips_flops"};

        public Team(int id)
        {
            TeamID = id;
            CurrentTask = 0;
            CurrentQuestion = 0;
            Points = 0;
            QuestStartedAt = DateTime.Now.ToLocalTime();
        }

    }
}
