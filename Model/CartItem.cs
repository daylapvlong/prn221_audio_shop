namespace MuzicStore.Model
{
    public class CartItem
    {
        public int AudioId { get; set; }
        public string AudioName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string AudioImage { get; set; }
    }
}
