using System.Web.Mvc;

namespace InsurranceServices.MVC.Controllers
{
    public class PartnerController : Controller
    {
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Title = "Partners";
            return View();
        }
    }
}