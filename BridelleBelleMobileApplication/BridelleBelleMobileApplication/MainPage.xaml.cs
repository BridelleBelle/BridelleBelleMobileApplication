using System;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Children =
                {
                    new CustomWebView
                    {
                        Uri = "Bride_GroomWeddingPlanner.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };
			var tapImage = new TapGestureRecognizer();
			tapImage.Tapped += tapImage_Tapped;
			img.GestureRecognizers.Add(tapImage);
			setImages();
			
		}

		async void tapImage_Tapped(object sender, EventArgs e)
		{
			// handle the tap - load PDF here. 
			 await DisplayAlert("Alert", img.ToString(), "OK");
		}

		void setImages()
		{
			img.Source = "Images\\test_cover.jpg";
        }
	}
}
