using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace BridelleBelleMobileApplication.Models
{
	public class RegisteredUser
	{
		[JsonProperty("username")]
		public string Username;
		[JsonProperty("password")]
		public string Password;
	}
}
