using INFT3050.Data;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INFT3050.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                            .ToListAsync();

            var viewModel = products.Select(prod => new ProductCategoryVM
            {
                Product = prod
            }).ToList();

            return View(viewModel);
        }
    }
}
