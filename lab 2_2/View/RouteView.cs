using DataBase.Controller;
using DataBase.Model;
using DataBase.View.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View
{
    public class RouteView : BaseView, IView
    {
        string connectionString = null;


        public RouteView(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Read()
        {
            List<Route> routes = new RouteController(connectionString).Read("");
            foreach (var i in routes)
            {
                Console.WriteLine($"route_id:\t {i.route_id}");
                Console.WriteLine($"start_stop:\t {i.start_stop}");
                Console.WriteLine($"finish_stop:\t {i.finish_stop}");
                Console.WriteLine($"number:\t {i.number}");
                Console.WriteLine();
            }
        }

        public void Create()
        {

            int start_stop = 0;
            int finish_stop = 0;
            int number = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Route properties:");
                Console.WriteLine("Start (first) stop:");
                correct = Int32.TryParse(Console.ReadLine(), out start_stop);
                if (correct == false)
                {
                    Console.WriteLine("Start stop must be a number...");
                    Console.ReadLine();
                    continue;
                }
                else if (start_stop <= 0)
                {
                    Console.WriteLine("Start stop must be greater than 0");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Finish (last) stop:");
                correct = Int32.TryParse(Console.ReadLine(), out finish_stop);
                if (correct == false)
                {
                    Console.WriteLine("Finish stop must be a number...");
                    Console.ReadLine();
                    continue;
                }
                else if (finish_stop <= 0)
                {
                    Console.WriteLine("Finish stop must be greater than 0");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Number:");
                correct = Int32.TryParse(Console.ReadLine(), out number);
                if (correct == false)
                {
                    Console.WriteLine("Number must be a number...");
                    Console.ReadLine();
                    continue;
                }
                else if (finish_stop <= 0)
                {
                    Console.WriteLine("Number must be greater than 0");
                    Console.ReadLine();
                    continue;
                }


                correct = true;
            } while (correct == false);

            new RouteController(connectionString).Create(start_stop, finish_stop, number);
        }

        public void Delete()
        {
            bool correct = false;
            int id = 0;
            do
            {
                Console.WriteLine("Enter number of record you want to delete (or 0 to step back):");
                correct = Int32.TryParse(Console.ReadLine(), out id);
                if (correct == false)
                {
                    Console.WriteLine("Id must be a number...");
                    Console.ReadLine();
                    continue;
                }
            } while (correct == false || id < 0);
            new RouteController(connectionString).Delete(id);
        }

        public void Find()
        {

            new RouteController(connectionString).Find(base.GetWhereCondition());
        }

        public void Update()
        {

            string sqlUpdate, fieldToSet, valueToSet, fieldToFind, valueToFind;
            Console.Clear();
            Console.WriteLine("Enter name of field you want to find:");
            fieldToFind = Console.ReadLine();
            Console.WriteLine("Enter value in this field you want to find:");
            valueToFind = Console.ReadLine();


            Console.WriteLine("Enter name of field you want to change:");
            fieldToSet = Console.ReadLine();
            Console.WriteLine("Enter new value in this field");
            valueToSet = Console.ReadLine();

            int ParseInt = 0;
            if (Int32.TryParse(valueToFind, out ParseInt) == false)
            {
                valueToFind = "'" + valueToFind + "'";
            }
            if (Int32.TryParse(valueToSet, out ParseInt) == false)
            {
                valueToSet = "'" + valueToSet + "'";
            }
            string updateString = "set " + fieldToSet + " = " + valueToSet + " where " + fieldToFind + " = " + valueToFind;
            new RouteController(connectionString).Update(updateString);

        }

        public void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);
            new RouteController(connectionString).Generate(recordsAmount);
        }
    }
}

