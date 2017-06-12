using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using BridelleBelleMobileApplication.Database;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Helpers;

namespace BridelleBelleMobileApplication.Helpers
{
	public class PayPalPayments
	{
		public async Task<PayPalStatus> Pay(Magazine mag)
		{
			try
			{
				var result = await PayPal.Forms.CrossPayPalManager.Current.Buy(new PayPalItem(mag.Name, new Decimal(5.99), "GBP"), new Decimal(0));
				if (result.Status == PayPalStatus.Cancelled)
				{
					System.Diagnostics.Debug.WriteLine("Cancelled");
				}
				else if (result.Status == PayPalStatus.Error)
				{
					System.Diagnostics.Debug.WriteLine(result.ErrorMessage);
				}
				else if (result.Status == PayPalStatus.Successful)
				{
					System.Diagnostics.Debug.WriteLine(result.ServerResponse.Response.Id);
					var magazineManager = new MagazineManager();
					magazineManager.AddMagazineToUserInventory(mag.Id);
				}

				return result.Status;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				return PayPalStatus.Error;
			}
		}
	}
}
