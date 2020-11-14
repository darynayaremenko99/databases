using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;  

namespace DataBase.Controller
{
    public class GarageController : BaseController
    {
        public GarageController(string connectionString) : base(connectionString) { }
        
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select garage_id, address, transport from garage";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("Adress: {0}", rdr.GetValue(1));
                    Console.WriteLine("Transport Id: {0}", rdr.GetValue(2));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }


            Console.ReadLine();
        }

        public override void Delete()
        {
            base.Delete("delete from garage where garage_id = ");
        } 

        public override void Create()
        {
            string sqlInsert = "Insert into garage(address, transport) VALUES(@address, @transport)";

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


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("address", address);
            cmd.Parameters.AddWithValue("transport", transport);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public override void Update()
        {
            base.Update("Update garage ");
        }

        public override void Find()
        {
            base.Find();
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into garage(address) (select " 
                + base.sqlRandomString + 
                " from generate_series(1, 1000000)  limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
