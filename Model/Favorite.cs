namespace MuzicStore.Model
{
    public class Favorite
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int AudioId { get; set; }
        public Audio Audio { get; set; }
    }
}
