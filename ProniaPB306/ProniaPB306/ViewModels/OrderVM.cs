using ProniaPB306.Models;

namespace ProniaPB306.ViewModels
{
    public class OrderVM
    {
        public string Address { get; set; }
        public decimal? Total { get; set; }
        public List<BasketItemInOrderVM>? BasketItemVMs { get; set; }
    }
}
