using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction;

public interface IRestSharpExtention
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="va_request_endpoint"></param>
    /// <param name="headers"></param>
    /// <param name="contenType"></param>
    /// <param name="stringJson"></param>
    /// <param name="parameterType"></param>
    /// <returns></returns>
    Task<(TObject, int)> GetRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <param name="va_request_endpoint"></param>
	/// <param name="headers"></param>
	/// <param name="contenType"></param>
	/// <param name="stringJson"></param>
	/// <param name="parameterType"></param>
	/// <returns></returns>
	(TObject, int) GetRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="va_request_endpoint"></param>
    /// <param name="headers"></param>
    /// <param name="contenType"></param>
    /// <param name="stringJson"></param>
    /// <param name="parameterType"></param>
    /// <returns></returns>
    Task<(TObject, int)> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <param name="va_request_endpoint"></param>
	/// <param name="headers"></param>
	/// <param name="contenType"></param>
	/// <param name="stringJson"></param>
	/// <param name="parameterType"></param>
	/// <returns></returns>
	(TObject, int) PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="va_request_endpoint"></param>
    /// <param name="headers"></param>
    /// <param name="contenType"></param>
    /// <param name="stringJson"></param>
    /// <param name="parameterType"></param>
    /// <returns></returns>
    Task<(TObject, int)> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <param name="va_request_endpoint"></param>
	/// <param name="headers"></param>
	/// <param name="contenType"></param>
	/// <param name="stringJson"></param>
	/// <param name="parameterType"></param>
	/// <returns></returns>
	(TObject, int) PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="va_request_endpoint"></param>
    /// <param name="headers"></param>
    /// <param name="contenType"></param>
    /// <param name="stringJson"></param>
    /// <param name="parameterType"></param>
    /// <returns></returns>
    Task<(TObject, int)> PatchRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <param name="va_request_endpoint"></param>
	/// <param name="headers"></param>
	/// <param name="contenType"></param>
	/// <param name="stringJson"></param>
	/// <param name="parameterType"></param>
	/// <returns></returns>
	(TObject, int) PatchRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <param name="va_request_endpoint"></param>
    /// <param name="headers"></param>
    /// <param name="contenType"></param>
    /// <param name="stringJson"></param>
    /// <param name="parameterType"></param>
    /// <returns></returns>
    Task<(TObject, int)> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TObject"></typeparam>
	/// <param name="va_request_endpoint"></param>
	/// <param name="headers"></param>
	/// <param name="contenType"></param>
	/// <param name="stringJson"></param>
	/// <param name="parameterType"></param>
	/// <returns></returns>
	(TObject, int) DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);
}
