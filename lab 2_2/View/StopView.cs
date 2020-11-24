using DataBase.Controller;
using DataBase.Model;
using DataBase.View.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View
{
    public class StopView : BaseView, IView
    {
        string connectionString = null;


        public StopView(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Read()
        {
            List<Stop> stops = new StopController(connectionString).Read("");
            foreach (var s in stops)
            {
                Console.WriteLine($"stop_id:\t {s.stop_id}");
                Console.WriteLine($"name:\t {s.name}");
                Console.WriteLine($"average_volume:\t {s.average_volume}");
                Console.WriteLine();
            }

        }

        public void Create()
        {

            string name;
            int average_volume = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Stop properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    correct = false;
                    Console.WriteLine("Length of name > 100. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Average volume:");
                correct = Int32.TryParse(Console.ReadLine(), out average_volume);
                if (correct == false)
                {
                    Console.WriteLine("average_volume must be a number...");
                    Console.ReadLine();
                    continue;
                }
                correct = true;
            } while (correct == false);
            new StopController(connectionString).Create(name, average_volume);
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
            new StopController(connectionString).Delete(id);

        }

        public void Find()
        {

            new StopController(connectionString).Find(base.GetWhereCondition());
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
            new StopController(connectionString).Update(updateString);
        }

        public void Generate()
        {

            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);
            new StopController(connectionString).Generate(recordsAmount);
        }
    }
}

