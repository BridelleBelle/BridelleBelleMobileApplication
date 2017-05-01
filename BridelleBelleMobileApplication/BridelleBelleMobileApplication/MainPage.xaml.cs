using System;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            var tapImage = new TapGestureRecognizer();
            tapImage.Tapped += tapImage_Tapped;
            NEMag1.GestureRecognizers.Add(tapImage);
            //setImages();

        }

        async void tapImage_Tapped(object sender, EventArgs e)
        {
            // handle the tap - load PDF here. 
            await DisplayAlert("Alert", NEMag1.ToString(), "OK");
        }

        //void setImages()
        //{
        //	img.Source = "Images\\test_cover.jpg";
        //}
    }
}
