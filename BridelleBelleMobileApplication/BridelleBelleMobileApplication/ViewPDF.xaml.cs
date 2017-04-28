using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPDF : ContentPage
	{
		public ViewPDF ()
		{
			InitializeComponent ();

            Button button = new Button
            {
                Text = "Click for Advertiser Information!",
                Style = (Style)Application.Current.Resources["buttonStyle"]
            };
            button.Clicked += Handle_Clicked;

            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            Content = new StackLayout
            {
                Children =
                {
                    new CustomWebView
                    {
                        Uri = "Bride_GroomWeddingPlanner.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    button
                }
            };
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Advertisers());
        }
	}
}
