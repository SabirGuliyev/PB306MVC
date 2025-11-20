using Microsoft.AspNetCore.Mvc;
using ProniaPB306.DAL;
using ProniaPB306.Models;


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

            Product? product=_context.Products.FirstOrDefault(p=>p.Id==id);
            if(product is null)
            {
                return NotFound();
            }



            return View();
        }
    }
}
