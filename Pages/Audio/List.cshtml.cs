using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Audio
{
    public class ListModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public ListModel(AudioMarketContext context)
        {
            _context = context;
        }

        public IList<Model.Audio> AudioList { get; set; } = default!;
        public IList<Favorite> Favorites { get; set; } = default!;
        [BindProperty]
        public User User { get; set; }

        public string CurrentFilter { get; set; }
        public string GenreId { get; set; }
        public string MoodId {  get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString, string genreId, string moodId)
        {
            String username = HttpContext.Session.GetString("USERNAME");
            User = _context.Users.FirstOrDefault(u => u.Username == username);
            CurrentFilter = searchString;

            IQueryable<Model.Audio> audioQuery;
            if (User != null) {
                Favorites = _context.Favorites
                            .Where(f => f.UserId == User.Id)
                            .ToList();
                var purchasedAudioIds = _context.OrderDetails
                    .Where(od => od.Order.BuyerId == User.Id)
                    .Select(od => od.AudioId)
                    .ToList();

                audioQuery = _context.Audios
                    .Include(a => a.Genre)
                    .Include(a => a.Mood)
                    .Include(a => a.Artist)
                    .Where(a => !purchasedAudioIds.Contains(a.Id) && a.Status == true);

            } 
            else
            {
                audioQuery = _context.Audios
                .Include(p => p.Genre)
                .Include(p => p.Mood)
                .Include(p => p.Artist);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int id))
                {
                    audioQuery = audioQuery.Where(p => p.Id == id);
                }
                else
                {
                    audioQuery = audioQuery.Where(p =>
                       p.Title.Contains(searchString));
                }
            }

            if (!String.IsNullOrEmpty(genreId))
            {
                GenreId = genreId;
                if (int.TryParse(genreId, out int genreIdInt))
                {
                    audioQuery = audioQuery.Where(p => p.GenreId == genreIdInt);
                }
            }

            if (!String.IsNullOrEmpty(moodId))
            {
                MoodId = moodId;
                if (int.TryParse(moodId, out int moodIdInt))
                {
                    audioQuery = audioQuery.Where(p => p.MoodId == moodIdInt);
                }
            }

            AudioList = await audioQuery.ToListAsync();

            ViewData["Genre"] = _context.Genres.ToList();
            ViewData["Mood"] = _context.Moods.ToList();
            
            return Page();
        }
    }
}
