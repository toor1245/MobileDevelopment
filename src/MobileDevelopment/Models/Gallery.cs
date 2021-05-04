using MobileDevelopment.ViewModels;
using Xamarin.Essentials;

namespace MobileDevelopment.Models
{
    public class Gallery : BaseViewModel
    {
        private GalleryImage _image00;
        private GalleryImage _image01;
        private GalleryImage _image02;
        private GalleryImage _image03;
        private GalleryImage _image13;
        private GalleryImage _image23;
        private GalleryImage _image33;
        private GalleryImage _imageColRowSpan3;
        private int _size = (int) DeviceDisplay.MainDisplayInfo.Height >> 4;
        
        public int GridHeight
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public GalleryImage Image00
        {
            get => _image00;
            set => SetProperty(ref _image00, value);
        }

        public GalleryImage Image01
        {
            get => _image01;
            set => SetProperty(ref _image01, value);
        }
        
        public GalleryImage Image02
        {
            get => _image02;
            set => SetProperty(ref _image02, value);
        }
        
        public GalleryImage Image03
        {
            get => _image03;
            set => SetProperty(ref _image03, value);
        }
        
        public GalleryImage Image13
        {
            get => _image13;
            set => SetProperty(ref _image13, value);
        }
        
        public GalleryImage Image23
        {
            get => _image23;
            set => SetProperty(ref _image23, value);
        }
        
        public GalleryImage Image33
        {
            get => _image33;
            set => SetProperty(ref _image33, value);
        }
        
        public GalleryImage ImageColRowSpan3
        {
            get => _imageColRowSpan3;
            set => SetProperty(ref _imageColRowSpan3, value);
        }
    }
}