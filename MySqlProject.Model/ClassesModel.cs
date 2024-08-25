using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Model
{
    public class ClassesModel : BindableBase
    {
        private int classId;

        public int ClassId
        {
            get { return classId; }
            set { SetProperty(ref classId, value); }
        }

        private string className;

        public string ClassName
        {
            get { return className; }
            set { SetProperty(ref className, value); }
        }

        private int teacherId;

        public int TeacherId
        {
            get { return teacherId; }
            set { SetProperty(ref teacherId, value); }
        }
    }
}
