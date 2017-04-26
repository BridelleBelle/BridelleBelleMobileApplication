namespace BridelleBelleMobileApplication
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			var tapImage = new TapGestureRecognizer();
			tapImage.Tapped += tapImage_Tapped;
			img.GestureRecognizers.Add(tapImage);
			setImages();
			
		}

		async void tapImage_Tapped(object sender, EventArgs e)
		{
			DisplayPDF("");
		}

		void setImages()
		{
			img.Source = "Images\\test_cover.jpg";
		}

		public DisplayPDF(string pdfFile)
		{
			Padding = new Thickness(0, 20, 0, 0);
			Content = new StackLayout
			{
				Children = {
					new CustomWebView {
						Uri = "BookPreview2-Ch18-Rel0417.pdf",
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand
					}
				}
			};
		}
	}
}
