using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpotOn.Domain
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> All();
        Task<T> FindAsync(Guid id);
        IEnumerable<T> Where(Expression<Func<T, bool>> p);
        T Add(T item);
        IEnumerable<T> AddRange(IEnumerable<T> item);
        void Update(T item);
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> p);
        long Count(Expression<Func<T, bool>> p);
        Task<int> SaveAsync();
        void DeleteAll(ISet<T> items);
        void Delete(T item);
    }
}

