using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Helpers;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInPage : ContentPage
	{
		public SignInPage ()
		{
			InitializeComponent ();
		}

		public async void Login(object sender,EventArgs e)
		{
			if(usernameTxt.Text != "" || passwordTxt.Text != "")
			{
				var userDatabase = new UserManager();
				var user = userDatabase.GetUser("danielle-19-93@live.com", "carla");
				if(user != null)
				{
					App.SignedInUser = user;

					var magazineManager = new MagazineManager();
					App.SignedInUser.OwnedMagazines = await magazineManager.GetMagazines(App.SignedInUser.Magazines.ToList());
				}
			}
		}
	}
}
