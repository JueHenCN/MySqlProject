using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Core
{
    public class StudentFilter : ISqlFilter
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        public string GetFilterClause()
        {
            var filters = new List<string>();
            if (Id.HasValue) filters.Add("StuID = @Id");
            if (!string.IsNullOrEmpty(Name)) filters.Add("StuName LIKE @Name");
            if (MinAge.HasValue) filters.Add("StuAge >= @MinAge");
            if (MaxAge.HasValue) filters.Add("StuAge <= @MaxAge");

            return filters.Any() ? "WHERE " + string.Join(" AND ", filters) : string.Empty;
        }

        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();
            if (Id.HasValue) parameters["@Id"] = Id.Value;
            if (!string.IsNullOrEmpty(Name)) parameters["@Name"] = $"%{Name}%";
            if (MinAge.HasValue) parameters["@MinAge"] = MinAge.Value;
            if (MaxAge.HasValue) parameters["@MaxAge"] = MaxAge.Value;
            return parameters;
        }
    }
}
