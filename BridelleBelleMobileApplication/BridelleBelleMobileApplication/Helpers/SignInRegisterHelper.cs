using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using BridelleBelleMobileApplication.Database;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication.Helpers
{
	public class SignInRegisterHelper
	{
        DocumentDBClient Client;

		public SignInRegisterHelper()
		{
			Client = new DocumentDBClient();
		}

		public async Task<SignInRegisterResponse> RegisterUser(string username, string password)
		{
			var user = new RegisteredUser
			{
				Username = username,
				Password = password
			};
			//check username
			var response = Client.ValidateUsername(user.Username);
			if(response != SignInRegisterResponse.OK)
			{
				return response;
			}
			else
			{
				return await Client.AddUser(user);
			}
		}
	}
}
