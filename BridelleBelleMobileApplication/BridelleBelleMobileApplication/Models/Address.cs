using Newtonsoft.Json;

namespace BridelleBelleMobileApplication.Models
{
	public class Address
	{
		[JsonProperty("city")]
		public string City;
		[JsonProperty("street")]
		public string Street;
		[JsonProperty("no")]
		public int No;
		[JsonProperty("geolocation")]
		public double [] GeoLocation = new double[2];
		[JsonProperty("county")]
		public string County;
		[JsonProperty("country")]
		public string Country;
	}
}
