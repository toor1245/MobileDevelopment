using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileDevelopment.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
    }
}