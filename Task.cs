using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace QUESTionBot
{
    class Task
    {
        public Location LinkedLocation { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }

        public static string[] KeyPhrasesList = new string[] { "Отцовская любовь", "1914", "Премьера", "6", "9", "Розовый", "14", "Кто моет форточки?", "Гуманитарный", "4930" };

        public Task(Location location, string text, string address)
        {
            LinkedLocation = location;
            Title = text;
            Address = address;
        }

        public static Dictionary<string, Task> CreateTaskList()
        {
            Dictionary<string, Task> taskList = new Dictionary<string, Task>();
            taskList.Add("Отцовская любовь", new Task(new Location() { Latitude= (float)59.963555, Longitude=(float)30.313474 }, "Сад Андрея Петрова", "Каменоостровский проспект"));
            taskList.Add("1914", new Task(new Location() { Latitude= (float)59.962176, Longitude=(float)30.310336 }, "Доходный дом Первого Российского страхового общества", "Кронверкская улица, 29/37Б"));
            taskList.Add("Премьера", new Task(new Location() { Latitude= (float)59.962757, Longitude=(float)30.313877 }, "Дом Бенуа", "Каменноостровский проспект, 26-28"));
            taskList.Add("6", new Task(new Location() { Latitude= (float)59.962321, Longitude=(float)30.315905 }, "Алексндровский лицей", "Сад Александровского Лицея"));
            taskList.Add("9", new Task(new Location() { Latitude= (float)59.965952, Longitude=(float)30.312643 }, "Площадь Льва Толстого", "Каменноостровский проспект, 35/75"));
            taskList.Add("Розовый",new Task(new Location() { Latitude= (float)59.968943, Longitude=(float)30.312686 }, "Дом Ленсовета", "набережная реки Карповки, 13Ж"));
            taskList.Add("14", new Task(new Location() { Latitude= (float)59.970301, Longitude=(float)30.308757 }, "Бывшая мебельная фабрика", "набережная реки Карповки, 13Ж"));
            taskList.Add("Кто моет форточки?", new Task(new Location() { Latitude= (float)59.973252, Longitude=(float)30.304854 }, "Особняк Игеля", "Каменноостровский проспект, 58-60"));
            taskList.Add("Гуманитарный", new Task(new Location() { Latitude= (float)59.972672, Longitude=(float)30.301998 }, "Вязьма", "Вяземский переулок, 5-7"));
            taskList.Add("4930", new Task(new Location() { Latitude= (float)59.973080, Longitude=(float)30.297192 }, "Мастерская Аникушина", "Вяземский переулок, 8"));
            return taskList;
        }
    }
}
