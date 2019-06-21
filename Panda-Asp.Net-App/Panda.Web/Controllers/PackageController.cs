using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Models.BindingModels;

namespace Panda.Web.Controllers
{
    public class PackageController : Controller
    {
        private readonly PandaDbContext context;

        public PackageController(PandaDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create()
        {
            this.ViewData["Recipients"] = this.context.Users.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePackageInputModel inputModel)
        {
            return View();
        }
    }
}