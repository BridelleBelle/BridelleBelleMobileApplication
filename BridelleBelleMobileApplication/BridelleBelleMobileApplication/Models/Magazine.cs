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
		[JsonProperty("pdfFileName")]
		public string PdfFileName; //shouldnt probably be a byte. just a dummy object for now
		[JsonProperty("pdf")]
		public byte[] PDF;
		[JsonProperty("coverImageFileName")]
		public string CoverImageFileName;
		[JsonProperty("coverImage")]
		public Image CoverImage;

		[JsonProperty("advertisers")]
		public IEnumerable<MagazineAdvertiser> Advertisers;

		[JsonProperty("magazineContent")]
		public IEnumerable<MagazineContent> MagazineContent;
	}
}
