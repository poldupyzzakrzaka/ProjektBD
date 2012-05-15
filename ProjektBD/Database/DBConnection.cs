using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;

namespace ProjektBD.Database
{
    // wykorzystanie wzorca Singleton
    class DBConnection
    {
        private MySqlConnection conn;
        private static DBConnection instance;

        public MySqlConnection Conn { get { return conn; } }

        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBConnection();
                }
                return instance;
            }
        }

        private DBConnection()
        {
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string user = ConfigurationManager.AppSettings["user"];
            string password = ConfigurationManager.AppSettings["password"];

            string MyConString = "SERVER=" + server + ";" +
                                 "DATABASE=" + database + ";" +
                                 "UID=" + user + ";" +
                                 "PASSWORD=" + password + ";";
            try
            {
                conn = new MySqlConnection(MyConString);
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }

    }
}
