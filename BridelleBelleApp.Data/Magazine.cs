namespace BridelleBelleApp.Data
{
	public class Magazine
	{
		public long ID;
		public string Name;
		public string Version;
		public string issue;
		public string Pages;

		public byte[] PDF; //shouldnt probably be a byte. just a dummy object for now
		public byte[] CoverImage;

		Advertiser Advertiser;
	}
}
