using INFT3050.Data;
using INFT3050.Extensions;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace INFT3050.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            var cartViewModel = new CartVM { Items = cart };
            return View(cartViewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddToCart(int productID, int quantity)
        {
            var product = _context.Products.Find(productID);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            var cartItem = cart.SingleOrDefault(item => item.ProductID == productID);
            if (cartItem == null)
            {
                cartItem = new CartItemVM
                {
                    ProductID = productID,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                };
                cart.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            SaveCart(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productID)
        {
            var cart = GetCart();
            var cartItem = cart.SingleOrDefault(item => item.ProductID == productID);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        private List<CartItemVM> GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJsonWithExpiry<List<CartItemVM>>("Cart") ?? new List<CartItemVM>();
            return cart;
        }

        private void SaveCart(List<CartItemVM> cart)
        {
            HttpContext.Session.SetObjectAsJsonWithExpiry("Cart", cart, TimeSpan.FromMinutes(1)); 
        }



        public IActionResult UpdateCart(int productID, string updateType)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductID == productID);

            if (item != null)
            {
                if (updateType == "add")
                {
                    item.Quantity++;
                }
                else if (updateType == "deduct")
                {
                    item.Quantity--;
                    if (item.Quantity <= 0)
                    {
                        cart.Remove(item);
                    }
                }
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

    }
}
