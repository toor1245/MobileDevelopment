using System;
using MobileDevelopment.Models;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class NewBookViewModel : BaseViewModel
    {
        private string _title;
        private string _subtitle;
        private string _price;
        public Command AddItemCommand { get; }

        public NewBookViewModel()
        {
            AddItemCommand = new Command(OnAddBook);
        }

        public string Name
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public string SubTitle
        {
            get => _subtitle;
            set => SetProperty(ref _subtitle, value);
        }

        public string Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private async void OnAddBook()
        {
            var book = new Book
            {
                Id = Guid.NewGuid().ToString(),
                Title = Name,
                SubTitle = SubTitle,
                Price = string.Concat("$", Price)
            };
            await BaseStoreViewModel.BookStore.AddItemAsync(book);
            await Shell.Current.GoToAsync("..");
        }
    }
}