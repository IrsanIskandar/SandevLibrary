using RestSharp;
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
        /// <param name="stringjson"></param>
        /// <param name="parameterType"></param>
        /// <param name="isJwt"></param>
        /// <returns></returns>
        Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="va_request_endpoint"></param>
        /// <param name="stringjson"></param>
        /// <param name="parameterType"></param>
        /// <param name="isJwt"></param>
        /// <returns></returns>
        Task<TObject> GetRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="va_request_endpoint"></param>
        /// <param name="stringjson"></param>
        /// <param name="parameterType"></param>
        /// <param name="isJwt"></param>
        /// <returns></returns>
        Task<TObject> PatchRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="va_request_endpoint"></param>
        /// <param name="stringjson"></param>
        /// <param name="parameterType"></param>
        /// <param name="isJwt"></param>
        /// <returns></returns>
        Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="va_request_endpoint"></param>
        /// <param name="stringjson"></param>
        /// <param name="parameterType"></param>
        /// <param name="isJwt"></param>
        /// <returns></returns>
        Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false);
    }
}
