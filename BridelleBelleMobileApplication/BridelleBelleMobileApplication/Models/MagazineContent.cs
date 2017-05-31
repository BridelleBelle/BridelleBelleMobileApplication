using Newtonsoft.Json;

namespace BridelleBelleMobileApplication.Models
{
	public class MagazineContent
	{
		[JsonProperty("pageNumber")]
		public int PageNumber;
		[JsonProperty("pageImage")]
		public byte[] PageImage;
		[JsonProperty("fileName")]
		public string FileName;
		[JsonProperty("uri")]
		public string Uri;
	}
}
