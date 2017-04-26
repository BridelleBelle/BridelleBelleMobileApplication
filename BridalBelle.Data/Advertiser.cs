using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BridalBelle.Data
{
	public class Advertiser
	{
		[JsonProperty("id")]
		public string Id;
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
