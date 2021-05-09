using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IBookStore : IDataStore<Book>
    {
        List<Book> LoadBooks();
        Task<BookValuation> GetBooksAsync(string title);
        Task<BookDetail> GetBookDetailAsync(string isbn13);
    }
}