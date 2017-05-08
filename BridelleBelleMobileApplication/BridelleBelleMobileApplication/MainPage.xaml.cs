using System;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
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
        }

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();
	        try
	        {
	            mag = await App.Manager.Get();
	            await GetCovers();
	        }
	        catch (Exception e)
	        {
	            System.Diagnostics.Debug.WriteLine(e.Message);
	        }
	    }

	    public async Task GetCovers()
	    {
	        var bytes = Convert.FromBase64String(await App.Manager.GetCovers(mag.CoverImageFileName));
	        Stream stream = new MemoryStream(bytes);
	        NEMag2.Source = ImageSource.FromStream(() => { return stream; });
        }
    }
}
