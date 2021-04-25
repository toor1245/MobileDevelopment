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

        private async void LoadItemId(string itemId)
        {
            try
            {
                var item = await BaseStoreViewModel.BookStore.GetBookDetail(itemId);
                BookDetail = item;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}