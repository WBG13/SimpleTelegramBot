using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;

namespace SimpleTelegramBot
{
    class Scrapper : IScrapper
    {
        private string Adress { get; set; }

        public Scrapper(string Adress)
        {
            this.Adress = Adress;
        }

        public List<string> scrap(string pathToTheTag, string targetedTag)
        {

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(Adress);
            HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes(pathToTheTag);

            StringBuilder sb = new StringBuilder();
            List<string> aList = new List<string>();

            foreach (var nNode in node.Descendants(targetedTag))
            {
                if (nNode.NodeType == HtmlNodeType.Element)
                {
                    aList.Add(nNode.InnerHtml);
                }
            }
            return aList;
        }
    }
}