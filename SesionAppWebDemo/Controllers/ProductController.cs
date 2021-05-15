using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesionAppWebDemo.Models;
using SesionAppWebDemo.Models.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SesionAppWebDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var product = _context.Products.ToList();
            return View(product);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);           
        }

        public IActionResult EditProduct(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }
            var productToEdit = _context.Products.Find(id);

            return View(productToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var productToEdit = _context.Products.Find(id);

            _context.Products.Remove(productToEdit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
