using DataBase.Controller.Base;
using DataBase.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;  

namespace DataBase.Controller
{
    public class GarageController : BaseController
    {
        public GarageController(string connectionString) : base(connectionString) { }
        
        public List<Garage> Read(string whereCondition)
        {
            return new Garage(connectionString).Read(whereCondition);
        }

        public void Delete(int id)
        {
            new Garage(connectionString).Delete(id);
        } 

        public void Create(string address, int transport)
        {
            new Garage(connectionString).Create(address, transport);
        }

        public void Update(string updateString)
        {
            new Garage(connectionString).Update(updateString);
        }

        public void Find(string whereCondition)
        {
            new Garage(connectionString).Find(whereCondition);
        }

        public void Generate(int recordsAmount)
        {
            new Garage(connectionString).Generate(recordsAmount);
        }
    }
}
