using System.Collections.Generic;
using System.Threading.Tasks;
using BelleBridal.Persistence;
using BridalBelle.Data;

namespace BelleBridal.Service
{
	public class MagazineService
	{
		public MagazinePersistence Persistence;

		public MagazineService()
		{
			Persistence = new MagazinePersistence();
		}

		public async Task<Magazine> Get(string id)
		{
			return await Persistence.Get(id);
		}

		public async Task<IEnumerable<Magazine>> GetAll()
		{
			return await GetAll();
		}
	}
}
