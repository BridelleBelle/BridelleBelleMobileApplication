using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;

using BridelleBelleMobileApplication.Types;
namespace BridelleBelleMobileApplication
{
	public class ImageHelper
	{
		private ImageClient client;

		public async Task<Image> ConvertImage(string fileName)
		{
			var bytes = Convert.FromBase64String(await App.Manager.GetCovers(fileName));
			Stream stream = new MemoryStream(bytes);
			var result = new Image();
			result.Source = ImageSource.FromStream(() => { return stream; });
			return result;
		}

		public async Task<Image>GetAdImage(string fileName)
		{
			client = new ImageClient();
			return ConvertByteToImage(Convert.FromBase64String(await client.GetImages(ImageType.Magazines, fileName)));
		}

		public string GetImageUri(ImageType type, string fileName)
		{
			client = new ImageClient();
			return client.GetImageUris(type, fileName);
		}

		public Image ConvertByteToImage(byte[]bytes)
		{
			Stream stream = new MemoryStream(bytes);
			var result = new Image();
			result.Source = ImageSource.FromStream(() => { return stream; });
			return result;

		}
	}
}
