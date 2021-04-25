using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    [QueryProperty(nameof(BookId), nameof(BookId))]
    public class BookDetailViewModel : BaseViewModel
    {
        private string _bookId;
        private string _title;
        private string _subtitle;
        private string _isbn13;
        private string _price;
        
        public string BookId
        {
            get => _bookId;
            set
            {
                _bookId = value;
                LoadItemId(value);
            }
        }
        
        public string Id { get; set; }

        public new string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public string SubTitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        public string Isbn13
        {
            get => _isbn13;
            set => SetProperty(ref _isbn13, value);
        }

        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private async void LoadItemId(string itemId)
        {
            try
            {
                var item = await BaseStoreViewModel.BookStore.GetItemAsync(itemId);
                Id = item.Id;
                Title = item.Title;
                SubTitle = item.SubTitle;
                Isbn13 = item.Isbn13;
                Price = item.Price;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}