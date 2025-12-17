using Microsoft.AspNetCore.Identity;

namespace ProniaPB306.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //relational

        public List<BasketItem> BasketItems { get; set; }

        public List<Order> Orders { get; set; }
        //public List<OrderItem> OrderItems { get; set; }
    }
}
