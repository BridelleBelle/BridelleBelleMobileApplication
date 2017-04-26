using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public class WebViewPage : ContentPage
	{
		public WebViewPage ()
		{
            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout {
                Children = {
                    new CustomWebView
                    {
                        Uri = "Bride&Groom-WeddingPlanner.pdf",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
				}
			};
		}
	}
}
