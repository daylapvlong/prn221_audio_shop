using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MuzicStore.Model;

namespace MuzicStore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AudioMarketContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, AudioMarketContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            ViewData["Genre"] = _context.Genres.ToList();
            ViewData["Mood"] = _context.Moods.ToList();

            return Page();
        }
    }
}