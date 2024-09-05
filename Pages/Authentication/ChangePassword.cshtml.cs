using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Authentication
{
    public class ChangePasswordModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public ChangePasswordModel(AudioMarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string oldpassword { get; set; }

        [BindProperty]
        public string newpassword { get; set; }

        [BindProperty]
        public string repassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            String username = HttpContext.Session.GetString("USERNAME");

            User user = await _context.Users.FirstOrDefaultAsync((u => u.Username == username));

            if (newpassword != repassword)
            {
                ViewData["msg"] = "2 password aren't same!";
                return Page();
            }
            else if (oldpassword != user.Password)
            {
                ViewData["msg"] = "Wrong old password!";
                return Page();
            }
            else
            {
                user.Password = newpassword;
                _context.SaveChanges();
                ViewData["msg"] = "Change Password successfully";
                return Page();
            }
        }
    }
}
