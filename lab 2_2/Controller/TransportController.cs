using DataBase.Controller.Base;
using DataBase.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class TransportController : BaseController
    {
        public TransportController(string connectionString) : base(connectionString) { }

        public List<Transport> Read(string whereCondition)
        {
            return new Transport(connectionString).Read(whereCondition);
        }

        public void Delete(int id)
        {
            new Transport(connectionString).Delete(id);
        }

        public void Create(int creation_year, int last_ti_date, int type, int route)
        {
            new Transport(connectionString).Create(creation_year, last_ti_date, type, route);
        }

        public void Update(string updateString)
        {
            new Transport(connectionString).Update(updateString);
        }
        public void Find(string whereCondition)
        {
            new Transport(connectionString).Find(whereCondition);
        }

        public void Generate(int recordsAmount)
        {
            new Transport(connectionString).Generate(recordsAmount);
        }
    }
}
