using Microsoft.Extensions.Logging;
using Polly;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.RestsharpAction;

public class RestSharpClient
{
    private readonly RestClient _client;
    private readonly ILogger<RestSharpClient> _logger;
    private readonly IAsyncPolicy<RestResponse> _retryPolicy;

    private static readonly CookieContainer _cookieContainer = new CookieContainer();

    public RestSharpClient(string baseUrl, ILogger<RestSharpClient> logger)
    {
        _logger = logger;

        var options = new RestClientOptions(baseUrl)
        {
            Timeout = TimeSpan.FromSeconds(120),
            CookieContainer = _cookieContainer
        };

        _client = new RestClient(options);

        _retryPolicy = Policy
            .HandleResult<RestResponse>(r => !r.IsSuccessful)
            .WaitAndRetryAsync(
                3,
                retry => TimeSpan.FromSeconds(Math.Pow(2, retry)),
                (result, time, retry, context) =>
                {
                    _logger.LogWarning(
                        "Retry {Retry} after {Delay}ms due to {StatusCode}",
                        retry,
                        time.TotalMilliseconds,
                        result.Result.StatusCode);
                });
    }

    private RestRequest BuildRequest(string endpoint, Method method, Dictionary<string, object>? headers = null, Dictionary<string, object>? query = null, object? body = null)
    {
        var request = new RestRequest(endpoint, method);

        if (headers != null)
        {
            foreach (var h in headers)
                request.AddHeader(h.Key, h.Value?.ToString());
        }

        if (query != null)
        {
            foreach (var q in query)
                request.AddQueryParameter(q.Key, q.Value?.ToString());
        }

        if (body != null)
        {
            if (body is string json)
                request.AddStringBody(json, DataFormat.Json);
            else
                request.AddJsonBody(body);
        }

        return request;
    }

    private async Task<TObject?> ExecuteAsync<TObject>(RestRequest request)
    {
        var activity = new System.Diagnostics.Activity("API CALL");
        activity.Start();

        var response = await _retryPolicy.ExecuteAsync(() => _client.ExecuteAsync(request));

        activity.Stop();

        _logger.LogInformation("API {Method} {Endpoint}", request.Method, request.Resource);
        _logger.LogInformation("Response Status: {Status}", response.StatusCode);
        _logger.LogInformation("Response Content: {Content}", response.Content);

        if (!response.IsSuccessful)
        {
            _logger.LogError(
                "API ERROR {Endpoint} {Status}",
                request.Resource,
                response.StatusCode);
        }

        if (string.IsNullOrWhiteSpace(response.Content))
            return default;

        try
        {
            return JsonSerializer.Deserialize<TObject>(
                response.Content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Deserialize ERROR for {Endpoint}", request.Resource);
            return default;
        }
    }

    public Task<TObject?> GetAsync<TObject>(string endpoint, Dictionary<string, object>? headers = null, Dictionary<string, object>? query = null)
    {
        var req = BuildRequest(endpoint: endpoint, method: Method.Get, headers: headers, query: query);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> PostAsync<TObject>(string endpoint, object? body = null, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Post, headers: headers, null, body: body);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> PutAsync<TObject>(string endpoint, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Put, headers: headers, null);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> PutAsync<TObject>(string endpoint, object? body = null, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Put, headers: headers, null, body: body);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> PatchAsync<TObject>(string endpoint, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Patch, headers: headers, null);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> PatchAsync<TObject>(string endpoint, object? body = null, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Patch, headers: headers, null, body: body);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> DeleteAsync<TObject>(string endpoint, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Delete, headers: headers);
        return ExecuteAsync<TObject>(req);
    }

    public Task<TObject?> DeleteAsync<TObject>(string endpoint, object? body = null, Dictionary<string, object>? headers = null)
    {
        var req = BuildRequest(endpoint: endpoint, Method.Delete, headers: headers);
        return ExecuteAsync<TObject>(req);
    }
}
