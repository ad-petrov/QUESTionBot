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

namespace QUESTionBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TelegramBotClient botClient;
        public User botInfo;
        public string log;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void botLaunchButton_Click(object sender, RoutedEventArgs e)
        {
            botClient = new TelegramBotClient("1379007033:AAF6K0EW8z8E9GGytASmSX0BwLDngGkIQnA");
            botInfo = botClient.GetMeAsync().Result;
            debugTextBlock.Text += $"Здравствуй, мир! Я бот по имени {botInfo.FirstName} и мой ID: {botInfo.Id}";
            botClient.OnMessage += ChatHandlingCommands.Bot_OnMessage;
            botClient.StartReceiving();
            debugTextBlock.Text += "\nБот начал принимать сообщения.";
        }

        private void botStopButton_Click(object sender, RoutedEventArgs e)
        {
            botClient.StopReceiving();
            debugTextBlock.Text += "\nБот перестал принимать сообщения.";
        }

    }
}
