namespace SimpleTelegramBot
{
    class TextScrapper
    {
        public string getStringFromFile(string fileLocation)
        {
            return System.IO.File.ReadAllText(fileLocation);
        }
    }
}
