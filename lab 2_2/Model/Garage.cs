using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class Garage : BaseModel
    {
        public object garage_id { get; set; }
        public object transport { get; set; }
        public object address { get; set; }


        public Garage(string connectionString) : base(connectionString) { }

        public List<Garage> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select garage_id, transport, address from garage" + whereCondition;


            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            List<Garage> garages = new List<Garage>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var garage = new Garage(connectionString);
                    garage.garage_id = rdr.GetValue(0);
                    garage.transport = rdr.GetValue(1);
                    garage.address = rdr.GetValue(2);
                    garages.Add(garage);
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

            return garages;
        }

        public void Delete(int id)
        {
            base.Delete("delete from garage where garage_id = ", id);
        }

        public void Update(string updateString)
        {
            base.Update("Update garage " + updateString);
        }
        public void Create(string address, int transport)
        {
            string sqlInsert = "Insert into garage(address, transport) VALUES(@address, @transport)";
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

        public void Find(string whereCondition)
        {
            base.Find(whereCondition);
        }

        public void Generate(int recordsAmount)
        {
            string sqlGenerate = "insert into garage(address) (select "
                + base.sqlRandomString +
                " from generate_series(1, 1000000)  limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
