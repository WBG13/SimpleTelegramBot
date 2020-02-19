using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleTelegramBot
{
    class FilesIO
    {
        static string defaultFileLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static void saveList(string fileName, List<string> savingText, string filePath = null)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath ?? defaultFileLocation, fileName)))
            {
                outputFile.WriteLine(buildStringFromList(savingText));
            }
        }

        public static void saveString(string fileName, string savingText, string filePath = null)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath ?? defaultFileLocation, fileName)))
            {
                outputFile.WriteLine(savingText);
            }
        }

        public static string getStringFromFile(string fileName, string filePath)
        {
            return File.ReadAllText(Path.Combine(filePath ?? defaultFileLocation, fileName));
        }

        public static void serialize(string fileName, ScrappedObject so, string filePath = null) {
            new JsonSerializer().Serialize(File.CreateText(Path.Combine(filePath ?? defaultFileLocation, fileName)), so);
        }

        public static void serializeList(string fileName, List<ScrappedAdress> so, string filePath = null)
        {
            //StringWriter sw = new StringWriter(new StringBuilder()); 
            string serObj = JsonConvert.SerializeObject(so);
            saveString(fileName, serObj, filePath);
        }

        private static string buildStringFromList(List<string> text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var textPart in text) sb.Append(textPart + "\n");
            return sb.ToString();
        }
    }
}