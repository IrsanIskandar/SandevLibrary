using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
                MaxTimeout = 10000
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

        public async Task<TObject> GetRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public TObject GetRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string ? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = Client.Execute<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public async Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public TObject PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            try
            {
                RestRequest request = RequestApi(va_request_endpoint, Method.Post);
                if (headers is not null)
                    foreach (var item in headers)
                        request.AddHeader(item.Key, item.Value.ToString());

                if (!string.IsNullOrEmpty(stringJson))
                    request.AddParameter(contenType, stringJson, parameterType);

                RestResponse<TObject>? response = Client.Execute<TObject>(request);

                if (response.IsSuccessful)
                    return response.Data;
                else if (response.IsSuccessStatusCode)
                {
                    TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return default(TObject);
        }

        public async Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Put);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public TObject PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi( va_request_endpoint, Method.Put);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = Client.Execute<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public async Task<TObject> PatchRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public TObject PatchRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = Client.Execute<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public async Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }

        public TObject DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
            if (headers is not null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contenType, stringJson, parameterType);

            RestResponse<TObject>? response = Client.Execute<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return default(TObject);
        }
    }
}
