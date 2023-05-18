using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class StudentVolgensNaamOplopendComparer : IComparer<Student>
    {
        public int Compare(Student student1, Student student2)
        {
            return string.Compare(student1.Naam, student2.Naam);
        }
    }
    public class StudentVolgensNaamAflopendComparer : IComparer<Student>
    {
        public int Compare(Student student1, Student student2)
        {
            return (-1) * string.Compare(student1.Naam, student2.Naam);
        }
    }
}
