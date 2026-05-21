using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AME.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [Required(ErrorMessage = "Teacher Name is required")]
        [StringLength(50, ErrorMessage = "Teacher Name must not exceed 50 characters")]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Teacher Age is required")]
        [Range(26, 59, ErrorMessage = "Age should be between 26 and 59")]
        public int TeacherAge { get; set; }

        [Required(ErrorMessage = "Teacher Gender is required")]
        public Gender TeacherGender { get; set; }

        [Required(ErrorMessage = "Teacher Phone Number is required")]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Invalid phone number format")]
        public string TeacherPhoneNO { get; set; }

        [Required(ErrorMessage = "Teaching Subject ID is required")]
        [ForeignKey("Subject")]
        [DefaultValue(1)]
        public int TeachingSubjectID { get; set; }

        public virtual Subject TeachingSubject { get; set; }
    }
}
