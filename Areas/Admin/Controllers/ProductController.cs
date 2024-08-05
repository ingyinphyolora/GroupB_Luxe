using INFT3050.Data;
using INFT3050.Models;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO;
using System;

namespace INFT3050.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            environment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Name)
                .ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Product";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            var viewModel = new ProductEditViewModel();
            return View("Edit", viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Product";
            ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                ImagePath = product.ImagePath,
                Status = product.Status
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (product == null)
                {
                    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    newFileName += Path.GetExtension(model.ImageFile!.FileName);
                    string imageFullPath = environment.WebRootPath + "/product/" + newFileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        model.ImageFile.CopyTo(stream);
                    }

                    var prod = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Stock = model.Stock,
                        CategoryId = model.CategoryId,
                        ImagePath = newFileName,
                        Status = model.Status
                    };
                    _context.Products.Add(prod);
                }
                else
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Stock = model.Stock;
                    product.CategoryId = model.CategoryId;
                    product.Status = model.Status; 

                    if (model.ImageFile != null)
                    {
                        string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        newFileName += Path.GetExtension(model.ImageFile.FileName);
                        string imageFullPath = Path.Combine(environment.WebRootPath, "product", newFileName);

                        using (var stream = new FileStream(imageFullPath, FileMode.Create))
                        {
                            model.ImageFile.CopyTo(stream);
                        }

                        product.ImagePath = newFileName;
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Product");
                
            }
            else
            {
                ViewBag.Action = (model.ProductId == 0) ? "Add" : "Edit";
                ViewBag.Categories = _context.Categories.OrderBy(g => g.Name).ToList();
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string room, string category, string productName)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(room))
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Category.Room.ToLower() == room.ToLower() &&
                                        p.Category.Name.ToLower() == category.ToLower() &&
                                            p.Name.ToLower() == productName.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string room, string category, string productName)
        {
            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(room))
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Category.Room.ToLower() == room.ToLower() &&
                                        p.Category.Name.ToLower() == category.ToLower() &&
                                            p.Name.ToLower() == productName.ToLower());

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


    }
}
