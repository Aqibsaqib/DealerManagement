using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DealerManagment.Models;
using Microsoft.AspNetCore.Identity;
using DealerManagment.Data;

namespace DealerManagment.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Current_Inventory", "Inventory");
            }
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                return View(user);
            }
       
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
