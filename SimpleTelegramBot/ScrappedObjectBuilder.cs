using HtmlAgilityPack;

namespace SimpleTelegramBot
{
    public abstract class ScrappedObjectBuilder
    {
        public abstract void FilterPart(HtmlNode node);
        public abstract ScrappedObject buildScrappedObject();
    }
}
