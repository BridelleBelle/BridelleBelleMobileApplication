using Newtonsoft.Json;

namespace BridalBelle.Data
{
	public class MagazineAdvertiser
	{
		[JsonProperty("advertiser")]
		public Advertiser Advertiser;
		[JsonProperty("page")]
		public int Page;
	}
}
