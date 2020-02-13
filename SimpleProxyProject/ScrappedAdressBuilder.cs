using HtmlAgilityPack;
using System;

namespace SimpleTelegramBot
{
    class ScrappedAdressBuilder : ScrappedObjectBuilder
    {
        public string ConnectionAdress { get; private set; }
        public int Port { get; private set; }
        public string Code { get; private set; }
        public string Country { get; private set; }
        public string Socks { get; private set; }

        public override ScrappedObject buildScrappedObject()
        {
            ScrappedAdress sa = new ScrappedAdress();
            sa.setCountry(Country);
            sa.setPort(Port);
            sa.setConnectionAdress(ConnectionAdress);
            return sa;
        }

        public override void FilterPart(HtmlNode node)
        {
            switch (stringQualifier(node.InnerHtml))
            {
                case ("Adress"):
                    ConnectionAdress = node.InnerHtml;
                    break;
                case ("Port"):
                    int i = 0;
                    if (!Int32.TryParse(node.InnerHtml, out i))
                    {
                        i = -1;
                    }
                    Port = i;
                    break;
                case ("Country"):
                    Country = node.InnerHtml;
                    break;
                case ("Code"):
                    Code = node.InnerHtml;
                    break;
            }
        }

        private string stringQualifier(string request)
        {
            int integerTest;
            if (request.Length == 2 && request != "no")
            {
                return "Country";
            }
            else if (request.Split('.').Length - 1 == 3)
            {
                return "Adress";
            }
            else if (int.TryParse(request, out integerTest))
            {
                return "Port";
            }
            else return "";
        }
    }
}
