using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TT_Common
{
    public class ConnectionString
    {
        public static string GetConnectionString()
        {
            string connection = string.Empty;
            connection = ConfigurationManager.ConnectionStrings["TrackingToolDB"].ConnectionString;
            return connection;
        }
    }
}
