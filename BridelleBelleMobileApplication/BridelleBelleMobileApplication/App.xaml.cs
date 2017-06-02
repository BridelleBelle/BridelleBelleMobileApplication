using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BridelleBelleMobileApplication.Database;
using BridelleBelleMobileApplication.Models;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication
{
	public partial class App : Application
	{
	    public static List<Magazine> Magazines;
	    public static MagazineManager Manager;
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
			Manager = new MagazineManager();
			this.Master = new SignInPage();
			this.Detail = new NavigationPage(new MainPage());
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
