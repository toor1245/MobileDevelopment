using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MobileDevelopment.Extensions;
using MobileDevelopment.Helpers;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;
using MobileDevelopment.Repositories;
using Xamarin.Forms;

namespace MobileDevelopment.Services
{
    public class BookStore : IBookStore
    {
        #region .fields
        
        private static BookValuation _bookValuation;
        private static HttpClient _httpClient;

        #endregion
        
        #region .ctors
        
        public BookStore()
        {
            _bookValuation = new BookValuation();
            _httpClient = new HttpClient();
        }
        
        #endregion
        
        #region .api
        
        public async Task<bool> AddItemAsync(Book item)
        {
            if (item is null)
            {
                return await Task.FromResult(false);
            }
            _bookValuation.Books.Add(item);
            var book = UnitOfWork.Books.GetItem(x => x.Isbn13 == item.Isbn13);
            if (book is null)
            {
                UnitOfWork.Books.SaveItem(item);
            }
            return await Task.FromResult(true);
        }

        public Task<bool> UpdateItemAsync(Book item)
        {
            for (var i = 0; i < _bookValuation.Books.Count; i++)
            {
                if (_bookValuation.Books[i].Isbn13 != item.Isbn13)
                {
                    continue;
                }
                _bookValuation.Books[i] = item;
                break;
            }
            return Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _bookValuation.Books.FirstOrDefault(arg => arg.Isbn13 == id);
            _bookValuation.Books.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public Task<Book> GetItemAsync(string id)
        {
            var book = _bookValuation.Books.FirstOrDefault(x => x.BookId == id);
            return Task.FromResult(book);
        }
        
        public async Task<BookDetail> GetBookDetailAsync(string isbn13)
        {
            var response = await _httpClient.GetAsync(string.Concat(Constants.URL_BOOK_DETAIL_INFO, isbn13));
            if (!response.IsSuccessStatusCode)
            {
                var book = UnitOfWork.BookDetails.GetItem(x => x.Isbn13 == isbn13);
                if (book is not null)
                {
                    return book;
                }
                await Shell.Current.DisplayAlert("Not found", $"Book not found by isbn13: {isbn13}", "OK");
                return null;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            var bookDetail = JsonSerializer.Deserialize<BookDetail>(content);
            UnitOfWork.BookDetails.SaveItem(bookDetail);
            return bookDetail;
        }

        public async Task<BookValuation> GetBooksAsync(string title)
        {
            var composer = new HttpBookRequestComposer(title);
            if (!composer.IsCorrectRequest)
            {
                await HttpRequestComposer.DisplayAlertAsync();
                return null;
            }
            try
            {
                var response = await _httpClient.GetAsync(composer.UrlRequest);
                if (!response.IsSuccessStatusCode)
                {
                    return HandleNotSuccessfulResponse();
                }
                var content = await response.Content.ReadAsStringAsync();
                _bookValuation = JsonSerializer.Deserialize<BookValuation>(content);
                UnitOfWork.Books.SaveItems(_bookValuation?.Books);
                return _bookValuation;
            }
            catch (Exception e)
            {
                return HandleNotSuccessfulResponse();
            }
        }

        private static BookValuation HandleNotSuccessfulResponse()
        {
            var collection = UnitOfWork.Books.GetItems();
            if (collection.Count != 0)
            {
                _bookValuation.Books = collection.ToList();
                return _bookValuation;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                Shell.Current.DisplayAlert(
                    "Incorrect search",
                    "Not found",
                    "OK");
            });
            return null;
        }
        #endregion
    }
}