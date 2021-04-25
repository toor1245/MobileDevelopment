using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileDevelopment.Models;
using MobileDevelopment.Views;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class BooksViewModel : BaseViewModel
    {
        private Book _selectedItem;
        private string _searchText;

        public ObservableCollection<Book> Books { get; }
        public Command LoadBooksCommand { get; }
        public Command AddBookCommand { get; }
        public Command<Book> DeleteBookCommand { get; }
        public ICommand SearchCommand { get; }
        public Command<Book> BookTapped { get; }

        public BooksViewModel()
        {
            Title = "Books";
            Books = new ObservableCollection<Book>();
            LoadBooksCommand = new Command(async () => await ExecuteLoadItemsCommand());
            BookTapped = new Command<Book>(OnItemSelected);
            AddBookCommand = new Command(OnAddItem);
            SearchCommand = new Command<string>(OnSearchCommand);
            DeleteBookCommand = new Command<Book>(OnDeleteCommand);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                OnSearchCommand(_searchText);
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Books.Clear();
                var items = await BaseStoreViewModel.BookStore.GetItemsAsync();
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

        #region .ui props

        public Book SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        
        #endregion

        #region .actions
        
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
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
            await Shell.Current.GoToAsync($"{nameof(BookDetailPage)}?{nameof(BookDetailViewModel.Isbn13)}={item.Isbn13}");
        }

        public void OnDeleteCommand(Book obj)
        {
            Books.Remove(obj);
        }

        public void OnSearchCommand(string query)
        {
            Books.Clear();
            var books = BaseStoreViewModel.BookStore.GetItemsAsync(x => x.Title.StartsWith(query)).Result;
            foreach (var book in books.Books)
            {
                Books.Add(book);
            }
        }

        #endregion
    }
}