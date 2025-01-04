namespace SandevLibraryClassicNetFramework.BaseResponseApi
{
	public class ResponseError
	{
		public int? ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorTrace { get; set; }
	}
}
