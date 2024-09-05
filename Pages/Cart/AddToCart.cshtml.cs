using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MuzicStore.Model;
using Newtonsoft.Json;

namespace MuzicStore.Pages.Cart
{
    public class AddToCartModel : PageModel
    {
        private readonly AudioMarketContext _context;
        public AddToCartModel(AudioMarketContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id, bool isInCart)
        {
            string username = HttpContext.Session.GetString("USERNAME");
            if (username == null)
            {
                return RedirectToPage("/Authentication/Login");
            }

            var audio = await _context.Audios.FirstOrDefaultAsync(a => a.Id == id);
            if(audio == null)
            {
                return NotFound("Không có sản phẩm đc thêm");
            }

            //Add cart
            var cart = GetCartItems();
            var cartItem = cart.Find(c => c.AudioId == id);
            if(cartItem == null)
            {
                cart.Add(new CartItem()
                {
                    AudioId = audio.Id,
                    AudioName = audio.Title,
                    Quantity = 1,
                    UnitPrice = audio.Price,
                    AudioImage = audio.Image
                });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            if(isInCart)
            {
                return RedirectToPage("/Cart/Index");
            }
            else
            {
                return RedirectToPage("/Audio/List");
            }
        }

        private List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("Cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("Cart");
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("Cart", jsoncart);
        }
    }
}
