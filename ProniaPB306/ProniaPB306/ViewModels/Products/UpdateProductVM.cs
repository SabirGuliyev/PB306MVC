using ProniaPB306.Models;
using System.ComponentModel.DataAnnotations;

namespace ProniaPB306.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public IFormFile? PrimaryPhoto { get; set; }
        public IFormFile? SecondaryPhoto { get; set; }
        public List<IFormFile>? AdditionalPhotos { get; set; }

        [Required]
        public int? CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public List<int>? ImageIds { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
    }
}
