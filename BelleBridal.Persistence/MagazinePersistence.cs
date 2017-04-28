using System.Threading.Tasks;
using BelleBridal.Client;
using BelleBridal.Data;

namespace BelleBridal.Persistence
{
	public class MagazinePersistence
	{
		private MagazineController Controller;

		public MagazinePersistence()
		{
			Controller = new MagazineController();
		}

		public async Task<Magazine> GetMagazine(long id)
		{
			return await Controller.GetMagazine(id);
		}
	}
}
