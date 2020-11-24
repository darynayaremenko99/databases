using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Controller.Base
{
    public class BaseController
    {
        protected string connectionString;
        public BaseController(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
