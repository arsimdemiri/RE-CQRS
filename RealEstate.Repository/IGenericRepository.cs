using RealEstate.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> Get(Guid id);
        Task<List<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(Guid id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
