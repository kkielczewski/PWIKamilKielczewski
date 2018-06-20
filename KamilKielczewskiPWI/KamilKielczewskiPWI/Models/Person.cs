using System;
using System.Collections.Generic;

namespace KamilKielczewskiPWI.Models
{
    public partial class Person
    {
        public Person()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            StudentGrade = new HashSet<StudentGrade>();
        }

        public int PersonId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<CourseInstructor> CourseInstructor { get; set; }
        public ICollection<StudentGrade> StudentGrade { get; set; }
    }
}
