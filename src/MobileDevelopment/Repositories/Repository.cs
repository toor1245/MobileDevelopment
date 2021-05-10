using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MobileDevelopment.Interfaces;

namespace MobileDevelopment.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbSet<TEntity> _entity;
        
        public Repository(DbContext context)
        {
            _entity = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
        }
    }
}