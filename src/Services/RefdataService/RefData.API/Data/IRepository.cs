using RefData.API.Models;
using System.Linq.Expressions;

namespace RefData.API.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetByIdAsync(string id);
        void Add(T entity);

        Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
