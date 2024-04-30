using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction
{
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
        Task<TObject> GetRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        TObject GetRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        TObject PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        TObject PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);


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
        Task<TObject> PatchRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        TObject PatchRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);

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
        TObject DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody);
    }
}
