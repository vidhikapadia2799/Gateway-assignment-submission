using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SourceControlAssignment1.Models;

namespace SourceControlAssignment1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Register register)
        {
            if(ModelState.IsValid)
            {
                ViewBag.StudentDetails = register;
                return View("Success");
            }
            return View("Index");
        }


    }
}