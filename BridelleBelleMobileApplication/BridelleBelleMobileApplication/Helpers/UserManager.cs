﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Database;

namespace BridelleBelleMobileApplication.Helpers
{
	public class UserManager
	{
        DocumentDBClient Client;

		public UserManager()
		{
			Client = new DocumentDBClient();
		}

		public async Task< Models.User> GetUser(string username, string password)
		{
			return await Client.GetUser(username, password);
		}

		public async Task CreateUser(string email,string password)
		{
			await Client.AddUser(new RegisteredUser { Username = email, Password = password });
		}
	}
}
