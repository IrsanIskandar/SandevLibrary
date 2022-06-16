using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction
{
    public class RestSharpExtention : IRestSharpExtention
    {
        private static RestClient existingClient;
        private static RestClient Client => existingClient ?? (existingClient = RestClientInitialize());

        private static string _BASE_URL = string.Empty;

        private static RestClient RestClientInitialize()
        {
            RestClientOptions clientOptions = new RestClientOptions(_BASE_URL)
            {
                ThrowOnAnyError = true,
                Timeout = 1500
            };
            RestClient client = new RestClient(clientOptions);

            return client;
        }

        private static RestRequest RequestApi(string va_request_endpoint, Method method)
        {
            string joinUrl = string.Concat(_BASE_URL, va_request_endpoint);
            RestRequest request = new RestRequest(joinUrl, method);
            //request.AddHeader("Authorization", "Bearer ");

            return request;
        }

        public RestSharpExtention() { }

        public RestSharpExtention(string baseUrl) 
        { 
            _BASE_URL = baseUrl;
        }

        public async Task<TObject> GetRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            request.AddParameter("application/Json", stringjson, parameterType);
            if (isJwt)
                request.AddHeader("Authorization", "Bearer ");

            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TObject);
        }

        public async Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            request.AddParameter("application/Json", stringjson, parameterType);
            if (isJwt)
                request.AddHeader("Authorization", "Bearer ");

            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TObject);
        }

        public async Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Put);
            request.AddParameter("application/Json", stringjson, parameterType);
            if (isJwt)
                request.AddHeader("Authorization", "Bearer ");

            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TObject);
        }

        public async Task<TObject> PatchRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
            request.AddParameter("application/Json", stringjson, parameterType);
            if (isJwt)
                request.AddHeader("Authorization", "Bearer ");

            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TObject);
        }

        public async Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, string stringjson, ParameterType parameterType, bool isJwt = false)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
            request.AddParameter("application/Json", stringjson, parameterType);
            if (isJwt)
                request.AddHeader("Authorization", "Bearer ");

            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return default(TObject);
        }
    }
}
