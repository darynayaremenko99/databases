using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class Type : BaseModel
    {
        public object type_id { get; set; }
        public object name { get; set; }
        public object description { get; set; }

        public Type(string connectionString) : base(connectionString) { }

        public List<Type> Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select type_id, name, description from type";
            List<Model.Type> list = new List<Type>();

            using var cmd = new NpgsqlCommand(sqlSelect, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Model.Type type = new Model.Type(connectionString);
                    type.type_id = rdr.GetValue(0);
                    type.name = rdr.GetValue(1);
                    type.description = rdr.GetValue(2);
                    list.Add(type);
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
            base.Delete("delete from type where type_id = ", id);
        }


        public void Create(string name, string description)
        {
            string sqlInsert = "Insert into type(name, description) VALUES(@name, @description)";

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

        public void Generate(int recordsAmount)
        {
           
            string sqlGenerate = "insert into type(name, description)  (select "
                + base.sqlRandomString
                + " , "
                + base.sqlRandomString
                + " from generate_series(1, 1000000) limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
