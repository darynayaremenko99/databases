using DataBase.Controller;
using DataBase.Model;
using DataBase.View.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View
{
    public class TransportView : BaseView , IView
    {
        string connectionString = null;


        public TransportView(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Read()
        {
            List<Transport> transports = new TransportController(connectionString).Read("");
            foreach (var s in transports)
            {
                Console.WriteLine($"creation year:\t {s.createion_year}");
                Console.WriteLine($"last TI date:\t {s.last_ti_date}");
                Console.WriteLine($"route:\t {s.route}");
                Console.WriteLine($"type: \t {s.type}");
                Console.WriteLine();
            }
        }

        public void Create()
        {


            int creation_year = 0;
            int last_ti_date = 0;
            int type = 0;
            int route = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Transport properties:");
                Console.WriteLine("Creation year:");
                correct = Int32.TryParse(Console.ReadLine(), out creation_year);
                if (correct == false)
                {
                    Console.WriteLine("Creation year must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Last TI Date (year):");
                correct = Int32.TryParse(Console.ReadLine(), out last_ti_date);
                if (correct == false)
                {
                    Console.WriteLine("Last TI Date must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Type (id):");
                correct = Int32.TryParse(Console.ReadLine(), out type);
                if (correct == false)
                {
                    Console.WriteLine("Type must be a number...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Route (id):");
                correct = Int32.TryParse(Console.ReadLine(), out route);
                if (correct == false)
                {
                    Console.WriteLine("Route must be a number...");
                    Console.ReadLine();
                    continue;
                }


                correct = true;
            } while (correct == false);

            new TransportController(connectionString).Create(creation_year, last_ti_date, type, route);
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
            new TransportController(connectionString).Delete(id);
        }

        public void Find()
        {

            new TransportController(connectionString).Find(base.GetWhereCondition());

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
            new TransportController(connectionString).Update(updateString);
        }

        public void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);
            new TransportController(connectionString).Generate(recordsAmount);
        }
    }
}

