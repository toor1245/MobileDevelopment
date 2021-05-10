using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MobileDevelopment.Interfaces;
using SQLite;

namespace MobileDevelopment.Repositories
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> 
        where TEntity : new()
    {
        private readonly SQLiteAsyncConnection _database;

        public RepositoryAsync(string connection)
        {
            _database = new SQLiteAsyncConnection(connection);
        }

        public async Task CreateTable()
        {
            await _database.CreateTableAsync<TEntity>();
        }

        public async Task<List<TEntity>> GetItemsAsync()
        {
            return await _database.Table<TEntity>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> GetItemAsync(int id)
        {
            return await _database.GetAsync<TEntity>(id).ConfigureAwait(false);
        }

        public async Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _database.GetAsync(predicate).ConfigureAwait(false);
        }

        public async Task<int> DeleteItemAsync(TEntity item)
        {
            return await _database.DeleteAsync(item).ConfigureAwait(false);
        }

        public async Task<int> SaveItemAsync(TEntity item)
        {
            return await _database.InsertAsync(item).ConfigureAwait(false);
        }

        public async Task SaveItemsAsync(List<TEntity> entities)
        {
            await _database.InsertAllAsync(entities).ConfigureAwait(false);
        }
    }
}