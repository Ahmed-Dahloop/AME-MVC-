using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AME.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        public string SubjectName { get; set; }

        public int SubjectScore { get; set; } = 100;
    }

}
