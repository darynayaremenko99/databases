using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class Stop : BaseModel
    {
        public object stop_id { get; set; }
        public object name { get; set; }
        public object average_volume { get; set; }

        public Stop(string connectionString) : base(connectionString) { }


        public List<Stop> Read(string whereCondition)
        {
            sqlConnection.Open();

            string sqlSelect = "select stop_id, name, average_volume from stop" + whereCondition;

            List<Stop> list = new List<Stop>();
            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Stop stop = new Stop(connectionString);
                    stop.stop_id = rdr.GetValue(0);
                    stop.name = rdr.GetValue(1);
                    stop.average_volume = rdr.GetValue(2);
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

            return list;
        }


        public override void Delete(int id)
        {
            base.Delete("delete from stop where stop_id = ", id);
        }

        public void Create(string name, int average_volume)
        {
            string sqlInsert = "Insert into stop(name, average_volume) VALUES(@name, @average_volume)";

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


        public void Update(string updateString)
        {
            base.Update("Update stop " + updateString);
        }

        public void Generate(int recordsAmount)
        {

            string sqlGenerate = "insert into stop(name, average_volume) (select "
                + base.sqlRandomString
                + " , "
                + base.sqlRandomInteger
                + " from generate_series(1, 1000000), stop limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
