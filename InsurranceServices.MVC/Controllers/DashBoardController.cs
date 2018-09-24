using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsurranceServices.MVC.Controllers
{
    public class DashBoardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "DashBoard";
            return View();
        }
    }
}