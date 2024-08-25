using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Model
{
    public class TeacherModel : BindableBase
    {
        private int teacherId;

        public int TeacherId
        {
            get { return teacherId; }
            set { SetProperty(ref teacherId, value); }
        }

        private string teacherName;

        public string TeacherName
        {
            get { return teacherName; }
            set { SetProperty(ref teacherName, value); }
        }
    }
}
