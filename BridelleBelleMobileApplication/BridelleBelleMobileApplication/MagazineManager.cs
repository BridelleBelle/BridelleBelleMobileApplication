using System.Collections.Generic;
using BridelleBelleMobileApplication.Database;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication
{
	public class MagazineManager
	{
		private DocumentDBClient docDb;
		private ImageClient Client;
		public MagazineManager()
		{
			docDb = new DocumentDBClient();
			Client = new ImageClient();
		}

		public async Task<string> GetCovers(string fileName)
		{
			return await Client.GetImages(ImageType.CoverImages, fileName);
		}

		public List<Magazine> GetLatest()
		{
			return docDb.GetLatest();
		}

		public IEnumerable<MagazineContent> GetMagazinePages(IEnumerable<MagazineContent> content)
		{
			foreach (var mag in content)
			{
				mag.Uri = Client.GetImageUris(ImageType.Magazines, mag.FileName);
			}

			return content;
		}
	}
}
