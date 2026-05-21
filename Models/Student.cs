using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AME.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Student
    {
        [Key]
    
        public int StudentID { get; set; }

        [Required]
        public required string StudentName { get; set; }

        [Range(14, 20,ErrorMessage ="Age should be between 14 and 20")]
        public int StudentAge { get; set; }

        [Range(1, 3)]
        public int StudentClass { get; set; }

        public string? StudentAddress { get; set; }

        [Required]
        public Gender StudentGender { get; set; }
        [Required]
        public string? StudentPhone { get; set; }

        public string? StudentImage { get; set; }

        // Navigation properties
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Practice>? Practices { get; set; }
    }
}
