using System;
using System.Collections.Generic;

namespace KamilKielczewskiPWI.Models
{
    public partial class CourseInstructor
    {
        public int CourseId { get; set; }
        public int PersonId { get; set; }

        public Course Course { get; set; }
        public Person Person { get; set; }
    }
}
