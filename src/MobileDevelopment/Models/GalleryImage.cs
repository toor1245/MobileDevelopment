using Xamarin.Forms;

namespace MobileDevelopment.Models
{
    public class GalleryImage : BaseModel
    {
        private ImageSource _image;
        private Color _color;
        private bool _isRunning;
        
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public bool IsRunning
        {
            get
            {
                if (_image is null)
                {
                    _isRunning = true;
                    return _isRunning;
                }

                _isRunning = false;
                return _isRunning;
            }
            set => SetProperty(ref _isRunning, value);
        }

        public GalleryImage()
        {
            Color = Color.Gray;
            IsRunning = false;
        }
    }
}