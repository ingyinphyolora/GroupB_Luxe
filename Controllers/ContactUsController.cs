using INFT3050.Data;
using INFT3050.Models;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                var contactMessage = new ContactMessage
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message
                };

                _context.Messages.Add(contactMessage);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Thank you for contacting us. We will get back to you soon.";
                return RedirectToAction("Index");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
