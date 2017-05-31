using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BridelleBelleMobileApplication.Types;
namespace BridelleBelleMobileApplication.Models
{
	public class SocialMedia
	{
		[JsonProperty("handleType")]
		public HandleTypes HandleType;
		[JsonProperty("handle")]
		public string Handle;
	}
}
