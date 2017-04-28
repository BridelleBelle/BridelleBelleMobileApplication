using Newtonsoft.Json;

namespace BelleBridal.Data
{
	public class SocialMedia
	{
		[JsonProperty("name")]
		public string Name;
		[JsonProperty("handle")]
		public string Handle;
	}
}
