using System.Text.Json.Serialization;

namespace SandevLibraryClassicNetFramework.BaseResponseApi
{
	public class BaseResponseJSon<TResponse>
	{
		[JsonPropertyName("error")]
		public ResponseError Error { get; set; }
        [JsonPropertyName("requestType")]
        public string RequestType { get; set; }
		[JsonPropertyName("statusCode")]
		public int? StatusCode { get; set; }
		[JsonPropertyName("message")]
		public string Message { get; set; }
		[JsonPropertyName("count")]
		public int Count { get; set; }
		[JsonPropertyName("data")]
		public TResponse Data { get; set; }
	}
}
