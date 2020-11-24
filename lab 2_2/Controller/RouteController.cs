using DataBase.Controller.Base;
using DataBase.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller
{
    public class RouteController : BaseController
    {
        public RouteController(string connectionString) : base(connectionString) { }


        public List<Route> Read(string whereCondition)
        {
            return new Route(connectionString).Read(whereCondition);
        }

        public void Delete(int id)
        {
            new Route(connectionString).Delete(id);
        }

        public void Create(int start_stop, int finish_stop, int number)
        {
            new Route(connectionString).Create(start_stop, finish_stop, number);
        }


        public void Update(string updateString)
        {
            new Route(connectionString).Update(updateString);
        }

        public void Find(string whereCondition)
        {
            new Route(connectionString).Find(whereCondition);
        }

        public void Generate(int recordsAmount)
        {
            new Route(connectionString).Generate(recordsAmount);
        }
    }
}
