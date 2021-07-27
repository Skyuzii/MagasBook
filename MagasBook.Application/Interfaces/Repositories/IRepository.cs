using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MagasBook.Domain.Entities;

namespace MagasBook.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class, IBaseDomain
    {
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<IList<T>> GetPagedResponseAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}