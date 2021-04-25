using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileDevelopment.Models;
using MobileDevelopment.Views;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        private Book _selectedItem;

        public ObservableCollection<Book> Books { get; }
        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<Book> BookTapped { get; }

        public BooksViewModel()
        {
            Title = "Books";
            Books = new ObservableCollection<Book>();
            LoadBooksCommand = new Command(async () => await ExecuteLoadItemsCommand());
            BookTapped = new Command<Book>(OnItemSelected);
            AddBookCommand = new Command(OnAddItem);
        }
        
        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Books.Clear();
                var items = await BaseStoreViewModel.BookStore.GetItemsAsync(true);
                foreach (var item in items.Books)
                {
                    Books.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Book SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewBookPage));
        }

        private async void OnItemSelected(Book item)
        {
            if (item is null)
            {
                return;
            }

            // This will push the BookDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.BookId)}={item.Id}");
        }
    }
}