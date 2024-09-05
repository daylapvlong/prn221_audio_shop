using System;
using System.Collections.Generic;

namespace MuzicStore.Model
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int AudioId { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Audio Audio { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
