using Microsoft.AspNet.Mvc;

namespace BearerAuthentication.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
