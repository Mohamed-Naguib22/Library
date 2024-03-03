using Library.Domain.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> criteria = null, Expression < Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
