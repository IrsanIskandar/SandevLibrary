
using System.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SandevLibrary.HttpClientExtensions.HttpClientAction
{
    public class RestClientService<TResource> : IDisposable where TResource : class
    {
        private HttpClient _httpClient;
        private string _baseAddress;
        private string _addressSuffix;
        private bool disposed = false;

        public HttpResponseMessage ResponseMessage { get; set; } = new HttpResponseMessage();
        public HttpRequestMessage RequestMessage { get; set; } = new HttpRequestMessage();

        public RestClientService(string baseAddress, string addressSuffix = "")
        {
            _baseAddress = baseAddress;
            _addressSuffix = addressSuffix;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Get all record from (database) based on <see cref="ResponseVM"/>
        /// </summary>
        /// <returns>ResponseVM object model</returns>
        public virtual TResource GetAll()
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}")
                };

                ResponseMessage = _httpClient.SendAsync(RequestMessage).Result;

                string resContent = ResponseMessage.Content.ReadAsStringAsync().Result;
                var x = JsonSerializer.Deserialize<TResource>(resContent);

                return x;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Get all record from (database) based on <see cref="ResponseVM"/>
        /// </summary>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> GetAllAsync()
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}")
                };

                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                var x = JsonSerializer.Deserialize<TResource>(resContent);

                return x;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Get all record from (database) based on <see cref="ResponseVM"/>
        /// </summary>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> GetAllQueryParameterAsync(Dictionary<string, string> queryStringParams)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}")
                };

                ResponseMessage = await _httpClient.GetWithQueryStringAsync(RequestMessage.RequestUri.ToString(), queryStringParams);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                var x = JsonSerializer.Deserialize<TResource>(resContent);

                return x;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Get specific record from (database) based on <see cref="ResponseVM"/>
        /// </summary>
        /// <param name="id">Objec Model primary key</param>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> GetByIdAsync(int id)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}/{id}")
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResource>(resContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TResource> GetSingleAsync<TResource>(dynamic param)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}/{param}")
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResource>(resContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Update specific Model based on <see cref="RequestVM"/>
        /// </summary>
        /// <param name="requestObject">RequestVM object model as requset body</param>
        /// <param name="id">Object Model primary key</param>
        /// <typeparam name="TRequest">RequestVM object model</typeparam>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> UpdateByIdAsync<TRequest>(TRequest requestObject, int id)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Content = JsonContent.Create(requestObject),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}/{id}")
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResource>(resContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Update specific Model based on <see cref="RequestVM"/>
        /// </summary>
        /// <param name="requestObject">RequestVM object model as requset body</param>
        /// <typeparam name="TRequest">RequestVM object model</typeparam>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> UpdateAsync<TRequest>(TRequest requestObject)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Content = JsonContent.Create(requestObject),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}")
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResource>(resContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Create new object entity throughth end point API 
        /// </summary>
        /// <param name="requestVM">Request object model as request body</param>
        /// <typeparam name="TRequest">Generic request object model</typeparam>
        /// <returns>ResponseVM object model</returns>
        public async Task<TResource> AddAsync<TRequest>(TRequest requestVM)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Content = JsonContent.Create(requestVM),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}"),
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<TResource>(resContent);
                }
                else
                {
                    return default(TResource);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// Delete specific object entity throughth end point API 
        /// </summary>
        /// <param name="id">Entity object model primary key</param>
        /// <param name="requestVM">RequestVM object model</param>
        /// <typeparam name="TRequest">Generic request object model</typeparam>
        /// <returns></returns>
        public async Task<TResource> DeleteAsync<TRequest>(int id, TRequest requestVM)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Content = JsonContent.Create(requestVM),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}/{id}")
                };
                string x = await RequestMessage.Content.ReadAsStringAsync();
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<TResource>(resContent);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="requestVM"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<TResource> GetFilterPost<TRequest>(TRequest requestVM)
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Content = JsonContent.Create(requestVM),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_baseAddress}{_addressSuffix}"),
                };
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();

                    return JsonSerializer.Deserialize<TResource>(resContent);
                }
                else
                {
                    return default(TResource);
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                }

                if (ResponseMessage != null)
                {
                    ResponseMessage.Dispose();
                }
                disposed = true;
            }
        }
    }
}