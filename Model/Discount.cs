using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class Discount
    {
        public Discount()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
