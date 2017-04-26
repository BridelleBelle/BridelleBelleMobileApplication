using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json;

namespace BridalBelle.Data
{
	public class Magazine
	{
		[JsonProperty("id")]
		public string Id;
		[JsonProperty("name")]
		public string Name;
		[JsonProperty("version")]
		public string Version;
		[JsonProperty("issue")]
		public string Issue;
		[JsonProperty("pages")]
		public int Pages;

		[JsonProperty("pdf")]
		public string PDF; //shouldnt probably be a byte. just a dummy object for now
		[JsonProperty("coverImage")]
		public string CoverImage;

		//public Advertiser Advertiser;
	}
}
