using System.Reflection.Metadata.Ecma335;
using E_learninig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_learninig.Controllers
{
    public class StudentController : Controller
    {
        private EleariningContext context { get; set; }


        public StudentController(EleariningContext ctx) => context = ctx;
        public IActionResult Index()
        {
            var student = context.Student.Include(s => s.Enrollments).ThenInclude(e => e.Course).ToList();
            return View(student);
        }
 
        public IActionResult Search(string SearchKey)
        {
            var student = context.Student
                .Where(s => string.IsNullOrEmpty(SearchKey) ||
                            s.FirstName.Contains(SearchKey) ||
                            s.LastName.Contains(SearchKey))
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

            return View("Index", student);
        }

    }
}
