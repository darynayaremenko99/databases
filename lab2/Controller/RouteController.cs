using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class RouteController : BaseController
    {
        public RouteController(string connectionString) : base(connectionString) { }

        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select route_id, start_stop, finish_stop, number from route";

            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("First(start) stop id: {0}", rdr.GetValue(1));
                    Console.WriteLine("Last(finish) stop id: {0}", rdr.GetValue(2));
                    Console.WriteLine("Nuber of route: {0}", rdr.GetValue(3));
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
            base.Delete("delete from route where route_id = ");
        }

        public override void Create()
        {
            string sqlInsert = "Insert into route(start_stop, finish_stop, number) VALUES(@start_stop, @finish_stop, @number)";

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
                else if(start_stop <= 0)
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


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("start_stop", start_stop);
            cmd.Parameters.AddWithValue("finish_stop", finish_stop);
            cmd.Parameters.AddWithValue("number", number);
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
            base.Update("Update route ");
        }


        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount = 0;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            if(correct == false)
            {
                Console.WriteLine("Records amount must be a number!");
                Console.ReadLine();
            }

            string sqlGenerate = "insert into route(start_stop, finish_stop, number) (select stop_id, stop_id,"
                + base.sqlRandomInteger +
                " from generate_series(1, 1000000), stop limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
