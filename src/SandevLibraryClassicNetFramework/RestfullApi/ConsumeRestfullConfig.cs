using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SandevLibraryClassicNetFramework.RestfullApi
{
    public class ConsumeRestfullConfig
    {
        private static string _baseUrl;
        private static RestClient existingClient;
        private static RestClient Client => existingClient ?? (existingClient = RestClientInitialize());

        public ConsumeRestfullConfig(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private static RestClient RestClientInitialize()
        {
            RestClientOptions options = new RestClientOptions();
            options.BaseUrl = new Uri(_baseUrl);
            RestClient client = new RestClient(options);

            return client;
        }

        private static RestRequest RequestApi(string va_request_endpoint, Method method)
        {
            string joinUrl = string.Concat(_baseUrl, va_request_endpoint);
            RestRequest request = new RestRequest(joinUrl, method);

            return request;
        }

        public TObject GetSingleRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter("application/Json", stringJson, parameterType);
            RestResponse<TObject> response = Client.ExecuteGet<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<TObject> GetSingleRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter("application/Json", stringJson, parameterType);
            RestResponse<TObject> response = await Client.ExecuteGetAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject GetSingleUrlSegmentRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> param = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (param != null)
                foreach (var item in param)
                    if (item.Value != null)
                        request.AddUrlSegment(item.Key, item.Value.ToString());

            RestResponse<TObject> response = Client.ExecuteGet<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject GetSingleUrlSegmentRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> param = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (param != null)
                foreach (var item in param)
                    if (item.Value != null)
                        request.AddUrlSegment(item.Key, item.Value.ToString());

            RestResponse<TObject> response = Client.ExecuteGet<TObject>(request);
            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public List<TObject> GetListRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter("application/Json", stringJson, parameterType);
            RestResponse<List<TObject>> response = Client.ExecuteGet<List<TObject>>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                List<TObject> result = JsonSerializer.Deserialize<List<TObject>>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<List<TObject>> GetListRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter("application/Json", stringJson, parameterType);
            RestResponse<List<TObject>> response = await Client.ExecuteAsync<List<TObject>>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                List<TObject> result = JsonSerializer.Deserialize<List<TObject>>(response.Content);
                return result;
            }

            return response.Data;
        }

        #region Query Parameters
        public TObject GetSingleQueryStringRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                {
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
                }
            }

            RestResponse<TObject> response = Client.ExecuteGet<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<TObject> GetSingleQueryStringRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }
            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public List<TObject> GetListQueryStringRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }


            RestResponse<List<TObject>> response = Client.ExecuteGet<List<TObject>>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                List<TObject> result = JsonSerializer.Deserialize<List<TObject>>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<List<TObject>> GetListQueryStringRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }
            RestResponse<List<TObject>> response = await Client.ExecuteAsync<List<TObject>>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                List<TObject> result = JsonSerializer.Deserialize<List<TObject>>(response.Content);
                return result;
            }

            return response.Data;
        }
        #endregion

        public TObject PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = Client.ExecutePost<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject PostRequest<TObject>(string va_request_endpoint, out int statusCode, out bool isSuccess, out string responseError, out string responseText, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            statusCode = 0;
            isSuccess = false;
            responseError = string.Empty;
            responseText = string.Empty;
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = Client.ExecutePost<TObject>(request);

            if (response.IsSuccessful)
            {
                isSuccess = true;
                statusCode = (int)response.StatusCode;
                responseText = response.Content;
                return response.Data;
            }
            else if (response.IsSuccessStatusCode)
            {
                isSuccess = true;
                statusCode = (int)response.StatusCode;
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                responseText = response.Content;
                return result;
            }
            else
            {
                statusCode = (int)response.StatusCode;
                responseError = response.Content;
            }
            return response.Data;
        }

        public async Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = await Client.ExecutePostAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<TObject> PostUploadRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string filePath = null, string fileName = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Post);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;

            //request.AddParameter(new BodyParameter("files", data, "multipart/form-data"));
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(fileName))
            {
                string fullPath = string.Concat(filePath, fileName);
                //request.AddFile("files", filePath);
                //request.AddParameter(new BodyParameter("files", data, "multipart/form-data"));
            }

            RestResponse<TObject> response = await Client.ExecutePostAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Put);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = Client.ExecutePut<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject PutRequest<TObject>(string va_request_endpoint, out int statusCode, out bool isSuccess, out string responseError, out string responseText, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            statusCode = 0;
            isSuccess = false;
            responseError = string.Empty;
            responseText = string.Empty;
            RestRequest request = RequestApi(va_request_endpoint, Method.Put);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = Client.ExecutePut<TObject>(request);

            if (response.IsSuccessful)
            {
                isSuccess = true;
                statusCode = (int)response.StatusCode;
                responseText = response.Content;
                return response.Data;
            }
            else if (response.IsSuccessStatusCode)
            {
                isSuccess = true;
                statusCode = (int)response.StatusCode;
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                responseText = response.Content;
                return result;
            }
            else
            {
                statusCode = (int)response.StatusCode;
                responseError = response.Content;
            }
            return response.Data;
        }

        public async Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Put);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = await Client.ExecutePutAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public TObject DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = Client.Execute<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public async Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object> headers = null, string contentType = null, string stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value.ToString());

            if (!string.IsNullOrEmpty(stringJson))
                request.AddParameter(contentType, stringJson, parameterType);
            RestResponse<TObject> response = await Client.ExecuteAsync<TObject>(request);

            if (response.IsSuccessful)
                return response.Data;
            else if (response.IsSuccessStatusCode)
            {
                TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
                return result;
            }

            return response.Data;
        }

        public byte[] DownloadByteFile(string va_request_endpoint, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);
            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }

            RestResponse<byte[]> response = Client.Execute<byte[]>(request);
            if (response.IsSuccessStatusCode)
                return Client.DownloadData(request);

            return response.Data;
        }

        public async Task<byte[]> DownloadByteFileAsync(string va_request_endpoint, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }

            return (await Client.DownloadDataAsync(request));
        }

        public Stream DownloadStreamFile(string va_request_endpoint, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }

            return (Client.DownloadStream(request));
        }

        public async Task<Stream> DownloadStreamFileAsync(string va_request_endpoint, Dictionary<string, object> queryParams = null)
        {
            RestRequest request = RequestApi(va_request_endpoint, Method.Get);

            if (queryParams != null)
            {
                foreach (var item in queryParams)
                    if (item.Value != null)
                        request.AddQueryParameter(item.Key, item.Value.ToString());
            }

            return (await Client.DownloadStreamAsync(request));
        }
    }
}
