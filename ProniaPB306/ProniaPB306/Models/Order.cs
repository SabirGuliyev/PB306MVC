using ProniaPB306.Utilities.Enums;

namespace ProniaPB306.Models
{
    public class Order:BaseEntity
    {

        public string Address { get; set; }
        public decimal Total { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public DateTime? CompletedAt { get; set; }

        //relational

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<OrderItem> OrderItems { get; set; }


    }
}
