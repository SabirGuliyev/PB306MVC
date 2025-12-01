using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProniaPB306.ViewModels
{
    public class CreateSlideVM
    {
        [MaxLength(50, ErrorMessage = "Deyer 50-den chox ola bilmiz")]
        [MinLength(2)]
        public string Title { get; set; }

        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
    }
}
