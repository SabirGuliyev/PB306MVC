using ProniaPB306.Models;

namespace ProniaPB306.ViewModels
{
    public class GetAdminProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
       
        public string CategoryName { get; set; }
        public string Image { get; set; }
    }
}
