using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Authentication
{
    public class SignupModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public SignupModel(AudioMarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public string RepeatPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _context.Users.FirstOrDefaultAsync(u => u.Username == User.Username);
            if (account != null)
            {
                ViewData["msg"] = "Username has been taken!";
                return Page();
            }

            if (RepeatPassword != User.Password)
            {
                ViewData["msg"] = "Passwords are not correct!";
                return Page();
            }

            User.Role = 0;
            User.Status = true;

            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Authentication/Login");
        }
    }
}
