using System;
using System.Collections.Generic;
using System.Text;
using BridelleBelleMobileApplication.Database;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication
{
	public class MagazineManager
	{
		private DocumnetDBClient docDb;
		private ImageClient Client;
		public MagazineManager()
		{
			docDb = new DocumnetDBClient();
			Client = new ImageClient();
		}

		public Task<Magazine> Get()
		{
			return docDb.Get("4");
		}

		public async Task<string> GetCovers(string fileName)
		{
			return await Client.GetImages(ImageType.CoverImages, fileName);
		}

		public List<Magazine> GetLatest()
		{
			return docDb.GetLatest();
		}
	}
}
