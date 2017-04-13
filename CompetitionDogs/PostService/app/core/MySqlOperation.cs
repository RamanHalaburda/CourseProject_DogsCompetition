using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using AbstractDB;
using MySql.Data.MySqlClient;

namespace CompetitionDog
{
    class OracleDBOperation : AbstractDBOperation
    {
        private MySqlCommand cmd = null;
        private OracleDBConnection connection = null;

        public override void update(AbstractEntity entity)
        {
            try
            {
                connection.open();
                using (cmd = new MySqlCommand(entity.queryUpdate(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции обновления");
            }
            finally
            {
                connection.close();
            }
        }

        public override void update()
        {
            try
            {
                connection.open();
                using (cmd = new OracleCommand(entity.queryUpdate(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции обновления!");
            }
            finally
            {
                connection.close();
            }
        }

        public override void insert(AbstractEntity entity)
        {
            try
            {
                connection.open();
                using (cmd = new OracleCommand(entity.queryInsert(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции добавления!");
            }
            finally
            {
                connection.close();
            }

        }

        public override void insert()
        {
            try
            {
                connection.open();
                using (cmd = new OracleCommand(entity.queryInsert(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции добавления");
            }
            finally
            {
                connection.close();
            }

        }

        public override void delete(AbstractEntity entity)
        {
            try
            {
                connection.open();
                using (cmd = new OracleCommand(entity.queryDelete(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции удаления");
            }
            finally
            {
                connection.close();
            }
        }

        public override void delete()
        {
            try
            {
                connection.open();
                using (cmd = new OracleCommand(entity.queryDelete(), connection.get()))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции удаления");
            }
            finally
            {
                connection.close();
            }
        }

        public override DataTable search(AbstractEntity entity)
        {
            try
            {
                DataTable dt = new DataTable();
                OracleDataReader rd = null;
                connection.open();
                using (cmd = new OracleCommand(entity.querySearch(), connection.get()))
                {
                    rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }
                return dt;
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции поиска данных");
            }
            finally
            {
                connection.close();
            }
        }

        public override DataTable select(AbstractEntity entity)
        {
            try
            {
                DataTable dt = new DataTable();
                OracleDataReader rd = null;
                connection.open();
                using (cmd = new OracleCommand(entity.querySelect(), connection.get()))
                {
                    rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }
                return dt;
            }
            catch (OracleException ex)
            {
                throw new CompetitionDogException("Ошибка операции получения данных!");
            }
            finally
            {
                connection.close();
            }
        }

        public override DataTable select()
        {
            try
            {
                DataTable dt = new DataTable();
                OracleDataReader rd = null;
                connection.open();
                using (cmd = new OracleCommand(entity.querySelect(), connection.get()))
                {
                    rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }
                return dt;
            }
            catch (OracleException)
            {
                throw new CompetitionDogException("Ошибка операции получения данных");
            }
            finally
            {
                connection.close();
            }

        }

        public override DataTable select(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                OracleDataReader rd = null;
                connection.open();
                using (cmd = new OracleCommand(query, connection.get()))
                {
                    rd = cmd.ExecuteReader();
                    dt.Load(rd);
                }
                return dt;
            }
            catch (OracleException ex)
            {
                throw new CompetitionDogException("Ошибка операции получения данных!"+ ex.Message);
            }
            finally
            {
                connection.close();
            }
        }

        public OracleDBOperation(AbstractDBConnection con) : base(con)
        {
            this.connection = (OracleDBConnection)con;
        }

        public OracleDBOperation(AbstractDBConnection con, AbstractEntity entity) : base(con, entity)
        {
            this.connection = (OracleDBConnection)con;
        }

    }
}
