using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Views;
namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageView : ContentPage
	{
		private int cap = 20;
		private int currLoad = 0;

		private Magazine Magazine;
		private List<Image> Images;
		private int currPage = 0;

		public PageView(Magazine mag)
		{
			InitializeComponent();
			if (mag != null)
			{

				this.Magazine = mag;
				MainCarouselView.ItemsSource = Load();
			}
		}

		private void MainCarouselView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			MainLabel.Text = e.SelectedItem as string;
			var selectedItem = this.Magazine.MagazineContent.Where(m => m.Uri == e.SelectedItem.ToString()).FirstOrDefault();
			currPage = selectedItem.PageNumber;
			var advertiser = this.Magazine.Advertisers.Where(p => p.Page == selectedItem.PageNumber);

			if (advertiser.Any())
			{
				AdsButton.IsVisible = true;
			}
			else
			{
				AdsButton.IsVisible = false;
			}
		}

		private List<string> Load()
		{
			var magManager = new MagazineManager();
			var images = new List<string>();
			this.Magazine.MagazineContent =  magManager.GetMagazinePages(this.Magazine.MagazineContent);

			foreach (var uri in this.Magazine.MagazineContent)
			{
				images.Add(uri.Uri);
			}

			return images;
		}

		async void ShowAdvertiserDetails(object sender, EventArgs e)
		{
			if (AdsButton.IsVisible)
			{
				await Navigation.PushAsync(new AdvertiserInformation(this.Magazine.Advertisers.Where(p => p.Page == currPage).FirstOrDefault()));
			}
		}
	}
}
