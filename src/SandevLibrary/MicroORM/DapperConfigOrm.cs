using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using SandevLibrary.SandevHelpers;

namespace SandevLibrary.MicroORM
{
    public class DapperConfigOrm
    {
        private static SqlConnection Connection { get; set; } = new SqlConnection();

        public DapperConfigOrm() { }

        /// <summary>
        /// Constructor to start Using Micro ORM with connection parameter to database
        /// </summary>
        /// <param name="connectionStrings"></param>
        public DapperConfigOrm(string connectionStrings)
        {
            Connection = GetOpenConnection(connectionStrings);
        }

        private static SqlConnection GetOpenConnection(string connectionStrings)
        {
            try
            {
                string connStrings = connectionStrings + ";pooling=true";
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
        /// Function Asynchronous return Enumerable List with Object or Value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Return Enumerable List data Object or Value</returns>
        /// <exception cref="Exception"></exception>
        public async static Task<IEnumerable<T>> ExecuteEnumerableAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Synchronous return Enumerable List with Object or Value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Return Enumerable List data Object or Value</returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<T> ExecuteEnumerable<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Asynchronous return List with Object or Value and data can still be manipulated
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Returns List with Object or Value</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<T>> ExecuteListAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Synchronous return List with Object or Value and data can still be manipulated
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Returns List with Object or Value</returns>
        /// <exception cref="Exception"></exception>
        public static List<T> ExecuteList<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Asynchronous return Single Object or Single Value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Returns Single Object Or Value</returns>
        /// <exception cref="Exception"></exception>
        public async static Task<T> ExecuteSingleAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Synchronous return Single Object or Single Value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Returns Single Object Or Value</returns>
        /// <exception cref="Exception"></exception>
        public static T ExecuteSingle<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        /// Function Asynchronous No return value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Return a Boolean True or False</returns>
        /// <exception cref="Exception"></exception>
        public async static Task<bool> ExecuteNoReturnAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            bool result = false;
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

                        result = true;
                    }
                }
                else
                {
                    await Connection.ExecuteAsync(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    result = true;
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

                result = false;

                throw new Exception(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Function Synchronous No return value
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <exception cref="Exception"></exception>
        /// <returns>Return a Boolean True or False</returns>
        public static bool ExecuteNoReturn<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            bool result = false;
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

                        result = true;
                    }
                }
                else
                {
                    Connection.Execute(sql: spName, param: param, commandType: commandType);
                    if (Connection.State == ConnectionState.Open)
                        Connection.Close();

                    result = true;
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

                result = true;

                throw new Exception(ex.Message);
            }

            return result;
        }
    }
}
