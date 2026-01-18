using System;
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



        // my part aa
        // login

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("stid") == null)
                return View();

            return View("Welcome");
        }

        [HttpPost]
        public IActionResult Login(int stid, string stPass)
        {
            Student s = context.Student
                .Where(x => x.StudentId == stid && x.Password == stPass)
                .FirstOrDefault();

            if (s != null)
            {
                HttpContext.Session.SetInt32("stid", s.StudentId);
                return View("Welcome", s.FirstName);
            }

            return View("Login");
        }

        // my part aa


        // details
        public IActionResult SDetails()
        {
            if (HttpContext.Session.GetInt32("stid") == null)
                return RedirectToAction("Login");

            int? studentID = HttpContext.Session.GetInt32("stid");
            Student s = context.Student.Find(studentID);

            return View(s);
        }



        public IActionResult StudentInfo()
        {
            int? sid = HttpContext.Session.GetInt32("stid");
            if (sid == null)
                return RedirectToAction("Login");

            var data = context.Enrollments
                .Where(e => e.StudentId == sid)
                .Select(e => new StudentTitleVM
                {
                    FirstName = e.Student.FirstName,
                    Title = e.Course.Title
                })
                .FirstOrDefault();

            return View(data);
        }


        //CHANGE PASSWORD

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
            Student s = context.Student.Find(sid);

            s.Password = newPass;
            context.Student.Update(s);
            context.SaveChanges();

            return RedirectToAction("SDetails");
        }

        // LOGOUT 

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}