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


        public static string[] KeyPhrasesList = new string[] { "отцовская любовь", "1914", "премьера", "6", "9", "розовый", "14", "кто моет форточки?", "гуманитарный", "4930", "хармс" };
        //public static string[] QuestionTriggers = new string[] {  "чти", "прокофьев", "фортуна меркурий", "пушкин", "348956", "бриллиантовая рука", "максим горький", "архитектоника"};

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


        public static async void TaskInteraction(Team team)
        {
            int currentStation = team.CurrentStation;
            int currentQuestion = team.CurrentQuestion;
            long chatId = team.LinkedChat;
            

            switch (currentStation)
            {
                case (1):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: TextTemplates.message7
                            );
                        Thread.Sleep(2000);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\I1v2bT5BSFM.jpg")) {
                            team.lastQuestion = await MainWindow.botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream,
                            caption: TextTemplates.message8,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask"))
                            );
                        }
                    }
                    else if (currentQuestion == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Пройдитесь по саду и отметьте, какой скрипки нет:\n1) Скрипка-женщина\n2) Скрипка-туфелька\n3) Скрипка-зонт\n4) Скрипка-граммофон",
                        replyMarkup: Task.InlineKeyboards.message9keyboard);
                    }
                    else if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer9);
                        Thread.Sleep(2000);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"Помните, что Андрей Павлович Петров лично посадил одно из деревьев в этом саду? Найдите его и отметьте, что это за дерево.\n1) Береза\n2) Рябина\n3) Лиственница\n4) Дуб",
                        replyMarkup: Task.InlineKeyboards.message10keyboard);
                    }
                    else if (currentQuestion == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Андрей Павлович Петров посадил рябину");
                        Thread.Sleep(2000);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message11);
                    }
                    else if (currentQuestion == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message12);
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: TextTemplates.message13);
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: TextTemplates.message14,
                         parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                         replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Подсказка", "hint")));
                        team.noWrongAnswer = true;
                    }
                    else if (currentQuestion == 5)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Собаку звали Чти. Оригинально, не так ли?");
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: TextTemplates.message16);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1712.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                         chatId: chatId,
                         photo: stream,
                         caption: "Я уверен, почти каждый видел его картины. А какая из представленных работ не его?",
                         replyMarkup: InlineKeyboards.message17keyboard);
                        }
                    }
                    else if (currentQuestion == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: TextTemplates.answer17);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Мало кто знает об этом, но Дали создал логотип для этой компании. Как она называется? _За это задание вы можете получить 2 балла._\n1) Coca-cola\n2)Chupa-chps\n3) Batman\n4) McDonald's",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: InlineKeyboards.message18keyboard);
                    }
                    else if (currentQuestion == 7)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\HYj3VP-tUPw.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.answer18);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message19);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message20);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\1.mp3"))
                        {
                            await MainWindow.botClient.SendAudioAsync(
                                chatId: chatId,
                                audio: stream);
                        }
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\2.mp3"))
                        {
                            await MainWindow.botClient.SendAudioAsync(
                                chatId: chatId,
                                audio: stream);
                        }
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\3.mp3"))
                        {
                            await MainWindow.botClient.SendAudioAsync(
                                chatId: chatId,
                                audio: stream);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Прослушайте три отрывка и определите, какой из них - часть Ленинградской симфонии.",
                        replyMarkup: InlineKeyboards.message21keyboard);
                    }
                    else if (currentQuestion == 8)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: "Ленинградская симфония находится под номером 2");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Дмитрий Лихачёв (1906-1999) — советский и российский филолог, культуролог, искусствовед, доктор филологических наук, профессор.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message23,
                        replyMarkup: InlineKeyboards.message23keyboard);
                    }
                    else if (currentQuestion == 9)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer24);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message25,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\AuJdMkI6xFI.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Погнали дальше!");
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (2):
                    if (currentQuestion == 0)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\Iy46pcYcYrQ.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message27,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                        }
                    }
                    if (currentQuestion == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Почему премьера симфонии состоялась 9 августа?\n1)Это любимое число Шостаковича\n2)Согласно приказу сталина\n3)В прошлом все премьеры должны были проходить 9-го числа\n4)В этот день по плану Гитлера Ленинград должен был пасть от блокады",
                        replyMarkup: InlineKeyboards.message28keyboard);
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Именно 9 августа 1942 Ленинград должен был пасть от блокады.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Искусство не умирало во время блокады. Удивительно, как изнеможенные холодом, голодом и обстрелами жители Ленинграда не падали духом.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вспомните, кто был голосом Ленинграда во времена блокады?\n1) Даниил Гранин\n2) Ольга Берггольц\n3) Анна Ахматова\n4) Вера Инбер\n5) Михаил Дудин",
                        replyMarkup: InlineKeyboards.message30keyboard);
                    }
                    if (currentQuestion == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer30);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\EnD4MBXdGEw.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Найдите мемориальную доску еще одному деятелю искусства, на этот раз поэту. Она расположена на фасаде здания. Напишите только его фамилию.");
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 4)
                    {
                        team.noWrongAnswer = false;
                        using(var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\xRWztQjYxJ0.jpg")){
                            await MainWindow.botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream,
                            caption: TextTemplates.answer31);
                        }
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\9Dk5AHTcA70.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message32);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message33);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вспомните историю и ответьте на вопрос: В каких политических событиях того времени Киров не принимал участие?" +
                        "\n1) Защищал интересы большевиков на Дальнем Востоке;" +
                        "\n2) Организация обороны Астрахани против сил Белой армии;" +
                        "\n3) Установление советской власти в Азербайджане и Грузии",
                        replyMarkup: InlineKeyboards.message34keyboard);
                    }
                    if (currentQuestion == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer34);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message35,
                        replyMarkup: InlineKeyboards.message35keyboard);
                    }
                    if (currentQuestion == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Партия РСДРП оставалась правящей партией под разными названиями до 1991.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message36);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (3):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Вы готовы приступить к заданию?",
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (currentQuestion == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message38+ "\n1) Два родных брата и один двоюродный" +
                        "\n2) Отец и два сына" +
                        "\n3) Они не были родственниками" +
                        "\n4) Три родных брата",
                        replyMarkup: InlineKeyboards.message38keyboard);
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer38);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1843.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message39,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        }
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 3)
                    {
                        team.noWrongAnswer = false;
                            await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer39,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendVenueAsync(chatId: chatId,
                                                latitude: (float)59.962526,
                                                longitude: (float)30.314253,
                                                title: "Переход в другой двор",
                                                address: "",
                                                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Готовы", "nexttask"))
                                               ) ;
                        
                    }
                    if (currentQuestion == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message40);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message41,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 5)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Это Александр Сергеевич Пушкин");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message42,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (4):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: TextTemplates.message43,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (currentQuestion == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Перед зданием лицея ранее было несколько бюстов различных людей. \nА чьего бюста перед учебным заведением никогда не было ?" +
                        "\n1) Пушкину" +
                        "\n2) Ленину" +
                        "\n3) Николаю I" +
                        "\n4) Александру I",
                        replyMarkup: InlineKeyboards.message44keyboard);
                    }
                    if (currentQuestion == 2)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1713.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.answer44);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message45);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message46);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Что случилось?" +
                        "\n1) Пистолеты забыли зарядить;" +
                        "\n2) Они стрелялись из поддельных пистолетов;" +
                        "\n3) Пистолеты зарядили ягодами;" +
                        "\n4) Порох в пистолетах намок и выстрела не получилось",
                        replyMarkup: InlineKeyboards.message47keyboard);
                    }
                    if (currentQuestion == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer47);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message48+ "\n1) “Горе от ума” Александр Грибоедов" +
                        "\n2) “Маскарад” М.Ю. Лермонтов" +
                        "\n3) “Ревизор” Н. В. Гоголь" +
                        "\n4) “Спящая царевна” В. А Жуковский",
                        replyMarkup: InlineKeyboards.message48keyboard);
                    }
                    if (currentQuestion == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer48);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message49+"\n1) Петр Гринев" +
                        "\n2) Владимир Дубровский" +
                        "\n3) Евгений Онегин" +
                        "\n4) Алексей Швабрин",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: InlineKeyboards.message49keyboard);
                    }
                    if (currentQuestion == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer49);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Ладно, заканчиваю доставать вас вопросами из школьной программы. Отправляемся дальше, к не менее интересному месту. \n*ТО*",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (5):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: TextTemplates.message52,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (currentQuestion == 1)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\e1w13JkJxfI.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message53);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Составьте число из цифр(чисел), между которыми располагаются следующие знаки: Весы Рыбы Стрелец\n_За это задание вы можете получить 2 балла._",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 2)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Правильный ответ - 348956");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message55,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\RPReplay_Final1600799227.mp4"))
                        {
                            await MainWindow.botClient.SendVideoAsync(
                        chatId: chatId,
                        video: stream);
                        }
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 3)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Бриллиантовая рука - легендарная советская комедия, снятая в 1986 году режиссёром Леонидом Гайдаем.");
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1714.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: "А вот такие показы были совсем рядом, в Доме Мод, в этом здании находится ст. метро “Петроградская”, у которой вы собирались сегодня." +
                        "Совсем недавно стало известно, что Дом Мод обновят до современного фэшн-пространства. Здесь будут располагаться шоурумы, офисы модных брендов и будут проводиться показы одежды.");
                        }
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1715.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message57 + "\n1) Ресторан" +
                        "\n2) Кинотеатр" +
                        "\n3) Туберкулезный диспансер" +
                        "\n4) Детский дом",
                        replyMarkup: InlineKeyboards.message57keyboard);
                        }
                    }
                    if (currentQuestion == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: TextTemplates.answer57);
                           await MainWindow.botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "В романе Ф.М. Достоевского “Преступление и наказание” один из героев умирает от болезни туберкулеза, которая в то время называлась чахоткой. Кто же этот герой?" +
                                                "\n1) Соня Мармеладова;" +
                                                "\n2) Семён Захарович Мармеладов;" +
                                                "\n3) Сестра Раскольникова - Авдотья;" +
                                                "\n4) Катерина Ивановна Мармеладова;" +
                                                "\n5) Петр Петрович Лужин",
                                                replyMarkup: InlineKeyboards.message58keyboard);
                    }
                    if (currentQuestion == 5)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer58);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message59+ "\n1) Слониха Бэтти" +
                        "\n2) Американский крокодил" +
                        "\n3) Детеныши медведей" +
                        "\n4) Бегемотиха Красавица",
                        replyMarkup: InlineKeyboards.message59keyboard);
                    }
                    if (currentQuestion == 6)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: "162 животных зоопарка удалось спасти, среди них была и бегемотиха Красавица, которая умерла в 1951 году.");
                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: "Половина маршрута уже пройдена. Хорошая работа, друзья! Двигаемся дальше. *К*",
                           parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (6):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message60);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message61);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\unnamed.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream,
                            caption: TextTemplates.message62,
                            replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                        }
                    }
                    if (currentQuestion == 1)
                    {
                            await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message63,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message64,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 2)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message65,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Больше никаких убийств сегодня! Идём дальше. *Р*",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (7):
                    if (currentQuestion == 0)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1844.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message68);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message69
                        );
                        await MainWindow.botClient.SendVenueAsync(chatId: chatId,
                                                latitude: (float)59.970433,
                                                longitude: (float)30.308610,
                                                title: "",
                                                address: "",
                                                replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Далее", "nexttask"))
                                               );
                    }
                    if (currentQuestion == 1)
                    {
                        
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1845.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message70);
                        }
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1847.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message71,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask"))
                        );
                        }
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message72,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "После завершения задания напишите сюда \"Мы готовы\"");
                    }
                    break;
                case (8):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message74);
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\Glsw61-v6o0.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message75);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message76,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (currentQuestion == 1)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1716.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message77,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: InlineKeyboards.message77keyboard);
                        }
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer77,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message78);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message79,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 3)
                    {
                        team.noWrongAnswer = false;
                        await MainWindow.botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: TextTemplates.message80,
                                                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message81,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (9):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message83,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                        
                    }
                    if (currentQuestion == 1)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                         chatId: chatId,
                         text: "А теперь давайте узнаем, насколько хорошо вы знаете историю нашего университета.\nПервое здание ИТМО (тогда ещё ЛИТМО) расположено по адресу:" +
                         "\n1) Кронверкский пр. 49" +
                         "\n2) ул. Ломоносова 9" +
                         "\n3) ул. Чайковского 11/2" +
                         "\n4) пер. Гривцова 14",
                         replyMarkup: InlineKeyboards.message85keyboard);
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Первое здание расположено по адресу: переулок Гривцова, 14");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "В каком году ИТМО получил статус национального исследовательского университета?",
                        replyMarkup: InlineKeyboards.message86keyboard);
                    }
                    if (currentQuestion == 3)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                                               chatId: chatId,
                                               text: "ИТМО получил статус национального исследовательского университета в 2009 году.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Во дворе какого корпуса был установлен первый в России памятник Стиву Джобсу?" +
                        "\n1) Биржевая линия 4" +
                        "\n2) Кронверкский пр. 49" +
                        "\n3) ул. Ломоносова 9" +
                        "\n4) пер. Гривцова 14",
                        replyMarkup: InlineKeyboards.message87keyboard);
                    }
                    if (currentQuestion == 4)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer87);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message88,
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Подсказка", "hint")));
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 5)
                    {
                        team.noWrongAnswer = false;

                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer88);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message89+ "\n1) Голова барана на фасаде корпуса на Чайковского" +
                        "\n2) Голова быка на фасаде главного корпуса" +
                        "\n3) Голова буйвола на фасаде корпуса на Гривцова",
                        replyMarkup: InlineKeyboards.message89keyboard);
                    }
                    if (currentQuestion == 6)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\Sge1i7YUy1E.jpg"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.answer89);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message90+ "\n1) Биржевая линия д.14" +
                        "\n2) улица Чайковского 11/2" +
                        "\n3) Кадетская линия В.О. д.3" +
                        "\n4) улица Гастелло д.12",
                        replyMarkup: InlineKeyboards.message90keyboard);
                    }
                    if (currentQuestion == 7)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.answer90);
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message901);
                        MainWindow.BetweenTaskInteraction(team);
                    }
                    break;
                case (10):
                    if (currentQuestion == 0)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Перед вами - мастерская Михаила Константиновича Аникушина (1917-1997), с которым мы уже сегодня встретились.",
                        replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Перейти к заданию", "nexttask")));
                    }
                    if (currentQuestion == 1)
                    {
                        using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\IMG_1853.png"))
                        {
                            await MainWindow.botClient.SendPhotoAsync(
                        chatId: chatId,
                        photo: stream,
                        caption: TextTemplates.message93);
                        }
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message94,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                        replyMarkup: InlineKeyboards.message94keyboard);
                    }
                    if (currentQuestion == 2)
                    {
                        await MainWindow.botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "К сожалению, Ленина не найти в окнах музея. Может, он внутри?");
                        await MainWindow.botClient.SendTextMessageAsync(
                                                chatId: chatId,
                                                text: "Кстати, на счету Аникушина 5 памятника А. С. Пушкину, а еще именем скульптора названа малая планета.");
                        await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message96,
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        team.noWrongAnswer = true;
                    }
                    if (currentQuestion == 3)
                    {
                        team.noWrongAnswer = false;

                        await MainWindow.botClient.SendTextMessageAsync(
                           chatId: chatId,
                           text: TextTemplates.answer96,
                           replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Завершить квест", "questend")));

                    }
                    break;
                default:
                    break;
            }
        }

        //public static void TriggerHandler(string trigger, Team team, chatId chatId)
        //{
        //    switch (trigger.Trim().ToLower())
        //    {
        //        case ("хармс"):
        //            team.CurrentQuestion++;
        //            TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("чти"):
        //            team.Points++;
        //            DB.AddAnswer(team, "чти");
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("прокофьев"):
        //            team.Points++;
        //            DB.AddAnswer(team, "прокофьев");
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("фортуна меркурий"):
        //            team.Points+=2;
        //            DB.AddAnswer(team, "фортуна меркурий");
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("пушкин"):
        //            DB.AddAnswer(team, "пушкин");
        //            team.Points += 3;
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("348956"):
        //            DB.AddAnswer(team, "348956");
        //            team.Points += 2;
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("бриллиантовая рука"):
        //            DB.AddAnswer(team, "бриллиантовая рука");
        //            team.Points += 2;
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("максим горький"):
        //            DB.AddAnswer(team, "максим горький");

        //            team.Points += 2;
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //        case ("архитектоника"):
        //            DB.AddAnswer(team, "архитектоника");
        //            team.Points += 3;
        //            team.CurrentQuestion++;
        //            Task.TaskInteraction(team.CurrentTask, team.CurrentQuestion, chatId);
        //            break;
        //    }
        //}

        public static async void HintHandler(Team team, long chatId)
        {
            if ((team.CurrentStation == 1) && (team.CurrentQuestion == 4))
            {
                await MainWindow.botClient.EditMessageReplyMarkupAsync(chatId: chatId, team.lastQuestion.MessageId);
                team.HintsUsed++;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Это одно из слов");
            }
            if ((team.CurrentStation == 1) && (team.CurrentQuestion == 6))
            {
                await MainWindow.botClient.EditMessageReplyMarkupAsync(chatId: chatId, team.lastQuestion.MessageId, replyMarkup: InlineKeyboards.message18keyboardWithoutHint);
                team.HintsUsed++;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Обратите внимание на страну, в которой жил Дали.");
            }
            if ((team.CurrentStation == 8) && (team.CurrentQuestion == 1))
            {
                await MainWindow.botClient.EditMessageReplyMarkupAsync(chatId: chatId, team.lastQuestion.MessageId, replyMarkup: InlineKeyboards.message77keyboardWithoutHint);
                team.HintsUsed++;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Обратите внимание на название последнего упомянутого заведения");
            }
            if ((team.CurrentStation == 9) && (team.CurrentQuestion == 4))
            {
                await MainWindow.botClient.EditMessageReplyMarkupAsync(chatId: chatId, team.lastQuestion.MessageId);
                team.HintsUsed++;
                await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Обратите внимание на название ближайшей станции  метро.");
            }


        }

        public struct InlineKeyboards
        {
            public static InlineKeyboardMarkup message9keyboard = new InlineKeyboardMarkup(new[]
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
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message10keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
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
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Подсказка", "hint"),
                }
                });

            public static InlineKeyboardMarkup message18keyboardWithoutHint = new InlineKeyboardMarkup(new[]
               {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
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
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                    },
                });

            public static InlineKeyboardMarkup message28keyboard = new InlineKeyboardMarkup(new[]
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

            public static InlineKeyboardMarkup message30keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("5", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message34keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
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
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message44keyboard = new InlineKeyboardMarkup(new[]
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
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });
            public static InlineKeyboardMarkup message47keyboard = new InlineKeyboardMarkup(new[]
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
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message48keyboard = new InlineKeyboardMarkup(new[]
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
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message49keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message57keyboard = new InlineKeyboardMarkup(new[]
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
                        InlineKeyboardButton.WithCallbackData("3", "right"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message58keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("4", "right"),
                    },
                new[]
                {
                        InlineKeyboardButton.WithCallbackData("5", "wrong"),
                }
                });

            public static InlineKeyboardMarkup message59keyboard = new InlineKeyboardMarkup(new[]
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

            public static InlineKeyboardMarkup message77keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                new[]
                {
                        InlineKeyboardButton.WithCallbackData("Подсказка", "hint"),

                }
                });

            public static InlineKeyboardMarkup message77keyboardWithoutHint = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message85keyboard = new InlineKeyboardMarkup(new[]
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

            public static InlineKeyboardMarkup message86keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1997", "wrong"),
                        InlineKeyboardButton.WithCallbackData("2005", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2009", "right"),
                        InlineKeyboardButton.WithCallbackData("2012", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message87keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message89keyboard = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "wrong"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("2", "right"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                    },
                });

            public static InlineKeyboardMarkup message90keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });

            public static InlineKeyboardMarkup message94keyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1", "right"),
                        InlineKeyboardButton.WithCallbackData("2", "wrong"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("3", "wrong"),
                        InlineKeyboardButton.WithCallbackData("4", "wrong"),
                    }
                });
        }
    }

}
