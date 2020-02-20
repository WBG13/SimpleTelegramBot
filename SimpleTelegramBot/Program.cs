using System;
using System.Net.NetworkInformation;

namespace SimpleTelegramBot
{
    class Program
    {
        public static void SimpleTelegramBot()
        {
            var aList = new ConnectionController().getScrappedAdresses(FilesIO.getStringFromFile("proxyListSite.txt", "E:\\"), "tbody/tr/td");

            for (int i = 0; i < aList.Count; i++)
            {
                Console.WriteLine($"Country: {aList[i].Country} \nAdress: {aList[i].ConnectionAdress} \nPort: {aList[i].Port}\n");
            }

            TelegramBotController telegram = new TelegramBotController(aList);

            telegram.connect();

            Console.ReadLine();
        }

        private static bool CanPing(string address)
        {
            Ping ping = new Ping();

            try
            {
                PingReply reply = ping.Send(address, 100000); //2000
                if (reply == null) return false;

                return (reply.Status == IPStatus.Success);
            }
            catch (PingException e)
            {
                return false;
            }
        }
    }
}
