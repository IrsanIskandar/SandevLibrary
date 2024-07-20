using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SandevLibrary.MicroORM;

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
    Task<IEnumerable<TEntity>> ExecuteEnumerableAsync<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    IEnumerable<TEntity> ExecuteEnumerable<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    Task<List<TEntity>> ExecuteListAsync<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    List<TEntity> ExecuteList<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    Task<TEntity> ExecuteSingleAsync<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="spName"></param>
	/// <param name="param"></param>
	/// <param name="commandType"></param>
	/// <param name="sqlTransaction"></param>
	/// <returns></returns>
	TEntity ExecuteSingle<TEntity>(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    Task<bool> ExecuteBooleanReturnAsync(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spName"></param>
    /// <param name="param"></param>
    /// <param name="commandType"></param>
    /// <param name="sqlTransaction"></param>
    /// <returns></returns>
    bool ExecuteBooleanReturn(string spName, object? param = null, CommandType commandType = CommandType.Text, bool sqlTransaction = false, int commandTimeout = 60);
}
