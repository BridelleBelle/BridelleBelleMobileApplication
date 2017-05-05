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

			MainPage = new BridelleBelleMobileApplication.MainPage();
            Manager = new MagazineManager();

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
