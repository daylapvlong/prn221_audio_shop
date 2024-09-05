using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Admin
{
    public class ManageOrderModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public ManageOrderModel(AudioMarketContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }
        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string username = HttpContext.Session.GetString("USERNAME");
            if (username != null)
            {
                if (_context.Orders != null)
                {
                    Orders = await _context.Orders.Include(o => o.Buyer).Include(o => o.OrderDetails).ThenInclude(od => od.Audio).ThenInclude(a => a.Artist).ToListAsync();
                }
                return Page();
            }

            return Redirect("/Index");

        }
    }
}
