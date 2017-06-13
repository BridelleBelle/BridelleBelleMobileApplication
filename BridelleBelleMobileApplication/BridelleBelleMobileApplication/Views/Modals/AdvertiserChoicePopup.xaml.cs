using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Models;

namespace BridelleBelleMobileApplication.Views.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdvertiserChoicePopup : PopupPage
	{
		List<MagazineAdvertiser> Advertisers;
		public AdvertiserChoicePopup (IEnumerable<MagazineAdvertiser>advertisers)
		{
			InitializeComponent ();
			this.Advertisers = advertisers.ToList();
			SetUpAdChoice(this.Advertisers);
		}

		private void SetUpAdChoice(IEnumerable<MagazineAdvertiser> advertisers)
		{
			var ads = advertisers.ToList();
			var adNames = new List<string>();

			foreach (var ad in ads)
			{
				adNames.Add(ad.Advertiser.Name);
			}

			options.ItemsSource = adNames.ToArray();
		}

		private async void OpenAdvertiser(object sender, SelectedItemChangedEventArgs e)
		{
			var advertiser = new MagazineAdvertiser();

			foreach (var ad in this.Advertisers)
			{
				if (ad.Advertiser.Name == e.SelectedItem.ToString())
				{
					advertiser = ad;
				}
			}

			await Navigation.PopAllPopupAsync();
			await Navigation.PushAsync(new AdvertiserInformation(advertiser));
		}
	}
}
