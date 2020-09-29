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
        public string MessageTrigger { get; set; }
        public string[] AttendantMessages { get; set; }
        public string[] AttendantMessagesWithTest { get; set; }

        public static string[] KeyPhrasesList = new string[] { "Отцовская любовь", "1914", "Премьера", "6", "9", "Розовый", "14", "Кто моет форточки?", "Гуманитарный", "4930" };

        public Task(Location location, string text, string address, string trigger)
        {
            LinkedLocation = location;
            Title = text;
            Address = address;
            MessageTrigger = trigger;
        }

        public static Dictionary<string, Task> CreateTaskList()
        {
            Dictionary<string, Task> taskList = new Dictionary<string, Task>();
            taskList.Add("Отцовская любовь", new Task(new Location() { 
                Latitude= (float)59.963555, 
                Longitude=(float)30.313474 }, 
                "Сад Андрея Петрова", 
                "Каменоостровский проспект",
                "Для того, чтобы приступить к заданию, напишите, какое словосочетание можно увидеть на спинке скамейки у входа в сад."));
            taskList.Add("1914", new Task(new Location() { 
                Latitude= (float)59.962176, 
                Longitude=(float)30.310336 }, 
                "Доходный дом Первого Российского страхового общества", 
                "Кронверкская улица, 29/37Б",
                "Для того, чтобы приступить к заданию, найдите арку во дворе дома с датами. Напишите вторую, а затем возвращайтесь к геометке."));
            taskList.Add("Премьера", new Task(new Location() { 
                Latitude= (float)59.962757, 
                Longitude=(float)30.313877 }, 
                "Дом Бенуа", 
                "Каменноостровский проспект, 26-28",
                "Для того, чтобы приступить к заданию, найдите афишу театра “Остров”, расположенного в этом доме. " +
                "Сами афиши расположены на окнах цокольного этажа. Какое слово написано красным цветом на афише комедии по роману Грэма Грина?"));
            taskList.Add("6", new Task(new Location() { 
                Latitude= (float)59.962321, 
                Longitude=(float)30.315905 },
                "Алексндровский лицей", 
                "Сад Александровского Лицея",
                "Для того, чтобы приступить к заданию, напишите количество скамеек в сквере перед лицеем."));
            taskList.Add("9", new Task(new Location() { 
                Latitude= (float)59.965952, 
                Longitude=(float)30.312643 },
                "Площадь Льва Толстого", 
                "Каменноостровский проспект, 35/75",
                "Для того, чтобы приступить к заданию, найдите афишу спектакля, премьера которого назначена на 10 октября. Сосчитайте количество людей на афише и напишите в чат."));
            taskList.Add("Розовый",new Task(new Location() { 
                Latitude= (float)59.968943, 
                Longitude=(float)30.312686 },
                "Дом Ленсовета", 
                "набережная реки Карповки, 13Ж",
                "Для того, чтобы приступить к заданию, пройдите во двор дома и напишите, какого цвета цветы в большой клумбе."));
            taskList.Add("14", new Task(new Location() { 
                Latitude= (float)59.970301, 
                Longitude=(float)30.308757 },
                "Бывшая мебельная фабрика", 
                "набережная реки Карповки, 13Ж",
                "Для того, чтобы приступить к заданию, напишите, сколько восклицательных знаком на предупреждении “проход через ворота запрещен” на воротах, мимо которых вы прошли."));
            taskList.Add("Кто моет форточки?", new Task(new Location() { 
                Latitude= (float)59.973252,
                Longitude=(float)30.304854 },
                "Особняк Игеля", 
                "Каменноостровский проспект, 58-60",
                "Для того, чтобы приступить к заданию, напишите, какая фраза написана на двери особняка. Не забудьте о последнем знаке."));
            taskList.Add("Гуманитарный", new Task(new Location() { 
                Latitude= (float)59.972672,
                Longitude=(float)30.301998 },
                "Вязьма",
                "Вяземский переулок, 5-7",
                "Для того, чтобы приступить к заданию, напишите, какой факультет располагался здесь раньше. " +
                "Найдите табличку с этой информацией на фасаде здания и напишите название факультета. "));
            taskList.Add("4930", new Task(new Location() { 
                Latitude= (float)59.973080, 
                Longitude=(float)30.297192 },
                "Мастерская Аникушина", 
                "Вяземский переулок, 8",
                "Для того, чтобы приступить к заданию, подойдите ко входу в мастерскую. " +
                "Найдите плакат, посвященный выставке и напишите последние 4 цифры номера, расположенного в правом нижнем углу (без доп. знаков и пробелов)."));
            return taskList;
        }


        public static void TaskInteraction(int tasknumber)
        {

        }
    }

}
