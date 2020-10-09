using GemBox.Document;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
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

namespace QUESTionBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TelegramBotClient botClient;
        public static Dictionary<long, Team> teamList = new Dictionary<long, Team>();
        static Dictionary<string, Task> taskList;
        static Dictionary<long, int> agreementMessages = new Dictionary<long, int>();
        
        
        


        public MainWindow()
        {
            InitializeComponent();           
            botClient = new TelegramBotClient("1379007033:AAF6K0EW8z8E9GGytASmSX0BwLDngGkIQnA");
            taskList = Task.CreateTaskList();
            botStopButton.IsEnabled = false;
        }

        //кнопки запуска Бота в программе
        private void botLaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (botClient.IsReceiving == false)
            {
                
                botClient.GetUpdatesAsync(-1);
                botClient.OnMessage += Bot_OnMessage;
                botClient.OnCallbackQuery += BotOnCallbackQueryReceived;
                botClient.StartReceiving(Array.Empty<UpdateType>());
                debugTextBlock.Text += "\nБот начал принимать сообщения.";
                botStopButton.IsEnabled = true;
                loadButton.IsEnabled = false;
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
                loadButton.IsEnabled = true;
                botStopButton.IsEnabled = false;
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (botClient.IsReceiving == false)
            {
                teamList = DB.LoadData();
                MessageBox.Show("База данных загружена");
            }
        }

        private async void startWorkingButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<long, Team> keyValue in teamList)
            {
                if ((keyValue.Value.QuestStartedAt != null)&&(keyValue.Value.QuestFinishedAt==null))
                {
                    await botClient.SendTextMessageAsync(
                          chatId: keyValue.Value.LinkedChat,
                          parseMode: ParseMode.Markdown,
                          text: "Капитаны! Бот возвращается! Меня за-за-запустят в течение минуты! По её прошествии смело продолжайте квест!"
                        );
                }
            }
        }

        private async void alarmBrokenButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<long, Team> keyValue in teamList)
            {
                if ((keyValue.Value.QuestStartedAt != null) && (keyValue.Value.QuestFinishedAt == null))
                {
                        await botClient.SendTextMessageAsync(
                          chatId: keyValue.Value.LinkedChat,
                          parseMode: ParseMode.Markdown,
                          text: "Капитаны! У бота технические шоколадки!... Я вернусь в течение 3-5 минут... Не пишите мне, пока я вам сам не скажу!" +
                          "\n Если я за-за-задержусь, свяжитесь, пожалуйста, с @katchern!"
                        );
                }
            }
        }

        public async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            long chatId = e.Message.Chat.Id;



            if (e.Message.Text == null) 
            {
                    Message message = await botClient.SendTextMessageAsync(
                      chatId: chatId,
                      parseMode: ParseMode.Markdown,
                      text: "Я пока не умею работать с сообщениями без текста. Попробуйте ещё раз!" +
                      "\nЕсли ситуация тупиковая, напишите @katchern и вам подскажут, что делать."
                    );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\nОтправлено сообщение { message.MessageId } " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. Отправлено сообщение без текста.";
                    });
                    return;
                
            }

            string recievedText = e.Message.Text.Trim().ToLower();

            // стартовый пак
            if (e.Message.Text == "/start")
            {
                Message message1 = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: TextTemplates.message1
                );

                Thread.Sleep(4000);
                Message message2 = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: TextTemplates.message2,
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Согласен/согласна", "agreement"))
                    );
                if (agreementMessages.ContainsKey(chatId)) agreementMessages[chatId] = message2.MessageId;
                else agreementMessages.Add(chatId, message2.MessageId);

                this.Dispatcher.Invoke(() =>
                {
                    debugTextBlock.Text += $"\nОтправлено приветственное сообщение { message1.MessageId} " +
                    $"в чат {message1.Chat.Id} в {message1.Date.ToLocalTime()}. ";
                });

            }
            // если человек прислал ключик
            else if (Team.KeyWordsList.Contains(recievedText))
            {
                if (!teamList.ContainsKey(chatId))
                {
                    teamList.Add(chatId, new Team(Team.KeyWordsList.ToList().IndexOf(e.Message.Text) + 1));

                    teamList[chatId].LinkedChat = e.Message.Chat.Id;

                    if (!DB.TeamAdd(teamList[chatId], recievedText))
                    {
                        await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: $"Команда № {teamList[e.Message.Chat.Id].TeamID} уже ввела этот ключ. Если вы являетесь членом команды" +
                                            $"и пытаетесь сменить капитана, обратитесь к организатору квеста (@katchren)",
                                            parseMode: ParseMode.Markdown
                                            );
                        teamList.Remove(chatId);
                        return;
                    };

                    Message message = await botClient.SendTextMessageAsync(
                                            chatId: chatId,
                                            text: $"Команда № {teamList[chatId].TeamID}, ваше время пошло. Первая станция во вложении ниже. *К*",
                                            parseMode: ParseMode.Markdown
                                            );

                    BetweenTaskInteraction(teamList[chatId]);

                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\nОтправлена инструкция { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                        $"Команда номер {teamList[e.Message.Chat.Id].TeamID} успешно ввела свой ключ и получила задания.";
                    });
                }
                else if (teamList[chatId].LinkedChat == chatId)
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
            }
            // приём ответов на вопросы-триггеры
            else if (Task.KeyPhrasesList.Contains(recievedText) || (recievedText == "розовые") || (recievedText == "фиолетовый") || (recievedText == "фиолетовые"))
            {
                if ((e.Message != null) && (e.Message.Chat != null))
                {
                    if (recievedText == "хармс")
                    {
                        teamList[e.Message.Chat.Id].CurrentQuestion++;
                        var message = await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: " ");
                        MainWindow.teamList[chatId].teamTimerMessageId = message.MessageId;
                        Task.TaskInteraction(teamList[chatId]);
                        return;
                    }
                    else if((recievedText == "отцовская любовь") && (teamList[chatId].CurrentStation == 1))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(15 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "1914") && (teamList[chatId].CurrentStation == 2))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(10 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "премьера") && (teamList[chatId].CurrentStation == 3))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(17 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "6") && (teamList[chatId].CurrentStation == 4))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(10 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "9") && (teamList[chatId].CurrentStation == 5))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(13 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if (((recievedText == "розовый")||((recievedText == "розовые") || (recievedText == "фиолетовый") || (recievedText == "фиолетовые")))&& (teamList[chatId].CurrentStation == 6))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(10 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "14") && (teamList[chatId].CurrentStation == 7))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(20 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "кто моет форточки?") && (teamList[chatId].CurrentStation == 8))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(15 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "гуманитарный") && (teamList[chatId].CurrentStation == 9))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(8 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }
                    else if ((recievedText == "4930") && (teamList[chatId].CurrentStation == 10))
                    {
                        teamList[chatId].teamTimer = new CancellationTokenSource();
                        Task.Timer(15 * 60, chatId, teamList[chatId].teamTimer.Token);
                        Task.TaskInteraction(teamList[e.Message.Chat.Id]);
                    }

                }

                //this.Dispatcher.Invoke(() =>
                //{
                //    debugTextBlock.Text += $"\nОтправлено сообщение { message.MessageId} " +
                //    $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                //    $"Это ответ на сообщение {e.Message.MessageId}. " +
                //    $"Команда номер {teamList[e.Message.Chat.Id].TeamID} верно ввела ответ на триггер номер {Task.KeyPhrasesList.ToList().IndexOf(e.Message.Text)+1}";
                //});
            }
            //приём "мы готовы" на задании с Лениным
            else if ((recievedText == "мы готовы") && (teamList[chatId].CurrentStation == 7))
            {
                BetweenTaskInteraction(teamList[chatId]);
            }
            // дефолтный ответ на нераспознанную команду
            else if (e.Message.Text != null)
            {
                if (teamList.ContainsKey(chatId))
                {
                    if (teamList[chatId].noWrongAnswer)
                    {
                        DB.AddAnswer(teamList[chatId], e.Message.Text);
                        teamList[chatId].CurrentQuestion++;
                        var message = await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: " ");
                        MainWindow.teamList[chatId].teamTimerMessageId = message.MessageId;
                        DB.UpdateTeamNote(teamList[chatId]);
                        Task.TaskInteraction(teamList[chatId]);
                    }
                    else
                    {
                        Message message = await botClient.SendTextMessageAsync(
                      chatId: chatId,
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
                else
                {
                    Message message = await botClient.SendTextMessageAsync(
                      chatId: chatId,
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
            CallbackQuery callbackQuery = callbackQueryEventArgs.CallbackQuery; ;
            Team team = null;
            long chatId = callbackQuery.Message.Chat.Id;;
            int lastMessageId = callbackQuery.Message.MessageId;


            if (callbackQuery.Data != "agreement")
            {
                if (!teamList.ContainsKey(chatId)) return;
                team = teamList[callbackQuery.Message.Chat.Id];
            }
            if ((callbackQuery.Data != "hint")&&(callbackQuery.Data != "agreement"))
            {
                await botClient.EditMessageReplyMarkupAsync(chatId: chatId, lastMessageId);
            }

            switch (callbackQuery.Data)
            {
                case ("agreement"):
                    await botClient.EditMessageReplyMarkupAsync(chatId: chatId, agreementMessages[chatId]);
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
                    
                    team.CurrentQuestion++;
                    DB.UpdateTeamNote(team);
                    Task.TaskInteraction(team);
                    break;
                case ("right"):
                    
                    team.Points++;
                    DB.AddAnswer(team, "верно");
                    team.CurrentQuestion++;
                    var message = await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: " ");
                    MainWindow.teamList[chatId].teamTimerMessageId = message.MessageId;
                    DB.UpdateTeamNote(team);
                    Task.TaskInteraction(team);
                    break;
                case ("wrong"):
                    
                    DB.AddAnswer(team, "неверно");
                    team.CurrentQuestion++;
                    var message1 = await MainWindow.botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: " ");
                    MainWindow.teamList[chatId].teamTimerMessageId = message1.MessageId;
                    DB.UpdateTeamNote(team);
                    Task.TaskInteraction(team);
                    break;
                case ("hint"):
                    Task.HintHandler(team, lastMessageId);
                    break;
                case ("questend"):
                    team.teamTimer.Cancel();
                    team.teamTimer.Dispose();
                    team.QuestFinishedAt = DateTime.Now.ToLocalTime();
                    DB.UpdateTeamNote(team);
                    
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message97
                    );
                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message98
                    );
                    Thread.Sleep(2000);
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: TextTemplates.message99,
                        parseMode: ParseMode.Markdown
                    );
                    Thread.Sleep(2000);
                    using (var stream = System.IO.File.OpenRead("D:\\Other\\BotMediaFiles\\hashtag.png"))
                    {
                        await botClient.SendPhotoAsync(
                            chatId: chatId,
                            photo: stream,
                            caption: TextTemplates.message100
                        );
                    }
                    break;
                default:
                    break;
            }
        }

        public static async void BetweenTaskInteraction(Team team)
        {
            team.CurrentStation++;
            team.CurrentQuestion = 0;

            float latitude = taskList[Task.KeyPhrasesList[team.CurrentStation - 1]].LinkedLocation.Latitude;
            float longitude = taskList[Task.KeyPhrasesList[team.CurrentStation - 1]].LinkedLocation.Longitude;
            string title = taskList[Task.KeyPhrasesList[team.CurrentStation - 1]].Title;
            string address = taskList[Task.KeyPhrasesList[team.CurrentStation - 1]].Address;
            string messageTrigger = taskList[Task.KeyPhrasesList[team.CurrentStation - 1]].MessageTrigger;

            if (team.teamTimer!=null)
            {
                team.teamTimer.Cancel();
                team.teamTimer.Dispose();
            }

            DB.UpdateTeamNote(team);
            if (team.CurrentStation == 10) 
            {
                await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: team.LinkedChat,
                                        text: messageTrigger
                                        );
            }
            else
            {
                await MainWindow.botClient.SendVenueAsync(chatId: team.LinkedChat,
                                                    latitude: latitude,
                                                    longitude: longitude,
                                                    title: title,
                                                    address: address
                                                   );
                await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: team.LinkedChat,
                                        text: messageTrigger
                                        );
            }
        }

        
    }
}
