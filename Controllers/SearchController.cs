using INFT3050.Data;
using INFT3050.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSearchResults(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new List<object>());
            }

            var products = _context.Products
                .Where(p => p.Name.Contains(query) || p.Category.Name.Contains(query))
                .Select(p => new
                {
                    p.Name,
                    Category = p.Category.Name,
                    p.Price,
                    ImagePath = p.ImagePath
                })
                .ToList();

            return Json(products);
        }
    }
}
