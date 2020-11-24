using DataBase.Controller.Base;
using Npgsql;
using System;
using System.Collections.Generic;
using DataBase.Model;
using System.Text;
using Type = DataBase.Model.Type;

namespace DataBase.Controller
{
    public class TypeController : BaseController
    {
        public TypeController(string connectionString) : base(connectionString) { }

        public List<Type> Read(string whereCondition)
        {
            return new Type(connectionString).Read(whereCondition);
        }

        public void Delete(int id)
        {
            new Type(connectionString).Delete(id);
        }

        public void Create(string name, string description)
        {
            new Type(connectionString).Create(name, description);
        }

        public void Update(string updateString)
        {
            new Transport(connectionString).Update(updateString);
        }
        public void Find(string whereCondition)
        {
            new Type(connectionString).Find(whereCondition);
        }

        public void Generate(int recordsAmount)
        {
            new Type(connectionString).Generate(recordsAmount);
        }
    }
}
