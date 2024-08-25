using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Model
{
    public class StudentModel : BindableBase
    {
        private int studentId;

        public int StudentId
        {
            get { return studentId; }
            set { SetProperty(ref studentId, value); }
        }

        private string studentCardId;

        public string StudentCardId
        {
            get { return studentCardId; }
            set { SetProperty(ref studentCardId, value); }
        }

        private string studentName;

        public string StudentName
        {
            get { return studentName; }
            set { SetProperty(ref studentName, value); }
        }

        private int studentAge;

        public int StudentAge
        {
            get { return studentAge; }
            set { SetProperty(ref studentAge, value); }
        }

        private int classId;

        public int ClassId
        {
            get { return classId; }
            set { SetProperty(ref classId, value); }
        }

        private DateTime studentRegisterDate;

        public DateTime StudentRegisterDate
        {
            get { return studentRegisterDate; }
            set { SetProperty(ref studentRegisterDate, value); }
        }

        private DateTime? studentGraduateDate;

        public DateTime? StudentGraduateDate
        {
            get { return studentGraduateDate; }
            set { SetProperty(ref studentGraduateDate, value); }
        }
    }
}
