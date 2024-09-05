using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Audio
{
    public class DetailModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public DetailModel(AudioMarketContext context)
        {
            _context = context;
        }

        public Model.Audio Audio { get; set; } = default!;
        public List<Review> Reviews { get; set; }

        [BindProperty]
        public Review review { get; set; }

        [BindProperty]
        public int AudioId {  get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Audios == null)
            {
                return NotFound();
            }

            var audio = await _context.Audios.Include(a => a.Genre)
                                            .Include(a => a.Mood)
                                            .FirstOrDefaultAsync(a => a.Id == id);
            if (audio == null)
            {
                return NotFound();
            }
            else
            {
                Audio = audio;
                AudioId = audio.Id;
            }

            ViewData["ReviewList"] = _context.Reviews.Where(r => r.Id == id).Include(r => r.User).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string username = HttpContext.Session.GetString("USERNAME");
            User user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return BadRequest();

            string actionValue = Request.Form["Action"];
            if (actionValue == "CREATE")
            {
                review.UserId = user.Id;
                review.AudioId = AudioId;
                review.Rating = 5;
                review.Status = 0;

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Detail", new { id = Audio.Id });
        }
    }
}
