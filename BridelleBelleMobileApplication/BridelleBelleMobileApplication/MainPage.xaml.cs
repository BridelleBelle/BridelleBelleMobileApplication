using System;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
	    private Magazine mag;
		public MainPage()
		{
			InitializeComponent();
            OnStart();
		}

	    void OnStart()
	    {
            var tapImage = new TapGestureRecognizer();
            tapImage.Tapped += tapImage_Tapped;
            NEMag1.GestureRecognizers.Add(tapImage);
        }

        async void tapImage_Tapped(object sender, EventArgs e)
        {
            // handle the tap - load PDF here. 
            await DisplayAlert("Alert", NEMag1.ToString(), "OK");
        }


	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        mag = await App.Manager.Get();
	    }
    }
}
