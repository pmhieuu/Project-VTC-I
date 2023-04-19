using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Services
{
    public class Connection
    {
         public MySqlConnection connection = new MySqlConnection
            {
            ConnectionString = @"server=localhost;userid=root;password=hoangapache;port=3306;database=project_1_VTCA;"
            };
            
    }
}