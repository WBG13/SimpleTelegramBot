using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTelegramBot.BL
{
    class ConnectionController
    {
        public List<ScrappedAdress> getScrappedAdresses(string siteAdress, string tagPath){
            string[] splitedTags = splitTags(tagPath);

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(siteAdress);

            StringBuilder sb = new StringBuilder();
            List<ScrappedAdress> aList = new List<ScrappedAdress>();
            HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes("//" + splitedTags[splitedTags.Length - 3]);

            Console.WriteLine(splitedTags[splitedTags.Length - 2]);
            Console.WriteLine(splitedTags[splitedTags.Length - 1]);

            foreach (var tagNode in node.Descendants(splitedTags[splitedTags.Length - 2])){
                ScrappedAdressBuilder sab = new ScrappedAdressBuilder();
                foreach (var tag in tagNode.Descendants(splitedTags[splitedTags.Length - 1]))
                {
                    if (tag.NodeType == HtmlNodeType.Element)
                    {
                        sab.FilterPart(tag);
                    }
                }
                aList.Add((ScrappedAdress)sab.buildScrappedObject());
            }
            return aList;
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

        private string[] splitTags(string tags) {
            return tags.Split('/');
        }

        private string getTagPath(string[] path) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < path.Length - 1; i++) {
                sb.Append(i == (path.Length - 1) ? path[i] : "//" + path[i]);
            }
            return sb.ToString();
        }
    }
}
