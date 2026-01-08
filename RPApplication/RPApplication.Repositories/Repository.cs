using Microsoft.EntityFrameworkCore;
using RPApplication.Entities.DatabaseContext;
using RPApplication.RepositoryContracts;

namespace RPApplication.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
