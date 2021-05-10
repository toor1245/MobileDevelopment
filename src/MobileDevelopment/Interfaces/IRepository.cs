using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MobileDevelopment.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : new()
    {
        void CreateTable();
        List<TEntity> GetItems();
        TEntity GetItem(int id);
        TEntity GetItem(Expression<Func<TEntity, bool>> predicate);
        int DeleteItem(TEntity item);
        int SaveItem(TEntity item);
        void SaveItems(List<TEntity> entities);
    }
}