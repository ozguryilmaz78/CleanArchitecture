using CleanArchitecture.Domain.Entities.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //EFCore
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllWithTracking();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<TEntity> GetByExpressionWithTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        Task DeleteByIdAsync(string id);
        Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        //Dapper
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
