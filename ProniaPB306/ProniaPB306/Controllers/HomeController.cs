using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaPB306.DAL;
using ProniaPB306.Models;
using ProniaPB306.ViewModels;

namespace ProniaPB306.Controllers
{
    //DI - Dependency Injection (Pattern)
    //IOC - Inverse of Control
    //DIP - Dependency Inversion Principle(SOLID)

    //DI/ IOC Container
    //ServiceLifetime 
    //Service Registration
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
                                           
        public HomeController(AppDbContext context)
        {
           _context = context;

        }
        public IActionResult Index()
        {
            List<Slide> slides = _context.Slides
                .OrderBy(s => s.Order)
                .Take(2)
                .ToList();

            List<Product> products=_context.Products
                .OrderBy(p=>p.CreatedAt)
                .Take(8)
                .Include(p=>p.ProductImages.Where(pi=>pi.IsPrimary!=null))
                .ToList();

            HomeVM homeVM = new HomeVM
            {
                Slides = slides,
                Products=products
            };

            return View(homeVM);


            //List<Slide> slides=new List<Slide>()
            //{
            //    new Slide
            //    {


            //        Title="Bashliq 1",
            //        SubTitle="Komekci Bashliq 1",
            //        Description="Gullerden qalmadi",
            //        CreatedAt=DateTime.Now,
            //        Image="1-2-524x617.png",
            //        IsDeleted=false,
            //        Order=2

            //    },
            //     new Slide
            //    {

            //        Title="Bashliq 2 test",
            //        SubTitle="Komekci Bashliq 2",
            //        Description="En gozel endirimler",
            //        CreatedAt=DateTime.Now,
            //        Image="flower.jpg",
            //        IsDeleted=false,
            //        Order=3

            //    },
            //       new Slide
            //    {


            //        Title="Bashliq 3",
            //        SubTitle="Komekci Bashliq 3",
            //        Description="xirdalana manatdan",
            //        CreatedAt=DateTime.Now,
            //        Image="1-1-524x617.png",
            //        IsDeleted=false,
            //        Order=1

            //    }

            //};

            //_context.Slides.AddRange(slides);
            //_context.SaveChanges();


        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
