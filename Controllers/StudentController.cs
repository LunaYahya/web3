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
            var students = context.Student
                                  .Include(s => s.Enrollments)
                                  .ThenInclude(e => e.Course)
                                  .ToList();

            return View(students);
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

        public IActionResult Delete(int id )
        {
            Student s = context.Student.Find(id);
           if (s!= null)
          { context.Student.Remove(s);
           context.SaveChanges();
          }
          return RedirectToAction("Index","student");

        
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Add(Student s)
        {
            if (ModelState.IsValid)
            {
                context.Student.Add(s);
                context.SaveChanges();
                return RedirectToAction("Index","student");
            }

            return View(s);
        }


    }
}
