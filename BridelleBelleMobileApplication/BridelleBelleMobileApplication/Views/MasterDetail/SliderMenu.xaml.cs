using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BridelleBelleMobileApplication.Views;
using BridelleBelleMobileApplication.Views.Modals;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
namespace BridelleBelleMobileApplication.Views.MasterDetail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SliderMenu : ContentPage
	{
		public SliderMenu ()
		{
			InitializeComponent ();
			sliderMenu.Header = "Menu";
			sliderMenu.ItemsSource = new List<string> { "User Profile" };
		}

		public async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			switch (e.SelectedItem.ToString())
			{
				case "User Profile":
					UserProfileLoad();
					break;
			}
		}

		public async void UserProfileLoad()
		{
			if(App.SignedInUser != null)
			{
				await Navigation.PushAsync(new UserProfile());
			}
			else
			{
				//signin
				await Navigation.PushPopupAsync(new SignInPageModal(BridelleBelleMobileApplication.Types.SignInIntent.ViewProfile,null));
			}
		}
	}
}
