using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public IndexModel(AudioMarketContext context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; } = default!;
        public IList<Model.Audio> LikedAudios { get; set;} = default!;

        [BindProperty]
        public User User { get; set; }

        //GET
        public async Task OnGetAsync()
        {
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
        }
    }
}
