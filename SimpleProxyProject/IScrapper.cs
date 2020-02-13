using System.Collections.Generic;

namespace SimpleTelegramBot.BL
{
    abstract class IScrapper
    {
        string Adress { get; set; }
        string Port { get; set; }

        List<string> scrap(ScrappedObject sObject) {
            throw new System.NotImplementedException();
        }
    }
}
