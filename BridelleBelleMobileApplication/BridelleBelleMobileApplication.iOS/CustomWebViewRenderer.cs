using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Foundation;
using UIKit;
using BridelleBelleMobileApplication;
using BridelleBelleMobileApplication.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace BridelleBelleMobileApplication.iOS
{
	public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				SetNativeControl(new UIWebView());
			}
			if (e.OldElement != null)
			{
				// Cleanup
			}
			if (e.NewElement != null)
			{
				var customWebView = Element as CustomWebView;
				string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
				Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
				Control.ScalesPageToFit = true;
			}
		}
	}
}