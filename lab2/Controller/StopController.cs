using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class StopController : BaseController
    {

        public StopController(string connectionString) : base(connectionString) { }


        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select stop_id, name, average_volume from stop";


            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("Name: {0}", rdr.GetValue(1));
                    Console.WriteLine("Average volume: {0}", rdr.GetValue(2));
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
            base.Delete("delete from stop where stop_id = ");
        }

        public override void Create()
        {
            string sqlInsert = "Insert into stop(name, average_volume) VALUES(@name, @average_volume)";

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


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("average_volume", average_volume);
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
            base.Update("Update stop ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into stop(name, average_volume) (select "
                + base.sqlRandomString 
                + " , "
                + base.sqlRandomInteger 
                + " from generate_series(1, 1000000), stop limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
