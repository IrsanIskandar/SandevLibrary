using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using SandevLibrary.SandevHelpers;

namespace SandevLibrary.MicroORM
{
    public class DapperConfigOrm : IDapperTaskAction
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionTimeout"></param>
        public DapperConfigOrm(string connectionStrings, int connectionTimeout = 30)
        {
            Connection = GetOpenConnection(connectionStrings, connectionTimeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionTimeout"></param>
        /// <param name="connectionLifetime"></param>
        public DapperConfigOrm(string connectionStrings, int connectionTimeout = 30, int connectionLifetime = 0)
        {
            Connection = GetOpenConnection(connectionStrings, connectionTimeout, connectionLifetime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionTimeout"></param>
        /// <param name="connectionLifetime"></param>
        /// <param name="minPool"></param>
        public DapperConfigOrm(string connectionStrings, int connectionTimeout = 30, int connectionLifetime = 0, int minPool = 0)
        {
            Connection = GetOpenConnection(connectionStrings, connectionTimeout, connectionLifetime, minPool);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionTimeout"></param>
        /// <param name="connectionLifetime"></param>
        /// <param name="minPool"></param>
        /// <param name="maxPool"></param>
        public DapperConfigOrm(string connectionStrings, int connectionTimeout = 30, int connectionLifetime = 0, int minPool = 0, int maxPool = 50)
        {
            Connection = GetOpenConnection(connectionStrings, connectionTimeout, connectionLifetime, minPool, maxPool);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings"></param>
        /// <param name="connectionTimeout"></param>
        /// <param name="connectionLifetime"></param>
        /// <param name="minPool"></param>
        /// <param name="maxPool"></param>
        /// <param name="pooling"></param>
        public DapperConfigOrm(string connectionStrings, int connectionTimeout = 30, int connectionLifetime = 0, int minPool = 0, int maxPool = 50, bool pooling = false)
        {
            Connection = GetOpenConnection(connectionStrings, connectionTimeout, connectionLifetime, minPool, maxPool, pooling);
        }

        private static SqlConnection GetOpenConnection(string connectionStrings)
        {
            try
            {
                string connStrings = connectionStrings;
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

        private static SqlConnection GetOpenConnection(string connectionStrings, int connectionTimeout)
        {
            try
            {
                string connStrings = connectionStrings + $";Connection Timeout={connectionTimeout};";
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

        private static SqlConnection GetOpenConnection(string connectionStrings, int connectionTimeout, int connectionLifetime)
        {
            try
            {
                string connStrings = connectionStrings + $";Connection Timeout={connectionTimeout};Connection Lifetime={connectionLifetime};";
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

        private static SqlConnection GetOpenConnection(string connectionStrings, int connectionTimeout, int connectionLifetime, int minPool)
        {
            try
            {
                string connStrings = connectionStrings + $";Connection Timeout={connectionTimeout};Connection Lifetime={connectionLifetime};Min Pool Size={minPool};";
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

        private static SqlConnection GetOpenConnection(string connectionStrings, int connectionTimeout, int connectionLifetime, int minPool, int maxPool)
        {
            try
            {
                string connStrings = connectionStrings + $";Connection Timeout={connectionTimeout};Connection Lifetime={connectionLifetime};Min Pool Size={minPool};Max Pool Size{maxPool};";
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

        private static SqlConnection GetOpenConnection(string connectionStrings, int connectionTimeout, int connectionLifetime, int minPool, int maxPool, bool pooling)
        {
            try
            {
                string connStrings = connectionStrings + $";Connection Timeout={connectionTimeout};Connection Lifetime={connectionLifetime};Min Pool Size={minPool};Max Pool Size{maxPool};Pooling={pooling};";
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
        public async Task<IEnumerable<T>> ExecuteEnumerableAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public IEnumerable<T> ExecuteEnumerable<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public async Task<List<T>> ExecuteListAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public List<T> ExecuteList<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public async Task<T> ExecuteSingleAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public T ExecuteSingle<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public async Task<bool> ExecuteNoReturnAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
        public bool ExecuteNoReturn<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
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
