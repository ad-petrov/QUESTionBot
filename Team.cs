using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QUESTionBot
{
    public class Team
    {
        public int TeamID { get; set; }
        public long LinkedChat { get; set; }
        public DateTime QuestStartedAt { get; set; }
        public DateTime? QuestFinishedAt { get; set; }
        public int CurrentStation { get; set; }
        public int CurrentQuestion { get; set; }
        public int Points { get; set; }
        public int HintsUsed { get; set; }
        public bool noWrongAnswer { get; set; }

        private int lastBotMessageId;
        public int LastBotMessageId { get { return lastBotMessageId; } set { lastBotMessageId = value; DB.UpdateTeamNote(this); } }
             

        public static string[] KeyWordsList = new string[] { "kronva228", "good_job_oleg", "chaikagopka", "pk2020", "mne_nujen_beliash", "ilovetiktok", "badboy14let", 
            "rovesnik8", "ktopridumal7", "belissimo_gracia", "rechka_donetck", "minecraft_moya_zizn", "bokal_polon_xo", "djip_v_moskve", "emae_kruto_uril",
            "its_m_to_the_b", "shoping_modni_look", "korol_drifta", "mama_ya_hochu_na_more", "gucci_flips_flops", "uchishsya_baletu_potter",
        "zolotaya_4asha", "tima_s_dr22", "dabdaya228", "krash_Bogdan14", "prokopievsk85", "ilovefrance89", "chernichka98", "gde_buterbrodi78", "hochu_v_voronezh6",
        "kultura_urala349", "marat_uvaev24", "otkroite_stolovku33", "privet_ya_podsyadu7", "v_smisle_5600", "cherepashka6", "zabirai_svoy_vetosh",
        "mali_povzroslel19", "sos_s_s_o_s", "fendi_gucci_prada1", "bababooey44", "ya_lublu_roblox", "kto_v_shararam", "hlebnaya_zhaba99", "smilga_face33"};

        public Team(int id)
        {
            TeamID = id;
            CurrentStation = 0;
            CurrentQuestion = 0;
            Points = 0;
            HintsUsed = 0;
            noWrongAnswer = false;
            QuestStartedAt = DateTime.Now.ToLocalTime();
            QuestFinishedAt = null;
        }

        public Team(DataRow row)
        {
            TeamID = Convert.ToInt32(row["ID"]);
            LinkedChat = (long)Convert.ToDouble(row["ChatId"]);
            CurrentStation = Convert.ToInt32(row["Task"]);
            CurrentQuestion = Convert.ToInt32(row["question"]);
            Points = Convert.ToInt32(row["points"]);
            HintsUsed = Convert.ToInt32(row["hints"]);
            QuestStartedAt = Convert.ToDateTime(row["start"]);
            LastBotMessageId = Convert.ToInt32(row["lastBotMessageId"]);
        }

    }
}
