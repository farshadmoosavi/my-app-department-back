using System;
using Line.Data;
using Line.Models;
using Line.Repositories.Interfaces;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;

namespace Line.Repositories.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LineDbContext _dbContext;

        public GenericRepository(LineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> Create(T t)
        {
            _dbContext.Set<T>().Add(t);
            await SaveChangesAsync();
            return t;
        }

        public async Task Delete(int id)
        {
            var modelToDelete = await _dbContext.Set<T>().FindAsync(id);
            if (modelToDelete != null)
            {
                _dbContext.Set<T>().Remove(modelToDelete);
                await SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("There is no record with that id.");
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var model = await _dbContext.Set<T>().FindAsync(id);
            if (model == null)
            {
                throw new ArgumentException("There is no record with that id.");
            }

            return model;
        }

        public async Task Update(T t)
        {
            _dbContext.Entry(t).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        protected async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }   
    }
}

