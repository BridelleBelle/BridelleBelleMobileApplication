using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Types;
using BridelleBelleMobileApplication.ViewModels;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Views.Modals;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMagazines : ContentPage
	{
		MagazineViewModel Magazines;
		public UserMagazines ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			header.IsVisible = true;

			if (App.SignedInUser != null)
			{
				magazines.IsVisible = false;
				signin.IsVisible = false;
				try
				{
					Magazines = SetupViewModel();
					magazines.ItemsSource = GetCovers(this.Magazines);
				}
				catch(Exception exception)
				{
					System.Diagnostics.Debug.WriteLine(exception.Message);
				}
			}
			else
			{
				header.Text = "Please Sign In";
				signin.IsVisible = true;
			}
		}

		public async void SignIn(object sender, EventArgs e)
		{
			await Navigation.PushPopupAsync(new SignInPageModal(SignInIntent.PageViewing, null));
		}

		public MagazineViewModel SetupViewModel()
		{
			var magazines = new List<Magazine>();

			foreach(var m in App.SignedInUser.OwnedMagazines)
			{
				magazines.Add(m);
			}
			return new MagazineViewModel()
			{
				Magazines = magazines
			};
		}

		public List<string> GetCovers(MagazineViewModel mag)
		{
			var imageHelper = new ImageHelper();
			List<string> uris = new List<string>();
			foreach(var m in mag.Magazines)
			{
				var uri = imageHelper.GetImageUri(ImageType.CoverImages,m.CoverImageFileName);
				uris.Add(uri);
				m.CoverImageUri = uri;
			}

			return uris;
		}

		private async void OpenMagazine(object sender, SelectedItemChangedEventArgs e)
		{
			var magazine = new Magazine();

			foreach(var mag in this.Magazines.Magazines)
			{
				if(mag.CoverImageUri == e.SelectedItem.ToString())
				{
					magazine = mag;
				}
			}

			await Navigation.PushAsync(new PageView(magazine));
		}
	}
}
