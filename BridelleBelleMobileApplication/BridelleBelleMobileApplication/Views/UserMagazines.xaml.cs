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
namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserMagazines : ContentPage
	{
		public UserMagazines ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			if (App.SignedInUser != null)
			{
				try
				{
					var userViewModel = SetupViewModel();
					var images = new List<Image>();
					var imagehelper = new ImageHelper();
					foreach (var magazine in userViewModel.OwnedMagazines)
					{
						magazine.Magazine.CoverImage = await imagehelper.GetImage(ImageType.CoverImages, magazine.Magazine.CoverImageFileName);

					}

					BindingContext = userViewModel;
				}
				catch(Exception exception)
				{

				}
			}
		}

		public UserViewModel SetupViewModel()
		{
			var magazines = new List<MagazineViewModel>();

			foreach(var m in App.SignedInUser.OwnedMagazines)
			{
				magazines.Add(new MagazineViewModel { Magazine = m });
			}
			return new UserViewModel()
			{
				Username = App.SignedInUser.Username,
				OwnedMagazines = magazines
			};
		}
	}
}
