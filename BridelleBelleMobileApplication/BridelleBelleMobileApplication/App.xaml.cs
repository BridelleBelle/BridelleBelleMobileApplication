using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BridelleBelleMobileApplication.Database;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Views.MasterDetail;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class App : Application
	{
	    public static List<Magazine> AvailableMagazines;
		public static Models.User SignedInUser;

		public App ()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MasterPage(BridelleBelleMobileApplication.Types.TabbedPage.HomePage));
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
