using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public User botInfo;
        Dictionary<string, Team> teamList;


        public MainWindow()
        {
            InitializeComponent();
            botClient = new TelegramBotClient("1379007033:AAF6K0EW8z8E9GGytASmSX0BwLDngGkIQnA");
            botInfo = botClient.GetMeAsync().Result;
            teamList = Team.CreateTeamList();
            debugTextBlock.Text += $"Здравствуй, мир! Я бот по имени {botInfo.FirstName} и мой ID: {botInfo.Id} \nЯ готов приступить к работе.";
            botStopButton.IsEnabled = false;
        }

        private void botLaunchButton_Click(object sender, RoutedEventArgs e)
        {
            if (botClient.IsReceiving == false)
            {
                botClient.OnMessage += Bot_OnMessage;
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
            if (e.Message.Text == "/start")
            {
                Message message = await MainWindow.botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  //replyToMessageId: e.Message.MessageId,
                  parseMode: ParseMode.Markdown,
                  text: "Приветствую! Я робот, созданный для квеста QUESTion." +
                  "\nТы, должно быть, капитан команды? Пришли мне свой ключ, и мы сможем продолжить."
                ) ;
                this.Dispatcher.Invoke(() =>
                {
                    debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId} " +
                    $"в чат {message.Chat.Id} в {message.Date}. " +
                    $"Это ответ на сообщение {e.Message.MessageId} ";
                });

            }
            else if (e.Message.Text == "276425")
            {
                if (teamList[e.Message.Text].linkedChat is null)
                {
                    Message message = await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        text: $"Ключ принят! Стало быть, вы представляете команду номер {teamList[e.Message.Text].teamID}!"
                                        );
                    teamList[e.Message.Text].linkedChat = e.Message.Chat;
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. " +
                        $"Команда номер {teamList[e.Message.Text].teamID} успешно ввела свой ключ и получила задания.";
                    });
                }
                else if(teamList[e.Message.Text].linkedChat.Id == e.Message.Chat.Id)
                {
                    Message message = await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        text: $"Необязательно присылать мне ключ во второй раз. " +
                                        $"Я уже знаю, что вы представляете команду номер {teamList[e.Message.Text].teamID}."
                                        );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. " +
                        $"Команда номер {teamList[e.Message.Text].teamID} повторно ввела свой ключ.";
                    });
                }
                else
                {
                    Message message = await MainWindow.botClient.SendTextMessageAsync(
                                        chatId: e.Message.Chat,
                                        text: $"К сожалению, эта команда уже ввела свой ключ и получила задания. " +
                                        $"Если вы уверены, что этот ключ именно ваш, то обратитесь к организаторам."
                                        );
                    this.Dispatcher.Invoke(() =>
                    {
                        debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId} " +
                        $"в чат {message.Chat.Id} в {message.Date}. " +
                        $"Это ответ на сообщение {e.Message.MessageId}. " +
                        $"Ключ был отклонён, поскольку команда номер {teamList[e.Message.Text].teamID} уже занята.";
                    });
                }
            }
            else if (e.Message.Text != null)
            {
                Message message = await MainWindow.botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  //replyToMessageId: e.Message.MessageId,
                  parseMode: ParseMode.Markdown,
                  text: "Я не смог распознать вашей команды. Попробуйте ввести её более чётко или используйте команду /help, чтобы узнать мои возможности"
                );
                this.Dispatcher.Invoke(() =>
                {
                    debugTextBlock.Text += $"\n{ message.From.FirstName} отправил сообщение { message.MessageId} " +
                    $"в чат {message.Chat.Id} в {message.Date}. " +
                    $"Это ответ на сообщение {e.Message.MessageId} ";
                });
            }
        }
    }
}
