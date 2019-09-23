using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary {
    public static class GlobalConfig {
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseType db) {

            if (db == DatabaseType.Sql) {
                // TODO - Set up the SQL Connector properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            } else if (db == DatabaseType.TextFile) {
                // TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        /// <summary>
        /// This will go to the App.config and give us the connection string by looking up the passed in name.
        /// </summary>
        /// <param name="name">The connection string name</param>
        /// <returns>The connection string value</returns>
        public static string CnnString(string name) {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
