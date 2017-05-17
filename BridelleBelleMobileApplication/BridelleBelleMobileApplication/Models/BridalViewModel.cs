using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Windows.Input;

using MvvmHelpers;
using BridelleBelleMobileApplication.Gallery;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication.Models
{
    public class BridalViewModel : BaseViewModel
    {
        ICommand _previewImageCommand = null;
        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();
        ImageSource _previewImage = null;

        public BridalViewModel()
        {
            _images.Add(new GalleryImage { Source = ConvertImage("110-13116.jpg") });
        }

        public ObservableCollection<GalleryImage> Images
        {
            get { return _images; }
        }

        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        public ICommand PreviewImageCommand
        {
            get
            {
                return _previewImageCommand ?? new Command<Guid>((img) => {

                    var image = _images.Single(x => x.ImageId == img).OrgImage;

                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));

                });
            }
        }

        public async Task<Image> ConvertImage(string fileName)
        {
            var bytes = Convert.FromBase64String(await App.Manager.GetCovers(fileName));
            Stream stream = new MemoryStream(bytes);
            var result = new Image();
            result.Source = ImageSource.FromStream(() => { return stream; });
            return result;
        }
    }
}
