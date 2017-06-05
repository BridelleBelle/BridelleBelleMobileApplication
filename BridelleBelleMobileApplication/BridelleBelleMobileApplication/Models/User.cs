using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using BridelleBelleMobileApplication.Models;
namespace BridelleBelleMobileApplication.Models
{
	public class User
	{
		[JsonProperty("id")]
		public string Id;
		[JsonProperty("username")]
		public string Username;
		[JsonProperty("magazines")]
		public IEnumerable<string> Magazines; //stores the ids

		public IEnumerable<Magazine> OwnedMagazines;
	}
}
