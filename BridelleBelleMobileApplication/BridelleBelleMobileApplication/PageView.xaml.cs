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

        // Pinch (needed for the OnPinchUpdate)
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

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

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            //Here is the On Pinch Update that is used in the PageView.xaml
            if (e.Status == GestureStatus.Started)
            {
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;
                
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;
                
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);
                
                Content.TranslationX = Clamp(targetX, -Content.Width * (currentScale - 1), 0);
                Content.TranslationY = Clamp(targetY, -Content.Height * (currentScale - 1), 0);
                
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

        static double Clamp(double self, double min, double max) // Needed, wouldn't be an extension to double.
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }
}