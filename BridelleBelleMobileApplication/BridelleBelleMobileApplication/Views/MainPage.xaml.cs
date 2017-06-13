using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

using BridelleBelleMobileApplication.Types;
using BridelleBelleMobileApplication.Views;
using BridelleBelleMobileApplication.Views.Modals;
using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Models;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			Setup();
		}

		void Setup()
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
				foreach (var mag in App.AvailableMagazines)
				{
					if (mag.Version == MagazineVersion.NorthEast)
					{
						mags.Add(mag);
					}
				}

				var selected = mags.OrderByDescending(x => x.Issue).FirstOrDefault();

				if (App.SignedInUser == null || App.SignedInUser.Magazines == null || !App.SignedInUser.Magazines.Contains(selected.Id))
				{
					await Navigation.PushPopupAsync(new MagazineViewingOptions(selected));
				}
				else
				{
					await Navigation.PushAsync(new PageView(mags.OrderByDescending(x => x.Issue).FirstOrDefault()));
				}
			}
			catch (Exception exception)
			{
				System.Diagnostics.Debug.WriteLine(exception.Message);
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				var magazineManager = new MagazineManager();
				App.AvailableMagazines = magazineManager.GetLatest();
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
				if (App.AvailableMagazines.ToList()[i].Version == MagazineVersion.NorthEast)
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
				var latest = App.AvailableMagazines.OrderByDescending(m => m.Issue).FirstOrDefault(m => m.Version == version);
				var img = await imageHelper.GetImage(ImageType.CoverImages,latest.CoverImageFileName);
				img1.Source = img.Source;
			}
			else if (img1.Source != null && img2.Source == null)
			{
				var img = await imageHelper.GetImage(ImageType.CoverImages,App.AvailableMagazines.ToList()[index].CoverImageFileName);
				img2.Source = img.Source;
			}
		}
	}
}