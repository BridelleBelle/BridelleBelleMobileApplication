using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

using BridelleBelleMobileApplication.Models;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MagazineViewingOptions : PopupPage
	{
		Magazine Magazine;
		public MagazineViewingOptions (Magazine mag)
		{
			InitializeComponent ();
			this.Magazine = mag;
			options.ItemsSource = new List<string> { "More Information","Preview", "Buy" };
		}

		public async void OpenAdvertiser(object sender, SelectedItemChangedEventArgs e)
		{
			switch (e.SelectedItem.ToString())
			{
				case "More Information":
					ClosePage();
					await Navigation.PushAsync(new MagazineInformation(this.Magazine));
					break;
				case "Buy":
					await Pay();
					break;
				case "Preview":
					ClosePage();
					await Navigation.PushPopupAsync(new MagazinePreview(this.Magazine));
					break;
			}
		}

		private async void ClosePage()
		{
			await Navigation.PopAllPopupAsync();
		}

		public async Task Pay()
		{
			try
			{
				var result = await PayPal.Forms.CrossPayPalManager.Current.Buy(new PayPalItem(this.Magazine.Name, new Decimal(this.Magazine.Price), "GBP"), new Decimal(0));
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
				}
			}
			catch(Exception e)
			{

			}
		}
	}
}
