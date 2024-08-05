using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class PolicyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
