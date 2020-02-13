
namespace SimpleTelegramBot
{
    public class ScrappedAdress : ScrappedObject
    {
        public string ConnectionAdress { get; private set; }
        public int Port { get; private set; }
        public string Country { get; private set; }

        public void setConnectionAdress(string connectionAdress)
        {
            this.ConnectionAdress = connectionAdress;
        }
        public void setPort(int port)
        {
            this.Port = port;
        }
        public void setCountry(string country)
        {
            this.Country = country;
        }
    }
}