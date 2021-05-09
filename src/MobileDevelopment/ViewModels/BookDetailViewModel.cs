using System;
using System.Diagnostics;
using MobileDevelopment.Models;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    [QueryProperty(nameof(Isbn13), nameof(Isbn13))]
    public class BookDetailViewModel : BaseViewModel
    {
        private BookDetail _bookDetail;
        private string _isbn13 { get; set; }

        public BookDetail BookDetail
        {
            get => _bookDetail;
            set => SetProperty(ref _bookDetail, value);
        }
        public string Isbn13
        {
            get => _isbn13;
            set
            {
                _isbn13 = value;
                LoadItemId(value);
            }
        }

        private async void LoadItemId(string isbn13)
        {
            try
            {
                var item = await BaseStore.BookStore.GetBookDetailAsync(isbn13);
                BookDetail = item;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}