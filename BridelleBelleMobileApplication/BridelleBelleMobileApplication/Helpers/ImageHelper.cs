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
		private ImageClient Client;

		public async Task<Image> ConvertImage(string fileName)
		{
			var bytes = Convert.FromBase64String(fileName);
			Stream stream = new MemoryStream(bytes);
			var result = new Image();
			result.Source = ImageSource.FromStream(() => { return stream; });
			return result;
		}

		public async Task<Image>GetImage(ImageType type, string fileName)
		{
			Client = new ImageClient();
			return ConvertByteToImage(Convert.FromBase64String(await Client.GetImages(type, fileName)));
		}

		private async Task<string> GetCovers(string fileName)
		{
			return await Client.GetImages(ImageType.CoverImages, fileName);
		}

		public string GetImageUri(ImageType type, string fileName)
		{
			Client = new ImageClient();
			return Client.GetImageUris(type, fileName);
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
