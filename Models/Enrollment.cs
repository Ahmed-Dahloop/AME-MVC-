using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AME.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        // Navigation properties
        public virtual Student? Student { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
