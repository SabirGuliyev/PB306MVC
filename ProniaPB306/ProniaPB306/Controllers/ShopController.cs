using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaPB306.DAL;
using ProniaPB306.Models;
using ProniaPB306.ViewModels;


namespace ProniaPB306.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if(id is null || id < 1)
            {
                return BadRequest();
            }

            Product? product=_context.Products
                .Include(p=>p.ProductImages.OrderByDescending(pi=>pi.IsPrimary))
                .Include(p=>p.Category)
                .FirstOrDefault(p=>p.Id==id);

            if(product is null)
            {
                return NotFound();
            }

            DetailsVM detailsVM=new DetailsVM 
            {
                Product = product,

                RelatedProducts=_context.Products
                .Where(p=>p.CategoryId==product.CategoryId && p.Id!=id)
                .Include(p=>p.ProductImages.Where(pi=>pi.IsPrimary!=null))
                .ToList()
            };


            return View(detailsVM);
        }
    }
}
