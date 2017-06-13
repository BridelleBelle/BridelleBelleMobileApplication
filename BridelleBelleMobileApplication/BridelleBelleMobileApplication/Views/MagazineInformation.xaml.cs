using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;
using BridelleBelleMobileApplication.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MagazineInformation : ContentPage
	{
		private Magazine Magazine;
		public MagazineInformation (Magazine mag)
		{
			InitializeComponent ();

			if(mag != null)
			{
				this.Magazine = mag;
			}

			SetupUI();
		}

		public void SetupUI()
		{
			name.Text = this.Magazine.Name;
			issue.Text = "Issue: " + this.Magazine.Issue;
			version.Text = this.Magazine.Version == MagazineVersion.NorthEast ? "North East" : "Yorkshire";

			var imageHelper = new ImageHelper();
			this.Magazine.CoverImageUri = imageHelper.GetImageUri(ImageType.CoverImages, this.Magazine.CoverImageFileName);
			cover.Source = this.Magazine.CoverImageUri;
		}

		public async void Preview(object sender, EventArgs e)
		{
			await Navigation.PushPopupAsync(new MagazinePreview(this.Magazine));
		}
	}
}
