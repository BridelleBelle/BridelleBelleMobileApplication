using System;
using System.Collections.Generic;
using System.Text;

namespace BridelleBelleMobileApplication.Helpers
{
	public class PasswordHash
	{
		public string Encode(string password)
		{
			var bytes = new UTF8Encoding().GetBytes(password);
			var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
			return Convert.ToBase64String(hashBytes);
		}
	}
}
