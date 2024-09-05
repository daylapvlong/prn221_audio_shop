using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Profile
{
    public class MyAudioModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public MyAudioModel(AudioMarketContext context)
        {
            _context = context;
        }

        public List<AudioWithSalesData> AudioWithSalesData { get; set; }

        public string CurrentFilter { get; set; }
        public string GenreId { get; set; }
        public string MoodId { get; set; }
        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString, string genreId, string moodId)
        {
            String username = HttpContext.Session.GetString("USERNAME");
            User = await _context.Users.FirstOrDefaultAsync((u => u.Username == username));

            CurrentFilter = searchString;
            IQueryable<Model.Audio> audioQuery = _context.Audios
                .Include(p => p.Genre)
                .Include(p => p.Mood)
                .Include(p => p.Artist)
                .Where(a => a.ArtistId == User.Id);

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

            var audioList = await audioQuery.ToListAsync();

            var audioIds = audioList.Select(a => a.Id).ToList();

            var audioSalesData = await _context.OrderDetails
            .Where(od => audioIds.Contains(od.AudioId))
            .GroupBy(od => od.AudioId)
            .Select(g => new
            {
                AudioId = g.Key,
                Revenue = g.Sum(od => od.UnitPrice),
                CountSold = g.Count()
            })
            .ToListAsync();

            AudioWithSalesData = audioList.Select(a => new AudioWithSalesData
            {
                Audio = a,
                Revenue = audioSalesData.FirstOrDefault(asd => asd.AudioId == a.Id)?.Revenue ?? 0,
                CountSold = audioSalesData.FirstOrDefault(asd => asd.AudioId == a.Id)?.CountSold ?? 0
            }).ToList();

            ViewData["Genre"] = _context.Genres.ToList();
            ViewData["Mood"] = _context.Moods.ToList();

            return Page();
        }
    }
}
