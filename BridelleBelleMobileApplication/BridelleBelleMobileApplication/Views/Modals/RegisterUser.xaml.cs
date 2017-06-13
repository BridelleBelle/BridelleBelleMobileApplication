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
using BridelleBelleMobileApplication.Helpers;
using BridelleBelleMobileApplication.Types;
namespace BridelleBelleMobileApplication.Views.Modals
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterUser : PopupPage
	{
		public RegisterUser ()
		{
			InitializeComponent ();
		}

		public async void Store(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(usernameTxt.Text) || !String.IsNullOrEmpty(passwordTxt.Text) || !String.IsNullOrEmpty(passwordTxt2.Text))
			{
				if(passwordTxt.Text == passwordTxt2.Text)
				{
					var signInHelper = new SignInRegisterHelper();
					var passwordHash = new PasswordHash();
					var response = await signInHelper.RegisterUser(usernameTxt.Text, passwordHash.Encode(passwordTxt.Text));
					if(response == SignInRegisterResponse.OK)
					{
						DisplayAlert("Successful", "Registeration Successful. Please sign in.", "OK");
						await Navigation.PopAllPopupAsync();
					}
					else if(response == SignInRegisterResponse.UsernameUsed)
					{
						DisplayAlert("Username in use.", "Usename has been taken.", "OK");
					}
					else
					{
						DisplayAlert("Error", "An issue occured. Please try again.", "OK");
					}
				}
			}
		}
	}
}
