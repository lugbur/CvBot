using CVBot.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CVBot.DataAccess.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(TEntity item);

        void Modify(TEntity item);

        void Remove(TEntity item);

        void Merge(TEntity persisted, TEntity current);

        TEntity Get(List<object> keyValues);

        Task<TEntity> GetAsync(List<object> keyValues);

        List<TEntity> GetAll(List<string> includes);

        Task<List<TEntity>> GetAllAsync(List<string> includes);

        List<TEntity> AllMatching(Expression<Func<TEntity, bool>> filter, List<string> includes);        

        int AllMatchingCount(Expression<Func<TEntity, bool>> filter, List<string> includes);

        Task<int> AllMatchingCountAsync(Expression<Func<TEntity, bool>> filter, List<string> includes);
    }
}
