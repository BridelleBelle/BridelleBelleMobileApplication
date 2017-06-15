using System.Collections.Generic;
using BridelleBelleMobileApplication.Types;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace BridelleBelleMobileApplication.Models
{
	public class Magazine
	{
		[JsonProperty("id")]
		public string Id;
		[JsonProperty("name")]
		public string Name;
		[JsonProperty("version")]
		public MagazineVersion Version;
		[JsonProperty("issue")]
		public string Issue;
		[JsonProperty("pages")]
		public int Pages;
		[JsonProperty("coverImageFileName")]
		public string CoverImageFileName;
		[JsonProperty("coverImage")]
		public string CoverImageUri;
		[JsonProperty("price")]
		public double Price;
		[JsonProperty("releaseDate")]
		public string ReleaseDate;
		[JsonProperty("advertisers")]
		public IEnumerable<MagazineAdvertiser> Advertisers;

		[JsonProperty("magazineContent")]
		public IEnumerable<MagazineContent> MagazineContent;
	}
}
