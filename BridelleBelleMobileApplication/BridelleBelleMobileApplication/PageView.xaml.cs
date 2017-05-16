using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;
using BridelleBelleMobileApplication.Types;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridelleBelleMobileApplication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageView : ContentPage
	{
		private int cap = 20;
		private int currLoad = 0;
		private Magazine Magazine;
		private List<Image> Images;

		public PageView (Magazine mag)
		{
			InitializeComponent ();
			this.Magazine = mag;
			MainCarouselView.ItemsSource = Load();
		}

		private void MainCarouselView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			MainLabel.Text = e.SelectedItem as string;
		}

		private List<string> Load()
		{
			var magManager = new MagazineManager();
			var images = new List<string>();
			this.Magazine.MagazineContent =  magManager.GetMagazinePages(this.Magazine.MagazineContent);

			foreach (var uri in this.Magazine.MagazineContent)
			{
				images.Add(uri.Uri);
			}

			return images;
		}
	}
}