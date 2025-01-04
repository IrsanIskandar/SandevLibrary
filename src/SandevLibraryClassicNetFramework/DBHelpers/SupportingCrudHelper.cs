using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SandevLibraryClassicNetFramework.DBHelpers
{
    /// <summary>
    /// This Class Implement Micro ORM with supporting Dapper Library
    /// </summary>
    /// <typeparam name="TEntityClass"></typeparam>
    public class SupportingCrudHelper<TEntityClass>
    {
        private static SqlConnection GetOpenConnection(string connectioStrings)
        {
            try
            {
                string connStrings = connectioStrings + ";pooling=true";
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connStrings);
                connectionStringBuilder.ConnectTimeout = 0;
                SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);

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
        /// This function is of type Asynchronouns and will return a List IEnumerable object
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>List Of EntityClass</returns>
        /// <exception cref="Exception"></exception>
        public async static Task<IEnumerable<TEntityClass>> ExecuteEnumerableAsync(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            SqlConnection connection = GetOpenConnection(connString);
            IEnumerable<TEntityClass> result = new List<TEntityClass>();
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        result = await connection.QueryAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    return result;
                }
                else
                {
                    result = await connection.QueryAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

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

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Fungsi ini bertipe Synchronouns dan akan mengembalikan objek List IEnumerable
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>List Of EntityClass</returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<TEntityClass> ExecuteEnumerable(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            SqlConnection connection = GetOpenConnection(connString);
            IEnumerable<TEntityClass> result = new List<TEntityClass>();
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        result = connection.Query<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    return result;
                }
                else
                {
                    result = connection.Query<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

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

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// This function is of type Asynchronouns and will return a List object, <br />
        /// unlike IEnumerable, this List type is an object that we can still manipulate, so we are free to use it.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>List Of EntityClass</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<List<TEntityClass>> ExecuteListAsync(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            SqlConnection connection = GetOpenConnection(connString);
            List<TEntityClass> result = new List<TEntityClass>();
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        result = await connection.QueryAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600) as List<TEntityClass>;
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    return result;
                }
                else
                {
                    result = await connection.QueryAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600) as List<TEntityClass>;
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

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

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This function is of type Synchronouns and will return a List object unlike IEnumerable, <br />
        /// this type of List is an object that we can still manipulate, so we are free to do with it.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>List Of EntityClass</returns>
        /// <exception cref="Exception"></exception>
        public static List<TEntityClass> ExecuteList(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            // return empty object when query returns no rows
            SqlConnection connection = GetOpenConnection(connString);
            List<TEntityClass> result = new List<TEntityClass>();
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        result = connection.Query<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600) as List<TEntityClass>;
                        transaction.Commit();
                        transaction.Dispose();
                    }

                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    return result;
                }
                else
                {
                    result = connection.Query<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600) as List<TEntityClass>;
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(sqlEx.Message);
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// This function is of type Asynchronouns and will return a single object.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Single Object</returns>
        /// <exception cref="Exception"></exception>
        public async static Task<TEntityClass> ExecuteSingleAsync(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlConnection connection = GetOpenConnection(connString);
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        TEntityClass result = await connection.QueryFirstOrDefaultAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();

                        if (connection.State == ConnectionState.Open)
                            connection.Close();

                        return result;
                    }
                }
                else
                {
                    TEntityClass result = await connection.QueryFirstOrDefaultAsync<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

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

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This function is of type Synchronouns and will return a single object.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns>Single Object</returns>
        /// <exception cref="Exception"></exception>
        public static TEntityClass ExecuteSingle(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlConnection connection = GetOpenConnection(connString);
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        TEntityClass result = connection.QueryFirstOrDefault<TEntityClass>(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();
                        if (connection.State == ConnectionState.Open)
                            connection.Close();

                        return result;
                    }
                }
                else
                {
                    TEntityClass result = connection.QueryFirstOrDefault<TEntityClass>(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

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

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This function is of type Asynchronouns and will not return an object or anything else.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async static Task ExecuteNoReturnAsync(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlConnection connection = GetOpenConnection(connString);
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();

                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
                else
                {
                    await connection.ExecuteAsync(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This function is of type Synchronouns and will not return an object or anything else.
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <exception cref="Exception"></exception>
        public static void ExecuteNoReturn(string connString, string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false)
        {
            SqlConnection connection = GetOpenConnection(connString);
            SqlTransaction transaction = null;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                if (sqlTransaction == true)
                {
                    using (transaction = connection.BeginTransaction())
                    {
                        connection.Execute(sql: spName, param: param, commandType: commandType, transaction: transaction, commandTimeout: 3600);
                        transaction.Commit();
                        transaction.Dispose();

                        if (connection.State == ConnectionState.Open)
                            connection.Close();
                    }
                }
                else
                {
                    connection.Execute(sql: spName, param: param, commandType: commandType, commandTimeout: 3600);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (sqlTransaction == true)
                {
                    if (transaction != null)
                        transaction.Rollback();
                }

                if (connection.State == ConnectionState.Open)
                    connection.Close();

                throw new Exception(ex.Message);
            }
        }
    }
}
