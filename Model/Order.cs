using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? BuyerId { get; set; }
        public int? Status { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int? DiscountId { get; set; }
        public decimal? Price { get; set; }

        public virtual User? Buyer { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
