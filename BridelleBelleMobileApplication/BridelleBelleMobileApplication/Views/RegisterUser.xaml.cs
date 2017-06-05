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
namespace BridelleBelleMobileApplication.Views
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
					var userManager = new UserManager();
					var passwordHash = new PasswordHash();
					await userManager.CreateUser(usernameTxt.Text, passwordHash.Encode(passwordTxt.Text));
					App.SignedInUser = await userManager.GetUser(usernameTxt.Text, passwordTxt.Text);
					await Navigation.PopAllPopupAsync();
				}
			}
		}
	}
}
