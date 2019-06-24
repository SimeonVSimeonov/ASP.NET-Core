using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models.ViewModels;

namespace Panda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PandaDbContext context;

        public HomeController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                List<PackageHomeViewModel> userPackages = this.context.Packages
                    .Where(package => package.Recipient.UserName == this.User.Identity.Name)
                    .Include(package => package.Status)
                    .Select(package => new PackageHomeViewModel
                    {
                        Id = package.Id,
                        Description = package.Description,
                        Status = package.Status.Name
                    }).ToList();

                return View(userPackages);
            }

            return this.View();
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
