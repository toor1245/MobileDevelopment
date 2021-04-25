using System;
using System.Collections.Generic;
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
        private static BookValuation _temporaryCache;
        private static bool _isCached;
        
        public BookStore() => _temporaryCache = new BookValuation();

        public async Task<bool> AddItemAsync(Book item)
        {
            if (!_isCached)
            {
                var items = await GetItemsAsync();
                items.Books.Add(item);
            }
            _temporaryCache.Books.Add(item);
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
            var oldItem = items.Books.FirstOrDefault(arg => arg.Isbn13 == id);
            items.Books.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Book> GetItemAsync(string id)
        {
            var items = await GetItemsAsync();
            return await Task.FromResult(items.Books.FirstOrDefault(s => s.Id == id));
        }

        public async Task<BookValuation> GetItemsAsync()
        {
            if (!_isCached)
            {
                return await DeserializeBookValuation();
            }
            return PrefillBooks(_ => true);
        }

        public async Task<BookValuation> GetItemsAsync(Func<Book, bool> predicate)
        {
            if (!_isCached)
            {
                return await DeserializeBookValuation();
            }

            return PrefillBooks(predicate);
        }

        public async Task<BookDetail> GetBookDetail(string isbn13)
        {
            var assembly = typeof(Book).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(string.Concat(Constants.RESOURCES, isbn13, ".json"));
            return await JsonSerializer.DeserializeAsync<BookDetail>(stream ?? throw new InvalidOperationException());
        }

        private async Task<BookValuation> DeserializeBookValuation()
        {
            var assembly = typeof(Book).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(Constants.BOOK_LIST_STORAGE);
            
            _isCached = true;
            var result = await JsonSerializer.DeserializeAsync<BookValuation>(stream ?? throw new InvalidOperationException());
            
            _temporaryCache = result;
            return result;
        }

        private static BookValuation PrefillBooks(Func<Book, bool> predicate)
        {
            var books = new BookValuation { Books = new List<Book>() };
            foreach (var book in _temporaryCache.Books.Where(predicate))
            {
                books.Books.Add(book);
            }
            return books;
        }
    }
}