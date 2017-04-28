using System.Net.Http;
using System.Threading.Tasks;
using BelleBridal.Data;
using Newtonsoft.Json;

namespace BelleBridal.Client
{
	public class MagazineController
	{
		public async Task<Magazine> GetMagazine(long id)
		{
			var client = new HttpClient();
			var result = await client.GetStringAsync("http://localhost:53351/api/Magazine/GetMagazine/" + id);
			return JsonConvert.DeserializeObject<Magazine>(result);
		}
	}
}
