using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileDevelopment.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class GridGalleryViewModel : BaseViewModel
    {
        private int _positionPriority;
        private int _dimension;
        
        public ObservableCollection<Gallery> Galleries { get; set; }
        public ICommand AddImageCommand { get; }

        public GridGalleryViewModel()
        {
            Title = "Gallery";
            Galleries = new ObservableCollection<Gallery>();
            AddImageCommand = new Command(OnAddImage);
        }

        private void OnAddImage()
        {
            if (_positionPriority < 8)
            {
                AddImageByPriority();
                return;
            }
            _positionPriority = 0;
            _dimension += 1;
        }

        private async void AddImageByPriority()
        {
            var image = await LoadImageFromGallery();
            if (image == null)
            {
                return;
            }

            var galleryImage = new GalleryImage();
            
            switch (_positionPriority)
            {
                case 0:
                    var gallery = new Gallery
                    {
                        Image00 = galleryImage
                    };
                    gallery.Image00.Image = image;
                    Galleries.Add(gallery);
                    break;
                case 1:
                    Galleries[_dimension].ImageColRowSpan3 = galleryImage;
                    Galleries[_dimension].ImageColRowSpan3.Image = image;
                    break;
                case 2:
                    Galleries[_dimension].Image01 = galleryImage;
                    Galleries[_dimension].Image01.Image = image;
                    break;
                case 3:
                    Galleries[_dimension].Image02 = galleryImage;
                    Galleries[_dimension].Image02.Image = image;
                    break;
                case 4:
                    Galleries[_dimension].Image03 = galleryImage;
                    Galleries[_dimension].Image03.Image = image;
                    break;
                case 5:
                    Galleries[_dimension].Image13 = galleryImage;
                    Galleries[_dimension].Image13.Image = image;
                    break;
                case 6:
                    Galleries[_dimension].Image23 = galleryImage;
                    Galleries[_dimension].Image23.Image = image;
                    break;
                case 7:
                    Galleries[_dimension].Image33 = galleryImage;
                    Galleries[_dimension].Image33.Image = image;
                    break;
            }
            _positionPriority += 1;
        }

        private static async Task<ImageSource> LoadImageFromGallery()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("no upload", "picking a photo is not supported", "ok");
                throw new NotSupportedException();
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            return file == null ? null : ImageSource.FromStream(() => file.GetStream());
        }
    }
}