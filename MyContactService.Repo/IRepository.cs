using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace MyContactService.Repo
{
    public interface IRepository<T>: IDisposable
    {
        string DBKey { get;}
        Task<bool> InsertAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
        
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(string id);
        Task<T> GetByIdAsync(string id);

        bool Update(string id, T entity);
        Task<bool> UpdateAsync(string id, T entity);
        bool Commit();
        

    }
}