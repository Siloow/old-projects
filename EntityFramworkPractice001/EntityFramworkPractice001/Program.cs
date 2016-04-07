using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramworkPractice001
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentConte)

        }
    }

    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }

        public virtual List<Subject> Subjects { get; set; }
    }

    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }

        public virtual Student Student { get; set; }
    }

    class StudentContext : DbContext
    {

    }
}
