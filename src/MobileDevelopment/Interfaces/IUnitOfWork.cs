using System;
using System.Threading.Tasks;
using MobileDevelopment.Models;

namespace MobileDevelopment.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Gallery> Galleries { get; }
        Task SaveChangesAsync();
        void SaveChanges();
    }
}