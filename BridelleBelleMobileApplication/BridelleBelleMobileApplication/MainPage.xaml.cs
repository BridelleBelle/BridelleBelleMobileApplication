using System;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
	    private Magazine mag;
	    private string x = System.String.Empty;
        public MainPage()
		{
			InitializeComponent();
            OnStart();
		}

	    void OnStart()
	    {
	        var tapImage = new TapGestureRecognizer();
	        tapImage.Tapped += tapImage_Tapped;
	        img.GestureRecognizers.Add(tapImage);
        }

		async void tapImage_Tapped(object sender, EventArgs e)
		{
			// handle the tap - load PDF here. 
		    if (x != null && x != "")
		    {
		        await DisplayAlert("Alert", x, "OK");
		    }
		    else
		    {
		        await DisplayAlert("Alert", "Mag is null", "OK");
		    }
		}

		void setImages()
		{
			img.Source = "Images\\test_cover.jpg";
		}


	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        //await App.Man.docDb.CreateDatabase();
	        mag = await App.Manager.Get();
	        await App.blob.Initialize();
	        await App.blob.GetCoverImages();
	    }
    }
}
