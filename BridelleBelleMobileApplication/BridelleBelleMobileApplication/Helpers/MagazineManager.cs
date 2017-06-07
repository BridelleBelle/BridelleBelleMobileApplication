using System.Collections.Generic;
using System.Threading.Tasks;

using BridelleBelleMobileApplication.Database;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication
{
	public class MagazineManager
	{
		private DocumnetDBClient DocumentClient;
		private ImageClient ImageClient;

		public MagazineManager()
		{
			DocumentClient = new DocumnetDBClient();
			ImageClient = new ImageClient();
		}

		public List<Magazine> GetLatest()
		{
			return DocumentClient.GetLatest();
		}

		public async Task<string> GetCovers(string fileName)
		{
			return await ImageClient.GetImages(ImageType.CoverImages, fileName);
		}

		public async Task<List<Magazine>> GetMagazines(List<string> ids)
		{
			var magazines = new List<Magazine>();

			foreach(var id in ids)
			{
				magazines.Add(await DocumentClient.Get(id));
			}

			return magazines;
		}

		public IEnumerable<MagazineContent> GetMagazinePages(IEnumerable<MagazineContent> content)
		{
			foreach (var mag in content)
			{
				mag.Uri = ImageClient.GetImageUris(ImageType.Magazines, mag.FileName);
			}

			return content;
		}
	}
}
