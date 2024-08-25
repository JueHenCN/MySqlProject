using MySqlProject.Core;
using MySqlProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new DatabaseConfig()
            {
                Server = "localhost",
                Port = 3306,
                Database = "school",
                UserId = "root",
                Password = "123456"
            };

            var dataOpearte = new DbConnection(config);
            var studentRepository = new StudentRepository(dataOpearte);

            await TestConnect(studentRepository);
            //await TestAddStudent(studentRepository);
            //await TestUpdateStudent(studentRepository);
            //await TestDelStudent(studentRepository);
            Console.ReadLine();
        }

        static async Task TestAddStudent(StudentRepository repository)
        {
            var students = new List<StudentModel>();
            for (int i = 0; i < 50; i++)
            {
                var student = new StudentModel()
                {
                    StudentCardId = Guid.NewGuid().ToString(),
                    StudentName = NameRandom.ChineseName(),
                    StudentAge = new Random().Next(8, 10),
                    ClassId = new Random().Next(1, 4),
                    StudentRegisterDate = DateTime.Now,
                    StudentGraduateDate = null
                };
                students.Add(student);
                //Console.WriteLine(student.StudentName);
            }

            var results = await repository.AddRangeAsync(students);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i])
                {
                    Console.WriteLine($"添加学生{students[i].StudentName}成功");
                }
                else
                {
                    Console.WriteLine($"添加学生{students[i].StudentName}失败");
                }
            }

        }

        static async Task TestUpdateStudent(StudentRepository repository)
        {
            var filter = new StudentFilter() { Id = 31 };
            var students = await repository.GetByFilterAsync(filter);
            if (students.Count > 0)
            {
                var student = students[0];
                student.StudentName = NameRandom.ChineseName();
                var updateResult = await repository.UpdateAsync(student);
                if (updateResult)
                {
                    Console.WriteLine($"更新学生{student.StudentName}成功");
                }
                else
                {
                    Console.WriteLine($"更新学生{student.StudentName}失败");
                }
            }
            else
            {
                Console.WriteLine("未找到学生");
            }
        }

        static async Task TestDelStudent(StudentRepository repository)
        {
            var filter = new StudentFilter() { Id = 32 };
            var students = await repository.GetByFilterAsync(filter);
            if (students.Count > 0)
            {
                var delResult = await repository.DeleteAsync(filter);
                if (delResult)
                {
                    Console.WriteLine($"删除学生{students[0].StudentName}成功");
                }
                else
                {
                    Console.WriteLine($"删除学生{students[0].StudentName}失败");
                }
            }
            else
            {
                Console.WriteLine("未找到学生");
            }
        }

        static async Task TestConnect(StudentRepository repository)
        {
            
            var students = await repository.GetAllAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"CardID: {student.StudentCardId}, Name: {student.StudentName}");
            }

            //if (await dataOpearte.Connect())
            //{
            //    Console.WriteLine("Connect success");
            //    var teachers = dataOpearte.GetTeachers(new TeacherModel() { TeacherName = "李"});

            //    foreach (var teacher in teachers)
            //    {
            //        Console.WriteLine($"TeacherId: {teacher.TeacherId}, TeacherName: {teacher.TeacherName}");
            //    }

            //    Console.WriteLine("=====================================");

            //    var students = dataOpearte.GetStudents();
            //    foreach (var student in students)
            //    {
            //        Console.WriteLine($"StudentId: {student.StudentId}, StudentName: {student.StudentName}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Connect failed");
            //}


        }
    }
}
