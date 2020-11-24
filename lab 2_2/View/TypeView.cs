using DataBase.Controller;
using DataBase.View.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Type = DataBase.Model.Type;

namespace DataBase.View
{
    public class TypeView : BaseView, IView
    {
        string connectionString = null;


        public TypeView(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Read()
        {
            List<Type> types = new TypeController(connectionString).Read("");
            foreach (var s in types)
            {
                Console.WriteLine($"name:\t {s.name}");
                Console.WriteLine($"description:\t {s.description}");
                Console.WriteLine();
            }
        }

        public void Create()
        {
            string name = null;
            string description = null;


            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Type properties:");
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (name.Length > 50)
                {
                    correct = false;
                    Console.WriteLine("Length of name > 50. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Description:");
                description = Console.ReadLine();


                correct = true;
            } while (correct == false);

            new TypeController(connectionString).Create(name, description);
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
            new TypeController(connectionString).Delete(id);
        }

        public void Find()
        {

            new TypeController(connectionString).Find(base.GetWhereCondition());
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
            new TypeController(connectionString).Update(updateString);
        }

        public void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);
            new TypeController(connectionString).Generate(recordsAmount);
        }
    }
}

