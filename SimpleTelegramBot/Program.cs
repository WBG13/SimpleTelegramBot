using System.IO;
using System;
using System.Net.NetworkInformation;

namespace SimpleTelegramBot
{
    class Program
    {
        public static void SimpleTelegramBot()
        {
            TextScrapper tx = new TextScrapper();
            var aList = new ConnectionController().getScrappedAdresses(tx.getStringFromFile(@"E:\proxyListSite.txt"), "tbody/tr/td");



            for (int i = 0; i < aList.Count; i++)
            {
                Console.WriteLine($"Country: {aList[i].Country} \nAdress: {aList[i].ConnectionAdress} \nPort: {aList[i].Port}\n");
            }

            TelegramBotController telegram = new TelegramBotController(aList);

            telegram.connect();

            Console.ReadLine();
        }


        private static void Save(string name, string savingText)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, name)))
            {
                outputFile.WriteLine(savingText);
            }
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
