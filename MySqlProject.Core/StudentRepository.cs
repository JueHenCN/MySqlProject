using MySqlConnector;
using MySqlProject.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Unity.Storage.RegistrationSet;

namespace MySqlProject.Core
{
    // 学生仓储
    public class StudentRepository : IRepository<StudentModel>
    {
        private readonly DbConnection dbConnection;

        public StudentRepository(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public async Task<List<StudentModel>> GetAllAsync()
        {
            return await GetByFilterAsync(new StudentFilter());
        }

        public async Task<List<StudentModel>> GetByFilterAsync(ISqlFilter filter)
        {
            var students = new List<StudentModel>();
            var openResult = await dbConnection.OpenAsync();
            if (!openResult)
            {
                return students;
            }
            var connection = dbConnection.MySqlConn;
            string sql = $"SELECT * FROM student {filter.GetFilterClause()}";
            using (var command = new MySqlCommand(sql, connection))
            {
                foreach (var param in filter.GetParameters())
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var student = new StudentModel
                        {
                            StudentId = reader.GetInt32("StuID"),
                            StudentCardId = reader.GetString("StuCardID"),
                            StudentName = reader.GetString("StuName"),
                            StudentAge = reader.GetInt32("StuAge"),
                            ClassId = reader.GetInt32("StuClassID"),
                            StudentRegisterDate = reader.GetDateTime("StuRegisterDate"),
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("StuGraduateDate")))
                        {
                            student.StudentGraduateDate = reader.GetDateTime("StuGraduateDate");
                        }
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public async Task<bool> AddAsync(StudentModel entity)
        {
            var openResult = await dbConnection.OpenAsync();
            if (!openResult)
            {
                return false;
            }
            var sql = "INSERT INTO student (StuCardID, StuName, StuAge, StuClassID, StuRegisterDate, StuGraduateDate) " +
                "VALUES (@StuCardID, @StuName, @StuAge, @StuClassID, @StuRegisterDate, @StuGraduateDate)";
            var connection = dbConnection.MySqlConn;
            using (var command = new MySqlCommand(sql, connection))
            {
                var parameters = GetSqlParams(entity);
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                int result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }

        }

        public async Task<List<bool>> AddRangeAsync(List<StudentModel> entities)
        {
            var openResult = await dbConnection.OpenAsync();
            var results = Enumerable.Repeat(false, entities.Count).ToList();
            if (!openResult)
            {
                return results;
            }
            var sql = "INSERT INTO student (StuCardID, StuName, StuAge, StuClassID, StuRegisterDate, StuGraduateDate) " +
                "VALUES (@StuCardID, @StuName, @StuAge, @StuClassID, @StuRegisterDate, @StuGraduateDate)";
            var connection = dbConnection.MySqlConn;

            using (var transaction = await connection.BeginTransactionAsync())
            {
                for (int i = 0; i < results.Count; i++)
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = sql;
                            var parameters = GetSqlParams(entities[i]);
                            foreach (var item in parameters)
                                command.Parameters.AddWithValue(item.Key, item.Value);
                            int result = await command.ExecuteNonQueryAsync();
                            results[i] = result > 0;
                        }
                    }
                    catch (Exception e)
                    {
                        results[i] = false;
                    }
                }

                await transaction.CommitAsync();
            }
            return results;
        }

        public async Task<bool> UpdateAsync(StudentModel entity)
        {
            var openResult = await dbConnection.OpenAsync();
            if (!openResult)
            {
                return false;
            }
            string sql = "UPDATE student SET StuCardID = @StuCardID, StuName = @StuName, StuAge = @StuAge, " +
                "StuClassID = @StuClassID, StuRegisterDate = @StuRegisterDate, StuGraduateDate = @StuGraduateDate " +
                "WHERE StuID = @StuID";
            var connection = dbConnection.MySqlConn;
            using(var command = new MySqlCommand(sql, connection))
            {
                var parameters = GetSqlParams(entity);
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                int result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(ISqlFilter filter)
        {
            var openResult = await dbConnection.OpenAsync();
            if (!openResult)
            {
                return false;
            }
            using (var connection = dbConnection.MySqlConn)
            {
                string sql = $"DELETE FROM student {filter.GetFilterClause()}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    foreach (var param in filter.GetParameters())
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    await command.ExecuteNonQueryAsync();
                };
            }
            return true;
        }

        public Dictionary<string, object> GetSqlParams(StudentModel entity)
        {
            return new Dictionary<string, object>
            {
                ["@StuID"] = entity.StudentId,
                ["@StuCardID"] = entity.StudentCardId,
                ["@StuName"] = entity.StudentName,
                ["@StuAge"] = entity.StudentAge,
                ["@StuClassID"] = entity.ClassId,
                ["@StuRegisterDate"] = entity.StudentRegisterDate,
                ["@StuGraduateDate"] = entity.StudentGraduateDate ?? (object)DBNull.Value
            };
        }
    }
}
