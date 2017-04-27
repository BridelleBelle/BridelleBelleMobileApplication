using System.Collections.Generic;
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
		[JsonProperty("pdfFileName")]
		public string PdfFileName; //shouldnt probably be a byte. just a dummy object for now
		[JsonProperty("pdf")]
		public byte[] PDF;
		[JsonProperty("coverImageFileName")]
		public string CoverImageFileName;
		[JsonProperty("coverImage")]
		public byte[] CoverImage;

		[JsonProperty("advertisers")]
		public IEnumerable<MagazineAdvertiser> Advertisers;
	}
}
