using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class Route : BaseModel
    {
        public object route_id { get; set; }
        public object start_stop { get; set; }
        public object finish_stop { get; set; }
        public object number { get; set; }


        public Route(string connectionString) : base(connectionString) { }

        public List<Route> Read(string whereCondition)
        {
            sqlConnection.Open();

            string sqlSelect = "select route_id, start_stop, finish_stop, number from route " + whereCondition;

            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            List<Route> routes = new List<Route>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var route = new Route(connectionString);
                    route.route_id = rdr.GetValue(0);
                    route.start_stop = rdr.GetValue(1);
                    route.finish_stop = rdr.GetValue(2);
                    route.number = rdr.GetValue(3);
                    routes.Add(route);
                }

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


            return routes;
        }

        public override void Delete(int id)
        {
            base.Delete("delete from route where route_id = ", id);
        }

        public void Create(int start_stop, int finish_stop, int number)
        {
            string sqlInsert = "Insert into route(start_stop, finish_stop, number) VALUES(@start_stop, @finish_stop, @number)";

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


        public void Update(string updateString)
        {
            base.Update("Update route " + updateString);
        }


        public void Generate(int recordsAmount)
        {
          
            string sqlGenerate = "insert into route(start_stop, finish_stop, number) (select stop_id, stop_id,"
                + base.sqlRandomInteger +
                " from generate_series(1, 1000000), stop limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
