using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleTelegramBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            TextScrapper tx = new TextScrapper();
            var aList = new ConnectionController().getScrappedAdresses(tx.getStringFromFile(@"E:\proxyListSite.txt"), "tbody/tr/td");

            /*
            for (int i = 0; i < aList.Count; i++)
            {
                Console.WriteLine($"Country: {aList[i].Country} \nAdress: {aList[i].ConnectionAdress} \nPort: {aList[i].Port}\n");
            }*/ //TODO Check empty IP

            TelegramBotController telegram = new TelegramBotController(aList);

            telegram.connect();
        }
    }
}
