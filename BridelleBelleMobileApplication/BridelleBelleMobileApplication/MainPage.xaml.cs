using System;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		Image magazine = new Image();

		public MainPage()
		{
			InitializeComponent();
			var tapImage = new TapGestureRecognizer();
			tapImage.Tapped += tapImage_Tapped;
			img.GestureRecognizers.Add(tapImage);
			
		}

		void tapImage_Tapped(object sender, EventArgs e)
		{
			// handle the tap  
			DisplayAlert("Alert", img.ToString(), "OK");
		}
	}
}
