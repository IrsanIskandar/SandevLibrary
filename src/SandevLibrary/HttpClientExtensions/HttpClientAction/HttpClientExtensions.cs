using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.HttpClientExtensions.HttpClientAction
{
    public static class HttpClientExtensions
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <param name="uri"></param>
		/// <param name="queryStringParams"></param>
		/// <returns></returns>
		public static async Task<HttpResponseMessage> GetWithQueryStringAsync(this HttpClient client, string uri, Dictionary<string, string> queryStringParams)
		{
			var url = BuildUriWithQueryString(uri, queryStringParams);

			return await client.GetAsync(url);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="requestUri"></param>
		/// <param name="queryStringParams"></param>
		/// <returns></returns>
		public static string BuildUriWithQueryString(string requestUri, Dictionary<string, string> queryStringParams)
		{
			bool startingQuestionMarkAdded = false;
			var sb = new StringBuilder();
			sb.Append(requestUri);
			foreach (var parameter in queryStringParams)
			{
				if (parameter.Value == null)
				{
					continue;
				}

				sb.Append(startingQuestionMarkAdded ? '&' : '?');
				sb.Append(parameter.Key);
				sb.Append('=');
				sb.Append(parameter.Value);
				startingQuestionMarkAdded = true;
			}

			return sb.ToString();
		}
	}
}
