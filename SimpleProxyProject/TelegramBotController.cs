using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace SimpleTelegramBot.BL
{
    class TelegramBotController
    {
        public string token { get; private set; }
        public List<ScrappedAdress> scrappedAdresses { get; private set; }

        public TelegramBotController(List<ScrappedAdress> scrappedAdresses) {
            TextScrapper tx = new TextScrapper();
            this.scrappedAdresses = scrappedAdresses;
            this.token = tx.getStringFromFile(@"E:\adress.txt"); ;
        }
        static public ITelegramBotClient botClient { get; private set; }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "You said:\n" + e.Message.Text
                );
            }
        }

        static private async void recieveMessage()
        {
            writeMessage("Start recieving message");
            var lastMessageId = 0;
            int lastMessageLength = 0;
            Telegram.Bot.Types.Update last = null;
            while (true)
            {
                try
                {
                    var message = await botClient.GetUpdatesAsync();
                    lastMessageLength = message.Length;
                    if (message.Length > 0)
                    {
                        last = message[message.Length - 1];
                        if (lastMessageId != last.Id)
                        {

                            lastMessageId = last.Id;
                            Console.WriteLine(last.Message.Text);

                            if (last.Message.Text.Contains("hello"))
                            {
                                botClient.SendTextMessageAsync(last.Message.From.Id, "hello");
                            }
                        }

                    }
                    if (lastMessageLength < message.Length) { Console.WriteLine(last.Message.Text); lastMessageLength = message.Length; }
                    Thread.Sleep(200);
                } catch (System.Net.Http.HttpRequestException e)
                //TODO Find exit if take this exception
                {

                }
            }
        }

        public async void connect(string username = "USERNAME", string password = "PASSWORD")
        {
            Telegram.Bot.Types.User me = null;
            for (int i = 0; i < scrappedAdresses.Count; i++)
            {
                if (scrappedAdresses[i].Port == 8080 || scrappedAdresses[i].Port == 80)
                {
                    try
                    {
                        Console.WriteLine($"Trying to connect to the: ({scrappedAdresses[i].Country}) {scrappedAdresses[i].ConnectionAdress} : {scrappedAdresses[i].Port}");
                        ServicePointManager.Expect100Continue = false;
                        var proxy = new WebProxy(scrappedAdresses[i].ConnectionAdress, scrappedAdresses[i].Port)
                        {
                            Credentials = new NetworkCredential(username, password)
                        };
                        Console.Write("Statement: ");
                        botClient = new TelegramBotClient(token, proxy);
                        me = botClient.GetMeAsync().Result;
                        
                        break;
                    } catch { writeMessage("Failed");}
                    }
            }
            if (me != null)
            {
                writeMessage("Connected");
                Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

                Thread thread = new Thread(TelegramBotController.recieveMessage);
                thread.Start();
            }
            else { writeMessage("Failed"); writeMessage("Can't connected to any adress."); }

        } 
        public static void writeMessage(string message) { Console.WriteLine(message); }
    }
}