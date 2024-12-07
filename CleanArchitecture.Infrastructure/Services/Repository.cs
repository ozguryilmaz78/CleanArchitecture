using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.State;
using CleanArchitecture.Infrastructure.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IDbConnection _connection;

        public Repository(ApplicationDbContext context, IDbConnection? connection = null)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _connection = connection ?? new SqlConnection(context.Database.GetDbConnection().ConnectionString);
        }

        #region EF Core

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> GetAllWithTracking()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression);
        }

        public IQueryable<TEntity> WhereWithTracking(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<TEntity> GetByExpressionWithTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(expression, cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); // Save changes
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges(); // Save changes
        }

        public async Task DeleteByIdAsync(string id) // Change string to int
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(); // Save changes
            }
        }

        public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken); // Save changes
            }
        }

        #endregion

        #region Dapper

        public async Task<TEntity> GetByIdAsync(string id)
        {
                string sql = $"SELECT * FROM {typeof(TEntity).Name}s WHERE Id = @Id";
                return await _connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
                string sql = $"SELECT * FROM {typeof(TEntity).Name}s";
                return await _connection.QueryAsync<TEntity>(sql);
        }

        #endregion


    }
}
