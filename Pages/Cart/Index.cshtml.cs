using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;
using Newtonsoft.Json;
using System.Text;


namespace MuzicStore.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public IndexModel(AudioMarketContext context)
        {
            _context = context;
        }
        public IList<Model.Audio> LikedAudios { get; set; } = default!;
        public List<CartItem> Carts { get; set; } = default!;
        public decimal? TotalPrice { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public User User { get; set; }

        //GET
        public async Task<IActionResult> OnGetAsync()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                Carts = JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                TotalPrice = Carts.Sum(item => item.UnitPrice * item.Quantity);
            }

            String username = HttpContext.Session.GetString("USERNAME");
            User = await _context.Users.FirstOrDefaultAsync((u => u.Username == username));
            if (User != null)
            {
                var likedAudioIds = _context.Favorites
                                    .Where(f => f.UserId == User.Id)
                                    .Select(f => f.AudioId)
                                    .ToList();
                LikedAudios = _context.Audios
                            .Include(p => p.Genre)
                            .Include(p => p.Mood)
                            .Include(p => p.Artist)
                            .Where(a => likedAudioIds.Contains(a.Id))
                            .ToList();
            }

            return Page();
        }

        //POST
        public async Task<IActionResult> OnPostAsync()
        { 
            //Xử lý lấy cart
            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                Carts = JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
                TotalPrice = Carts.Sum(item => item.UnitPrice * item.Quantity);
            }

            //Xử lý thêm Order
            var customer = await _context.Users.FirstOrDefaultAsync();
            var order = new Order
            {
                BuyerId = customer.Id,
                PurchaseDate = DateTime.Now,
                Price = TotalPrice,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in Carts)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id, // Use the order ID generated after saving the order
                    AudioId = item.AudioId,
                    UnitPrice = item.UnitPrice.GetValueOrDefault(0m),
                };
                await _context.OrderDetails.AddAsync(orderDetail);
            }
            await _context.SaveChangesAsync();


            HttpContext.Session.Remove("Cart");

            return RedirectToPage("/Orders/Index");
        }
    }
}
