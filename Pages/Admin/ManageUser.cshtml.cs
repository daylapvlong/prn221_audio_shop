using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;
using System.Security.Principal;

namespace MuzicStore.Pages.Admin
{
    public class ManageUserModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public ManageUserModel(AudioMarketContext context)
        {
            _context = context;
        }
        public IList<User> Users { get; set; }
        [BindProperty]
        public User User { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if(_context.Users != null)
            {
                Users = _context.Users.ToList();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            string actionValue = Request.Form["Action"];
            if (actionValue == "EDIT")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Id);
                if (user != null)
                {
                    user.Username = User.Username;
                    user.Name = User.Name;
                    user.Role = User.Role;
                    user.Status = User.Status;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
            }

            if (actionValue == "BANNED")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Id);
                if (user != null)
                {
                    user.Status = false; // Set the status to "Banned"
                    await _context.SaveChangesAsync();
                }
            }

            if (actionValue == "UNBANNED")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.Id);
                if (user != null)
                {
                    user.Status = true; // Set the status to "Unbanned"
                    await _context.SaveChangesAsync();
                }
            }    
            return RedirectToPage("./ManageUser");
        }
    }
}
