using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class BuyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }
    }
}
