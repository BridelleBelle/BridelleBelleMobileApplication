using System;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BridelleBelleMobileApplication.Types;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		private IEnumerable<Magazine> Magazines;

		public MainPage()
		{
			InitializeComponent();
			OnStart();
		}

		void OnStart()
		{
			var tapImage = new TapGestureRecognizer();

			tapImage.Tapped += tapImage_Tapped;
			NEMag1.GestureRecognizers.Add(tapImage);
		}

		async void tapImage_Tapped(object sender, EventArgs e)
		{
			try
			{
				var mags = new List<Magazine>();
				foreach (var mag in App.Magazines)
				{
					if (mag.Version == MagazineVersion.NorthEast)
					{
						mags.Add(mag);
					}
				}

				await Navigation.PushAsync(new PageView(mags.OrderByDescending(x => x.Issue).FirstOrDefault()));
			}
			catch (Exception exception)
			{

			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				App.Magazines = App.Manager.GetLatest();
				await GetCovers();
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public async Task GetCovers()
		{
			for(var i = 0;i< 4; i++)
			{
				if (App.Magazines.ToList()[i].Version == MagazineVersion.NorthEast)
				{
					await ValidateSource(NEMag1, NEMag2, i,MagazineVersion.NorthEast);
				}
				else
				{
					await ValidateSource(YMag1, YMag2, i,MagazineVersion.Yorkshire);
				}
			}
		}

		public async Task ValidateSource(Image img1,Image img2,int index,MagazineVersion version)
		{
			var imageHelper = new ImageHelper();
			if (img1.Source == null && img2.Source == null)
			{
				//get latest - can do this by date or something laters.
				var latest = App.Magazines.OrderByDescending(m => m.Issue).FirstOrDefault(m => m.Version == version);
				var img = await imageHelper.ConvertImage(latest.CoverImageFileName);
				img1.Source = img.Source;
			}
			else if (img1.Source != null && img2.Source == null)
			{
				var img = await imageHelper.ConvertImage(App.Magazines.ToList()[index].CoverImageFileName);
				img2.Source = img.Source;
			}
		}
	}
}