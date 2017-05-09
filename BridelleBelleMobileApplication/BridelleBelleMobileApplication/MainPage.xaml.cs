using System;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
// private Magazine mag;
		private IEnumerable<Magazine> Magazines;
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
	            Magazines = App.Manager.GetLatest();
	            await GetCovers();
	        }
	        catch (Exception e)
	        {
	            System.Diagnostics.Debug.WriteLine(e.Message);
	        }
	    }

	    public async Task GetCovers()
	    {
		    var streams = new List<Image>
		    {
			    NEMag1,
				NEMag2,
				YMag1,
				YMag2
		    };
		    for(var i = 0;i< 4; i++)
		    {
				var bytes = Convert.FromBase64String(await App.Manager.GetCovers(Magazines.ToList()[i].CoverImageFileName));
			    Stream stream = new MemoryStream(bytes);
			    streams[i].Source = ImageSource.FromStream(() => { return stream; });
			}
        }
    }
}
