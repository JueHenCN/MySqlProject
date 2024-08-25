using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Core
{
    // SQL 过滤器接口
    public interface ISqlFilter
    {
        string GetFilterClause();
        Dictionary<string, object> GetParameters();
    }

}
