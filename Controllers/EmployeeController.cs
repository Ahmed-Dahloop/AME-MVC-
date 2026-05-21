using System;
using Microsoft.AspNetCore.Mvc;
using AME.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AME.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AMEDbContext _context;

        public EmployeeController(AMEDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult Employee()
        {
            return View();
        }

        private void SaveEmployee(Employee model)
        {
            model.EmployeeCode = model.EmployeeCode.Trim();
            model.EmployeeName = model.EmployeeName.Trim();
            model.EmployeeJob = model.EmployeeJob.Trim();
            model.EmployeePhone = model.EmployeePhone.Trim();
        
            string hashedPassword = HashPassword(model.EmployeePassword);
            if (_context.Employees.Any(e => e.EmployeeCode == model.EmployeeCode))
            {
                TempData["ErrorMessage"] = "Employee code already exists.";
                return;
            }
            if (!IsValidEmployeeCode(model.EmployeeCode))
            {
                TempData["ErrorMessage"] = "Employee code must start with a character, contain both characters and numbers, and have a length of 2 or 3.";
                return;
            }

            if (!IsValidEmployeeJob(model.EmployeeJob))
            {
                TempData["ErrorMessage"] = "Invalid employee job. The job should be Manager, Co-Manager, Employee, Assistant, or Helper.";
                return;
            }

            if (!IsValidPhoneNumber(model.EmployeePhone))
            {
                TempData["ErrorMessage"] = "Invalid phone number. The phone number should start with '01' and have a total length of 11 digits. Can be separated by '-'";
                return;
            }

            if (!IsValidAge(model.EmployeeAge))
            {
                TempData["ErrorMessage"] = "Invalid age. The age should be between 25 and 50.";
                return;
            }

            if (!IsValidName(model.EmployeeName))
            {
                TempData["ErrorMessage"] = "Invalid name format. Please enter both first name and last name separated by a space.";
                return;
            }

            var newEmployee = new Employee
            {
                EmployeeCode = model.EmployeeCode,
                EmployeeName = model.EmployeeName,
                EmployeeJob = model.EmployeeJob,
                EmployeePhone = model.EmployeePhone,
                EmployeeGender = model.EmployeeGender,
                EmployeeAge = model.EmployeeAge,
                EmployeePassword = hashedPassword
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }
        // Display all Employees 
        public IActionResult Index()
        {
            IEnumerable<Employee> employees = _context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(m => m.EmployeeCode == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,EmployeeName,EmployeeJob,EmployeePhone,EmployeeGender,EmployeeAge,EmployeePassword")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = HashPassword(employee.EmployeePassword);
                employee.EmployeePassword = hashedPassword;
                _context.Add(employee);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Employee created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,EmployeeName,EmployeeJob,EmployeePhone,EmployeeGender,EmployeeAge,EmployeePassword")] Employee employee)
        {
            if (id != employee.EmployeeCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeCode))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        private bool EmployeeExists(string employeeCode)
        {
            return _context.Employees.Any(e => e.EmployeeCode == employeeCode);
        }


        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(m => m.EmployeeCode == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfirmed(string id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute hash from password string
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        private bool IsValidEmployeeCode(string code)
        {
            // Check if code is null or empty
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            // Check if code length is not between 2 and 3 characters
            if (code.Length < 2 || code.Length > 3)
            {
                return false;
            }

            // Check if the first character is a letter
            if (!char.IsLetter(code[0]))
            {
                return false;
            }

            // Check if code contains at least one letter and one number
            bool hasLetter = false;
            bool hasNumber = false;
            foreach (char c in code)
            {
                if (char.IsLetter(c))
                {
                    hasLetter = true;
                }
                else if (char.IsNumber(c))
                {
                    hasNumber = true;
                }

                // If both a letter and a number are found, the code is valid
                if (hasLetter && hasNumber)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidEmployeeJob(string job)
        {
            string[] validJobs = { "Manager", "Co-Manager", "Employee", "Assistant", "Helper" };


            return !string.IsNullOrEmpty(job)
                   && job.Length <= 12
                   && char.IsUpper(job[0])  // Check if the first character is uppercase
                   && Array.Exists(validJobs, j => j.Equals(job, StringComparison.OrdinalIgnoreCase));
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            // Remove any non-numeric characters
            string numericPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Check if the phone number starts with "01" and has a total length of 11 digits
            return numericPhoneNumber.StartsWith("01") && numericPhoneNumber.Length == 11;
        }

        private bool IsValidAge(int age)
        {
            return age >= 25 && age <= 50;
        }
        private bool IsValidName(string name)
        {
            // Split the name into first name and last name
            string[] parts = name.Trim().Split(' ');
            if (parts.Length != 2) return false; // Ensure both first name and last name are provided

            // Check length constraints for first and last names
            if (parts[0].Length > 12 || parts[1].Length > 12) return false;

            // Check if both parts start with a capital letter
            return char.IsUpper(parts[0][0]) && char.IsUpper(parts[1][0]);
        }


        [HttpPost]
        public IActionResult Employee(Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaveEmployee(model);
               
                    TempData["SuccessMessage"] = "All information is Correctly check if not Stored in Database then there is invalid input .";
                    return RedirectToAction("Employee");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while saving data: {ex}");
                    TempData["ErrorMessage"] = "An error occurred while saving data. Please try again.";
                    return View(model);
                }
            }
            else
            {
                // If model state is not valid, return to the view with validation errors
                TempData["ErrorMessage"] = "Invalid data. Please correct the errors below.";
                return View(model);
            }
        }



    }
}
