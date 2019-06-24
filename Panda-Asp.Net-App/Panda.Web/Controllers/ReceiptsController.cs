using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly PandaDbContext context;

        public ReceiptsController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            this.ViewData["Receipts"] = this.context.Users.Include(x => x.Receipts.ToList());

            return this.View(new List<ReceiptsViewModel>());
        }
    }
}