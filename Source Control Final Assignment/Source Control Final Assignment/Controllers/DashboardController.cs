 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Source_Control_Final_Assignment.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["StudId"] == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else 
            {
                return View();
            }
            
        }
    }
}