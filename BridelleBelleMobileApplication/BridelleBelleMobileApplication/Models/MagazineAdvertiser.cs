using Newtonsoft.Json;

namespace BridelleBelleMobileApplication.Models
{
	public class MagazineAdvertiser
	{
		[JsonProperty("advertiser")]
		public Advertiser Advertiser;
		[JsonProperty("page")]
		public int Page;
	}
}
