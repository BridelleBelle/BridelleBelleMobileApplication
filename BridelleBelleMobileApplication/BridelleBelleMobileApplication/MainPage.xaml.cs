using System;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            InitializeComponent();

            // MainPage had navigation bar with no Nav just white bar (even though it's meant to be false by default)
            NavigationPage.SetHasNavigationBar(this, false); 

            var tapImage = new TapGestureRecognizer();
			tapImage.Tapped += tapImage_Tapped;
			img.GestureRecognizers.Add(tapImage);
			//setImages();
		}

		void tapImage_Tapped(object sender, EventArgs e)
		{
            // handle the tap - load PDF here. 
            var pdf = new ViewPDF();

            //NavigationPage.SetBackButtonTitle(pdf, "PDF Viewer");
            NavigationPage.SetHasNavigationBar(pdf, false);

            Navigation.PushAsync(pdf);
        }

		void setImages()
		{
			img.Source = "Images\\test_cover.jpg";
        }
    }
}
