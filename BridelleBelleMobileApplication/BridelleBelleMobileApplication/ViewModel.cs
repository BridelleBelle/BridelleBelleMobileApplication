using BridelleBelleMobileApplication.Models;
using System;
using PSC.Xamarin.MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
namespace BridelleBelleMobileApplication
{
	public class ViewModel : BaseViewModel
	{
		ObservableCollection<MagazineContent>_images = new ObservableCollection<MagazineContent>();
		private ImageSource _previewImage = null;

		public ViewModel() { }

		public ObservableCollection<MagazineContent> Images
		{
			get { return _images; }
		}

		public ImageSource PreviewImage
		{
			get { return _previewImage; }
			set { SetProperty(ref _previewImage, value); }
		}
	}
}
