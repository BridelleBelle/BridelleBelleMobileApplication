using System.Collections.Generic;
using System.Threading.Tasks;
using BridalBelle.Data;
using BridalBelle.Database;

namespace BelleBridal.Persistence
{
	public class MagazinePersistence
	{
		public DocumentDbClient<Magazine> Client;

		public MagazinePersistence()
		{
			Client = new DocumentDbClient<Magazine>("magazines");
		}

		public async Task<Magazine> Get(string id)
		{
			return await Client.Get(id);
		}

		public async Task<IEnumerable<Magazine>> GetAll()
		{
			return await Client.GetAll();
		}
	}
}
