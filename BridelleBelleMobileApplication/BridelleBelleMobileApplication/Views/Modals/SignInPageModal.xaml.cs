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
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;

using BridelleBelleMobileApplication.Types;
using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Views;
using BridelleBelleMobileApplication.Views.MasterDetail;
namespace BridelleBelleMobileApplication.Views.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInPageModal : PopupPage
	{
		SignInIntent Intent;
		Magazine Magazine;
		public SignInPageModal (SignInIntent intent,Magazine mag)
		{
			InitializeComponent ();
			this.Intent = intent;
			Magazine = mag != null ? mag : null;
		}

		public async void Proceed()
		{
			switch (Intent)
			{
				case SignInIntent.Standard:
					await Navigation.PopAllPopupAsync();
					break;
				case SignInIntent.Purchasing:
					if (this.Magazine != null)
					{
						var payPalPayment = new PayPalPayments();
						var result = payPalPayment.Pay(this.Magazine);
					}
					break;
				case SignInIntent.PageViewing:
					await Navigation.PopAllPopupAsync();
						await Navigation.PushAsync(new MasterPage(BridelleBelleMobileApplication.Types.TabbedPage.UserMagazines));
					break;
				case SignInIntent.ViewProfile:
					await Navigation.PopAllPopupAsync();
					await Navigation.PushAsync(new UserProfile());
					break;
			}
		}
		public async void Login(object sender, EventArgs e)
		{
			try
			{
				if (usernameTxt.Text != "" || passwordTxt.Text != "")
				{
					var userDatabase = new UserManager();
					var user = await userDatabase.GetUser(usernameTxt.Text, passwordTxt.Text);
					if (user != null)
					{
						App.SignedInUser = user;

						var magazineManager = new MagazineManager();
						if (App.SignedInUser.Magazines != null)
						{
							App.SignedInUser.OwnedMagazines = await magazineManager.GetMagazines(App.SignedInUser.Magazines.ToList());
						}

						Proceed();
					}
					else
					{
						DisplayAlert("Error", "Error signing in. Please check your username and password and try again.", "OK");
					}
				}
			}
			catch(Exception ex)
			{

			}
		}

		public async void Register(object sender, EventArgs e)
		{
			await Navigation.PushPopupAsync(new RegisterUser());
		}
	}
}
