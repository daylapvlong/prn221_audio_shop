using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Profile
{
    public class MyOrderModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public MyOrderModel(AudioMarketContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = default!;
        [BindProperty]
        public User User { get; set; }

        public async Task OnGetAsync()
        {
            String username = HttpContext.Session.GetString("USERNAME");
            User = await _context.Users.FirstOrDefaultAsync((u => u.Username == username));

            if (_context.Orders != null)
            {
                Orders = await _context.Orders.Include(o => o.Buyer).Include(o => o.OrderDetails).ThenInclude(od => od.Audio).ThenInclude(a => a.Artist).Where(o => o.BuyerId == User.Id).ToListAsync();
            }
        }
    }
}
