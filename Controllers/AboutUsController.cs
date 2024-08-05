using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
