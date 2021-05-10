using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MobileDevelopment.Interfaces;
using MobileDevelopment.Models;
using SQLite;

namespace MobileDevelopment.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : new()
    {
        private readonly SQLiteConnection _database;

        public Repository(string connection)
        {
            _database = new SQLiteConnection(connection);
            _database.CreateTable<Book>();
            _database.CreateTable<BookDetail>();
            _database.CreateTable<Hit>();
        }

        public void CreateTable()
        {
            _database.CreateTable<TEntity>();
        }
        
        public List<TEntity> GetItems()
        {
            return _database.Table<TEntity>().ToList();
        }

        public TEntity GetItem(int id)
        {
            return _database.Get<TEntity>(id);
        }

        public TEntity GetItem(Expression<Func<TEntity, bool>> predicate)
        {
            return _database.Get(predicate);
        }

        public int DeleteItem(TEntity item)
        {
            return _database.Delete(item);
        }

        public int SaveItem(TEntity item)
        {
            return _database.Insert(item);
        }

        public void SaveItems(List<TEntity> entities)
        {
            _database.InsertAll(entities);
        }
    }
}