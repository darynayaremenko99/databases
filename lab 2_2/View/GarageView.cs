using DataBase.Controller;
using DataBase.Model;
using DataBase.View.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.View
{
    public class GarageView : BaseView, IView 
    {
        string connectionString = null;
        public GarageView(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Read()
        {
            List<Garage> garages = new GarageController(connectionString).Read("");
            foreach(var g in garages)
            {
                Console.WriteLine($"garage_id:\t {g.garage_id}");
                Console.WriteLine($"transport:\t {g.transport}");
                Console.WriteLine($"address:\t {g.address}");
                Console.WriteLine();
            }
        }

        public void Create()
        {

            string address = null;
            int transport = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Garage properties:");
                Console.WriteLine("Address:");
                address = Console.ReadLine();
                if (address.Length > 100)
                {
                    correct = false;
                    Console.WriteLine("Length of address > 100. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Transport (id):");
                correct = Int32.TryParse(Console.ReadLine(), out transport);
                if (correct == false)
                {
                    Console.WriteLine("Tranposport (id) must be a number...");
                    Console.ReadLine();
                    continue;
                }

                correct = true;
            } while (correct == false);



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
            new GarageController(connectionString).Delete(id);
        }

        public void Find()
        {
            new GarageController(connectionString).Find(base.GetWhereCondition());
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
            new GarageController(connectionString).Update(updateString);
        }

        public void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            new GarageController(connectionString).Generate(recordsAmount);
        }
    }
}
