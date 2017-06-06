using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Views;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInPage : ContentPage
	{
		public SignInPage ()
		{
			InitializeComponent ();
			UpdateUI();
		}

		public async void Login(object sender,EventArgs e)
		{
			if(usernameTxt.Text != "" || passwordTxt.Text != "")
			{
				var userDatabase = new UserManager();
				var user = await userDatabase.GetUser(usernameTxt.Text, passwordTxt.Text);
				if(user != null)
				{
					App.SignedInUser = user; 

					var magazineManager = new MagazineManager();
					if(App.SignedInUser.Magazines != null)
					{
						App.SignedInUser.OwnedMagazines = await magazineManager.GetMagazines(App.SignedInUser.Magazines.ToList());
					}
				}

				UpdateUI();
			}
		}

		public async void Register(object sender,EventArgs e)
		{
			await Navigation.PushPopupAsync(new RegisterUser());
		}

		public async void Logout(object sender, EventArgs e)
		{
			App.SignedInUser = null;
			UpdateUI();
		}

		protected override async void OnAppearing()
		{
			UpdateUI();
		}

		public void UpdateUI()
		{
			if (App.SignedInUser != null)
			{
				usernameTxt.IsVisible = false;
				passwordTxt.IsVisible = false;
				loginBtn.IsVisible = false;
				registerBtn.IsVisible = false;
				logout.IsVisible = true;
			}
			else
			{
				usernameTxt.IsVisible = true;
				passwordTxt.IsVisible = true;
				loginBtn.IsVisible = true;
				registerBtn.IsVisible = true;
				logout.IsVisible = false;
			}
		}
	}
}
