using Xamarin.Forms;

namespace MobileDevelopment.Models
{
    public class GalleryImage : BaseModel
    {
        private ImageSource _image;
        private Color _color = Color.White;
        
        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public Color Color
        {
            get
            {
                if (Image is null)
                {
                    return _color;
                }
                _color = Color.Gray;
                return _color;
            }
            set => SetProperty(ref _color, value);
        }

        public GalleryImage()
        {
            Color = Color.Gray;
        }
    }
}