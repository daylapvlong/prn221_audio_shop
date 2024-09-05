using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public LoginModel(AudioMarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var account = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Username && u.Password == User.Password);
            if (account == null)
            {
                ViewData["msg"] = "Username and Password are not correct!";
                return Page();
            }
            if(account.Status == false)
            {
                ViewData["msg"] = "Your account has been banned!";
                return Page();
            }

            HttpContext.Session.SetString("USERNAME", User.Username);
            int role = Convert.ToInt32(account.Role);
            HttpContext.Session.SetInt32("ROLE", role);
            return RedirectToPage("/Index");
        }
    }
}
