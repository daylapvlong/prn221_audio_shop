using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MuzicStore.Model;

namespace MuzicStore.Pages.Audio
{
    public class UploadModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public UploadModel (AudioMarketContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.Audio Audio { get; set; }
        public User User { get; set; }
        public string GenreId { get; set; }
        public string MoodId { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            string username = HttpContext.Session.GetString("USERNAME");
            if (username == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            ViewData["Genre"] = _context.Genres.ToList();
            ViewData["Mood"] = _context.Moods.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string audioTitle, IFormFile audioFile, IFormFile audioImage, string genreId, string moodId)
        {
            try
            {
                // Check if audioFile and audioImage are not null
                if (audioFile != null && audioImage != null)
                {
                    // Save audioFile to a specified location
                    var audioFileName = Path.Combine("wwwroot/audio", audioFile.FileName);
                    using (var stream = new FileStream(audioFileName, FileMode.Create))
                    {
                        audioFile.CopyTo(stream);
                    }

                    // Save audioImage to a specified location
                    var imageFileName = Path.Combine("wwwroot/audio/img", audioImage.FileName);
                    using (var stream = new FileStream(imageFileName, FileMode.Create))
                    {
                        audioImage.CopyTo(stream);
                    }

                    GenreId = genreId;
                    MoodId = moodId;

                    Model.Audio audio = new Model.Audio();
                    String username = HttpContext.Session.GetString("USERNAME");
                    User = _context.Users.FirstOrDefault(u => u.Username == username);

                    audio.Title = audioTitle;
                    audio.Filename = "/audio/" + audioFile.FileName;
                    audio.Image = "/audio/img/" + audioImage.FileName;
                    audio.ArtistId = User.Id;
                    audio.Price = 1;
                    audio.GenreId = int.Parse(genreId); audio.MoodId = int.Parse(moodId);
                    audio.Status = true;

                    _context.Audios.Add(audio);
                    _context.SaveChanges();

                    return RedirectToPage("./List");
                }
                else
                {
                    // Handle the case where audioFile or audioImage is not provided
                    return Redirect("./Upload?error");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file I/O errors
                return Redirect("./Upload?error");
            }
        }
    }
}
