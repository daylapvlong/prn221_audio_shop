using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Admin
{
    public class ManageAudioModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public ManageAudioModel(AudioMarketContext context)
        {
            _context = context;
        }

        public IList<Model.Audio> AudioList { get; set; } = default!;
        public Model.Audio Audio { get; set; }

        public string CurrentFilter { get; set; }
        public string GenreId { get; set; }
        public string MoodId { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString, string genreId, string moodId)
        {
            CurrentFilter = searchString;
            IQueryable<Model.Audio> audioQuery = _context.Audios
                .Include(p => p.Genre)
                .Include(p => p.Mood)
                .Include(p => p.Artist);

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

        public async Task<IActionResult> OnPostAsync()
        {
            string actionValue = Request.Form["Action"];
            if (actionValue == "DISABLED")
            {
                string audioIdStr = Request.Form["AudioId"];
                if (int.TryParse(audioIdStr, out int audioId))
                {
                    Model.Audio audio = await _context.Audios.FirstOrDefaultAsync(a => a.Id == audioId);
                    if (audio != null)
                    {
                        audio.Status = false; // Set the status to "Banned"
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (actionValue == "UNDISABLED")
            {
                string audioIdStr = Request.Form["AudioId"];
                if (int.TryParse(audioIdStr, out int audioId))
                {
                    Model.Audio audio = _context.Audios.FirstOrDefault(a => a.Id == audioId);
                    if (audio != null)
                    {
                        audio.Status = true; // Set the status to "Unbanned"
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToPage("./ManageAudio");
        }
    }
}
