using RefData.API.Models;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace RefData.API.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IConnectionMultiplexer _redis;

        public Repository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(entity));
            }

            var db = _redis.GetDatabase();
            entity.Id = $"{typeof(T).Name.ToLower()}:{Guid.NewGuid()}";

            var serialEntity = JsonSerializer.Serialize(entity);

            db.HashSet($"refData_hash_{typeof(T).Name.ToLower()}", new HashEntry[]
               {new HashEntry(entity.Id, serialEntity)});

        }

        public Task<T> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            var db = _redis.GetDatabase();


            var completeSet = db.HashGetAll($"refData_hash_{typeof(T).Name.ToLower()}");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val =>
                    JsonSerializer.Deserialize<T>(val.Value)).ToList();
                return obj;
            }

            return null;
        }

        public T GetByIdAsync(string id)
        {
            var db = _redis.GetDatabase();
            var plat = db.HashGet(typeof(T).Name.ToLower(), id);
            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<T>(plat);
            }
            return null;
        }

      
    }
}
