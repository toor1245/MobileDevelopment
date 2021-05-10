using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MobileDevelopment.Interfaces
{
    public interface IRepositoryAsync<TEntity> 
        where TEntity : new()
    {
        Task CreateTable();
        Task<List<TEntity>> GetItemsAsync();
        Task<TEntity> GetItemAsync(int id);
        Task<TEntity> GetItemAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteItemAsync(TEntity item);
        Task<int> SaveItemAsync(TEntity item);
        Task SaveItemsAsync(List<TEntity> entities);
    }
}