using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MagasBook.Application.Interfaces.Repositories;
using MagasBook.Domain.Entities;
using MagasBook.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MagasBook.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseDomain
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetPagedResponseAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(_dbSet, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}