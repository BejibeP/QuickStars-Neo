using HDA.Domain.Entities;
using System.Linq.Expressions;

namespace HDA.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(bool ignoreArchived = true, params Expression<Func<T, object>>[] includes);

        Task<T?> GetById(long id, bool ignoreArchived = true, params Expression<Func<T, object>>[] includes);

        Task<T?> Create(T entity);

        Task<T?> Update(T entity);

        Task<bool> Delete(long id);
    }
}