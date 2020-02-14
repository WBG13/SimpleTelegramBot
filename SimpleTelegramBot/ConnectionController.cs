using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTelegramBot
{
    class ConnectionController
    {
        public List<ScrappedAdress> getScrappedAdresses(string siteAdress, string tagPath)
        {
            string[] splitedTags = splitTags(tagPath);

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(siteAdress);

            StringBuilder sb = new StringBuilder();
            List<ScrappedAdress> aList = new List<ScrappedAdress>();
            HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes("//" + splitedTags[splitedTags.Length - 3]);

            for (int i = 0; i < aList.Count; i++)
            {
                Console.WriteLine($"Country: {aList[i].Country} \nAdress: {aList[i].ConnectionAdress} \nPort: {aList[i].Port}\n");
            }

            foreach (var tagNode in node.Descendants(splitedTags[splitedTags.Length - 2]))
            {
                ScrappedAdressBuilder sab = new ScrappedAdressBuilder();
                foreach (var tag in tagNode.Descendants(splitedTags[splitedTags.Length - 1]))
                {
                    if (tag.NodeType == HtmlNodeType.Element)
                    {
                        sab.FilterPart(tag);
                    }
                }
                ScrappedAdress sa = (ScrappedAdress)sab.buildScrappedObject();
                if (sa.Country == "") { aList.Add(sa); } //TODO find error
                aList.Add(sa);
            }
            return aList;
        }

        private string stringQualifier(string request)
        {
            int integerTest;
            if (request.Length == 2 && request != "no" && request != "RU")
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

        private string[] splitTags(string tags)
        {
            return tags.Split('/');
        }

        private string getTagPath(string[] path)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < path.Length - 1; i++)
            {
                sb.Append(i == (path.Length - 1) ? path[i] : "//" + path[i]);
            }
            return sb.ToString();
        }
    }
}
