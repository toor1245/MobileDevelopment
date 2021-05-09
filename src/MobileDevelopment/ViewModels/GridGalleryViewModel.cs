using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileDevelopment.Extensions;
using MobileDevelopment.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class GridGalleryViewModel : BaseViewModel
    {
        #region .fields
        
        private int _positionPriority;
        private int _dimension;

        #endregion

        #region .props
        
        public ObservableCollection<Gallery> Galleries { get; set; }
        public ICommand AddImageCommand { get; }
        public ICommand LoadImagesCommand { get; }
        
        #endregion

        private void InitializeCache()
        {
            Galleries = new ObservableCollection<Gallery>();
            for (var i = 0; i < Constants.COUNT >> 3; i++)
            {
                Galleries.Add(new Gallery());
                for (var j = 0; j < Constants.MAX_IMAGES_IN_GRID; j++)
                {
                    Galleries[i].Image00 = new GalleryImage();
                    Galleries[i].Image01 = new GalleryImage();
                    Galleries[i].Image02 = new GalleryImage();
                    Galleries[i].Image03 = new GalleryImage();
                    Galleries[i].Image13 = new GalleryImage();
                    Galleries[i].Image23 = new GalleryImage();
                    Galleries[i].Image33 = new GalleryImage();
                    Galleries[i].ImageColRowSpan3 = new GalleryImage();
                }
            }
        }

        #region .ctor

        public GridGalleryViewModel()
        {
            Title = "Gallery";
            AddImageCommand = new Command(OnAddImage);
            LoadImagesCommand = new Command(async () => await OnLoadImagesAsync());
            IsBusy = true;
            InitializeCache();
        }
        
        #endregion

        #region .actions
        
        private async Task OnLoadImagesAsync()
        {
            IsBusy = true;
            _dimension = 0;
            _positionPriority = 0;
            try
            {
                var response = await BaseStore.GalleryStore.GetImagesAsync();
                foreach (var t in response.Hits)
                {
                    ImageSource imageSource = new UriImageSource
                    {
                        Uri = new Uri(t.PreviewUrl)
                    };
                    RefreshImages(new GalleryImage(), imageSource);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void OnAddImage()
        {
            if (_positionPriority < 8)
            {
                AddImageByPriorityAsync();
                return;
            }
            _positionPriority = 0;
            _dimension += 1;
        }
        
        #endregion

        #region .methods

        private void RefreshImages(GalleryImage galleryImage, ImageSource imageSource)
        {
            if (_positionPriority < 8)
            {
                AddImageByPrioritySoftwareFallback(galleryImage, imageSource);
                return;
            }
            _positionPriority = 0;
            _dimension += 1;
        }
        
        private async void AddImageByPriorityAsync()
        {
            var image = await LoadImageFromGalleryAsync();
            if (image == null)
            {
                return;
            }
            var galleryImage = new GalleryImage();
            AddImageByPrioritySoftwareFallback(galleryImage, image);
        }
        
        private void AddImageByPrioritySoftwareFallback(GalleryImage galleryImage, ImageSource image)
        {
            switch (_positionPriority)
            {
                case 0:
                    if (Galleries.Count < 4)
                    {
                        Galleries[_dimension].Image00 = galleryImage;
                        Galleries[_dimension].Image00.Image = image;
                        Galleries[_dimension].Image00.IsRunning = false;
                        break;
                    }
                    var gallery = new Gallery
                    {
                        Image00 = galleryImage
                    };
                    gallery.Image00.Image = image;
                    Galleries[_dimension].Image00.IsRunning = false;
                    Galleries.Add(gallery);
                    break;
                case 1:
                    Galleries[_dimension].ImageColRowSpan3 = galleryImage;
                    Galleries[_dimension].ImageColRowSpan3.Image = image;
                    Galleries[_dimension].ImageColRowSpan3.IsRunning = false;
                    break;
                case 2:
                    Galleries[_dimension].Image01 = galleryImage;
                    Galleries[_dimension].Image01.Image = image;
                    Galleries[_dimension].Image01.IsRunning = false;
                    break;
                case 3:
                    Galleries[_dimension].Image02 = galleryImage;
                    Galleries[_dimension].Image02.Image = image;
                    Galleries[_dimension].Image02.IsRunning = false;
                    break;
                case 4:
                    Galleries[_dimension].Image03 = galleryImage;
                    Galleries[_dimension].Image03.Image = image;
                    Galleries[_dimension].Image03.IsRunning = false;
                    break;
                case 5:
                    Galleries[_dimension].Image13 = galleryImage;
                    Galleries[_dimension].Image13.Image = image;
                    Galleries[_dimension].Image13.IsRunning = false;
                    break;
                case 6:
                    Galleries[_dimension].Image23 = galleryImage;
                    Galleries[_dimension].Image23.Image = image;
                    Galleries[_dimension].Image23.IsRunning = false;
                    break;
                case 7:
                    Galleries[_dimension].Image33 = galleryImage;
                    Galleries[_dimension].Image33.Image = image;
                    Galleries[_dimension].Image33.IsRunning = false;
                    break;
            }
            _positionPriority += 1;
        }

        private static async Task<ImageSource> LoadImageFromGalleryAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("no upload", "picking a photo is not supported", "ok");
                return null;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            return file == null ? null : ImageSource.FromStream(() => file.GetStream());
        }
        
        #endregion
    }
}