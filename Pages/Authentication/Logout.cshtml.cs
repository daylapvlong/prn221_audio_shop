using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuzicStore.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            HttpContext.Session.Remove("USERNAME");
            HttpContext.Session.Remove("ROLE");
            return RedirectToPage("./Login");
        }
    }
}
