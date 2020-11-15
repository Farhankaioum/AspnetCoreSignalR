using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RawCoding_ChapAPp.Data;
using System.Linq;
using System.Security.Claims;

namespace RawCoding_ChatApp.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext _ctx;

        public RoomViewComponent(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var chats = _ctx.ChatUsers
                .Include(u => u.Chat)
                .Where(x => x.UserId == userId)
                .Select(x => x.Chat)
                .ToList() ;
            return View(chats);
        }
    }
}
