using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class TypeController : BaseController
    {
        public TypeController(string connectionString) : base(connectionString) { }


        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select type_id, name, description from type";


            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Id: {0}", rdr.GetValue(0));
                    Console.WriteLine("Type name: {0}", rdr.GetValue(1));
                    Console.WriteLine("Description: {0}", rdr.GetValue(2));
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
            base.Delete("delete from type where type_id = ");
        }


        public override void Create()
        {
            string sqlInsert = "Insert into type(name, description) VALUES(@name, @description)";


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


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("description", description);


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
            base.Update("Update type ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into type(name, description)  (select "
                + base.sqlRandomString
                + " , "
                + base.sqlRandomString
                + " from generate_series(1, 1000000) limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
