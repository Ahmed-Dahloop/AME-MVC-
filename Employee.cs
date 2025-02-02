    using System.ComponentModel.DataAnnotations;

    namespace AME.Models
    {
        public class Employee
        {
            [Key]
            [Required(ErrorMessage = "Employee ID is required")]
            public string EmployeeCode { get; set; }

            [Required(ErrorMessage = "Employee Name is required")]
            public string EmployeeName { get; set; }

            [Required(ErrorMessage = "Employee Job is required")]
            public string EmployeeJob { get; set; }

            [Phone(ErrorMessage = "Invalid phone number format")]
            public string EmployeePhone { get; set; }

            [Required(ErrorMessage = "Employee Gender is required")]
            public Gender EmployeeGender { get; set; }

            [Range(25, 50, ErrorMessage = "Age should be between 25 and 50")]
            public int EmployeeAge { get; set; }

            [Required(ErrorMessage = "Employee Password is required")]
            public string EmployeePassword { get; set; }
        }
    }
