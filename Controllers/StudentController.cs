using System;
using Microsoft.AspNetCore.Mvc;
using AME.Models;
using Microsoft.EntityFrameworkCore;

namespace AME.Controllers
{
    public class StudentController : Controller
    {
        private readonly AMEDbContext _context;

        public StudentController(AMEDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult StudentRegister()
        {
            return View();
        }

        private void SaveStudent(Student model)
        {
            if (!IsValidAge(model.StudentAge))
            {
                TempData["ErrorMessage"] = "Invalid age. The age should be between 14 and 20.";
                return;
            }
            if (!IsValidPhoneNumber(model.StudentPhone))
            {
                TempData["ErrorMessage"] = "Invalid phone number. The phone number should start with '01' and have a total length of 11 digits. Can be separated by '-'";
                return;
            }
            if (!IsValidName(model.StudentName))
            {
                TempData["ErrorMessage"] = "Invalid name format. Please enter both first name and last name separated by a space.";
                return;
            }
            if (!IsValidAddress(model.StudentAddress))
            {
                TempData["ErrorMessage"] = "Invalid address format. The address should contain a mix of numbers, characters, commas, and spaces.";
                return;
            }
            if (!IsValidStudentID(model.StudentID))
            {
                TempData["ErrorMessage"] = "Invalid student ID. The ID should be an integer between 0 and 100.";
                return;
            }
            var newStudent = new Student
            {
                StudentID =model.StudentID,
                StudentName = model.StudentName,
                StudentAddress = model.StudentAddress,
                StudentPhone = model.StudentPhone,
                StudentGender = model.StudentGender,
                StudentAge = model.StudentAge,
                StudentClass = model.StudentClass,
                StudentImage = model.StudentImage
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();

        }
        // display students in list 

        public IActionResult Index()
        {
            IEnumerable<Student> students = _context.Students.ToList();
            return View(students);
        }

        // GET: Student/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.FirstOrDefault(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StudentID,StudentName,StudentAddress,StudentPhone,StudentGender,StudentAge,StudentClass,StudentImage")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StudentID,StudentName,StudentAddress,StudentPhone,StudentGender,StudentAge,StudentClass,StudentImage")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Student/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.FirstOrDefault(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
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
            return age >= 14 && age <= 20;
        }
        private bool IsValidAddress(string address)
        {
            // Check if address contains a mix of numbers, characters, commas, and spaces
            return address.Any(char.IsDigit) && address.Any(char.IsLetter) &&
                   address.Contains(',') && address.Contains(' ');
        }
        private bool IsValidStudentID(int id)
        {
            return id >= 0 && id <= 100;
        }

        [HttpPost]
        public IActionResult StudentRegister(Student model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaveStudent(model);
                    TempData["SuccessMessage"] = "All information is Correctly check if not Stored in Database then there is invalid input .";
                    return RedirectToAction("StudentRegister"); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while saving data: {ex}");
                    
                    throw;
                }
            }

           
            return View(model);
        }
    }
}
