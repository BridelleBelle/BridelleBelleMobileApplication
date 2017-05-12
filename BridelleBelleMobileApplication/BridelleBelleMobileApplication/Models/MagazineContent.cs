using Newtonsoft.Json;

namespace BridelleBelleMobileApplication.Models
{
	public class MagazineContent
	{
		[JsonProperty("pageNumer")]
		public int PageNumber;
		[JsonProperty("pageImage")]
		public byte[] PageImage;
		[JsonProperty("fileName")]
		public string FileName;

		[JsonProperty("base64Val")] public string Base64Value;
	}
}
