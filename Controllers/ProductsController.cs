using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopping_website.Models;
using System.Linq;

namespace shopping_website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Products
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}