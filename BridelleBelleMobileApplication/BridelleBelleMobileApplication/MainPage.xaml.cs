using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		Image magazine = new Image();
		
		public MainPage()
		{
			magazine.Source = ImageSource.FromFile("Images\\test_cover.jpg");
			this.LoadFromXaml(typeof(MainPage));
			var tapGesture = new TapGestureRecognizer();
			tapGesture.SetBinding(TapGestureRecognizer.CommandProperty,"Tap Command");
		}
	}
}
