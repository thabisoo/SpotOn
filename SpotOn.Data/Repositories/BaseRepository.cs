using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SpotOn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
           where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(120);
        }

        public T Add(T item)
        {
            AsSet().Add(item);
            return item;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> item)
        {
            AsSet().AddRange(item);
            return item;
        }

        public virtual IEnumerable<T> All()
        {
            return AsSet();
        }

        public Task<long> CountAsync()
        {
            return AsSet().LongCountAsync();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> p)
        {
            return AsSet().LongCountAsync(p);
        }

        public long Count(Expression<Func<T, bool>> p)
        {
            return AsSet().LongCount(p);
        }

        public void Delete(T item)
        {
            AsSet().Remove(item);
        }

        public void DeleteAll(ISet<T> items)
        {
            AsSet().RemoveRange(items);
        }

        public virtual async Task<T> FindAsync(Guid id)
        {
            return await AsSet().FindAsync(id);
        }

        public virtual IEnumerable<T> Paginate<TKey>(int pageSize, int pageNumber, Func<T, TKey> keySelector)
        {
            return AsSet().OrderBy(keySelector)
                          .Skip(pageNumber * pageSize)
                          .Take(pageSize);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T item)
        {
            var entry = item as EntityEntry<T>;
            if (entry != null)
            {
                entry.State = EntityState.Modified;
            }
        }

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> p)
        {
            return AsSet().Where(p);
        }

        protected DbSet<T> AsSet()
        {
            return _context.Set<T>();
        }
    }
}

