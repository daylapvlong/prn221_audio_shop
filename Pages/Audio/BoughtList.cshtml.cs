using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Audio
{
    public class BoughtListModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public BoughtListModel(AudioMarketContext context)
        {
            _context = context;
        }

        public List<Model.Audio> PurchasedAudios { get; set; } = new List<Model.Audio>();
        public IList<Favorite> Favorites { get; set; } = default!;
        [BindProperty]
        public User User { get; set; }

        public string CurrentFilter { get; set; }
        public string GenreId { get; set; }
        public string MoodId { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString, string genreId, string moodId)
        {
            string username = HttpContext.Session.GetString("USERNAME");
            User = _context.Users.FirstOrDefault(u => u.Username == username);

            if (User != null)
            {
                Favorites = _context.Favorites
                            .Where(f => f.UserId == User.Id)
                            .ToList();
            }

            CurrentFilter = searchString;

            IQueryable<Model.Audio> audioQuery = _context.OrderDetails
                                                        .Include(od => od.Audio)
                                                            .ThenInclude(a => a.Genre)
                                                        .Include(od => od.Audio)
                                                            .ThenInclude(a => a.Mood)
                                                        .Include(od => od.Audio)
                                                            .ThenInclude(a => a.Artist)
                                                        .Where(od => od.Order.BuyerId == User.Id)
                                                        .Select(od => od.Audio);

            if (!String.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int id))
                {
                    audioQuery = audioQuery.Where(a => a.Id == id);
                }
                else
                {
                    audioQuery = audioQuery.Where(a =>
                       a.Title.Contains(searchString));
                }
            }

            if (!String.IsNullOrEmpty(genreId))
            {
                GenreId = genreId;
                if (int.TryParse(genreId, out int genreIdInt))
                {
                    audioQuery = audioQuery.Where(a => a.GenreId == genreIdInt);
                }
            }

            if (!String.IsNullOrEmpty(moodId))
            {
                MoodId = moodId;
                if (int.TryParse(moodId, out int moodIdInt))
                {
                    audioQuery = audioQuery.Where(a => a.MoodId == moodIdInt);
                }
            }
            
            PurchasedAudios = await audioQuery.ToListAsync();

            ViewData["Genre"] = _context.Genres.ToList();
            ViewData["Mood"] = _context.Moods.ToList();

            return Page();
        }
    }
}
