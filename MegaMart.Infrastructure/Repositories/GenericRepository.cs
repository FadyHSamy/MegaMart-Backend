using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MegaMart.Core.Interfaces;
using MegaMart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MegaMart.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _Context;

        public GenericRepository(AppDbContext appDbContext)
        {
            _Context = appDbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _Context.Set<T>().AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _Context.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, item) => current.Include(item));

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _Context.Set<T>().AsQueryable();
            query = includes.Aggregate(query, (current, item) => current.Include(item));

            var entity = await query.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return entity;

        }

        public async Task UpdateAsync(T entity)
        {
            _Context.Entry(entity).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
        }
    }
}
