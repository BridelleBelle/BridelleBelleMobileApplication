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
			if(App.AvailableMagazines != null)
			{
				SetCovers();
			}
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
				if (App.AvailableMagazines == null)
				{
					var magazineManager = new MagazineManager();
					App.AvailableMagazines = magazineManager.GetLatest();


					if (App.NEMagCovers == null || App.YorkMagCovers == null)
					{
						await GetCovers();
					}

					SetCovers();
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}
		}

		public async Task GetCovers()
		{
			try
			{
				foreach (var mag in App.AvailableMagazines)
				{
					var imageHelper = new ImageHelper();
					mag.CoverImageUri = imageHelper.GetImageUri(ImageType.CoverImages, mag.CoverImageFileName);
				}

				var mags = App.AvailableMagazines;
				var neMags = new List<Magazine>();
				var yorkMags = new List<Magazine>();

				foreach(var mag in mags)
				{
					if(mag.Version == MagazineVersion.NorthEast)
					{
						neMags.Add(mag);
					}
					else
					{
						yorkMags.Add(mag);
					}
				}

				CacheCovers(neMags.OrderByDescending(m => m.ReleaseDate).ToList(), yorkMags.OrderByDescending(m => m.ReleaseDate).ToList());
			}
			catch(Exception exception) { }
		}

		private void CacheCovers(List<Magazine> orderedNECovers, List<Magazine> orderedYorkMags)
		{
			App.NEMagCovers = new List<string>();
			App.YorkMagCovers = new List<string>();
			App.NEMagCovers.Add(orderedNECovers[0].CoverImageUri);
			App.NEMagCovers.Add(orderedNECovers[1].CoverImageUri);
			App.YorkMagCovers.Add(orderedYorkMags[0].CoverImageUri);
			App.YorkMagCovers.Add(orderedYorkMags[1].CoverImageUri);
		}

		private void SetCovers()
		{
			NEMag1.Source = App.NEMagCovers[0];
			NEMag2.Source = App.NEMagCovers[1];
			YMag1.Source = App.YorkMagCovers[0];
			YMag2.Source = App.YorkMagCovers[1];
		}
	}
}