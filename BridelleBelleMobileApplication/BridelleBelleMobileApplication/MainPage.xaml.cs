using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            //Label header = new Label
            //{
            //    Text = "WebView",
            //    FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
            //    HorizontalOptions = LayoutOptions.Center
            //};

            //WebView webView = new WebView
            //{
            //    Source = new UrlWebViewSource
            //    {
            //        Url = "http://xamarin.com/",
            //    },
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            //this.Content = new StackLayout
            //{
            //    Children =
            //    {
            //        header,
            //        webView
            //    }
            //};

            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Children =
                {
                    new CustomWebView
                    {
                        Uri = "BookPreview2-Ch18-Rel0417.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }
            };

        }
	}
}
