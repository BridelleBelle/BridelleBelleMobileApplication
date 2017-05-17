using System;
using Xamarin.Forms;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication
{
	public class HandleController
	{
		public string FacebookHandle= "https://www.facebook.com/";
		public string TwitterHandle = "https://twitter.com/";
		public string InstagramHandle = "https://www.instagram.com/";

		public void OpenHandle(HandleTypes media, string id)
		{
			var handle = String.Empty;

			if(media == HandleTypes.Facebook)
			{
				handle = FacebookHandle;
			}
			else if(media == HandleTypes.Twitter)
			{
				handle = TwitterHandle;
			}
			else if(media == HandleTypes.Instagram)
			{
				handle = InstagramHandle;
				
			}

			handle += id;

			Device.OpenUri(new Uri(handle));
		}

		public void OpenMaps(string address)
		{
			var location = String.Empty;

			if(Device.OS == TargetPlatform.Android)
			{
				location = string.Format("geo:0,0?q={0}({1})", address, "location test");
			}
			else if(Device.OS == TargetPlatform.iOS)
			{
				location = string.Format("http://maps.apple.com/maps?q={0}&sll={1}", "test", address);
			}

			Device.OpenUri(new Uri(location));
		}
	}
}
