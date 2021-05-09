using System.Collections.ObjectModel;
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
        public Command AddBookCommand { get; }
        public Command<Book> DeleteBookCommand { get; }
        
        public ICommand SearchCommand { get; }
        public Command<Book> BookTapped { get; }

        public BooksViewModel()
        {
            Title = "Books";
            Books = new ObservableCollection<Book>();
            BookTapped = new Command<Book>(OnItemSelected);
            AddBookCommand = new Command(OnAddItem);
            SearchCommand = new Command<string>(OnSearchCommand);
            DeleteBookCommand = new Command<Book>(OnDeleteCommand);
        }

        #region .ui props

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                OnSearchCommand(_searchText);
            }
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

        private void OnDeleteCommand(Book obj)
        {
            Books.Remove(obj);
        }

        private async void OnSearchCommand(string query)
        {
            Books.Clear();
            if (query.Length < 3)
            {
                return;
            }

            var books = await BaseStore.BookStore.GetBooksAsync(query);
            if (books is null)
            {
                return;
            }
            foreach (var book in books.Books)
            {
                Books.Add(book);
            }
        }

        #endregion
    }
}