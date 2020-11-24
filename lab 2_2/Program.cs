using DataBase.View;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=p;Database=CityTransport";
            MainMenu.Render(connectionString);
        }
    }
}
