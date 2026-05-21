using AME.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AME.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AMEDbContext _context;

        public TeacherController(AMEDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TeacherRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherRegister(Teacher model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // You can add additional validation here if needed

                    SaveTeacher(model);
                    TempData["SuccessMessage"] = "Teacher data has been successfully saved.";
                    return RedirectToAction(nameof(TeacherRegister));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while saving data: {ex.Message}";
                }
            }

            return View(model);
        }

        private void SaveTeacher(Teacher model)
        {
            _context.Teachers.Add(model);
            _context.SaveChanges();
        }
    }
}
