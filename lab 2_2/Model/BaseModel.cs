using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Model
{
    public class BaseModel
    {
        public string connectionString;
        protected NpgsqlConnection sqlConnection;

        string[] fieldsToFind = new string[10];
        string[] valuesToFind = new string[10];


        public readonly string sqlUpdate = "Update @table set @field_to_update = @new_value where @field_to_find = @old_value";
        public readonly string sqlRandomString = "chr(trunc(65 + random() * 50)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int)";
        public readonly string sqlRandomInteger = "trunc(random()*1000)::int";
        public readonly string sqlRandomDate = "timestamp '2014-01-10 20:00:00' + random() * (timestamp '2014-01-20 20:00:00' - timestamp '2014-01-10 10:00:00')";
        public readonly string sqlRandomBoolean = "trunc(random()*2)::int::boolean";


        public BaseModel(string connectionString)
        {
            this.connectionString = connectionString;
            this.sqlConnection = new NpgsqlConnection(connectionString);
        }


        public virtual void Create()
        {
            throw new NotImplementedException();
        }
        public void Read()
        {
            Read("");
        }
        public virtual void Update()
        {
            throw new NotImplementedException();
        }
        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public virtual void Find(string whereCondition)
        {
            Read(whereCondition);
        }
        virtual public void Generate()
        {
            throw new NotImplementedException();
        }

        virtual public void Read(string whereCondition)
        {

        }

        protected void Delete(string sqlDelete, int id)
        {


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlDelete + id, sqlConnection);


            try
            {
                cmd.Prepare();
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


        protected void Update(string sqlUpdate)
        {



            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlUpdate, sqlConnection);

            try
            {
                cmd.Prepare();
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

        protected void Generate(string sqlGenerate)
        {

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlGenerate, sqlConnection);

            try
            {
                cmd.Prepare();
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
    }
}
