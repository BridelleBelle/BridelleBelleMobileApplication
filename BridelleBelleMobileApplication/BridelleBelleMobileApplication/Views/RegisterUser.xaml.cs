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
			var userManager = new UserManager();
			await userManager.CreateUser(usernameTxt.Text, passwordTxt.Text);
		}
	}
}
