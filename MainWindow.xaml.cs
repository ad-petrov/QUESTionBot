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
        Dictionary<long, Team> teamList = new Dictionary<long, Team>();
        Dictionary<string, Task> taskList;


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
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Я и моя команда обязуемся соблюдать правила дорожного движения", "agreement"))
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
                        await botClient.SendVenueAsync(chatId: e.Message.Chat,
                                                latitude: taskList[Task.KeyPhrasesList[teamList[e.Message.Chat.Id].CurrentTask]].LinkedLocation.Latitude,
                                                longitude: taskList[Task.KeyPhrasesList[teamList[e.Message.Chat.Id].CurrentTask]].LinkedLocation.Longitude,
                                                title: taskList[Task.KeyPhrasesList[teamList[e.Message.Chat.Id].CurrentTask]].Title,
                                                address: taskList[Task.KeyPhrasesList[teamList[e.Message.Chat.Id].CurrentTask]].Address
                                               );
                    await botClient.SendTextMessageAsync(
                                            chatId: e.Message.Chat,
                                            text: taskList[Task.KeyPhrasesList[teamList[e.Message.Chat.Id].CurrentTask]].MessageTrigger
                                            );
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
            else if (Task.KeyPhrasesList.Contains(e.Message.Text))
            {
                Task.TaskInteraction(Task.KeyPhrasesList.ToList().IndexOf(e.Message.Text));
                //this.Dispatcher.Invoke(() =>
                //{
                //    debugTextBlock.Text += $"\nОтправлено сообщение { message.MessageId} " +
                //    $"в чат {message.Chat.Id} в {message.Date.ToLocalTime()}. " +
                //    $"Это ответ на сообщение {e.Message.MessageId}. " +
                //    $"Команда номер {teamList[e.Message.Chat.Id].TeamID} верно ввела ответ на триггер номер {Task.KeyPhrasesList.ToList().IndexOf(e.Message.Text)+1}";
                //});
            }
            // дефолтный ответ на нераспознанную команду
            else if (e.Message.Text != null)
            {
                Message message = await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  parseMode: ParseMode.Markdown,
                  text: "Я не смог распознать вашей команды. Попробуйте ввести её более чётко или используйте команду /help, чтобы узнать мои возможности"
                );
                this.Dispatcher.Invoke(() =>
                {
                    debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId } " +
                    $"в чат {message.Chat.Id} в {message.Date}. " +
                    $"Это ответ на сообщение {e.Message.MessageId}. Команда участника не была распознана.";
                });
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            if (callbackQuery.Data == "agreement")
            {
                await botClient.AnswerCallbackQueryAsync(
                    callbackQueryId: callbackQuery.Id,
                    text: $"Спасибо, что цените установленные правила!"
                );

                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: $"Спасибо, что цените установленные правила!"
                ) ;

                Thread.Sleep(2000);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: TextTemplates.message3
                ) ;

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
            }
        }
    }
}
