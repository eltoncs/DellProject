using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsurranceServices.MVC.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            ViewBag.Title = "Customer";
            return View();
        }


    }
}