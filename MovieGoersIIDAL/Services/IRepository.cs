using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersIIDAL.Services
{
    public interface IRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> PostAsync(T entity);
        Task<T> PutAsync(T entity);
        Task<T> DeleteAsync(int id);
        
    }
}
