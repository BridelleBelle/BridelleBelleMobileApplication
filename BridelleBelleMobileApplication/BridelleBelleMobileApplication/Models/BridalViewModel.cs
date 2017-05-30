using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Windows.Input;

using MvvmHelpers;
using BridelleBelleMobileApplication.Gallery;
using BridelleBelleMobileApplication.Types;
using Xamarin.Forms;

namespace BridelleBelleMobileApplication.Models
{
    public class BridalViewModel : BaseViewModel
    {
        private ImageClient Client;
        ICommand _previewImageCommand = null;
        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();
        ImageSource _previewImage = null;

        public BridalViewModel()
        {
            

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

        public ICommand CameraCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCommand(), () => CanExecuteCameraCommand()); }
        }

        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;
        }

        public void ExecuteCommand()
        {
            var bytes = Convert.FromBase64String(ImageClient.GetImages(ImageType.Magazines, "110-13116.jpg"));
            Stream stream = new MemoryStream(bytes);
            var result = new Image();
            result.Source = ImageSource.FromStream(() => { return stream; });
            _images.Add(new GalleryImage { Source = result.Source, OrgImage = bytes });
        }
    }
}
