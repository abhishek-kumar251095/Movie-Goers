using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersIIDAL.Services
{
    public abstract class Repository<TEntity, TContext>: IRepository<TEntity> 
        where TEntity: class
        where TContext: DbContext
    {

        protected readonly ApplicationDBContext _context;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);

        }

        public async Task<TEntity> PostAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> PutAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var res =  await _context.Set<TEntity>().FindAsync(id);
            if(res == null)
            {
                return res;
            }
            _context.Set<TEntity>().Remove(res);
            await _context.SaveChangesAsync();
            return res;
        }
    }
}
