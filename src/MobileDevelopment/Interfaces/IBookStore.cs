using System;
using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IBookStore : IDataStore<Book>
    {
        Task<BookValuation> GetItemsAsync();
        Task<BookValuation> GetItemsAsync(Func<Book, bool> predicate);
        Task<BookDetail> GetBookDetail(string isbn13);
    }
}