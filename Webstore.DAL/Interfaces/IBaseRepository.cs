using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        IQueryable<T> ReadAll();
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}
