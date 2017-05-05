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
		    {
		        await DisplayAlert("Alert", mag.Id, "OK");
		    }
		    else
		    {
		        await DisplayAlert("Alert", "Mag is null", "OK");
		    }
        }


	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        //await App.Man.docDb.CreateDatabase();
	        mag = await App.Manager.Get();
	    }
    }
}
