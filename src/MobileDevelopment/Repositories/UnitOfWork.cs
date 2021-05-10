using System;
using System.IO;
using MobileDevelopment.Models;

namespace MobileDevelopment.Repositories
{
    public static class UnitOfWork
    {
        private const string DATABASE_NAME = "mobile.db3";
        private static readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
        
        private static Repository<Book> _bookRepository;
        public static Repository<Book> Books => _bookRepository ??= new Repository<Book>(path);
        
        private static Repository<Hit> _hitRepository;
        public static Repository<Hit> Hits => _hitRepository ??= new Repository<Hit>(path);
        
        private static Repository<BookDetail> _bookDetailRepository;
        public static Repository<BookDetail> BookDetails => _bookDetailRepository ??= new Repository<BookDetail>(path);
    }
}