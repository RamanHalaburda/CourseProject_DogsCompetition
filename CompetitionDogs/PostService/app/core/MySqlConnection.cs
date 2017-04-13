using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using AbstractDB;
using MySql.Data.MySqlClient;

namespace CompetitionDog
{
    class MySqlConnection : AbstractDBConnection
    {
        private MySqlConnection connection = null;

        public MySqlConnection(string user, string password, string database, string server) : base(user, password, database, server) { }

        public override void initialConnection()
        {
            
            //connectionString = string.Format("Data Source={2}; Persist Security Info=True; User ID={1}; Password={0};",PASSWORD,DATABASE,SERVER);
        }

        public override void open()
        {
            try
            {
                //connection = new OracleConnection(connectionString);
                connection = new MySqlConnection( ("datasourse=127.0.0.1;port=3306;username=root;password=1656");
                connection.Open();
            }
            catch (MySqlException)
            {
                throw new CompetitionDogException("Ошибка соединения с базой данных");
            }
        }

        public override void close()
        {
            connection.Close();
        }

        public MySqlConnection get()
        {
            return connection;
        }

    }
}
