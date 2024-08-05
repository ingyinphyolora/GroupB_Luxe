using Microsoft.AspNetCore.Mvc; 
using INFT3050.ViewModel;
using INFT3050.Data;
using Microsoft.EntityFrameworkCore;
using INFT3050.Models;
using Microsoft.Data.SqlClient;

namespace INFT3050.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FurnitureController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        private string GetImageUrlForCategory(string categoryId)
        {
            return categoryId switch
            {
                "LivingRoom" => "/images/living_room.jpeg",
                "DiningRoom" => "/images/dining_room.jpeg",
                "BedRoom" => "/images/bedroom.jpeg",
                "A" => "/images/tbl.jpeg",
                "B" => "/images/sofa.jpg",
                "C" => "/images/tv.jpeg",
                "D" => "/images/livingroom_shelves.jpeg",
                "E" => "/images/headrest_sofa.jpeg",
                "F" => "/images/diningroom.jpeg",
                "G" => "/images/cabinet_diningroom.jpeg",
                "H" => "/images/round_dining_table.jpeg",
                "I" => "/images/main_dining_chairs.jpeg",
                "J" => "/images/bedroom.jpeg",
                "K" => "/images/main_bedsidetables.jpeg",
                "L" => "/images/mirror.png",
                "M" => "/images/clothesrack.jpg",
                "Q" => "/images/studydesk.jpeg",
                _ => "/images/agile.jpeg"
            };
        }

        private IQueryable<Product> SortProducts(IQueryable<Product> products, string sortOrder)
        {
            return sortOrder switch
            {
                "az" => products.OrderBy(p => p.Name),
                "price-high-to-low" => products.OrderByDescending(p => p.Price),
                "price-low-to-high" => products.OrderBy(p => p.Price),
                _ => products
            };
        }

        public async Task<IActionResult> LivingRoom(string categoryId, string sortOrder)
        {
            if (categoryId == "LivingRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Living-Room");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Living Room";
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                @ViewBag.CategoryId = "LivingRoom";

                var productsQuery = _context.Products.Where(p => p.Category.Room == "Living-Room");

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                @ViewBag.CategoryId = categoryId;


                var productsQuery = _context.Products.Where(p => p.CategoryId == categoryId);

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }

        public async Task<IActionResult> DiningRoom(string categoryId, string sortOrder)
        {
            if (categoryId == "DiningRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Dining-Room");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Dining Room";
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                ViewBag.CategoryId = "DiningRoom";

                var productsQuery = _context.Products.Where(p => p.Category.Room == "Dining-Room");

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                ViewBag.CategoryId = categoryId;

                var productsQuery = _context.Products.Where(p => p.CategoryId == categoryId);

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }

        public async Task<IActionResult> BedRoom(string categoryId, string sortOrder)
        {
            if (categoryId == "BedRoom")
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Room == "Beds");

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = "Bed Room";
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                ViewBag.CategoryId = "BedRoom";

                var productsQuery = _context.Products.Where(p => p.Category.Room == "Beds");

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
            else
            {
                var category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound();
                }

                ViewBag.Title = category.Name;
                ViewBag.ImageUrl = GetImageUrlForCategory(categoryId);
                ViewBag.CategoryId = categoryId;

                var productsQuery = _context.Products.Where(p => p.CategoryId == categoryId);

                productsQuery = SortProducts(productsQuery, sortOrder);

                var products = await productsQuery.ToListAsync();

                var viewModel = products.Select(prod => new ProductCategoryVM
                {
                    Product = prod,
                    Category = category
                }).ToList();

                return View(viewModel);
            }
        }

        public async Task<IActionResult> Popular()
        {

            ViewBag.Title = "Popular";
            var products = await _context.Products
                                          .Where(p => p.Status == ProductStatus.Popular)
                                          .ToListAsync();

            var viewModel = products.Select(prod => new ProductCategoryVM
            {
                Product = prod
            }).ToList();

            return View(viewModel);
            
        }

        public async Task<IActionResult> NewArrivals()
        {

            ViewBag.Title = "New Arrivals";

            var products = await _context.Products
                                          .Where(p => p.Status == ProductStatus.NewArrivals)
                                          .ToListAsync();

            var viewModel = products.Select(prod => new ProductCategoryVM
            {
                Product = prod
            }).ToList();

            return View(viewModel);

        }

        public IActionResult Product(string name)
        {
            var product = _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
