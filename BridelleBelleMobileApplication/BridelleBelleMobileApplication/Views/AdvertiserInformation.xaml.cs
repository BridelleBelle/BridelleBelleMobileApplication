using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;
using BridelleBelleMobileApplication.Helpers;

namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdvertiserInformation : ContentPage
	{
		MagazineAdvertiser Advertiser;
		HandlesController HandleController;

		string facebookHandle = String.Empty;
		string twitterHandle = String.Empty;
		string instagramHandle = String.Empty;
		string googleAddress = String.Empty;

		public AdvertiserInformation (MagazineAdvertiser advertiser)
		{
			InitializeComponent ();
			this.Advertiser = advertiser;
			this.HandleController = new HandlesController();
			advertiserLabel.Text = advertiser.Advertiser.Name;
			advertiserInformation.Text = advertiser.Advertiser.AdvertiserInformation;

			SetUpSocialMedia(advertiser);
			SetupContactLabels(advertiser);
		}

		async void Facebook_Tapped(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.facebookHandle))
			{
				this.HandleController.OpenHandle(HandleTypes.Facebook, this.facebookHandle);
			}
		}

		async void Twitter_Tapped(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.twitterHandle))
			{
				this.HandleController.OpenHandle(HandleTypes.Twitter, this.twitterHandle);
			}
		}

		async void Insta_Tapped(object sender,EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.instagramHandle))
			{
				this.HandleController.OpenHandle(HandleTypes.Instagram, this.instagramHandle);
			}
		}

		async void Maps_Tapped(object sender,EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.googleAddress))
			{
				this.HandleController.OpenMaps(this.googleAddress);
			} 
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				var imageHelper = new ImageHelper();
				var image = await imageHelper.GetImage(ImageType.Magazines,this.Advertiser.Advertiser.ImageFileName);
				advertiserImage.Source = image.Source;
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}
		private void SetUpSocialMedia(MagazineAdvertiser advertiser)
		{
			foreach (var media in advertiser.Advertiser.SocialMedia)
			{
				if (media.HandleType == HandleTypes.Facebook)
				{
					FacebookIcon.IsVisible = true;
					this.facebookHandle = media.Handle;
				}
				else if (media.HandleType == HandleTypes.Twitter)
				{
					TwitterIcon.IsVisible = true;
					this.twitterHandle = media.Handle;
				}
				else if (media.HandleType == HandleTypes.Instagram)
				{
					InstagramIcon.IsVisible = true;
					this.instagramHandle = media.Handle;
				}
			}

			if (advertiser.Advertiser.Address != null)
			{
				MapsIcon.IsVisible = true;
				this.googleAddress = advertiser.Advertiser.Address.No + " " + advertiser.Advertiser.Address.Street + " " + advertiser.Advertiser.Address.City;
			}

			var tapFacebook = new TapGestureRecognizer();
			var tapMaps = new TapGestureRecognizer();
			var tapInsta = new TapGestureRecognizer();
			var tapTwitter = new TapGestureRecognizer();

			if (FacebookIcon.IsVisible)
			{
				tapFacebook.Tapped += Facebook_Tapped;
				FacebookIcon.GestureRecognizers.Add(tapFacebook);
			}
			if (MapsIcon.IsVisible)
			{
				tapMaps.Tapped += Maps_Tapped;
				MapsIcon.GestureRecognizers.Add(tapMaps);
			}
			if (TwitterIcon.IsVisible)
			{
				tapTwitter.Tapped += Twitter_Tapped;
				TwitterIcon.GestureRecognizers.Add(tapTwitter);
			}
			if (InstagramIcon.IsVisible)
			{
				tapInsta.Tapped += Insta_Tapped;
				InstagramIcon.GestureRecognizers.Add(tapInsta);
			}
		}

		private void SetupContactLabels(MagazineAdvertiser advertiser)
		{
			telephone.Text = advertiser.Advertiser.Telephone;
			website.Text = advertiser.Advertiser.Website;
			email.Text = advertiser.Advertiser.Email;

			address.Text = !String.IsNullOrEmpty(googleAddress) ? googleAddress + " " + advertiser.Advertiser.Address.Postcode : String.Empty;
		}
	}
}
