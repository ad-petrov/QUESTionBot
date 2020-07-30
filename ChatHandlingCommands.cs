using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace QUESTionBot
{
    class ChatHandlingCommands
    {
        public static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if(e.Message.Text == "/start")
            {
                await MainWindow.botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "Приветствую! Я робот, созданный для квеста QUESTion." +
                  "\nТы, должно быть, капитан команды? Пришли мне свой ключ, и мы сможем продолжить."
                );
            }
            else if (e.Message.Text != null)
            {
                await MainWindow.botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "Пока что всё, что я могу, это отвечать на всё подряд. Вы сказали:\n" + e.Message.Text
                );
            }
        }
    }
}
