
namespace SimpleTelegramBot
{
    class TextScrapper
    {
        public string getStringFromFile(string fileLocation)
        {
            string text = System.IO.File.ReadAllText(fileLocation);
            return text;
        }
    }
}
