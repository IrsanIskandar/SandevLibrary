//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SandevLibraryClassicNetFramework.RestfullApi
{
    public class RestClientService : IDisposable
    {
        private HttpClient _httpClient;
        private string _baseAddress;
        private bool disposed = false;

        private HttpResponseMessage ResponseMessage { get; set; } = new HttpResponseMessage();
        private HttpRequestMessage RequestMessage { get; set; } = new HttpRequestMessage();

        public RestClientService(string baseAddress)
        {
            _baseAddress = baseAddress;
            _httpClient = new HttpClient();
        }

        public async Task<TResource> GetRequest<TResource>(string addressSuffix, Dictionary<string, object> headers) where TResource : class
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{addressSuffix}")
                };

                foreach (var item in headers)
                    RequestMessage.Headers.Add(item.Key, item.Value.ToString());

                //RequestMessage.Headers.Add("X-No", header.No);
                //RequestMessage.Headers.Add("X-No1", header.No1);
                //RequestMessage.Headers.Add("X-Mid", header.Mid);
                //RequestMessage.Headers.Add("X-Sos-International-Api", header.SosInternationalApiKey);


                ResponseMessage = _httpClient.SendAsync(RequestMessage).Result;
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = ResponseMessage.Content.ReadAsStringAsync().Result;

                    #region Lib NewtonsoftJson
                    //JObject jsonObject = JObject.Parse(resContent);
                    //JArray dataArray = (JArray)jsonObject[]
                    //TResource result = JsonConvert.DeserializeObject<TResource>(jsonObject.ToString());
                    #endregion

                    #region Lib NewtonsoftJson
                    TResource result = JsonSerializer.Deserialize<TResource>(resContent);
                    #endregion

                    return result;
                }
                else
                    return default(TResource);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public async Task<TResource> GetRequestQueryParameterAsync<TResource>(string addressSuffix, Dictionary<string, object> headers, Dictionary<string, string> queryStringParams = null) where TResource : class
        {
            try
            {
                RequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseAddress}{addressSuffix}")
                };

                foreach (var item in headers)
                    RequestMessage.Headers.Add(item.Key, item.Value.ToString());

                ResponseMessage = await _httpClient.GetWithQueryStringAsync(RequestMessage.RequestUri.ToString(), queryStringParams);
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = ResponseMessage.Content.ReadAsStringAsync().Result;

                    #region Lib NewtonsoftJson
                    //JObject jsonObject = JObject.Parse(resContent);
                    //JArray dataArray = (JArray)jsonObject[]
                    //TResource result = JsonConvert.DeserializeObject<TResource>(jsonObject.ToString());
                    #endregion

                    #region Lib NewtonsoftJson
                    TResource result = JsonSerializer.Deserialize<TResource>(resContent);
                    #endregion

                    return result;
                }
                else
                    return default(TResource);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public async Task<TResource> PostAsync<TResource>(string addressSuffix, Dictionary<string, object> headers, object requestVM) where TResource : class
        {
            try
            {
                //string stringJson = JsonConvert.SerializeObject(requestVM);
                string stringJson = JsonSerializer.Serialize(requestVM);
                HttpContent requestContent = new StringContent(stringJson, Encoding.UTF8, "application/json");


                RequestMessage = new HttpRequestMessage
                {
                    Content = requestContent,
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_baseAddress}{addressSuffix}"),
                };

                foreach (var item in headers)
                    RequestMessage.Headers.Add(item.Key, item.Value.ToString());

                RequestMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                RequestMessage.Headers.Add("Access-Control-Allow-Credentials", "true");
                RequestMessage.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                    #region Lib Newtonsoft Json
                    //JObject jsonObject = JObject.Parse(resContent);
                    //TResource result = JsonConvert.DeserializeObject<TResource>(resContent);
                    #endregion

                    #region Lib System Text Json
                    TResource result = JsonSerializer.Deserialize<TResource>(resContent);
                    #endregion


                    return result;
                }
                else
                    return default(TResource);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public async Task<TResource> UpdateAsync<TResource>(string addressSuffix, Dictionary<string, object> headers, object requestVM) where TResource : class
        {
            try
            {
                //string stringJson = JsonConvert.SerializeObject(requestVM);
                string stringJson = JsonSerializer.Serialize(requestVM);
                HttpContent requestContent = new StringContent(stringJson, Encoding.UTF8, "application/json");

                

                RequestMessage = new HttpRequestMessage
                {
                    Content = requestContent,
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"{_baseAddress}{addressSuffix}"),
                };

                foreach (var item in headers)
                    RequestMessage.Headers.Add(item.Key, item.Value.ToString());

                RequestMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                RequestMessage.Headers.Add("Access-Control-Allow-Credentials", "true");
                RequestMessage.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);
                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                    #region Lib Newtonsoft Json
                    //JObject jsonObject = JObject.Parse(resContent);
                    //TResource result = JsonConvert.DeserializeObject<TResource>(resContent);
                    #endregion

                    #region Lib System Text Json
                    TResource result = JsonSerializer.Deserialize<TResource>(resContent);
                    #endregion

                    return result;
                }
                else
                    return default(TResource);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        public async Task<TResource> DeleteAsync<TResource>(string addressSuffix, Dictionary<string, object> headers, object requestVM) where TResource : class
        {
            try
            {
                //string stringJson = JsonConvert.SerializeObject(requestVM);
                string stringJson = JsonSerializer.Serialize(requestVM);
                HttpContent requestContent = new StringContent(stringJson, Encoding.UTF8, "application/json");

                RequestMessage = new HttpRequestMessage
                {
                    Content = requestContent,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{_baseAddress}{addressSuffix}")
                };

                foreach (var item in headers)
                    RequestMessage.Headers.Add(item.Key, item.Value.ToString());

                RequestMessage.Headers.Add("Access-Control-Allow-Origin", "*");
                RequestMessage.Headers.Add("Access-Control-Allow-Credentials", "true");
                RequestMessage.Headers.Add("Access-Control-Allow-Headers", "Access-Control-Allow-Origin,Content-Type");
                ResponseMessage = await _httpClient.SendAsync(RequestMessage);

                if (ResponseMessage.IsSuccessStatusCode)
                {
                    string resContent = await ResponseMessage.Content.ReadAsStringAsync();
                    #region Lib Newtonsoft Json
                    //JObject jsonObject = JObject.Parse(resContent);
                    //TResource result = JsonConvert.DeserializeObject<TResource>(resContent);
                    #endregion

                    #region Lib System Text Json
                    TResource result = JsonSerializer.Deserialize<TResource>(resContent);
                    #endregion

                    return result;
                }
                else
                    return default(TResource);
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
