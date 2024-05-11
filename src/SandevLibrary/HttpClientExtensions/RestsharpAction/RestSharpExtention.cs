using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction;

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

	public async Task<(TObject, int)> GetRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Get);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public (TObject, int) GetRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Get);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = Client.Execute<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public async Task<(TObject, int)> PostRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Post);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public (TObject, int) PostRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Post);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = Client.Execute<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public async Task<(TObject, int)> PutRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Put);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public (TObject, int) PutRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Put);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = Client.Execute<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public async Task<(TObject, int)> PatchRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public (TObject, int) PatchRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Patch);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = Client.Execute<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public async Task<(TObject, int)> DeleteRequestAsync<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = await Client.ExecuteAsync<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}

	public (TObject, int) DeleteRequest<TObject>(string va_request_endpoint, Dictionary<string, object>? headers = null, string? contenType = "application/Json", string? stringJson = null, ParameterType parameterType = ParameterType.RequestBody)
	{
		RestRequest request = RequestApi(va_request_endpoint, Method.Delete);
		if (headers is not null)
			foreach (KeyValuePair<string, object> item in headers)
				request.AddHeader(item.Key, item.Value.ToString());

		if (!string.IsNullOrEmpty(stringJson))
			request.AddParameter(contenType, stringJson, parameterType);

		RestResponse<TObject>? response = Client.Execute<TObject>(request);

		if (response.IsSuccessful)
		{
			return (response.Data, (int)response.StatusCode);
		}
		else if (response.IsSuccessStatusCode)
		{
			TObject result = JsonSerializer.Deserialize<TObject>(response.Content);
			return (result, (int)response.StatusCode);
		}
		else
		{
			return (response.Data, (int)response.StatusCode);
		}
	}
}
