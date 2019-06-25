using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FDMC.Models;
using FDMC.Models.Data;

namespace FDMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly FDMCDbContext context;

        public HomeController(FDMCDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allCats = this.context.Cats
                .Select(cats => new AllCatsViewModel {
                    Id = cats.Id,
                    Name = cats.Name
                }).ToList();

            return View(allCats);
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
