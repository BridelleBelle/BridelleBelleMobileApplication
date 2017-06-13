using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfile : ContentPage
	{
		public UserProfile ()
		{
			InitializeComponent ();
		}

		public async void Logout(object sender,EventArgs e)
		{
			App.SignedInUser = null;
			await Navigation.PushAsync(new HomePage(BridelleBelleMobileApplication.Types.HomePage));
		}
	}
}
