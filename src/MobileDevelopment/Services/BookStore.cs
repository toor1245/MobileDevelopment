using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MobileDevelopment.Extensions;
using MobileDevelopment.Helpers;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;
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
            if (item == null)
            {
                return await Task.FromResult(false);
            }
            _bookValuation.Books.Add(item);
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
            var book = _bookValuation.Books.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(book);
        }
        
        public async Task<BookDetail> GetBookDetailAsync(string isbn13)
        {
            var response = await _httpClient.GetAsync(string.Concat(Constants.URL_BOOK_DETAIL_INFO, isbn13));
            var content = await response.Content.ReadAsStringAsync();
            return content == null ? null : JsonSerializer.Deserialize<BookDetail>(content);
        }

        public List<Book> LoadBooks()
        {
            return _bookValuation.Books;
        }

        public async Task<BookValuation> GetBooksAsync(string title)
        {
            var composer = new HttpBookRequestComposer(title);
            if (!composer.IsCorrectRequest)
            {
                await Shell.Current.DisplayAlert(
                    "Incorrect search", 
                    "restriction characters: ';', '/', '?', ':', '@', '&', '=', '$'", 
                    "OK");
                return null;
            }
            var response = await _httpClient.GetAsync(composer.UrlRequest);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            _bookValuation = JsonSerializer.Deserialize<BookValuation>(content);
            return _bookValuation;
        }
        
        #endregion
        
    }
}