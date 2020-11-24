using DataBase.Controller.Base;
using DataBase.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class StopController : BaseController
    {

        public StopController(string connectionString) : base(connectionString) { }


        
        public List<Stop> Read(string whereCondition)
        {
            return new Stop(connectionString).Read(whereCondition);
        }

        public void Delete(int id)
        {
            new Stop(connectionString).Delete(id);
        }

        public void Create(string address, int transport)
        {
            new Stop(connectionString).Create(address, transport);
        }

        public void Update(string updateString)
        {
            new Stop(connectionString).Update(updateString);
        }
        public void Find(string whereCondition)
        {
            new Stop(connectionString).Find(whereCondition);
        }

        public  void Generate(int recordsAmount)
        {
            new Stop(connectionString).Generate(recordsAmount);
        }
    }
}
