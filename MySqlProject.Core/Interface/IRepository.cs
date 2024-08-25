using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Core
{
    // 基础仓储接口
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetByFilterAsync(ISqlFilter filter);
        Task<bool> AddAsync(T entity);
        Task<List<bool>> AddRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(ISqlFilter filter);
        Dictionary<string, object> GetSqlParams(T entity);
    }
}
