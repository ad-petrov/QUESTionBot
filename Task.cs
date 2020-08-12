using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QUESTionBot
{
    class Task
    {
        public Location linkedLocation { get; set; }
        public string taskText { get; }

        public Task(Location location, string text)
        {
            linkedLocation = location;
            taskText = text;
        }
    }
}
