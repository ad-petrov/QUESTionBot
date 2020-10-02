using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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


        public static string[] KeyPhrasesList = new string[] { "отцовская любовь", "1914", "премьера", "6", "9", "розовый", "14", "кто моет форточки?", "гуманитарный", "4930" };
        public static string[] QuestionTriggers = new string[] { "хармс", "чти", "прокофьев", "фортуна меркурий", "пушкин", "348956", "бриллиантовая рука"};

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
            taskList.Add("отцовская любовь", new Task(new Location() { 
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
            taskList.Add("премьера", new Task(new Location() { 
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
            taskList.Add("розовый",new Task(new Location() { 
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
            taskList.Add("кто моет форточки?", new Task(new Location() { 
                Latitude= (float)59.973252,
                Longitude=(float)30.304854 },
                "Особняк Игеля", 
                "Каменноостровский проспект, 58-60",
                "Для того, чтобы приступить к заданию, напишите, какая фраза написана на двери особняка. Не забудьте о последнем знаке."));
            taskList.Add("гуманитарный", new Task(new Location() { 
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


        public static async void TaskInteraction(int tasknumber, int questionnumber, ChatId chatid)
        {
            switch (tasknumber)
            {
                case (1):
                    if (questionnumber == 0)
                    {
                        Message message = await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatid,
                            text: TextTemplates.message7
                            );
                        Thread.Sleep(2000);
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatid,
                            text: TextTemplates.message8,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask"))
                            );
                    }
                    else if (questionnumber == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: $"Пройдитесь по саду и отметьте, какой скрипки нет:",
                        replyMarkup: Task.InlineKeyboards.message9keyboard);
                    }
                    else if(questionnumber == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer9);
                        Thread.Sleep(2000);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: $"Помните, что Андрей Павлович Петров лично посадил одно из деревьев в этом саду? Найдите его и отметьте, что это за дерево.",
                        replyMarkup: Task.InlineKeyboards.message10keyboard);
                    }
                    else if(questionnumber == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Андрей Павлович Петров посадил рябину");
                        Thread.Sleep(2000);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message11);
                    }
                    else if (questionnumber == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message12); 
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatid,
                         text: TextTemplates.message13);
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatid,
                         text: TextTemplates.message14,
                         parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                         replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Подсказка", "hint")));
                        MainWindow.noWrongAnswer = true;
                    }
                    else if(questionnumber == 5)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Собаку звали Чти. Оригинально, не так ли?");
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatid,
                         text: TextTemplates.message16);
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatid,
                         text: "Я уверен, почти каждый видел его картины. А какая из представленных работ не его?",
                         replyMarkup: InlineKeyboards.message17keyboard);
                    }
                    else if (questionnumber == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatid,
                         text: TextTemplates.answer17);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Мало кто знает об этом, но Дали создал логотип для этой компании. Как она называется? _За это задание вы можете получить 2 балла._",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: InlineKeyboards.message18keyboard);
                    }
                    else if(questionnumber == 7)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer18);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message19);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message20);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Прослушайте три отрывка и определите, какой из них - часть Ленинградской симфонии.",
                        replyMarkup: InlineKeyboards.message21keyboard);
                    }
                    else if(questionnumber == 8)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatid,
                           text: "Симфония под номером 2");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Дмитрий Лихачёв (1906-1999) — советский и российский филолог, культуролог, искусствовед, доктор филологических наук, профессор.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message23,
                        replyMarkup: InlineKeyboards.message23keyboard);
                    }
                    else if(questionnumber == 9)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer24);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message25,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Пикча с бодровым");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Погнали дальше!");
                        MainWindow.BetweenTaskInteraction(chatid);
                    }
                    break;
                case (2):
                    if (questionnumber == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message27,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if(questionnumber == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Почему премьера симфонии состоялась 9 августа?",
                        replyMarkup: InlineKeyboards.message28keyboard);
                    }
                    if(questionnumber == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Именно 9 августа 1942 Ленинград должен был пасть от блокады.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Искусство не умирало во время блокады. Удивительно, как изнеможенные холодом, голодом и обстрелами жители Ленинграда не падали духом.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вспомните, кто был голосом Ленинграда во времена блокады?",
                        replyMarkup: InlineKeyboards.message30keyboard );
                    }
                    if(questionnumber == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer30);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Пикча Ленинграда");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Найдите мемориальную доску еще одному деятелю искусства, на этот раз поэту. Она расположена на фасаде здания. Напишите только его фамилию.");
                        MainWindow.noWrongAnswer = true;
                    }
                    if (questionnumber == 4)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer31);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message32);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message33);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вспомните историю и ответьте на вопрос: В каких политических событиях того времени Киров не принимал участие?",
                        replyMarkup: InlineKeyboards.message34keyboard);
                    }
                    if (questionnumber == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer34);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message35,
                        replyMarkup: InlineKeyboards.message35keyboard);
                    }
                    if(questionnumber == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Партия РСДРП оставалась правящей партией под разными названиями до 1991.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message36);
                        MainWindow.BetweenTaskInteraction(chatid);
                    }
                    break;
                case (3):
                    if (questionnumber == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вы готовы приступить к заданию?",
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if(questionnumber == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message38,
                        replyMarkup: InlineKeyboards.message38keyboard);
                    }
                    if(questionnumber == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer38);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message39,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.noWrongAnswer = true;
                    }
                    if(questionnumber == 3)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message40);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message41);
                        MainWindow.noWrongAnswer = true;
                    }
                    if(questionnumber == 4)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Это Александр Сергеевич Пушкин");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message42,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(chatid);
                    }
                    break;
                case (4):
                    if (questionnumber == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatid,
                            text: TextTemplates.message43,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (questionnumber == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Перед зданием лицея ранее было несколько бюстов различных людей. \nА чьего бюста перед учебным заведением никогда не было ?",
                        replyMarkup: InlineKeyboards.message44keyboard);
                    }
                    if(questionnumber == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer44);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message45);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message46);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Что случилось?",
                        replyMarkup: InlineKeyboards.message47keyboard);
                    }
                    if (questionnumber == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer47);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message48,
                        replyMarkup: InlineKeyboards.message48keyboard);                       
                    }
                    if (questionnumber == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer48);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message49,
                        replyMarkup: InlineKeyboards.message49keyboard);
                    }
                    if (questionnumber == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer49);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ладно, заканчиваю доставать вас вопросами из школьной программы. Отправляемся дальше, к не менее интересному месту. \n*ТО*",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(chatid);
                    }
                    break;
                case (5):
                    if(questionnumber == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatid,
                            text: TextTemplates.message52,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (questionnumber == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message53);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Составьте число из цифр(чисел), между которыми располагаются следующие знаки: Весы Рыбы Стрелец\n_За это задание вы можете получить 2 балла._",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.noWrongAnswer = true;
                    }
                    if(questionnumber == 2)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Правильный ответ - 348956");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message55);
                        MainWindow.noWrongAnswer = true;
                    }
                    if (questionnumber == 3)
                    {
                        MainWindow.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Бриллиантовая рука - легендарная советская комедия, снятая в 1986 году режиссёром Леонидом Гайдаем.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "А вот такие показы были совсем рядом, в Доме Мод, в этом здании находится ст. метро “Петроградская”, у которой вы собирались сегодня." +
                        "Совсем недавно стало известно, что Дом Мод обновят до современного фэшн-пространства. Здесь будут располагаться шоурумы, офисы модных брендов и будут проводиться показы одежды.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message57,
                        replyMarkup: InlineKeyboards.message57keyboard); 
                    }
                    if (questionnumber == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                                                chatId: chatid,
                                                text: "В романе Ф.М. Достоевского “Преступление и наказание” один из героев умирает от болезни туберкулеза, которая в то время называлась чахоткой. Кто же этот герой?",
                                                replyMarkup: InlineKeyboards.message58keyboard);
                    }
                    if (questionnumber == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.answer58);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: TextTemplates.message59,
                        replyMarkup: InlineKeyboards.message59keyboard);
                    }
                    if (questionnumber == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatid,
                           text: "162 животных зоопарка удалось спасти, среди них была и бегемотиха Красавица, которая умерла в 1951 году.");
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatid,
                           text: "Половина маршрута уже пройдена. Хорошая работа, друзья! Двигаемся дальше. *К*",
                           parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(chatid);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void TriggerHandler(string trigger, Team team, ChatId chatid)
        {
            switch (trigger.Trim().ToLower())
            {
                case ("хармс"):
                    team.CurrentQuestion++;
                    TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("чти"):
                    if (team.hint1used)
                    {
                        team.Points+=2;
                    }
                    else
                    {
                        team.Points++;
                    }
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("прокофьев"):
                    team.Points++;
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("фортуна меркурий"):
                    team.Points+=2;
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("пушкин"):
                    team.Points += 3;
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("348956"):
                    team.Points += 2;
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
                case ("бриллиантовая рука"):
                    team.Points += 2;
                    team.CurrentQuestion++;
                    Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatid);
                    break;
            }
        }

        public static async void HintHandler(Team team, ChatId chatid)
        {
            if ((team.CurrentTask == 1) && (team.CurrentQuestion == 4))
            {
                team.hint1used = true;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Это одно из слов");
            }
            if ((team.CurrentTask == 1) && (team.CurrentQuestion == 6))
            {
                team.hint2used = true;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Обратите внимание на страну, в которой жил Дали.");
            }


        }

        public struct InlineKeyboards
        {
            public static InlineKeyboardMarkup message9keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Скрипка-женщина", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Скрипка-туфелька", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Скрипка-зонт", "right"),
                        InlineKeyboardButton.WithCallbackData("Скрипка-граммофон", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message10keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Берёза", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Рябина", "right"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Лиственница", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Дуб", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message17keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "right"),
                    }
                });
            public static InlineKeyboardMarkup message18keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Coca-Cola", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Chupa-Chups", "right"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Batman", "wrong"),
                        InlineKeyboardButton.WithCallbackData("McDonald's", "wrong"),
                    },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Подсказка", "hint"),
                }
                });
            public static InlineKeyboardMarkup message21keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                    },
                });
            public static InlineKeyboardMarkup message23keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Императорский Санкт-Петербургский университет", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Санкт-Петербургский государственный университет", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Петроградский государственный университет", "right"),
                    },
                });

            public static InlineKeyboardMarkup message28keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Это любимое число Шостаковича", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Согласно приказу Сталина", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("В прошлом все премьеры должны были проходить 9-го числа", "wrong"),
                    },
                    new[]
                    {
                    
                        InlineKeyboardButton.WithCallbackData("В этот день по плану Гитлера Ленинград должен был пасть от блокады", "right"),
                    }
                });

            public static InlineKeyboardMarkup message30keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Даниил Гранин", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Ольга Берггольц", "right"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Анна Ахматова", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Вера Инбер", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Михаил Дудин", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message34keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Защита интересов большевиков на Дальнем Востоке", "right"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Организация обороны Астрахани против сил Белой армии", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Установление советской власти В Азербайджане и Грузии", "wrong"),
                    },
                });

            public static InlineKeyboardMarkup message35keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1924", "wrong"),
                        InlineKeyboardButton.WithCallbackData("1952", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1962", "wrong"),
                        InlineKeyboardButton.WithCallbackData("1991", "right"),
                    }
                });

            public static InlineKeyboardMarkup message38keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Два родных брата и один двоюродный", "right"),
                        InlineKeyboardButton.WithCallbackData("Отец и два сына", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Они не были родственниками", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Три родных брата", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message44keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Пушкину", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Ленину", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Николаю I", "right"),
                        InlineKeyboardButton.WithCallbackData("Александру I", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message47keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Пистолеты забыли зарядить", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Они стрелялись из поддельных пистолетов", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Пистолеты зарядили ягодами", "right"),
                        InlineKeyboardButton.WithCallbackData("Порох в пистолетах намок и выстрела не cлучилось", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message48keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("“Горе от ума” Александр Грибоедов", "wrong"),
                        InlineKeyboardButton.WithCallbackData("“Маскарад” М.Ю. Лермонтов", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("“Ревизор” Н. В. Гоголь", "right"),
                        InlineKeyboardButton.WithCallbackData("“Спящая царевна” В. А Жуковский", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message49keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Петр Гринев", "right"),
                        InlineKeyboardButton.WithCallbackData("Владимир Дубровский", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Евгений Онегин", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Алексей Швабрин", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message57keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Ресторан", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Кинотеатр", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Туберкулезный диспансер", "right"),
                        InlineKeyboardButton.WithCallbackData("Детский дом", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message58keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Соня Мармеладова;", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Семён Захарович Мармеладов", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Сестра Раскольникова - Авдотья", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Катерина Ивановна Мармеладова", "right"),
                    },
                new[]
                {
                        InlineKeyboardButton.WithCallbackData("Петр Петрович Лужин", "wrong"),
                }
                });

            public static InlineKeyboardMarkup message59keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Слониха Бэтти", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Американский крокодил", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Детеныши медведей", "wrong"),
                        InlineKeyboardButton.WithCallbackData("Бегемотиха Красавица", "right"),
                    }
                });
        }
    }

}
