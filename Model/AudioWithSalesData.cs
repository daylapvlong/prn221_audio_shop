namespace MuzicStore.Model
{
    public class AudioWithSalesData
    {
        public Audio Audio { get; set; }
        public decimal Revenue { get; set; }
        public int CountSold { get; set; }
    }
}
