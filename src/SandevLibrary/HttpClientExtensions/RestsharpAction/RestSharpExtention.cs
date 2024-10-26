using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction;

public class RestSharpExtention
{
	private static RestClient existingClient;
	private static RestClient Client => existingClient ?? (existingClient = RestClientInitialize());

	private static string _BASE_URL = string.Empty;

    public RestSharpExtention() { }

    public RestSharpExtention(string baseUrl)
    {
        _BASE_URL = baseUrl;
    }

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

    public TObject GetSingleRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);
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

    public async Task<TObject> GetSingleRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);
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

    public TObject GetSingleUrlSegmentRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? param = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (param is not null)
            foreach (var item in param)
                if (item.Value is not null)
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

    public TObject GetSingleUrlSegmentRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? param = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (param is not null)
            foreach (var item in param)
                if (item.Value is not null)
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

    public List<TObject> GetListRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);
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

    public async Task<List<TObject>> GetListRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);
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
    public TObject GetSingleQueryStringRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (queryParams != null)
        {
            foreach (var item in queryParams)
            {
                if (item.Value is not null)
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

    public async Task<TObject> GetSingleQueryStringRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
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

    public List<TObject> GetListQueryStringRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
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

    public async Task<List<TObject>> GetListQueryStringRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
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

    public TObject PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Post);
        if (headers is not null)
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

    public (TObject, int) PostRequestWithStatusCode<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Post);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);

        int statusCode = 99;
        TObject data = default(TObject);
        RestResponse<TObject> response = Client.ExecutePost<TObject>(request);

        if (response.IsSuccessful)
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }
        else if (response.IsSuccessStatusCode)
        {
            statusCode = (int)response.StatusCode;
            data = JsonSerializer.Deserialize<TObject>(response.Content);
        }
        else if (response.StatusCode.ToString().Substring(0, 1).Equals(5))
        {
            statusCode = (int)response.StatusCode;
        }
        else
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }

        return (data, statusCode);
    }

    public async Task<TObject> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Post);
        if (headers is not null)
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

    public async Task<TObject> PostUploadRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? filePath = null, string? fileName = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Post);
        if (headers is not null)
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

    public TObject PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Put);
        if (headers is not null)
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

    public (TObject, int) PutRequestWithStatusCode<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Put);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);

        int statusCode = 99;
        TObject data = default(TObject);
        RestResponse<TObject> response = Client.ExecutePost<TObject>(request);

        if (response.IsSuccessful)
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }
        else if (response.IsSuccessStatusCode)
        {
            statusCode = (int)response.StatusCode;
            data = JsonSerializer.Deserialize<TObject>(response.Content);
        }
        else
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }

        return (data, statusCode);
    }

    public async Task<TObject> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Put);
        if (headers is not null)
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

    public TObject PatchRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
        if (headers is not null)
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

    public (TObject, int) PatchRequestWithStatusCode<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
        if (headers is not null)
            foreach (var item in headers)
                request.AddHeader(item.Key, item.Value.ToString());

        if (!string.IsNullOrEmpty(stringJson))
            request.AddParameter(contentType, stringJson, parameterType);

        int statusCode = 99;
        TObject data = default(TObject);
        RestResponse<TObject> response = Client.ExecutePost<TObject>(request);

        if (response.IsSuccessful)
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }
        else if (response.IsSuccessStatusCode)
        {
            statusCode = (int)response.StatusCode;
            data = JsonSerializer.Deserialize<TObject>(response.Content);
        }
        else
        {
            statusCode = (int)response.StatusCode;
            data = response.Data;
        }

        return (data, statusCode);
    }

    public async Task<TObject> PatchRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
        if (headers is not null)
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

    public TObject DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
        if (headers is not null)
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

    public async Task<TObject> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contentType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
        if (headers is not null)
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

    public byte[] DownloadByteFile(string va_request_endpoint, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);
        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
                    request.AddQueryParameter(item.Key, item.Value.ToString());
        }

        RestResponse<byte[]> response = Client.Execute<byte[]>(request);
        if (response.IsSuccessStatusCode)
            return Client.DownloadData(request);

        return response.Data;
    }

    public async Task<byte[]> DownloadByteFileAsync(string va_request_endpoint, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
                    request.AddQueryParameter(item.Key, item.Value.ToString());
        }

        return (await Client.DownloadDataAsync(request));
    }

    public Stream DownloadStreamFile(string va_request_endpoint, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
                    request.AddQueryParameter(item.Key, item.Value.ToString());
        }

        return (Client.DownloadStream(request));
    }

    public async Task<Stream> DownloadStreamFileAsync(string va_request_endpoint, Dictionary<string, object>? queryParams = null)
    {
        RestRequest request = RequestApi(va_request_endpoint, Method.Get);

        if (queryParams != null)
        {
            foreach (var item in queryParams)
                if (item.Value is not null)
                    request.AddQueryParameter(item.Key, item.Value.ToString());
        }

        return (await Client.DownloadStreamAsync(request));
    }
}
