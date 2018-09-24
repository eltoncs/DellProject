using System.Web.Mvc;

namespace InsurranceServices.MVC.Controllers
{
    public class SimulationController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Simulations";
            return View();
        }
    }
}