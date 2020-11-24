using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class Transport : BaseModel
    {
        public object transport_id { get; set; }
        public object createion_year { get; set; }
        public object last_ti_date { get; set; }
        public object type { get; set; }
        public object route { get; set; }

        public Transport(string connectionString) : base(connectionString) { }

        public List<Transport> Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select transport_id, creation_year, last_ti_date, type, route from transport";

            List<Transport> list = new List<Transport>();

            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Transport transport = new Transport(connectionString);
                    transport.transport_id = rdr.GetValue(0);
                    transport.createion_year = rdr.GetValue(1);
                    transport.last_ti_date = rdr.GetValue(2);
                    transport.type = rdr.GetValue(3);
                    transport.route = rdr.GetValue(4);
                    list.Add(transport);
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

            return list;
        }


        public override void Delete(int id)
        {
            base.Delete("delete from transport where transport_id = ", id);
        }

        public void Create(int creation_year, int last_ti_date, int type, int route)
        {
            string sqlInsert = "Insert into transport(creation_year, last_ti_date, type, route) VALUES(@creation_year, @last_ti_date, @type, @route)";

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("creation_year", new DateTime(creation_year, 1, 1));
            cmd.Parameters.AddWithValue("last_ti_date", new DateTime(last_ti_date, 1, 1));
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


        public void Update(string updateString)
        {
            base.Update("Update transport " + updateString);
        }


        public void Generate(int recordsAmount)
        {

            string sqlGenerate = "insert into transport(creation_year, last_ti_date, type, route)  (select "
                + base.sqlRandomDate
                + " , "
                + base.sqlRandomDate
                + ", type_id, route_id from generate_series(1, 1000000), type, route limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
