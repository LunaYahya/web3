using System;
using System.Linq;
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
            var students = context.Student
                .Where(s => string.IsNullOrEmpty(SearchKey) ||
                            s.FirstName.Contains(SearchKey) ||
                            s.LastName.Contains(SearchKey))
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

            return View("Index", students);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login");

            Student s = context.Student.Find(id);
            if (s != null)
            {
                context.Student.Remove(s);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login");

            ViewBag.Courses = context.Course.ToList();
            return View(new StudentCreateVM());
        }

        // POST Add
        [HttpPost]
        public IActionResult Add(StudentCreateVM vm)
        {
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Email = vm.Email
                };

                context.Student.Add(student);
                context.SaveChanges();

                if (vm.SelectedCourseIds != null)
                {
                    foreach (var courseId in vm.SelectedCourseIds)
                    {
                        context.Enrollment.Add(new Enrollment
                        {
                            StudentId = student.StudentId,
                            CourseId = courseId,
                            Grade = "N/A"
                        });
                    }
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Courses = context.Course.ToList();
            return View(vm);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int stid, string stPass)
        {
            var s = context.Student.SingleOrDefault(x => x.StudentId == stid && x.Password == stPass);

            if (s != null)
            {
                HttpContext.Session.SetInt32("stid", s.StudentId);
                HttpContext.Session.SetString("Role", s.Role);
                return RedirectToAction("Welcome");
            }

            ViewBag.Error = "Invalid ID or Password";
            return View();
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            int? sid = HttpContext.Session.GetInt32("stid");
            if (sid == null)
                return RedirectToAction("Login");

            Student s = context.Student.Find(sid);
            ViewBag.StudentName = s.FirstName;
            ViewBag.Role = s.Role;
            return View();
        }

        public IActionResult SDetails()
        {
            int? studentID = HttpContext.Session.GetInt32("stid");
            if (studentID == null)
                return RedirectToAction("Login");

            var student = context.Student
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .SingleOrDefault(s => s.StudentId == studentID);

            return View(student);
        }

        public IActionResult StudentInfo()
        {
            int? sid = HttpContext.Session.GetInt32("stid");
            if (sid == null)
                return RedirectToAction("Login");

            var data = context.Enrollment
                .Where(e => e.StudentId == sid)
                .Select(e => new StudentTitleVM
                {
                    FirstName = e.Student.FirstName,
                    Title = e.Course.Title
                })
                .ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetInt32("stid") == null)
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string newPass)
        {
            int? sid = HttpContext.Session.GetInt32("stid");
            if (sid == null) return RedirectToAction("Login");

            Student s = context.Student.Find(sid);
            if (s != null)
            {
                s.Password = newPass;
                context.Student.Update(s);
                context.SaveChanges();
            }

            return RedirectToAction("SDetails");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
