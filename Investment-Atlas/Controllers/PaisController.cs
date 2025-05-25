using Microsoft.AspNetCore.Mvc;

namespace Investment_Atlas.Controllers
{
    public class PaisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
