using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Extensions;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Helpers;

namespace BridelleBelleMobileApplication.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MagazinePreview : PopupPage
	{
		private Magazine Magazine;
		public MagazinePreview (Magazine mag)
		{
			InitializeComponent ();
			if(mag != null)
			{
				this.Magazine = mag;
				MainCarouselView.ItemsSource = Load();
				magazineName.Text = mag.Name;
				issue.Text = "Issue: " + mag.Issue;
			}
		}

		private List<string> Load()
		{
			var magManager = new MagazineManager();
			var images = new List<string>();
			this.Magazine.MagazineContent = magManager.GetMagazinePages(this.Magazine.MagazineContent);

			for(var i = 0; i < 20; i++)
			{
				images.Add(this.Magazine.MagazineContent.ToList()[i].Uri);
			}

			return images;
		}
	}
}
