using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace BridelleBelleMobileApplication.Droid
{
	[Activity (Label = "BridelleBelleMobileApplication", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			//TabLayoutResource = Resource.Layout.Tabbar;
			//ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Image Slider";

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            ImageAdapter adapter = new ImageAdapter(this);
            viewPager.Adapter = adapter;
            
			//global::Xamarin.Forms.Forms.Init (this, bundle);
			//LoadApplication (new BridelleBelleMobileApplication.App ());
		}
	}
}

