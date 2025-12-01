using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaPB306.DAL;
using ProniaPB306.Models;
using ProniaPB306.ViewModels;

namespace ProniaPB306.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;


        }
        public async Task<IActionResult> Index()
        {
            var productVMs = await _context.Products
                .Select(p => new GetAdminProductVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true).Image,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();

            return View(productVMs);
        }
        //2 5 6
        public async Task<IActionResult> Create()
        {
            CreateProductVM productVM = new()
            {
                Categories = await _context.Categories.ToListAsync(),
                Tags = await _context.Tags.ToListAsync()

            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {

            productVM.Categories = await _context.Categories.ToListAsync();
            productVM.Tags = await _context.Tags.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            bool result = productVM.Categories.Any(c => c.Id == productVM.CategoryId);
            if (!result)
            {
                ModelState.AddModelError(nameof(CreateProductVM.CategoryId), "Category does not exist(Men yazmisham)");
                return View(productVM);
            }

            //1 2 3 30         1,2,3,4,5,6
            if(productVM.TagIds is null) productVM.TagIds = new();
            //2  5  6 
            productVM.TagIds=productVM.TagIds.Distinct().ToList();

            bool tagResult = productVM.TagIds.Any(tId => !productVM.Tags.Exists(t => t.Id == tId));

            if (tagResult)
            {
                ModelState.AddModelError(nameof(CreateProductVM.TagIds), "Tags are wrong");
                return View(productVM);
            }



            bool resultName = await _context.Products.AnyAsync(p => p.Name == productVM.Name);
            if (resultName)
            {
                ModelState.AddModelError(nameof(CreateProductVM.Name), "Product Name already exists");
                return View(productVM);
            }


            Product product = new()
            {
                Name = productVM.Name,
                SKU = productVM.SKU,
                Price = productVM.Price.Value,
                Description = productVM.Description,
                CategoryId = productVM.CategoryId.Value,
                CreatedAt = DateTime.Now,
                ProductTags = productVM.TagIds.Select(tId => new ProductTag { TagId = tId }).ToList()
            };


            //foreach (var tId in productVM.TagIds)
            //{
            //  product.ProductTags.Add(new ProductTag
            //   {
            //       TagId = tId

            //   });
            //}


            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1)
            {
                return BadRequest();
            }
            Product? existed = await _context.Products
                .Include(p=>p.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existed is null)
            {
                return NotFound();
            }

            UpdateProductVM productVM = new()
            {
                Name = existed.Name,
                SKU = existed.SKU,
                Description = existed.Description,
                CategoryId = existed.CategoryId,
                Price = existed.Price,
                TagIds=existed.ProductTags.Select(pt=>pt.TagId).ToList(),
                Categories = await _context.Categories.ToListAsync(),
                Tags=await _context.Tags.ToListAsync()

            };


            return View(productVM);
        }


        //1 2           1 2 6 7

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM productVM)
        {
            productVM.Categories = await _context.Categories.ToListAsync();
            productVM.Tags = await _context.Tags.ToListAsync();
            if (!ModelState.IsValid)
            {
                return View(productVM);
            }

            bool result = productVM.Categories.Any(c => c.Id == productVM.CategoryId);
            if (!result)
            {
                ModelState.AddModelError(nameof(UpdateProductVM.CategoryId), "Category does not exist(Men yazmisham)");
                return View(productVM);
            }

        
            if (productVM.TagIds is null) productVM.TagIds = new();
            productVM.TagIds = productVM.TagIds.Distinct().ToList();

            bool tagResult = productVM.TagIds.Any(tId => !productVM.Tags.Exists(t => t.Id == tId));

            if (tagResult)
            {
                ModelState.AddModelError(nameof(CreateProductVM.TagIds), "Tags are wrong");
                return View(productVM);
            }



            bool resultName = await _context.Products.AnyAsync(p => p.Name == productVM.Name && p.Id != id);
            if (resultName)
            {
                ModelState.AddModelError(nameof(UpdateProductVM.Name), "Product Name already exists");
                return View(productVM);
            }

            Product? existed = await _context.Products
                .Include(p=>p.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id);


            _context.ProductTags
                .RemoveRange(existed.ProductTags
                    .Where(pt => !productVM.TagIds
                        .Exists(tId => pt.TagId == tId))
                    .ToList());

            _context.ProductTags
                .AddRange(productVM.TagIds
                    .Where(tId => !existed.ProductTags
                        .Exists(pt => pt.TagId == tId))
                    .Select(tId => new ProductTag { TagId = tId,ProductId=existed.Id })
                    .ToList());

            existed.Name = productVM.Name;
            existed.SKU = productVM.SKU;
            existed.Description = productVM.Description;
            existed.Price = productVM.Price.Value;
            existed.CategoryId = productVM.CategoryId.Value;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
