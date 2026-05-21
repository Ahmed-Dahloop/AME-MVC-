using AME.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AME.Controllers
{
    public class HomeController : Controller
    {
        private readonly AMEDbContext _context;

        public HomeController(AMEDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string employeeCode, string password)
        {
            if (string.IsNullOrWhiteSpace(employeeCode) || string.IsNullOrWhiteSpace(password))
            {
                TempData["ErrorMessage"] = "Employee code and password are required.";
                return RedirectToAction("Login");
            }

            // Check if the entered employee code exists in the database
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);

            if (employee == null)
            {
                // Employee not found
                TempData["ErrorMessage"] = "Invalid employee code or password.";
                return RedirectToAction("Login");
            }

            // If the employee exists, check if the entered code is uppercase
            if (!employeeCode.Equals(employee.EmployeeCode, StringComparison.Ordinal))
            {
                TempData["ErrorMessage"] = "Employee code should be uppercase.";
                return RedirectToAction("Login");
            }

            // Validate password
            if (!VerifyPassword(password, employee.EmployeePassword))
            {
                // Incorrect password
                TempData["ErrorMessage"] = "Invalid employee code or password.";
                return RedirectToAction("Login");
            }

            // If both employee code and password are correct, redirect to Dashboard
            for (int i = 0; i < 2; i++)
            {
                Console.Beep();
            }
            return RedirectToAction("Dashboard", "Employee");
        }





        // Method to verify hashed password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string enteredHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                string storedHash = hashedPassword.ToLower(); // Convert stored hash to lowercase

                return enteredHash == storedHash;
            }
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
