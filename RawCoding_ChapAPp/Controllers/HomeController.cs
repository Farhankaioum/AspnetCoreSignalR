using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RawCoding_ChapAPp.Data;
using RawCoding_ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RawCoding_ChapAPp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _ctx;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext ctx )
        {
            _logger = logger;
            _ctx = ctx;
        }

        public IActionResult Index() {

            var chats = new List<Chat>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                chats = _ctx.Chats
                    .Include(u => u.Users)
                    .Where(u => !u.Users.Any(u => u.UserId == userId))
                    .ToList();
                
            }
            return View(chats);
        } 

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = _ctx.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int chatId, string message)
        {
            var userName = User.Identity.Name;

            var messageToSave = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = userName,
                Timestamp = DateTime.Now
            };

            _ctx.Messages.Add(messageToSave);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId});
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Admin
            });

            _ctx.Chats.Add(chat);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Member
            };

            _ctx.ChatUsers.Add(chatUser);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = id});

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
