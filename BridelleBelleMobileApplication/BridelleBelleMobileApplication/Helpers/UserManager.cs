using System;
using System.Collections.Generic;
using System.Text;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Database;
namespace BridelleBelleMobileApplication.Helpers
{
	public class UserManager
	{
		DocumnetDBClient Client;

		public UserManager()
		{
			Client = new DocumnetDBClient();
		}

		public Models.User GetUser(string username, string password)
		{
			return Client.GetUser(username, password);
		}
	}
}
