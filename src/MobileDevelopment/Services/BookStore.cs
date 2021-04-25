using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using MobileDevelopment.Extensions;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;

namespace MobileDevelopment.Services
{
    public class BookStore : IBookStore
    {
        private BookValuation _temporaryCache;
        private static bool _isCached;

        public BookStore()
        {
            _temporaryCache = new BookValuation();
        }

        public async Task<bool> AddItemAsync(Book item)
        {
            var items = await GetItemsAsync();
            items.Books.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Book item)
        {
            var items = await GetItemsAsync();
            var oldItem = items.Books.FirstOrDefault(arg => arg.Id == item.Id);
            items.Books.Remove(oldItem);
            items.Books.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var items = await GetItemsAsync();
            var oldItem = items.Books.FirstOrDefault(arg => arg.Id == id);
            items.Books.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Book> GetItemAsync(string id)
        {
            var items = await GetItemsAsync();
            return await Task.FromResult(items.Books.FirstOrDefault(s => s.Id == id));
        }

        public async Task<BookValuation> GetItemsAsync(bool forceRefresh = false)
        {
            if (_isCached)
            {
                return _temporaryCache;
            }

            var assembly = typeof(Book).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(Constants.BOOK_LIST_STORAGE);
            
            _isCached = true;
            var result = await JsonSerializer.DeserializeAsync<BookValuation>(stream ?? throw new InvalidOperationException());
            
            _temporaryCache = result;
            return result;
        }
    }
}