using System;
using System.Threading.Tasks;
using MobileDevelopment.EFContext;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;

namespace MobileDevelopment.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IRepository<Book> _bookRepository;
        private IRepository<Gallery> _galleryRepository;
        private bool _disposed;
        
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        
        public IRepository<Book> Books => _bookRepository ??= new Repository<Book>(_context);
        public IRepository<Gallery> Galleries => _galleryRepository ??= new Repository<Gallery>(_context);
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) 
                return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}