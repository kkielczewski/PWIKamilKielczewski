using System;
using System.Collections.Generic;

namespace KamilKielczewskiPWI.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            StudentGrade = new HashSet<StudentGrade>();
        }

        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public OnlineCourse OnlineCourse { get; set; }
        public OnsiteCourse OnsiteCourse { get; set; }
        public ICollection<CourseInstructor> CourseInstructor { get; set; }
        public ICollection<StudentGrade> StudentGrade { get; set; }
    }
}
