using Microsoft.AspNetCore.Mvc;

namespace Investment_Atlas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
