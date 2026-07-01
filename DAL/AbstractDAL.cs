using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public abstract class AbstractDAL<T>where T : class
    {
        protected SqlConnection _sqlserver;
        protected SqlCommand _sqlcommand;
        
        protected AbstractDAL()
        {
            _sqlserver = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Alejandria_DB;Integrated Security=True;TrustServerCertificate=True");
            _sqlcommand = new SqlCommand();
            _sqlcommand.Connection = _sqlserver;
            _sqlcommand.CommandType = System.Data.CommandType.Text;
        }
    }
}
