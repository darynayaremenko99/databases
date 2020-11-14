using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class TransportController : BaseController
    {
        public TransportController(string connectionString) : base(connectionString) { }

        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select transport_id, creation_year, last_ti_date, type, route from transport";


            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("Creation year: {0}", rdr.GetValue(1));
                    Console.WriteLine("Last TI date: {0}", rdr.GetValue(2));
                    Console.WriteLine("Type (id): {0}", rdr.GetValue(3));
                    Console.WriteLine("Route (id): {0}", rdr.GetValue(4));
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
            base.Delete("delete from transport where transport_id = ");
        }

        public override void Create()
        {
            string sqlInsert = "Insert into transport(creation_year, last_ti_date, type, route) VALUES(@creation_year, @last_ti_date, @type, @route)";


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


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("creation_year", new DateTime(creation_year, 1, 1));
            cmd.Parameters.AddWithValue("last_ti_date", new DateTime(last_ti_date,1,1));
            cmd.Parameters.AddWithValue("type", type);
            cmd.Parameters.AddWithValue("route", route);
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
            base.Update("Update transport ");
        }


        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into transport(creation_year, last_ti_date, type, route)  (select "
                + base.sqlRandomDate
                + " , "
                + base.sqlRandomDate
                + ", type_id, route_id from generate_series(1, 1000000), type, route limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
