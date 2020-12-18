using Source_Control_Final_Assignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Source_Control_Final_Assignment.Controllers
{
    public class HomeController : Controller
    {
        StudentDbEntities db = new StudentDbEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Student s)
        {
            var student = db.Students.Where(model => model.UserName == s.UserName && model.Password == s.Password).FirstOrDefault() ;
            if (student != null)
            {
                Session["StudId"] = s.Id.ToString();
                Session["StudUsername"] = s.UserName.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('Login Successful !')</script>";
                return RedirectToAction("Index", "Dashboard");
            }
            else 
            {
                ViewBag.ErrorMessage = "<script>alert('Username or Password is incorrect !')</script>";
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Student s, HttpPostedFileBase ImagePath)
        {
            if(ModelState.IsValid == true)
            {

                try
                {
                    if (ImagePath != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), Path.GetFileName(ImagePath.FileName));
                        ImagePath.SaveAs(path);
                        s.ImagePath = path;

                    }
                    
                    db.Students.Add(s);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        ViewBag.InsertMessage = "<script>alert('Registered Successfully !')</script>";
                        ModelState.Clear();
                    }
                }
                catch(Exception)
                {
                    ViewBag.InsertMessage = "<script>alert('Registered Failed !')</script>";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
    }
}