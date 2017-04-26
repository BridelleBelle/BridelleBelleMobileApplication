using System;

namespace BridalBelle.Data
{
	public class Voucher
	{
		public string Code;

		public string GenerateVoucher()
		{
			var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz1234567890";
			var code = String.Empty;
			var rand = new Random();
			for(var i = 0; i < 25; i++)
			{
				code += characters.ToCharArray()[rand.Next(0, characters.ToCharArray().Length)];
			}

			return code;
		}
	}
}
