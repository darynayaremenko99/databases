using DataBase.Controller;
using DataBase.Controller.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View
{
    public static class MainMenu
    {
        public static void Render(string connectionString)
        {

            int table = 0;
            int action = 0;
            do
            {
                table = FirstMenu();
                if (table == 0)
                {
                    return;
                }

                IView view = null;
                BaseController controller = null;

                switch (table)
                {
                    case 1:
                        action = SecondMenu("Garage");
                        view = new GarageView(connectionString);
                        controller = new GarageController(connectionString);
                        break;
                    case 2:
                        action = SecondMenu("Route");
                        view = new RouteView(connectionString);
                        controller = new RouteController(connectionString);
                        break;
                    case 3:
                        action = SecondMenu("Stop");
                        view = new StopView(connectionString);
                        controller = new StopController(connectionString);
                        break;
                    case 4:
                        action = SecondMenu("Transport");
                        view = new TransportView(connectionString);
                        controller = new TransportController(connectionString);
                        break;
                    case 5:
                        action = SecondMenu("Type");
                        view = new TypeView(connectionString);
                        controller = new TypeController(connectionString);
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }


                switch (action)
                {
                    case 1:
                        view.Create();
                        break;
                    case 2:
                        view.Read();
                        break;
                    case 3:
                        view.Update();
                        break;
                    case 4:
                        view.Delete();
                        break;
                    case 5:
                        view.Find();
                        break;
                    case 6:
                        view.Generate();
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }



            } while (true);
        }

        public static int FirstMenu()
        {
            var choice = 0;

            Console.WriteLine("Choose table you want to manipulate with:");
            Console.WriteLine("Enter table number in range 1-5 or 0 to exit:");
            Console.WriteLine("1.Garage");
            Console.WriteLine("2.Route");
            Console.WriteLine("3.Stop");
            Console.WriteLine("4.Transport");
            Console.WriteLine("5.Type");
            Int32.TryParse(Console.ReadLine(), out choice);

            return choice;
        }

        public static int SecondMenu(string tableToChange)
        {
            var choice = 0;

            Console.WriteLine("Choose what you want to do with '" + tableToChange + "' table:");
            Console.WriteLine("Enter number in range 1-6 or 0 to exit:");
            Console.WriteLine("1.Create");
            Console.WriteLine("2.Read");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Delete");
            Console.WriteLine("5.Find");
            Console.WriteLine("6.Generate");
            Int32.TryParse(Console.ReadLine(), out choice);

            return choice;
        }
    }
}
