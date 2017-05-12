using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
namespace BridelleBelleMobileApplication
{
	public class ImageHelper
	{
		public async Task<Image> ConvertImage(string fileName)
		{
			var bytes = Convert.FromBase64String(await App.Manager.GetCovers(fileName));
			Stream stream = new MemoryStream(bytes);
			var result = new Image();
			result.Source = ImageSource.FromStream(() => { return stream; });
			return result;
		}
	}
}
