using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
