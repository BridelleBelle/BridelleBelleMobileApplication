using System.Collections.Generic;

namespace BridalBelleApp.Data
{
	public class Advertiser
	{
		public long ID;
		public string Name;

		public string Email;
		public string Telephone;
		public string Website;

		public Address Address;
		public IEnumerable<SocialMedia> SocialMedia;
	}
}
