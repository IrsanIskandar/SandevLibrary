using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using SandevLibrary.SandevHelpers;

namespace SandevLibrary.MicroORM
{
    public class DapperConfigOrm<T>
    {
        private static SqlConnection existingConnection;
        private static SqlConnection Connection => existingConnection ?? (existingConnection = GetOpenConnection());

        private static SqlConnection GetOpenConnection()
        {
            try
            {
                string connStrings = ConstantsHelper.CONNECTION_STRINGS + ";pooling=true";
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connStrings);
                SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
            catch (SqlException MyEx)
            {
                throw new Exception(MyEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async static Task<IEnumerable<T>> ExecuteEnumerableAsync(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            IEnumerable<T> result = new List<T>();
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        result = await Connection.QueryAsync<T>(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
                else
                {
                    result = await Connection.QueryAsync<T>(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<T> ExecuteEnumerable(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            IEnumerable<T> result = new List<T>();
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        result = Connection.Query<T>(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
                else
                {
                    result = Connection.Query<T>(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<T>> ExecuteListAsync(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            List<T> result = new List<T>();
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        result = await Connection.QueryAsync<T>(sql: spName, param: param, commandType: commandType, transaction: transaction) as List<T>;
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
                else
                {
                    result = await Connection.QueryAsync<T>(sql: spName, param: param, commandType: commandType) as List<T>;
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<T> ExecuteList(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            List<T> result = new List<T>();
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        result = Connection.Query<T>(sql: spName, param: param, commandType: commandType, transaction: transaction) as List<T>;
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
                else
                {
                    result = Connection.Query<T>(sql: spName, param: param, commandType: commandType) as List<T>;
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async static Task<T> ExecuteSingleAsync(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        T result = await Connection.QueryFirstOrDefaultAsync<T>(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();

                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();

                        return result;
                    }
                }
                else
                {
                    T result = await Connection.QueryFirstOrDefaultAsync<T>(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T ExecuteSingle(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        T result = Connection.QueryFirstOrDefault<T>(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();
                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();

                        return result;
                    }
                }
                else
                {
                    T result = Connection.QueryFirstOrDefault<T>(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async static Task ExecuteNoReturnAsync(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        await Connection.ExecuteAsync(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();

                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();
                    }
                }
                else
                {
                    await Connection.ExecuteAsync(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <exception cref="Exception"></exception>
        public static void ExecuteNoReturn(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlTransaction transaction = null;

            try
            {
                if (sqlTransaction == true)
                {
                    Connection.Open();
                    using (transaction = Connection.BeginTransaction())
                    {
                        Connection.Execute(sql: spName, param: param, commandType: commandType, transaction: transaction);
                        transaction.Commit();
                        transaction.Dispose();

                        if (Connection.State == ConnectionState.Open)
                            Connection.Close();
                    }
                }
                else
                {
                    Connection.Execute(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (Connection.State == ConnectionState.Open)
                    Connection.Close();

                throw new Exception(ex.Message);
            }
        }
    }
}
