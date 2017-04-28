using Newtonsoft.Json;

namespace BelleBridal.Data
{
	public class MagazineAdvertiser
	{
		[JsonProperty("advertiser")]
		public Advertiser Advertiser;
		[JsonProperty("page")]
		public int Page;
	}
}
