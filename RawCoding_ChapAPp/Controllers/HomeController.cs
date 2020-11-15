using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawCoding_ChapAPp.Data;
using RawCoding_ChapAPp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RawCoding_ChapAPp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _ctx;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            _ctx.Chats.Add(new Chat
            {
                Name = name,
                Type = ChatType.Room
            });

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
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
