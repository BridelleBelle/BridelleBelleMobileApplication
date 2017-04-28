using System.Collections.Generic;
using Newtonsoft.Json;

namespace BelleBridal.Data
{
	public class Advertiser
	{
		[JsonProperty("id")]
		public long ID;
		[JsonProperty("name")]
		public string Name;
		[JsonProperty("email")]
		public string Email;
		[JsonProperty("telephone")]
		public string Telephone;
		[JsonProperty("website")]
		public string Website;
		[JsonProperty("address")]
		public Address Address;
		[JsonProperty("socialMedia")]
		public IEnumerable<SocialMedia> SocialMedia;
	}
}