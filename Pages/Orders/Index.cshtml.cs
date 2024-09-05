using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public IndexModel(AudioMarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            String username = HttpContext.Session.GetString("USERNAME");
            User = await _context.Users.FirstOrDefaultAsync((u => u.Username == username));
            Order = await _context.Orders
                                    .Include(o => o.Buyer)
                                    .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.Audio)
                                            .ThenInclude(a => a.Artist)
                                    .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.Audio)
                                            .ThenInclude(a => a.Mood)
                                    .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.Audio)
                                            .ThenInclude(a => a.Genre)
                                    .Where(o => o.BuyerId == User.Id)
                                    .OrderByDescending(o => o.PurchaseDate)
                                    .FirstOrDefaultAsync();
            return Page();
        }
    }
}
