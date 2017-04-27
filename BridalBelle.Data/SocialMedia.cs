using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BridalBelle.Data
{
	public class SocialMedia
	{
		[JsonProperty("name")]
		public string Name;
		[JsonProperty("handle")]
		public string Handle;
	}
}
