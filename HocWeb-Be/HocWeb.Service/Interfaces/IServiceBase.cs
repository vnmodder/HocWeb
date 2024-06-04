using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HocWeb.Service.Interfaces
{
    public interface IServiceBase<TEntity>
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<TEntity?> Add(TEntity model);
        Task<bool> Update(TEntity model);
        Task<bool> Delete(int id);
    }
}
