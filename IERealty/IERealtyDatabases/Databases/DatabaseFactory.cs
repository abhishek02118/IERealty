using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IERealtyDatabases.Databases.SqlServer;

namespace IERealtyDatabases.Databases
{

    public enum DbType
    {
        SqlServer
    }
    public class DatabaseFactory
    {
        public static IDbManager GetDatbase(DbType dbName)
        {
            if (dbName == DbType.SqlServer)
            {
                return new SqlServerDb();
            }
            return null;
        }
    }
}
