using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SandevLibrary.MicroORM
{
    public interface IDapperTaskAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteEnumerableAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteEnumerable<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        Task<List<T>> ExecuteListAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        List<T> ExecuteList<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        Task<T> ExecuteSingleAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        T ExecuteSingle<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        Task<bool> ExecuteNoReturnAsync<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        bool ExecuteNoReturn<T>(string spName, object param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false);
    }
}
