using Microsoft.AspNetCore.Mvc;
using RawCoding_ChapAPp.Data;
using System.Linq;

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
            var chats = _ctx.Chats.ToList();
            return View(chats);
        }
    }
}
