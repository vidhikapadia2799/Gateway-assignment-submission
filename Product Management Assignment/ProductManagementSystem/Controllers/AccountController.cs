using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagementSystem.Models;
using System.Web.Security;
using log4net;

namespace ProductManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(AccountController));      //type of class
       
        // GET: Account
        public ActionResult Login()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

            return View();
        }

        //Login method 
        [HttpPost]
        public ActionResult Login(Logintbl login)
        {
            using (var context = new ProductManagementEntities())
            {
                bool isValid = context.Usertbls.Any(x => x.Email == login.Email && x.Password == login.Password); // check email and password is entered correct by user or not
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(login.Email, false); 
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    TempData["Login failed"] = "Email Address or Password is incorrect";
                    ModelState.AddModelError("", "Invalid Email or Passowrd");
                }
            }
            return RedirectToAction("Dashboard");
        }
        public ActionResult Signup()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");

            return View();
        }

        [HttpPost]
        public ActionResult Signup(Usertbl register)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ProductManagementEntities())
                {
                    context.Usertbls.Add(register);
                    context.SaveChanges();
                    TempData["User registered successfully"] = "User registered successfully";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ModelState.AddModelError("","Data is not valid");

            }
          
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            return RedirectToAction("Login");
        }

       
    }

    
}