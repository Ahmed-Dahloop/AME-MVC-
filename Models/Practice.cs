    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    namespace AME.Models
    {
        public class Practice
        {
            [Key]
            public int PracticeID { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        [ForeignKey("Student")]
            public int StudentID { get; set; }
            [ForeignKey("Hobby")]
            public int HobbyID { get; set; }

            // Navigation properties
            public virtual Student? Student { get; set; }
            public virtual Hobby? Hobby { get; set; }
        }
    }
