using System;
using System.Net.Http;
using System.Threading.Tasks;
using BelleBridal.Data;
using Newtonsoft.Json;

namespace BelleBridal.Client
{
	public class MagazineController
	{
		private string URL = "http://localhost:53351/api/Magazine";

		public async Task<Magazine> GetMagazine(long id)
		{
			var client = new HttpClient();
			URL += String.Format("/GetMagazine/{0}", id);
			var result = await client.GetStringAsync(URL);
			return JsonConvert.DeserializeObject<Magazine>(result);
		}
	}
}
