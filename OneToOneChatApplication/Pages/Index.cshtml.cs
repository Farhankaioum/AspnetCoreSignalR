using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OneToOneChatApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneToOneChatApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public List<ApplicationUser> Users = new List<ApplicationUser>();

        public void OnGet()
        {
            var userLoginId = _userManager.GetUserId(HttpContext.User);

            Users = _userManager.Users.ToList();

            if (!string.IsNullOrEmpty(userLoginId))
                Users = _userManager.Users.Where(u => u.Id != userLoginId).ToList();

        }
    }
}
