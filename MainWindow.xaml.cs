using GemBox.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xceed.Words.NET;

namespace QUESTionBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TelegramBotClient botClient;
        public User botInfo;
        static Dictionary<long, Team> teamList = new Dictionary<long, Team>();
        static Dictionary<string, Task> taskList;
        public static bool noWrongAnswer = false;


        public MainWindow()
        {
            InitializeComponent();           
            botClient = new TelegramBotClient("1379007033:AAF6K0EW8z8E9GGytASmSX0BwLDngGkIQnA");
            botInfo = botClient.GetMeAsync().Result;
            taskList = Task.CreateTaskList();
            debugTextBlock.Text += $"Здравствуй, мир! Я бот по имени {botInfo.FirstName} и мой ID: {botInfo.Id} \nЯ готов приступить к работе.";
            botStopButton.IsEnabled = false;

            // заготовка для работы с Word
            try
            {
                if (Directory.GetFiles("D:\\Other\\BotLog\\", "Log.docx").Length == 0)
                {
                    ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                    var doc = new DocumentModel();
                    doc.Save("D:\\Other\\BotLog\\Log.docx");
                }
                else
                {
                    MessageBoxResult mbResult = MessageBox.Show("Файл с логами найден. Хотите продолжить его заполнять, не стирая записей?", "Логи", MessageBoxButton.YesNo);
                    if (mbResult == MessageBoxResult.Yes)
                    {

                    }
                    else if (mbResult == MessageBoxResult.No)
                    {
                        System.IO.File.Delete("D:\\Other\\BotLog\\Log.docx");
                        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
                        var doc = new DocumentModel();
                        doc.Save("D:\\Other\\BotLog\\Log.docx");
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //кнопки запуска Бота в программе
        private void botLaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (botClient.IsReceiving == false)
            {
                botClient.MessageOffset = -1;
                botClient.OnMessage += Bot_OnMessage;
                botClient.OnCallbackQuery += BotOnCallbackQueryReceived;
                botClient.StartReceiving();
                debugTextBlock.Text += "\nБот начал принимать сообщения.";
                botStopButton.IsEnabled = true;
                botLaunchButton.IsEnabled = false;
            }
        }

        private void botStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (botClient.IsReceiving == true)
            {
                botClient.StopReceiving();
                debugTextBlock.Text += "\nБот перестал принимать сообщения.";
                botLaunchButton.IsEnabled = true;
                botStopButton.IsEnabled = false;
            }
        }

        public async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == null) 
            {
                if (noWrongAnswer)
                {
                    teamList[e.Message.Chat.Id].CurrentQuestion++;
                    Task.TaskInteraction(teamList[e.Message.Chat.Id].CurrentTask, teamList[e.Message.Chat.Id].CurrentQuestion, e.Message.Chat);
                }
                else
                {
                    Message message = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      parseMode: ParseMode.Markdown,
                      text: "Либо вы пишете мне неправильный ответ, либо я не могу распознать вашей команды. Попробуйте ещё раз!" +
                      "\nЕсли ситуация тупиковая, напишите @katchern и вам подскажут, что делать."
                    );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId } " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. Команда участника не была распознана.";
                    });
                }
            }
                // стартовый пак
                if (e.Message.Text == "/start")
            {
                Message message1 = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: TextTemplates.message1
                );

                Thread.Sleep(4000);
                Message message2 = await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: TextTemplates.message2,
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Согласен/согласна", "agreement"))
                    );

                this.Dispatcher.Invoke(() =>
                {
                    debugTextBlock.Text += $"\nОтправлено приветственное сообщение { message1.MessageId} " +
                    $"в чат {message1.Chat.Id} в {message1.Date.ToLocalTime()}. ";
                });

            }
            // если человек прислал ключик
            else if (Team.KeyWordsList.Contains(e.Message.Text))
            {
                if (!teamList.ContainsKey(e.Message.Chat.Id))
                {
                    teamList.Add(e.Message.Chat.Id, new Team(Team.KeyWordsList.ToList().IndexOf(e.Message.Text) + 1));
                        Message message = await botClient.SendTextMessageAsync(
                                            chatId: e.Message.Chat,
                                            text: $"Команда № {teamList[e.Message.Chat.Id].TeamID}, ваше время пошло. Первая станция во вложении ниже."
                                            );
                    BetweenTaskInteraction(e.Message.Chat.Id);
                  teamList[e.Message.Chat.Id].LinkedChat = e.Message.Chat;
                        this.Dispatcher.Invoke(() =>
                        {
                            debugTextBlock.Text += $"\nОтправлена инструкция { message.MessageId} " +
                            $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                            $"Команда номер {teamList[e.Message.Chat.Id].TeamID} успешно ввела свой ключ и получила задания.";
                        });
                }
                else if (teamList[e.Message.Chat.Id].LinkedChat.Id == e.Message.Chat.Id)
                {
                    Message message = await botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        text: $"Необязательно присылать мне ключ во второй раз. " +
                                        $"Я уже знаю, что вы представляете команду номер {teamList[e.Message.Chat.Id].TeamID}."
                                        );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"Отправлено сообщение { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. " +
                        $"Команда номер {teamList[e.Message.Chat.Id].TeamID} повторно ввела свой ключ.";
                    });
                }
                else
                {
                    Message message = await botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        replyToMessageId: e.Message.MessageId,
                                        text: $"К сожалению, Этот ключ был введён ранее другой командой. Может, кто-либо из вашей команды уже является капитаном? " +
                                        $"Если нет, и вы уверены, что этот ключ именно ваш, то обратитесь к организаторам."
                                        );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\nОтправлено сообщение { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. " +
                        $"Ключ был отклонён, поскольку команда номер {teamList[e.Message.Chat.Id].TeamID} уже занята.";
                    });
                }
            }
            // приём ответов на вопросы-триггеры
            else if (Task.KeyPhrasesList.Contains(e.Message.Text.Trim().ToLower())||(e.Message.Text.Trim().ToLower() == "розовые") || (e.Message.Text.Trim().ToLower() == "фиолетовый") || (e.Message.Text.Trim().ToLower() == "фиолетовые"))
            {
                try
                {
                    //if ((e.Message.Text.Trim().ToLower() == "розовые") || (e.Message.Text.Trim().ToLower() == "фиолетовый") || (e.Message.Text.Trim().ToLower() == "фиолетовые")) { }
                    Task.TaskInteraction(teamList[e.Message.Chat.Id].CurrentTask, teamList[e.Message.Chat.Id].CurrentQuestion, e.Message.Chat);
                }
                catch
                {

                }
                //this.Dispatcher.Invoke(() =>
                //{
                //    debugTextBlock.Text += $"\nОтправлено сообщение { message.MessageId} " +
                //    $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                //    $"Это ответ на сообщение {e.Message.MessageId}. " +
                //    $"Команда номер {teamList[e.Message.Chat.Id].TeamID} верно ввела ответ на триггер номер {Task.KeyPhrasesList.ToList().IndexOf(e.Message.Text)+1}";
                //});
            }
            // приём триггера вопросов внутри станции
            else if (Task.QuestionTriggers.Contains(e.Message.Text.Trim().ToLower()))
            {
                Task.TriggerHandler(e.Message.Text, teamList[e.Message.Chat.Id], e.Message.Chat.Id);
            }
            //приём "мы готовы" на задании с Лениным
            else if ((e.Message.Text.Trim().ToLower() == "мы готовы")&&(teamList[e.Message.Chat.Id].CurrentTask==7))
            {
                BetweenTaskInteraction(e.Message.Chat.Id);
            }
            // дефолтный ответ на нераспознанную команду
            else if (e.Message.Text != null)
            {
                if (noWrongAnswer)
                {
                    teamList[e.Message.Chat.Id].CurrentQuestion++;
                    Task.TaskInteraction(teamList[e.Message.Chat.Id].CurrentTask, teamList[e.Message.Chat.Id].CurrentQuestion, e.Message.Chat);
                }
                else
                {
                    Message message = await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      parseMode: ParseMode.Markdown,
                      text: "Либо вы пишете мне неправильный ответ, либо я не могу распознать вашей команды. Попробуйте ещё раз!"+
                      "\nЕсли ситуация тупиковая, напишите @katchern и вам подскажут, что делать."
                    );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId } " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. Команда участника не была распознана.";
                    });
                }
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            switch (callbackQuery.Data)
            {
                case ("agreement"):
                    await botClient.AnswerCallbackQueryAsync(
                    callbackQueryId: callbackQuery.Id,
                    text: $"Спасибо, что цените установленные правила!"
                );

                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: $"Спасибо, что цените установленные правила!"
                    );

                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message3
                    );

                    Thread.Sleep(3000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message4,
                        parseMode: ParseMode.Markdown
                    );

                    Thread.Sleep(3000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message5
                    );
                    break;
                case ("nexttask"):
                    teamList[callbackQuery.Message.Chat.Id].CurrentQuestion++;
                    Task.TaskInteraction(teamList[callbackQuery.Message.Chat.Id].CurrentTask, 
                        teamList[callbackQuery.Message.Chat.Id].CurrentQuestion, 
                        callbackQuery.Message.Chat.Id);
                    break;
                case ("right"):
                    if(((teamList[callbackQuery.Message.Chat.Id].CurrentTask == 1) && (teamList[callbackQuery.Message.Chat.Id].CurrentQuestion == 6)) && (teamList[callbackQuery.Message.Chat.Id].hint2used == false))
                    {
                        teamList[callbackQuery.Message.Chat.Id].Points++;
                    }
                    teamList[callbackQuery.Message.Chat.Id].Points++;
                    goto case ("wrong");
                case ("wrong"):
                    teamList[callbackQuery.Message.Chat.Id].CurrentQuestion++;
                    Task.TaskInteraction(teamList[callbackQuery.Message.Chat.Id].CurrentTask,
                        teamList[callbackQuery.Message.Chat.Id].CurrentQuestion,
                        callbackQuery.Message.Chat.Id);
                    break;
                case ("hint"):
                    Task.HintHandler(teamList[callbackQuery.Message.Chat.Id], callbackQuery.Message.Chat.Id);
                    break;
                case ("questend"):
                    teamList[callbackQuery.Message.Chat.Id].QuestFinishedAt = DateTime.Now.ToLocalTime();
                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message97
                    );
                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message98
                    );
                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: TextTemplates.message99,
                        parseMode: ParseMode.Markdown
                    );
                    Thread.Sleep(2000);
                    using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\hashtag.png"))
                    {
                        await botClient.SendPhotoAsync(
                            chatId: callbackQuery.Message.Chat.Id,
                            photo: stream,
                            caption: TextTemplates.message100
                        );
                    }
                    break;
                default:
                    break;
            }
        }

        public static async void BetweenTaskInteraction(ChatId chatid)
        {
            teamList[chatid.Identifier].CurrentQuestion = 0;
            teamList[chatid.Identifier].CurrentTask++;
            if (teamList[chatid.Identifier].CurrentTask == 10) 
            {
                await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: chatid,
                                        text: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].MessageTrigger
                                        );
            }
            else
            {
                await MainWindow.botClient.SendVenueAsync(chatId: chatid,
                                                    latitude: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].LinkedLocation.Latitude,
                                                    longitude: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].LinkedLocation.Longitude,
                                                    title: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].Title,
                                                    address: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].Address
                                                   );
                await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: chatid,
                                        text: taskList[Task.KeyPhrasesList[teamList[chatid.Identifier].CurrentTask - 1]].MessageTrigger
                                        );
            }
        }
    }
}
