using System;
using System.Collections.Generic;
using System.Text;

namespace BridelleBelleMobileApplication.Helpers
{
	public class PasswordHash
	{
		public string Encode(string password)
		{
			var toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
			var result = System.Convert.ToBase64String(toEncodeAsBytes);
			return result;
		}
	}
}
