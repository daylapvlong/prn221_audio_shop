using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;

namespace MuzicStore.Pages.Audio
{
    public class FavorModel : PageModel
    {
        private readonly AudioMarketContext _context;

        public FavorModel(AudioMarketContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int userId, int audioId)
        {
            DbSet<Favorite> favorites = _context.Favorites;
            Favorite favorite = favorites.FirstOrDefault(f => f.UserId == userId && f.AudioId == audioId);

            if (favorite != null)
            {
                favorites.Remove(favorite);
            }
            else
            {
                favorites.Add(new Favorite { UserId = userId, AudioId = audioId });
            }

            _context.SaveChanges();
            return Redirect("/Audio/List");
        }
    }
}
