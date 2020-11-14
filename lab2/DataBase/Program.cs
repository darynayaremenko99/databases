using DataBase.Controller;
using DataBase.Controller.Base;
using System;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=p;Database=CityTransport";

            int table = 0;
            int action = 0;
            do
            {
                table =  FirstMenu();
                if (table == 0)
                {
                    return;
                }

                BaseController controller = null;

                switch (table)
                {
                    case 1:
                        action = SecondMenu("Garage");
                        controller = new GarageController(connectionString);
                        break;
                    case 2:
                        action = SecondMenu("Route");
                        controller = new RouteController(connectionString);
                        break;
                    case 3:
                        action = SecondMenu("Stop");
                        controller = new StopController(connectionString);
                        break;
                    case 4:
                        action = SecondMenu("Transport");
                        controller = new TransportController(connectionString);
                        break;
                    case 5:
                        action = SecondMenu("Type");
                        controller = new TypeController(connectionString);
                        break;
                }


                switch(action)
                {
                    case 1:
                        controller.Create();
                        break;
                    case 2:
                        controller.Read();
                        break;
                    case 3:
                        controller.Update();
                        break;
                    case 4:
                        controller.Delete();
                        break;
                    case 5:
                        controller.Find();
                        break;
                    case 6:
                        controller.Generate();
                        break;
                }



            } while (true);
        }

        public static int FirstMenu()
        {
            var choice = 0;
            var correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose table you want to manipulate with:");
                Console.WriteLine("Enter table number in range 1-5 or 0 to exit:");
                Console.WriteLine("1.Garage");
                Console.WriteLine("2.Route");
                Console.WriteLine("3.Stop");
                Console.WriteLine("4.Transport");
                Console.WriteLine("5.Type");
                correct = Int32.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 5 || correct == false);


            return choice;
        }

        public static int SecondMenu(string tableToChange)
        {
            var choice = 0;
            var correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose what you want to do with '" + tableToChange + "' table:");
                Console.WriteLine("Enter number in range 1-6 or 0 to exit:");
                Console.WriteLine("1.Create");
                Console.WriteLine("2.Read");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
                Console.WriteLine("5.Find");
                Console.WriteLine("6.Generate");
                correct = Int32.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 6 || correct == false);


            return choice;
        }
    }
}
