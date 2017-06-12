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
using BridelleBelleMobileApplication.Helpers;
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
					ClosePage();
					Payment();
					break;
				case "Preview":
					ClosePage();
					await Navigation.PushPopupAsync(new MagazinePreview(this.Magazine));
					break;
			}
		}

		public async Task Payment()
		{
			if (App.SignedInUser != null)
			{
				if (this.Magazine.Price > 0.0)
				{
					await Navigation.PushPopupAsync(new CheckoutOptions(this.Magazine));
				}
				else
				{
					var magazineHelper = new MagazineManager();
					magazineHelper.AddMagazineToUserInventory(this.Magazine.Id);
					DisplayAlert("Magazine Purchased", this.Magazine.Name + " was purchased and is now viewable.", "OK");
				}
			}
			else
			{
				DisplayAlert("Error", "Please sign in first.", "OK");
			}
		}
		private async void ClosePage()
		{
			await Navigation.PopAllPopupAsync();
		}
	}
}
